<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('System.User.Detail.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="save" autocomplete="off">
                        <!-- disable autocomplete -->
                        <input autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="text" name="username" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="email" name="email" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="password" name="password" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <!-- disable autocomplete -->
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.user.compId.$model"
                                                  :state="$v.user.compId.$error ? false : null">
                                    </b-form-input>
                                    <treeselect :multiple="false"
                                                v-if="editing"
                                                :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="user.compId" />
                                    <label class="col-form-label" v-if="!editing"> {{ user.company }} </label>
                                    <b-form-invalid-feedback>
                                        {{$t("System.Company.Detail.Label.ParentRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="">
                                    <label class="col-form-label"> {{ user.username }} </label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.FullName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.user.fullName.$model"
                                                  @input="$v.user.fullName.$touch"
                                                  :state="$v.user.fullName.$dirty? !$v.user.fullName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t("System.User.Detail.Label.NameRequire")}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing"> {{ user.fullName }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Guess.Create.Form.Phone')"
                                              :label-cols="4"
                                              label-align-md="left"
                                              :horizontal="true">
                                    <b-form-input type="tel"
                                                  maxlength="20"
                                                  v-if="editing"
                                                  v-model="user.phoneNumber">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing"> {{ user.phoneNumber }} </label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Email:"
                                              label-for="email"
                                              :label-cols="4"
                                              label-align-md="left"
                                              :horizontal="true">
                                    <b-form-input type="email"
                                                  v-if="editing"
                                                  v-model="user.email">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing"> {{ user.email }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <b-form-group :label="this.$t('System.User.Detail.Label.Role')"
                                              label-for="roles"
                                              :label-cols="2"
                                              label-align-md="left"
                                              :horizontal="true">
                                    <b-form-radio-group v-model="user.roleId"
                                                        class="radio-group"
                                                        v-if="editing">
                                        <b-form-radio v-for="role in listRoles"
                                                      :key="role.id"
                                                      :value="role.id"
                                                      class="col-md-6">
                                            {{ $t(role.text) }}
                                        </b-form-radio>
                                    </b-form-radio-group>
                                    <label class="col-form-label" v-if="!editing && user.roleNames"> {{ $t(user.roleNames.join(', ')) }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>

                        </b-row>
                        <div class="text-center">
                            <b-button type="button"
                                      variant="primary"
                                      @click="edit"
                                      style="width: 120px"
                                      v-if="!editing && authorize(['ManageUser'])">
                                {{ $t("Button.Edit") }}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="back"
                                      style="width: 120px"
                                      v-if="!editing">
                                {{ $t("Button.Back") }}
                            </b-button>
                            <b-button type="submit"
                                      variant="primary"
                                      style="width: 120px"
                                      v-if="editing">
                                {{ $t("Button.Submit") }}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      style="width: 120px"
                                      @click="cancel"
                                      v-if="editing">
                                {{ $t("Button.Cancel") }}
                            </b-button>
                        </div>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required, sameAs, minLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/)
    import i18n from '@/libs/i18n'

    export default {
        name: 'UserDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                user: {
                    id: null,
                    username: null,
                    fullName: null,
                    email: null,
                    PhoneNumber: null,
                    roleIds: [],
                    roleId: null,
                    roleNames: null,
                }, lstcompany: [],
                editing: false,
                listRoles: [],
                treeselect: {
                    value: null,
                    options: [],
                }
            }
        },
        computed: {
            userId() {
                return this.$route.params.userId;
            },
        },
        validations: {
            user: {
                compId: { required },
                fullName: { required },
                newPassword: { minLength: minLength(8), passwordPattern },
                newPasswordConfirmation: { sameAsPassword: sameAs('newPassword') },
            }
        },
        created() {
            var vm = this;
            this.$services.get('lookup/roles').done(response => {
                for (var i = 0; i < response.length; i++) {
                    let item = response[i];
                    let name = i18n.t(item.text);
                    item.text = name;
                    vm.listRoles.push(item);
                }
            });
            this.loadUserDetail();
            this.loadCompanyTree();
        },
        methods: {
            //Danh sách công ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            loadUserDetail() {
                var vm = this;
                return this.$services.get(`users/${this.userId}`).done(res => {
                    res.newPassword = "";
                    res.newPasswordConfirmation = "";
                    res.roleId = res.roleIds[0];
                    vm.user = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/system/users' });
            },
            cancel() {
                var vm = this;
                this.loadUserDetail().done(() => vm.editing = false);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.user.roleIds = [];
                    vm.user.roleIds.push(vm.user.roleId);
                    this.$services.put(`users/${this.userId}`, this.user).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
