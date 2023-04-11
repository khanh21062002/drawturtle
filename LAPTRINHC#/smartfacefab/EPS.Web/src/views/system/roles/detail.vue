<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{ $t('System.Role.Detail.Header') }} </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Role.List.SearchForm.RoleName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.role.name.$model"
                                                  :state="$v.role.name.$dirty? !$v.role.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('System.Role.Detail.Label.NameRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ $t(role.name) }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Role.Detail.Label.Description')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-textarea type="text"
                                                     v-model="role.description"
                                                     v-if="editing">
                                    </b-form-textarea>
                                    <label class="col-form-label" v-if="!editing"> {{ role.description }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Role.Detail.Label.Status')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-select class="col-sm-12"
                                                   :placeholder="this.$t('PlaceHolder.Select')"
                                                   v-if="editing"
                                                   v-model="role.status">
                                        <b-form-select-option value="1">
                                            {{ $t('Categories.Person.Detail.Label.Activate') }}
                                        </b-form-select-option>
                                        <b-form-select-option value="0">
                                            {{ $t('Categories.Person.Detail.Label.Stop') }}
                                        </b-form-select-option>
                                    </b-form-select>
                                    <label class="col-form-label" v-if="!editing"> {{ $t(role.statusName) }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button"
                                      variant="primary"
                                      @click="edit"
                                      style="width: 120px"
                                      v-if="!editing && authorize(['ManageRole'])">
                                <i class="fa fa-pencil-square-o"></i> {{ $t("Button.Edit") }}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="back"
                                      style="width: 120px"
                                      v-if="!editing">
                                <i class="fa fa-ban"></i> {{ $t("Button.Back")}}
                            </b-button>
                            <b-button type="submit"
                                      variant="primary"
                                      v-if="editing"
                                      style="width: 120px">
                                <i class="fa fa-floppy-o"></i> {{ $t("Button.Submit")}}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="cancel"
                                      style="width: 120px"
                                      v-if="editing">
                                <i class="fa fa-ban"></i> {{ $t("Button.Cancel")}}
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
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'

    export default {
        name: 'RoleDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                role: {
                    id: null,
                    name: null,
                    description: null
                },
                lstcompany: [],
                editing: false
            }
        },
        computed: {
            roleId() {
                return this.$route.params.roleId;
            },
        },
        validations: {
            role: {
                name: { required }
            }
        },
        created() {
            this.loadRoleDetail();
            this.loadCompany();
        },
        methods: {
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            loadRoleDetail() {
                var vm = this;
                return this.$services.get(`roles/${this.roleId}`).done(res => {
                    vm.role = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/system/roles' });
            },
            cancel() {
                var vm = this;
                this.loadRoleDetail().done(() => vm.editing = false);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`roles/${this.roleId}`, this.role).done(() => {
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
