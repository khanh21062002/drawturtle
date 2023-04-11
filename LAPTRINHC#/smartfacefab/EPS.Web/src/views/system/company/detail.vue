<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{$t("System.Company.Detail.Header")}} </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.ParentName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <label class="col-form-label"> {{ company.parentName }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.company.code.$model"
                                                  :state="$v.company.code.$dirty? !$v.company.code.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.company.code.required">
                                        {{$t("System.Company.Detail.Label.CodeRequire")}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing"> {{ company.code }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.company.name.$model"
                                                  :state="$v.company.name.$dirty? !$v.company.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t("System.Company.Detail.Label.NameRequire")}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing"> {{ company.name }} </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.Address')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="company.address">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ company.address }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Guess.Create.Form.Phone')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.company.tel.$model"
                                                  :state="$v.company.tel.$dirty? !$v.company.tel.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.company.tel.numeric">
                                        {{$t("Guess.Create.Form.PhoneNumeric")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.company.tel.maxLength">
                                        {{$t("Guess.Create.Form.PhoneMaxLength")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.company.tel.minLength">
                                        {{$t("Guess.Create.Form.PhoneMinLength")}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ company.tel }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.TaxCode')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.company.taxCode.$model"
                                                  :state="$v.company.taxCode.$dirty? !$v.company.taxCode.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.company.taxCode.numeric">
                                        {{$t("Guess.Create.Form.PhoneNumeric")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.company.taxCode.minLength">
                                        {{$t("System.Company.Detail.Label.TaxCodeRequire")}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ company.taxCode }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button"
                                      variant="primary"
                                      @click="edit"
                                      style="width: 120px"
                                      v-if="!editing && authorize(['ManageCompany'])">
                                <i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="back"
                                      style="width: 120px"
                                      v-if="!editing">
                                <i class="fa fa-ban"></i> {{$t("Button.Back")}}
                            </b-button>
                            <b-button type="submit"
                                      variant="primary"
                                      v-if="editing"
                                      style="width: 120px">
                                <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="cancel"
                                      style="width: 120px"
                                      v-if="editing">
                                <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
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
    import { required, numeric, maxLength, minLength } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'

    export default {
        name: 'CompanyDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                company: {
                    id: null,
                    code: null,
                    name: null,
                    note: null,
                    status: null,
                    deleted: null,
                    supId: null,
                    statusName: null,
                    companyType: null,
                },
                editing: false,
                treeselect: {
                    value: null,
                    options: [],
                },
                listCompanyType: [],
            }
        },
        computed: {
            companyId() {
                return this.$route.params.companyId;
            },
        },
        validations: {
            company: {
                code: { required },
                name: { required },
                tel: {
                    numeric,
                    maxLength: maxLength(11),
                    minLength: minLength(10)
                },
                taxCode: {
                    numeric,
                    minLength: minLength(9),
                },
            }
        },
        created() {
            this.loadCompanyDetail();
        },
        methods: {
            loadCompanyDetail() {
                var vm = this;
                return this.$services.get(`company/${this.companyId}`).done(res => {
                    if (res && !res.companyType) {
                        res.companyType = 0;
                    }
                    vm.company = res;
                });
            },
            //Danh sách công ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/system/company/list' });
            },
            cancel() {
                var vm = this;
                this.loadCompanyDetail().done(() => vm.editing = false);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`company/${this.companyId}`, this.company).done(() => {
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
