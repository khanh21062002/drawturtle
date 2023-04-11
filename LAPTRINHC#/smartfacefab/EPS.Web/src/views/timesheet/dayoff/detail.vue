<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.Dayoff.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.CompanyName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label class="col-form-label">{{ dayoff.compName }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.DepartmentName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.dayoff.deptId.$model"
                                                          :state="$v.dayoff.deptId.$error ? false : null">
                                            </b-form-input>
                                            <!--<select2 v-if="editing" :options="lstdepartment" @change="loadPersonByDept($event)" placeholder="Chọn giá trị" id="dpl_departmentid" :settings="{allowClear: true}"
                                                     v-model="dayoff.deptId">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadPersonByDept($event.id)" v-if="editing"
                                                        :options="treeselect.options"
														:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="dayoff.deptId" />

                                            <label v-if="!editing" class="col-form-label">{{ dayoff.deptName }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Detail.Form.DepartmentRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.EmployeeName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.dayoff.personId.$model"
                                                          :state="$v.dayoff.personId.$error ? false : null">
                                            </b-form-input>
                                            <select2 v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="dayoff.personId" :options="lstperson">
                                            </select2>
                                            <label v-if="!editing" class="col-form-label">{{ dayoff.personName }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Detail.Form.EmployeeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.Reason')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.dayoff.reason.$model"
                                                          :state="$v.dayoff.reason.$error ? false : null">
                                            </b-form-input>
                                            <b-form-input v-if="editing" type="text"
                                                          :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                          v-model="dayoff.reason">
                                            </b-form-input>
                                            <label v-if="!editing" class="col-form-label">{{ dayoff.reason }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Detail.Form.ReasonRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.DateFrom')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.dayoff.dateFrom.$model"
                                                          :state="$v.dayoff.dateFrom.$error ? false : null">
                                            </b-form-input>
                                            <date-picker v-if="editing" style="width: 100%;" v-model="dayoff.dateFrom" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <label v-if="!editing" class="col-form-label">{{ dayoff.formatDateFrom }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.DateTo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.dayoff.dateTo.$model"
                                                          :state="$v.dayoff.dateTo.$error ? false : null">
                                            </b-form-input>
                                            <label v-if="!editing" class="col-form-label">{{ dayoff.formatDateTo }}</label>
                                            <date-picker v-if="editing" style="width: 100%;" v-model="dayoff.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback v-if="!$v.dayoff.dateTo.required">
                                                {{$t('Timesheet.Dayoff.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                            <b-form-invalid-feedback v-if="!$v.dayoff.dateTo.isUnique">
                                                {{$t('Timesheet.Dayoff.Detail.Form.DateToUnique')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.HalfDay')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox :disabled="!editing" id="chk_sup" style="top: 10px;" @change="changeHalfDay($event)"
                                                             v-model="dayoff.isHalfDay" value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.TotalDay')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input v-if="editing" type="text" :disabled="true"
                                                          v-model="totalDay">
                                            </b-form-input>
                                            <label v-if="!editing" class="col-form-label">{{ dayoff.totalDay }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Detail.Form.Salary')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox :disabled="!editing" id="chk_salary" style="top: 10px;"
                                                             v-model="dayoff.salary" value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group>
                                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageDayOff'])">
                                                <i class="fa fa-pencil-square-o"></i>
                                                {{$t("Button.Edit")}}
                                            </b-button>
                                            <b-button type="button" variant="secondary" @click="back" v-if="!editing">
                                                <i class="fa fa-ban"></i>
                                                {{$t("Button.Back")}}
                                            </b-button>
                                            <b-button type="submit" variant="primary" v-if="editing">
                                                <i class="fa fa-floppy-o"></i>
                                                {{$t("Button.Submit")}}
                                            </b-button>
                                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing">
                                                <i class="fa fa-ban"></i>
                                                {{$t("Button.Cancel")}}
                                            </b-button>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
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
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                dayoff: {
                    id: 0,
                    compId: null,
                    deptId: null,
                    personId: null,
                    dateFrom: null,
                    dateTo: null,
                    isHalfDay: false,
                    salary: false,
                    reason: null,
                    totalDay: null,
                },
                lstcompany: [],
                lstdepartment: [],
                lstperson: [],
                isAdmin: false,
                isExist: false,
                editing: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        validations: {
            dayoff: {
                deptId: { required },
                personId: { required },
                dateFrom: { required },
                dateTo: {
                    required,
                    greaterFrom(value) {
                        var dateFromParse = new Date(this.dayoff.dateFrom);
                        dateFromParse.setHours(0, 0, 0, 0);
                        var dateToParse = new Date(this.dayoff.dateTo);
                        dateToParse.setHours(0, 0, 0, 0);
                        if (dateFromParse > dateToParse) {
                            return false
                        }
                        else
                            return true;
                    }
                },
                reason: { required },
            }
        },
        computed: {
            totalDay: function () {
                var rs = 0;
                if (this.dayoff.dateFrom && this.dayoff.dateTo) {
                    var dateFrom = new Date(this.dayoff.dateFrom);
                    dateFrom.setHours(0, 0, 0, 0);
                    var dateTo = new Date(this.dayoff.dateTo);
                    dateTo.setHours(0, 0, 0, 0);
                    rs = this.getDifferenceInDays(dateFrom, dateTo);
                    if (rs >= 0) {
                        rs = rs + 1;
                        if (this.dayoff.isHalfDay == "true" || this.dayoff.isHalfDay == true) {
                            rs = rs - 0.5;
                        }
                    } else {
                        rs = 0;
                    }
                }
                return rs;
            },
            dayoffId() {
                return this.$route.params.dayoffId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.dayoff.compId = accessToken.comId;
            this.loadCompany();
            this.loadDepartment();
            this.loadDetails();
            this.loadDepartmentTree();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(lstdepartment => {
                    vm.treeselect.options = lstdepartment;
                });
            },
            loadPersonByDept(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.dayoff.personId = null;
                })
            },
            initListPerson(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.dayoff.personId = vm.dayoff.personId.toUpperCase();
                })
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.dayoff.totalDay = vm.totalDay;
                    this.$services.put(`timesheet/dayoff/${this.dayoffId}`, this.dayoff).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            getDifferenceInDays(date1, date2) {
                const diffInMs = date2 - date1;
                return diffInMs / (1000 * 60 * 60 * 24);
            },
            changeHalfDay(isHalfDay) {
                var vm = this;
                if (vm.dayoff.dateFrom && (isHalfDay == 'true' || isHalfDay == true)) {
                    vm.dayoff.dateTo = vm.dayoff.dateFrom;
                }
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/timesheet/dayoff/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadDetails();
                vm.editing = false;
            },
            async loadDetails() {
                var vm = this;
                return this.$services.get(`timesheet/dayoff/${vm.dayoffId}`).done(dayoff => {
                    vm.dayoff = dayoff;
                    vm.initListPerson(vm.dayoff.deptId);
                });
            },
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }

    button {
        width: 100px;
    }
</style>
