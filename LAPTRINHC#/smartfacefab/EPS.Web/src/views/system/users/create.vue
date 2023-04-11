<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{ $t('System.User.Create.Header') }} </strong>
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
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.user.compId.$model"
                                                  :state="$v.user.compId.$error ? false : null">
                                    </b-form-input>
                                    <treeselect :multiple="false"
                                                :options="treeselect.options"
                                                :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="user.compId" />
                                    <b-form-invalid-feedback>
                                        {{ $t("System.Company.Detail.Label.ParentRequire") }}
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
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.user.username.$model"
                                                  :state="$v.user.username.$dirty? !$v.user.username.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.username.required">
                                        {{ $t("System.User.Detail.Label.UserNameRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.FullName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.user.fullName.$model"
                                                  @input="$v.user.fullName.$touch"
                                                  :state="$v.user.fullName.$dirty? !$v.user.fullName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback id="fullNameFeedback">
                                        {{ $t("System.User.Detail.Label.NameRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.Password')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="password"
                                                  v-model="$v.user.password.$model"
                                                  @input="$v.user.password.$touch"
                                                  :state="$v.user.password.$dirty? !$v.user.password.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.password.required">
                                        {{ $t("System.User.Detail.Label.PasswordRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.user.password.minLength">
                                        {{ $t("System.User.Detail.Label.MaxlengthRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="$v.user.password.minLength && !$v.user.password.passwordPattern">
                                        {{ $t("System.User.Detail.Label.SpecialRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.ConfirmPassword')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="password"
                                                  v-model="$v.user.passwordConfirmation.$model"
                                                  @input="$v.user.passwordConfirmation.$touch"
                                                  :state="$v.user.passwordConfirmation.$dirty? !$v.user.passwordConfirmation.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.passwordConfirmation.required">
                                        {{ $t("System.User.Detail.Label.ConfirmPasswordRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="$v.user.passwordConfirmation.required && !$v.user.passwordConfirmation.sameAsPassword">
                                        {{ $t("System.User.Detail.Label.ConfirmationRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Guess.Create.Form.Phone')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="">
                                    <b-form-input type="tel"
                                                  maxlength="20"
                                                  v-model="user.PhoneNumber">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Email:"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="">
                                    <b-form-input type="email"
                                                  v-model="user.email">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.Detail.Label.RoleName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display:none"
                                                  v-model="$v.user.roleId.$model"
                                                  :state="$v.user.roleId.$dirty? !$v.user.roleId.$error : null">
                                    </b-form-input>
                                    <b-form-radio-group v-model="$v.user.roleId.$model"
                                                        class="radio-group"
                                                        :state="$v.user.roleId.$dirty? !$v.user.roleId.$error : null">
                                        <b-form-radio v-for="role in listRoles"
                                                      :key="role.id"
                                                      :value="role.id"
                                                      class="col-md-6">
                                            {{ $t(role.text) }}
                                        </b-form-radio>
                                    </b-form-radio-group>
                                    <b-form-invalid-feedback v-if="!$v.user.roleId.required">
                                        {{ $t("System.User.Detail.Label.RoleNameRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button @click="save"
                                              v-if="authorize(['ManageUser'])"
                                              style="width: 120px"
                                              variant="primary">
                                        {{ $t("Button.Submit" )}}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              style="width: 120px"
                                              @click="cancel">
                                        {{ $t("Button.Cancel") }}
                                    </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>

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
        name: 'CreateUser',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                listRoles: [],
                user: {
                    compId: null,
                    status: null,
                    username: null,
                    password: null,
                    passwordConfirmation: null,
                    unitId: 1,
                    fullName: null,
                    email: null,
                    PhoneNumber: null,
                    roleIds: [],
                    roleId: null,
                },
                lstcompany: {},
                editing: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        validations: {
            user: {
                username: {
                    required
                },
                password: { required, minLength: minLength(8), passwordPattern },
                passwordConfirmation: { required, sameAsPassword: sameAs('password') },
                compId: { required },
                fullName: { required },
                roleId: { required },
                compId: { required },
            }
        },
        created() {
            var vm = this;
            this.$services.get('lookup/roles').done(response => {
                vm.listRoles = response;
            });
            this.loadCompanyTree();
            this.user.status = 1;
        },
        methods: {
            edit() {
                this.editing = false;
            },
            //Danh sách công ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            //Danh sách công ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            cancel() {
                this.$router.push({ path: '/system/users/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.user.roleIds = [];
                    vm.user.roleIds.push(vm.user.roleId);
                    this.$services.post('users', this.user).done((id) => {
                        let message = i18n.t("Messages.ConfirmCreate");
                        vm.$toastr.s(message);
                        vm.$router.push({ path: '/system/users/list' });
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
