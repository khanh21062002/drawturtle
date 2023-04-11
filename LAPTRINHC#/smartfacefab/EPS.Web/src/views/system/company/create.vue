<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{$t("System.Company.Create.Header")}} </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.ParentName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.company.parentId.$model"
                                                  :state="$v.company.parentId.$error ? false : null">
                                    </b-form-input>
                                    <treeselect :multiple="false"
                                                :options="treeselect.options"
                                                :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="company.parentId" />
                                    <b-form-invalid-feedback>
                                        {{$t("System.Company.Detail.Label.ParentRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <!--<b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.CompanyType')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <select2 :settings="{allowClear: true}"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="lstCompanyType"
                                             v-model="company.companyType">
                                    </select2>
                                    <b-form-input type="text"
                                                  style="display: none;"
                                                  v-model="$v.company.companyType.$model"
                                                  :state="$v.company.companyType.$dirty? !$v.company.companyType.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.company.companyType.required">
                                        {{$t("System.Company.Detail.Label.CompanyTypeRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>-->
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.company.code.$model"
                                                  :state="$v.company.code.$dirty? !$v.company.code.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.company.code.required">
                                        {{$t("System.Company.Detail.Label.CodeRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.company.name.$model"
                                                  :state="$v.company.name.$dirty? !$v.company.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t("System.Company.Detail.Label.NameRequire")}}
                                    </b-form-invalid-feedback>
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
                                                  v-model="company.address">
                                    </b-form-input>
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
                                                  v-model="$v.company.taxCode.$model"
                                                  :state="$v.company.taxCode.$dirty? !$v.company.taxCode.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.company.taxCode.numeric">
                                        {{$t("Guess.Create.Form.PhoneNumeric")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.company.taxCode.minLength">
                                        {{$t("System.Company.Detail.Label.TaxCodeRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageCompany'])"
                                              type="submit"
                                              style="width: 120px" variant="primary">
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
    import { required, numeric, maxLength, minLength } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'CompanyDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                company: {
                    projecT_ID: null,
                    buildinG_ID: null,
                    code: null,
                    name: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    tel: null,
                    taxCode: null,
                    supId: 1,
                    parentId: null,
                    hiddenParentField: null,
                    treeLevel: null,
                    companyType: null,
                },
                lstcompany: [],
                treeselect: {
                    value: null,
                    options: [],
                },
                lstCompanyType: [],
            }
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
                parentId: { required },
            }
        },
        created() {
            var accessToken = Services.getAccessToken();
            this.company.departmenT_ID = accessToken.unitId;
            this.loadCompany();
            this.loadCompanyTree();
            this.initListCompanyType();
            var parentId = this.parentId;
            if (parentId != null) {
                this.company.parentId = parentId;
            } else {
                this.company.parentId = accessToken.comId;
            }
        },
        computed: {
            parentId() {
                return this.$route.params.parentId;
            },
        },
        methods: {
            //Danh sách công ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            initListCompanyType() {
                var vm = this;
                vm.lstCompanyType = [];
                let item = {};
                item.id = 0;
                item.value = 0;
                item.text = this.$t('System.Company.Detail.Label.CompanyTypeItem1');
                vm.lstCompanyType.push(item);
                let item2 = {};
                item2.id = 1;
                item2.value = 1;
                item2.text = this.$t('System.Company.Detail.Label.CompanyTypeItem2');
                vm.lstCompanyType.push(item2);
            },
            //Danh sách công ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            back() {
                this.$router.push({ path: '/system/company/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('company', this.company).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/system/company/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/system/company/list' });
            }
        }
    }
</script>
<style scoped>
</style>
