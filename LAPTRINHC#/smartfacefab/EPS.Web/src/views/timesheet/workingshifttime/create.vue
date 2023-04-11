<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.WorkingShiftTimes.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.Company')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="shifttime.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.OT')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" max="10000" id="txt_earlyaccept" step="0.01"
                                                          v-model="shifttime.ot">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.ShiftCode')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_code"
                                                          v-model="$v.shifttime.code.$model"
                                                          :state="$v.shifttime.code.$dirty? !$v.shifttime.code.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Create.Form.ShiftCodeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.StartCheckIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_name" style="display: none;"
                                                          v-model="$v.shifttime.startCheckIn.$model"
                                                          :state="$v.shifttime.startCheckIn.$dirty? !$v.shifttime.startCheckIn.$error : null">
                                            </b-form-input>
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startCheckIn"></vue-timepicker>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Create.Form.StartCheckInRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.ShiftName')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_name"
                                                          v-model="$v.shifttime.name.$model"
                                                          :state="$v.shifttime.name.$dirty? !$v.shifttime.name.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Create.Form.ShiftNameRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.EndCheckIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_name" style="display: none;"
                                                          v-model="$v.shifttime.endCheckIn.$model"
                                                          :state="$v.shifttime.endCheckIn.$dirty? !$v.shifttime.endCheckIn.$error : null">
                                            </b-form-input>
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endCheckIn"></vue-timepicker>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Create.Form.EndCheckInRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.StartTime')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.StartCheckOut')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startCheckOut"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.EndTime')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.EndCheckOut')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endCheckOut"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.StartBreak')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startBreak"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.NotCheckIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" max="10000" id="txt_notcheckin"
                                                          v-model="shifttime.notCheckIn">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.EndBreak')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endBreak"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.NotCheckOut')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" max="10000" id="txt_notcheckout"
                                                          v-model="shifttime.notCheckOut">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.TotalTime')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label">{{ totalTime }} {{$t('Timesheet.WorkingShiftTimes.Create.Form.Minute')}}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.LateAccept')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" max="10000" id="txt_lateaccept"
                                                          v-model="shifttime.lateAccept">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.WorkingShift')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" max="10000" id="txt_workingShift" step="0.01"
                                                          v-model="shifttime.workingShift">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.EarlyAccept')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" min="0" max="10000" id="txt_earlyaccept"
                                                          v-model="shifttime.earlyAccept">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.StartOT')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startCheckInOverTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Create.Form.EndOT')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <vue-timepicker placeholder="00:00" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endCheckOutOverTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group>
                                            <b-button v-if="authorize(['ManageShiftTime'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
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
    import { required } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                shifttime: {
                    id: 0,
                    code: null,
                    name: null,
                    compId: null,
                    compName: null,
                    startTime: '00:00',
                    endTime: '00:00',
                    startBreak: '00:00',
                    endBreak: '00:00',
                    total: 0,
                    workingShift: null,
                    ot: null,
                    lateAccept: null,
                    earlyAccept: null,
                    startCheckIn: '00:00',
                    startCheckOut: '00:00',
                    endCheckIn: '00:00',
                    endCheckOut: '00:00',
                    notCheckIn: null,
                    notCheckOut: null,
                    type: null,
                    startCheckInOverTime: "00:00",
                    endCheckOutOverTime: "00:00",
                },
                lstcompany: [],
                isAdmin: false,
                isExist: false,
            }
        },
        validations: {
            shifttime: {
                name: { required },
                code: { required },
                startTime: { required },
                endTime: { required },
                startCheckIn: { required },
                endCheckIn: { required },
                startCheckOut: { required },
                endCheckOut: { required },
            }
        },
        computed: {
            companyId: function () {
                return this.shifttime.compId;
            },
            totalTime: function () {
                var vm = this;
                var _startTime = vm.convertTimeStampToMinutes(vm.shifttime.startTime);
                var _endTime = vm.convertTimeStampToMinutes(vm.shifttime.endTime);
                var _startBreak = vm.convertTimeStampToMinutes(vm.shifttime.startBreak);
                var _endBreak = vm.convertTimeStampToMinutes(vm.shifttime.endBreak);
                var _startCheckOut = vm.convertTimeStampToMinutes(vm.shifttime.startCheckOut);
                var _endCheckOut = vm.convertTimeStampToMinutes(vm.shifttime.endCheckOut);
                var totalTime1 = 0;
                var totalTime2 = 0;

                if (_endTime >= _startTime) {
                    totalTime1 = _endTime - _startTime;
                    totalTime2 = (_endBreak - _startBreak) <= 0 ? 0 : (_endBreak - _startBreak);
                    vm.shifttime.type = 0;
                    if (_endCheckOut < _startCheckOut) {
                        //this is night shift
                        vm.shifttime.type = 1;
                    }
                } else {
                    // night shift, then calute wrong
                    let _startTimeLeft = 60 * 24 - _startTime;
                    totalTime1 = _endTime + _startTimeLeft;
                    let _startBreakTimeLeft = 60 * 24 - _startBreak;
                    totalTime2 = (_endBreak - _startBreak) < 0 ? (_endBreak + _startBreakTimeLeft) : (_endBreak - _startBreak);
                    vm.shifttime.type = 1;
                }
                var result = totalTime1 - totalTime2;
                if (result < 0) result = 0;
                return result;
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.shifttime.compId = accessToken.comId;
            await this.loadCompany();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            async loadShiftTimeByCompId() {
                var vm = this;
                return this.$services.get(`timesheet/shifttime/loadShiftTimeByCompId/${vm.companyId}`).done(shifttime => {
                    if (shifttime) {
                        vm.isExist = true;
                    } else {
                        vm.isExist = false;
                    }
                });
            },
            back() {
                this.$router.push({ path: '/timesheet/working-shift-times/list' });
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.shifttime.total = vm.totalTime;
                    this.$services.post('timesheet/working-shift-times', this.shifttime).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/timesheet/working-shift-times/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/timesheet/working-shift-times/list' });
            },
            convertTimeStampToMinutes(strTime) {
                if (strTime) {
                    var times = strTime.split(":");
                    if (!times || times.length <= 0) return 0;
                    var hours = parseInt(times[0]);
                    var minutes = parseInt(times[1]);
                    var result = hours * 60 + minutes;
                    return result;
                }
            }
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
