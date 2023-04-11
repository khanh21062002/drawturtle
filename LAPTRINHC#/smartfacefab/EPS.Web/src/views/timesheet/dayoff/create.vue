<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.Dayoff.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.CompanyName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="dayoff.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.DepartmentName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.dayoff.deptId.$model"
                                                          :state="$v.dayoff.deptId.$error ? false : null">
                                            </b-form-input>
                                            <!--<select2 :options="lstdepartment" @change="loadPersonByDept($event)" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_departmentid" :settings="{allowClear: true}"
                                                     v-model="dayoff.deptId">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadPersonByDept($event.id)"
                                                        :options="treeselect.options"
														:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="dayoff.deptId" />


                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Create.Form.DepartmentRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.EmployeeName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.dayoff.personId.$model"
                                                          :state="$v.dayoff.personId.$error ? false : null">
                                            </b-form-input>
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="dayoff.personId" :options="lstperson">
                                            </select2>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Create.Form.EmployeeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.Reason')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.dayoff.reason.$model"
                                                          :state="$v.dayoff.reason.$error ? false : null">
                                            </b-form-input>
                                            <b-form-input type="text"
                                                          :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                          v-model="dayoff.reason">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Create.Form.ReasonRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.DateFrom')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.dayoff.dateFrom.$model"
                                                          :state="$v.dayoff.dateFrom.$error ? false : null">
                                            </b-form-input>
                                            <date-picker style="width: 100%;" v-model="dayoff.dateFrom" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.DateTo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.dayoff.dateTo.$model"
                                                          :state="$v.dayoff.dateTo.$error ? false : null">
                                            </b-form-input>
                                            <date-picker style="width: 100%;" v-model="dayoff.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback v-if="!$v.dayoff.dateTo.required">
                                                {{$t('Timesheet.Dayoff.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                            <b-form-invalid-feedback v-if="!$v.dayoff.dateTo.isUnique">
                                                {{$t('Timesheet.Dayoff.Create.Form.DateToUnique')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.HalfDay')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_sup" style="top: 10px;" @change="changeHalfDay($event)"
                                                             v-model="dayoff.isHalfDay" value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.TotalDay')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="inp_name" :disabled="true"
                                                          v-model="totalDay">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.Create.Form.Salary')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_salary" style="top: 10px;"
                                                             v-model="dayoff.salary" value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group>
                                            <b-button v-if="authorize(['ManageDayOff'])" type="submit" variant="primary">
                                                <i class="fa fa-floppy-o"></i>
                                                {{$t("Button.Submit")}}
                                            </b-button>
                                            <b-button type="button" variant="secondary" @click="cancel">
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
                        if (this.dayoff.isHalfDay == "true") {
                            rs = rs - 0.5;
                        }
                    } else {
                        rs = 0;
                    }
                }
                return rs;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.dayoff.compId = accessToken.comId;
            await this.loadCompany();
            this.loadDepartment();
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
            back() {
                this.$router.push({ path: '/timesheet/dayoff/list' });
            },
            loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            loadPersonByDept(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.dayoff.personId = null;
                })
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.dayoff.totalDay = vm.totalDay;
                    this.$services.post('timesheet/dayoff', this.dayoff).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/timesheet/dayoff/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/timesheet/dayoff/list' });
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
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(lstdepartment => {
                    vm.treeselect.options = lstdepartment;
                });
            },
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
