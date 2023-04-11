<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{ $t('System.Role.Create.Header') }} </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Role.List.SearchForm.RoleName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.role.name.$model"
                                                  :state="$v.role.name.$dirty? !$v.role.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('System.Role.Detail.Label.NameRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Role.Detail.Label.Description')"
                                              label-align-md="left"
                                              label-for="description"
                                              :label-cols="4"
                                              :horizontal="true">
                                    <b-form-textarea type="text"
                                                     v-model="role.description">
                                    </b-form-textarea>
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
                                                   v-model="role.status">
                                        <b-form-select-option value="1">
                                            {{ $t('Categories.Person.Detail.Label.Activate') }}
                                        </b-form-select-option>
                                        <b-form-select-option value="0">
                                            {{ $t('Categories.Person.Detail.Label.Stop') }}
                                        </b-form-select-option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageRole'])"
                                              type="submit"
                                              style="width: 120px"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              style="width: 120px"
                                              @click="cancel">
                                        <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
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
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'

    export default {
        name: 'CreateRole',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                role: {
                    status: null,
                    name: null,
                    description: null
                }, lstcompany: [],
            }
        },
        validations: {
            role: { name: { required }, },
        },
        created() {
            this.loadCompany();
            this.role.status = 1;
        },
        methods: {
            //Danh sách công ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            cancel() {
                this.$router.push({ path: '/system/roles/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('roles', this.role).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/system/roles/list' });
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
