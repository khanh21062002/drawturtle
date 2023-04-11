<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.OverTime.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12" offset="1">
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.Company')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true" v-if="editing"
                                                     v-model="overtime.comP_ID" :options="lstcompany">
                                            </select2>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.comP_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.DepartmentName')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <!--<select2 :options="lstdepartment" :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: true}"
                                                     @change="loadPersonByDept($event)" v-if="editing"
                                                     v-model="overtime.departmenT_ID">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadPersonByDept($event.id)" v-if="editing"
                                                        :options="treeselect.options"
														:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="overtime.departmenT_ID"></treeselect>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.departmenT_ID.$model"
                                                          :state="$v.overtime.departmenT_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTime.Detail.Form.DepartmentRequire')}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.departmenT_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.Employee')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" v-if="editing"
                                                     v-model="overtime.persoN_ID" :options="lstperson">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.persoN_ID.$model"
                                                          :state="$v.overtime.persoN_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTime.Detail.Form.EmployeeRequire')}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.persoN_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.Date')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <date-picker v-if="editing" style="width: 100%;" @change="checkHolidays($event)" v-model="overtime.datE_OT" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.datE_OT.$model"
                                                          :state="$v.overtime.datE_OT.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTime.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.formaT_DATE_OT }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.Shift')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     style="margin-left: 5px;" :settings="{allowClear: true}"
                                                     v-if="editing" @change="loadOtInOut($event)"
                                                     v-model="overtime.shifT_ID" :options="lstshiftime">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.shifT_ID.$model"
                                                          :state="$v.overtime.shifT_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTime.Detail.Form.ShiftRequire')}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" style="margin-left: 5px;" v-if="!editing">{{ overtime.shifT_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.BreakTime')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label" v-if="!editing">{{ overtime.breaK_TIME }}</label>
                                            <b-form-input type="number" min="0" step="1" v-if="editing"
                                                          @change="changeBreakTime($event)"
                                                          v-model="$v.overtime.breaK_TIME.$model" :state="$v.overtime.breaK_TIME.$dirty? !$v.overtime.breaK_TIME.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.OverTime.Detail.Form.BreakTimeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.TimeIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker v-if="editing" input-width="100%" @change="changeFrom($event)" style="width: 100%; margin-left: 5px;" advanced-keyboard hide-clear-button v-model="overtime.oT_IN"></vue-timepicker>
                                            <label class="col-form-label" style="margin-left: 5px;" v-if="!editing">{{ overtime.formaT_OT_IN }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.TimeOut')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <vue-timepicker v-if="editing" @change="changeTo($event)" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="overtime.oT_OUT"></vue-timepicker>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.formaT_OT_OUT }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.Hour')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <label class="col-form-label" style="width: 100%; margin-left: 5px;">{{ overtime.suM_OT }}</label>
                                        </b-form-group>
                                    </b-col>

                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.OverTime.Detail.Form.Note')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-textarea id="textarea" v-if="editing"
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
                                                {{$t('Timesheet.OverTime.Detail.Form.NoteRequire')}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.note }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group >
                                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageOverTime'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import i18n from '@/libs/i18n'

    export default {
        name: 'OverTimeDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                overtime: {
                    departmenT_ID: null,
                    persoN_ID: null,
                    datE_OT: null,
                    oT_IN: '00:00',
                    oT_OUT: '00:00',
                    breaK_TIME: 0,
                    suM_OT: 0,
                    shifT_ID: null,
                    note: null,
                    comP_ID: null,
                },
                lstcompany: [],
                lstdepartment: [],
                lstperson: [],
                lstshiftime: [],
                editing: false,
                isHolidays: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        validations: {
            overtime: {
                departmenT_ID: { required },
                persoN_ID: { required },
                datE_OT: { required },
                shifT_ID: { required },
                suM_OT: { required },
                note: { maxLength: maxLength(250) },
                breaK_TIME: {
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
            overtimeId() {
                return this.$route.params.overtimeId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.overtime.compId = accessToken.comId;
            this.loadDetails();
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
            async loadDetails() {
                var vm = this;
                return this.$services.get(`timesheet/overtime/${vm.overtimeId}`).done(overtime => {
                    vm.overtime = overtime;
                    if (vm.overtime.breaK_TIME == null) {
                        vm.overtime.breaK_TIME = 0;
                    };
                    vm.initListPerson(vm.overtime.departmenT_ID);
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
            initListPerson(departmenT_ID) {
                var vm = this;
                if (departmenT_ID == null) return;
                return this.$services.get(`lookup/persons-by-dept/${departmenT_ID}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.overtime.persoN_ID = vm.overtime.persoN_ID.toUpperCase();
                })
            },
            //Danh sách ca
            async loadWorkingShiftTime() {
                var vm = this;
                return this.$services.get(`lookup/working-shift-time`).done(lstshiftime => {
                    vm.lstshiftime = lstshiftime;
                })
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`timesheet/overtime/${this.overtimeId}`, this.overtime).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/timesheet/overtime/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadDetails();
                vm.editing = false;
            },
            convertTimeStamp(strTime) {
                var times = strTime.split(":");
                if (!times || times.length <= 0) return 0;
                var hours = parseInt(times[0]);
                var minutes = parseInt(times[1]);
                var result = hours + minutes / 60;
                return result;
            },
            loadOtInOut(shiftTimeId) {
                var vm = this;
                if (shiftTimeId == null) return;
                return this.$services.get(`timesheet/working-shift-times/${shiftTimeId}`).done(shifttime => {
                    if (vm.isHolidays != true) {
                        if (shifttime.startCheckInOverTime != null) {
                            vm.overtime.oT_IN = shifttime.startCheckInOverTime;
                        }
                        if (shifttime.endCheckOutOverTime != null) {
                            vm.overtime.oT_OUT = shifttime.endCheckOutOverTime;
                        }
                    } else {
                        if (shifttime.startCheckIn != null) {
                            vm.overtime.oT_IN = shifttime.startCheckIn;
                        }
                        if (shifttime.endCheckOut != null) {
                            vm.overtime.oT_OUT = shifttime.endCheckOut;
                        }
                    }
                });
            },
            changeFrom(fromNew) {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(fromNew.displayTime);
                var _oT_OUT = vm.convertTimeStamp(vm.overtime.oT_OUT);
                var _breaK_TIME = vm.overtime.breaK_TIME / 60;
                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN - _breaK_TIME;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24 - _breaK_TIME;
                }
                var rounded = Math.round(totalTime * 100) / 100;
                vm.overtime.suM_OT = rounded;
            },
            changeTo(toNew) {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(vm.overtime.oT_IN);
                var _oT_OUT = vm.convertTimeStamp(toNew.displayTime);
                var _breaK_TIME = vm.overtime.breaK_TIME / 60;
                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN - _breaK_TIME;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24 - _breaK_TIME;
                }
                var rounded = Math.round(totalTime * 100) / 100;
                vm.overtime.suM_OT = rounded;
            },
            changeBreakTime(breakTime) {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(vm.overtime.oT_IN);
                var _oT_OUT = vm.convertTimeStamp(vm.overtime.oT_OUT);
                var _breaK_TIME = breakTime / 60;
                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN - _breaK_TIME;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24 - _breaK_TIME;
                }
                var rounded = Math.round(totalTime * 100) / 100;
                vm.overtime.suM_OT = rounded;
            },
            checkHolidays(date) {
                var vm = this;
                return this.$services.get(`timesheet/overtime/check-holidays/${date}`).done(isHoliday => {
                    vm.isHolidays = isHoliday;
                    if (vm.overtime.shifT_ID != null) {
                        vm.loadOtInOut(vm.overtime.shifT_ID);
                    }
                });
            }
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
