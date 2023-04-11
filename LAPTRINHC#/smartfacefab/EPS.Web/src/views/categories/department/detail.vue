<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Department.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="2">
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
                            <b-col md="6" offset="2">
                                <b-form-group :label="this.$t('Categories.Department.Detail.Label.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="txt_code" v-if="editing"
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
                                    <label class="col-form-label" v-if="!editing">{{ department.code }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="2">
                                <b-form-group :label="this.$t('Categories.Department.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="txt_name" v-if="editing"
                                                  v-model="$v.department.name.$model"
                                                  :state="$v.department.name.$dirty? !$v.department.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.department.name.required">
                                        {{$t('Categories.Department.Detail.Label.Namerequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.department.name.maxLength">
                                        {{$t('Categories.Department.Detail.Label.DepartmentNamerequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ department.name }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="2">
                                <b-form-group :label="this.$t('Categories.Department.Detail.Label.NameParent')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :multiple="false" :disabled="!editing" :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="department.parentId" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageDepartment'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import { required, maxLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^[a-zA-Z0-9_]*$/)
    import i18n from '@/libs/i18n'

    export default {
        name: 'DepartmentDetail',
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                department: {
                    Id: null,
                    code: null,
                    name: null,
                    comId: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    userId: null
                },
                lstcompany: [],
                lstdepartment: [],
                editing: false,
                isAdmin: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        computed: {
            Id() {
                return this.$route.params.Id;
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
            await this.loadDepartmentDetail();
            await this.loadCompany();
            await this.loadDepartment();
            this.loadDepartmentTree();
        },
        methods: {
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree-exceptItself/${this.Id}`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                    if (res.length == 1) {
                        // neu chi return ra 1 ket qua, thi day khong phai la cong ti admin ( by default, admin is load all)
                        // disable select company and refresh search form
                        var company = vm.lstcompany[0];
                        vm.isAdmin = false;
                        vm.department.comId = company.id;
                        vm.$forceUpdate();
                    } else {
                        vm.isAdmin = true;
                    }
                });
            },
            async loadDepartmentDetail() {
                var vm = this;
                return this.$services.get(`department/${this.Id}`).done(res => {
                    vm.department = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/department/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadDepartmentDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`department/${this.Id}`, this.department).done(() => {
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
