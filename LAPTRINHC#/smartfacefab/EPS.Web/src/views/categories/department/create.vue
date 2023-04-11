<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Department.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">

                                    <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                   v-model="department.comId" :options="lstcompany">
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Department.Detail.Label.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">

                                    <b-form-input type="text" id="txt_code"
                                                  v-model.trim="$v.department.code.$model"
                                                  :state="$v.department.code.$dirty? !$v.department.code.$error : null">
                                    </b-form-input>

                                    <b-form-invalid-feedback v-if="!$v.department.code.required">
                                        {{$t('Categories.Department.Detail.Label.Coderequire')}}
                                    </b-form-invalid-feedback>


                                    <b-form-invalid-feedback v-if="!$v.department.code.maxLengthCode">
                                        {{$t('Categories.Department.Detail.Label.MalenghtCode')}}
                                    </b-form-invalid-feedback>
                                    <!--<b-form-invalid-feedback v-if="!$v.department.code.passwordPattern">
                                        {{$t('Categories.Department.Detail.Label.EspeciallyCode')}}
                                    </b-form-invalid-feedback>-->
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Department.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">

                                    <b-form-input type="text" id="txt_name"
                                                  v-model="$v.department.name.$model"
                                                  :state="$v.department.name.$dirty? !$v.department.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.department.name.required">
                                        {{$t('Categories.Department.Detail.Label.Namerequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.department.name.maxLength">
                                        {{$t('Categories.Department.Detail.Label.DepartmentNamerequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Department.Detail.Label.NameParent')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <!--<b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')"
                                                   v-model="department.parentid" :options="lstdepartment">
                                    </b-form-select>-->
                                    <treeselect :multiple="false" :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="department.parentid" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageDepartment'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import { required, maxLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^[a-zA-Z0-9_]*$/)
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'DepartmentDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                department: {
                    code: null,
                    name: null,
                    comId: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    userId: null,
                    parentid: null,
                },
                lstcompany: [],
                lstdepartment: [],
                isAdmin: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        computed: {
            parentId() {
                return this.$route.params.parentId;
            },
        },
        validations: {
            department: {
                code: {
                    //passwordPattern,
                    required,
                    maxLengthCode: maxLength(50),
                },
                name: {
                    required,
                    maxLength: maxLength(250),
                }
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.department.comId = accessToken.comId;
            this.department.departmenT_ID = accessToken.unitId;

            await this.loadCompany();
            await this.loadDepartment();
            this.loadDepartmentTree();
            if (this.parentId != null) {
                this.department.parentid = this.parentId;
            }
        },
        methods: {
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                })
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            back() {
                this.$router.push({ path: '/categories/department/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('department', this.department).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/categories/department/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/department/list' });
            }
        }
    }
</script>
<style scoped>
</style>
