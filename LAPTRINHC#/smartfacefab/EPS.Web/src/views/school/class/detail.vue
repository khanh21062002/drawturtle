<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('School.Class.Detail.Header')}}</strong>
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
                                <b-form-group :label="this.$t('School.Class.Detail.Label.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">

                                    <b-form-input type="text" id="txt_code" v-if="editing"
                                                  v-model.trim="$v.department.code.$model"
                                                  :state="$v.department.code.$dirty? !$v.department.code.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.department.code.required">
                                        {{$t('School.Class.Detail.Label.CodeRequire')}}
                                    </b-form-invalid-feedback>

                                    <b-form-invalid-feedback v-if="!$v.department.code.maxLengthCode">
                                        {{$t('School.Class.Detail.Label.CodeMaxlength')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.department.code.passwordPattern">
                                        {{$t('School.Class.Detail.Label.EspeciallyCode')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ department.code }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('School.Class.List.SearchForm.ClassName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">

                                    <b-form-input type="text" id="txt_name" v-if="editing"
                                                  v-model="$v.department.name.$model"
                                                  :state="$v.department.name.$dirty? !$v.department.name.$error : null">
                                    </b-form-input>


                                    <b-form-invalid-feedback v-if="!$v.department.name.required">
                                        {{$t('School.Class.Detail.Label.NameRequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.department.name.maxLength">
                                        {{$t('School.Class.Detail.Label.NameMaxlength')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ department.name }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('School.Class.Detail.Label.Unit')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left" :label-class="editing? 'required' : ''">

                                    <b-form-select class="col-sm-12" placeholder="Chọn giá trị" v-if="editing"
                                                   v-model="department.gradesId" :options="lstgrades">
                                    </b-form-select>
                                    <b-form-input type="text" id="txt_grades" style="display: none;"
                                                  v-model="$v.department.gradesId.$model"
                                                  :state="$v.department.gradesId.$dirty? !$v.department.gradesId.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.department.gradesId.required">
                                        {{$t('School.Class.Detail.Label.UnitRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ department.gradeName }}</label>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <!--<b-row>
        <b-col md="6">
            <b-form-group label="Trạng thái:"
                          :label-cols="4"
                          :horizontal="true"
                          label-align-md="right">
                <b-form-select class="col-sm-12" placeholder="Chọn giá trị" v-if="editing"
                               v-model="department.status">
                    <b-form-select-option value="1">Hoạt động</b-form-select-option>
                    <b-form-select-option value="0">Ngừng hoạt động</b-form-select-option>
                </b-form-select>
                <label class="col-form-label" v-if="!editing">{{ department.statusName }}</label>
            </b-form-group>
        </b-col>
    </b-row>-->

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
    const passwordPattern = helpers.regex('passwordPattern', /^[a-zA-Z0-9-]*$/)
    import i18n from '@/libs/i18n'

    export default {
        name: 'DepartmentDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                department: {
                    Id:null,
                    projecT_ID: null,
                    buildinG_ID: null,
                    code: null,
                    name: null,
                    comId: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    userId: null,
                    gradesId: null,
                }, lstcompany: [], lstdepartment: [],
                editing: false,
                isAdmin: false,
                lstgrades: [],
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
                    passwordPattern,
                    required,
                    maxLengthCode: maxLength(50),
                   
                },
                gradesId: { required },
                name: {
                    required,
                    maxLength: maxLength(250),
                }
            }
        },
        async created() {
            await this.loadDepartmentDetail();
            await this.loadCompany();
            //await this.loadDepartment();
            this.loadGrades();

        },
        methods: {
            //Danh sách công ty
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                });
            },
            async loadGrades() {
                var vm = this;
                return this.$services.get(`lookup/grades`).done(lstgrades => {
                    vm.lstgrades = lstgrades;

                })
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                   
                });
            },
            async loadDepartmentDetail() {
                var vm = this;
                return this.$services.get(`department/${this.Id}`).done(department => {
                    vm.department = department;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/school/class/list' });
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
                    this.$services.put(`department/class/${this.Id}`, this.department).done(() => {
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
