<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.OverTimeHours.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12" offset="1">
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.Company')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="overtime.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.DepartmentName')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <!--<select2 :options="lstdepartment" :placeholder="this.$t('PlaceHolder.Select')" :settings="{allowClear: true}"
                                                     @change="loadPersonByDept($event)"
                                                     v-model="overtime.deptId">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadPersonByDept($event.id)"
											:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :options="treeselect.options"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                         v-model="overtime.deptId"></treeselect>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.deptId.$model"
                                                          :state="$v.overtime.deptId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTimeHours.Create.Form.DepartmentRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.Employee')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="overtime.personId" :options="lstperson">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.personId.$model"
                                                          :state="$v.overtime.personId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTimeHours.Create.Form.EmployeeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>

                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.Date')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <date-picker style="width: 100%;" v-model="overtime.day" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.day.$model"
                                                          :state="$v.overtime.day.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTimeHours.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.Shift')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     style="margin-left: 5px;" :settings="{allowClear: true}"
                                                     v-model="overtime.workingShiftId" :options="lstshiftime">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.workingShiftId.$model"
                                                          :state="$v.overtime.workingShiftId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTimeHours.Create.Form.ShiftRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.BreakTime')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" step="1"
                                                          v-model="$v.overtime.breakTime.$model" :state="$v.overtime.breakTime.$dirty? !$v.overtime.breakTime.$error : null">
                                                {{$t('Timesheet.OverTimeHours.Create.Form.Minute')}}
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTimeHours.Create.Form.BreakTimeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.TimeIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker input-width="100%" style="width: 100%; margin-left: 5px;" advanced-keyboard hide-clear-button v-model="overtime.oT_IN"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.TimeOut')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="overtime.oT_OUT"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.Hour')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <label class="col-form-label" style="width: 100%; margin-left: 5px;">{{totalTimeDraff }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTimeHours.Create.Form.Note')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-textarea id="textarea"
                                                             v-model="overtime.note"
                                                             placeholder=""
                                                             rows="3"
                                                             max-rows="6">
                                            </b-form-textarea>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.note.$model"
                                                          :state="$v.overtime.note.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTimeHours.Create.Form.NoteRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label-cols="1">
                                            <b-button v-if="authorize(['ManageOverTimeHours'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import { required, maxLength } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'
    import moment from 'moment';
    import i18n from '@/libs/i18n'

    export default {
        name: 'OverTimeCreate',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                overtime: {
                    compId: null,
                    deptId: null,
                    personId: null,
                    day: null,
                    oT_IN: '00:00',
                    oT_OUT: '00:00',
                    breakTime: 0,
                    timeFrom: null,
                    timeTo: null,
                    total: null,
                    workingShiftId: null,
                    note: null,
                },
                lstcompany: [],
                lstdepartment: [],
                lstperson: [],
                lstshiftime: [],
                totalCalculate: null,
                treeselect: {
                    value: null,
                    options: [],
                },

            }
        },
        validations: {
            overtime: {
                deptId: { required },
                personId: { required },
                day: { required },
                workingShiftId: { required },
                note: { maxLength: maxLength(250) },
                breakTime: {
                    checkBreakTime(value) {
                        if (value && this.overtime.oT_IN && this.overtime.oT_OUT) {
                            var _oT_IN = this.convertTimeStamp(this.overtime.oT_IN);
                            var _oT_OUT = this.convertTimeStamp(this.overtime.oT_OUT);
                            var totalTime = 0;
                            if (_oT_OUT >= _oT_IN) {
                                totalTime = _oT_OUT - _oT_IN - value / 60;
                            } else {
                                totalTime = _oT_OUT - _oT_IN + 24 - value / 60;
                            }
                            if (totalTime < 0) {

                                return false;
                            }
                            return true;

                        }
                        return true;
                    }
                }
            }
        },
        computed: {
            totalTime: function () {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(vm.overtime.oT_IN);
                var _oT_OUT = vm.convertTimeStamp(vm.overtime.oT_OUT);
                var _breaK_TIME = vm.overtime.breakTime / 60;
                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN - _breaK_TIME;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24 - _breaK_TIME;
                }

                return totalTime;
            },
            totalTimeDraff: function () {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(vm.overtime.oT_IN);
                var _oT_OUT = vm.convertTimeStamp(vm.overtime.oT_OUT);
                var _breaK_TIME = vm.overtime.breakTime / 60;
                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN - _breaK_TIME;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24 - _breaK_TIME;
                }

                var rounded = Math.round(totalTime * 100) / 100;
                return rounded;
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.overtime.compId = accessToken.comId;
            this.loadCompany();
            this.loadDepartment();
            this.loadWorkingShiftTime();
            this.loadDepartmentTree();
        },
        methods: {
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(lstdepartment => {
                    vm.treeselect.options = lstdepartment;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            //Danh sách nhân viên
            loadPersonByDept(departmenT_ID) {
                var vm = this;
                if (departmenT_ID == null) return;
                return this.$services.get(`lookup/persons-by-dept/${departmenT_ID}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.overtime.persoN_ID = null;
                })
            },
            //Danh sách ca
            async loadWorkingShiftTime() {
                var vm = this;
                return this.$services.get(`lookup/working-shift-time`).done(lstshiftime => {
                    vm.lstshiftime = lstshiftime;
                })
            },
            back() {
                this.$router.push({ path: '/timesheet/overtimehours/list' });
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.overtime.totalReal = vm.totalTime;

                    var day = new Date(vm.overtime.day);
                    //set time x
                    var times = vm.overtime.oT_IN.split(":");
                    if (!times || times.length <= 0) {

                    } else {
                        var hours = parseInt(times[0]);
                        var minutes = parseInt(times[1]);
                        day.setHours(hours, minutes, 0);
                        vm.overtime.timeFrom = day;
                    }
                    if (vm.overtime.oT_IN > vm.overtime.oT_OUT) {
                        // ca dem
                        var dayTo = new Date(vm.overtime.day);
                        // add a day
                        dayTo.setDate(dayTo.getDate() + 1);
                        //set time x
                        var timesTo = vm.overtime.oT_OUT.split(":");
                        if (!timesTo || timesTo.length <= 0) {

                        } else {
                            var hours = parseInt(timesTo[0]);
                            var minutes = parseInt(timesTo[1]);
                            dayTo.setHours(hours, minutes, 0);
                            vm.overtime.timeTo = dayTo;
                        }
                    } else {
                        // ca ngay
                        var dayTo = new Date(vm.overtime.day);
                        //set time x
                        var timesTo = vm.overtime.oT_OUT.split(":");
                        if (!timesTo || timesTo.length <= 0) {

                        } else {
                            var hours = parseInt(timesTo[0]);
                            var minutes = parseInt(timesTo[1]);
                            dayTo.setHours(hours, minutes, 0);
                            vm.overtime.timeTo = dayTo;
                        }
                    }
                    vm.overtime.timeFrom = moment(vm.overtime.timeFrom).format('YYYY/MM/DD HH:mm:ss');
                    vm.overtime.timeTo = moment(vm.overtime.timeTo).format('YYYY/MM/DD HH:mm:ss');

                    this.$services.post('timesheet/overtimehours', this.overtime).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/timesheet/overtimehours/list' });
                    });
                }
            },
            cancel() {
                var vm = this;
                vm.$router.push({ path: '/timesheet/overtimehours/list' });
            },
            convertTimeStamp(strTime) {
                var times = strTime.split(":");
                if (!times || times.length <= 0) return 0;
                var hours = parseInt(times[0]);
                var minutes = parseInt(times[1]);
                var result = hours + minutes / 60;
                return result;
            },
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
