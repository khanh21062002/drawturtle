<template>

    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.WorkingShiftTimes.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.Company')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <label class="col-form-label">{{ shifttime.compName }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.OT')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input v-if="editing" type="number" id="txt_earlyaccept" step="0.01"
                                                          v-model="shifttime.ot">
                                            </b-form-input>
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.ot }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.ShiftCode')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input v-if="editing" type="text" id="txt_code"
                                                          v-model="$v.shifttime.code.$model"
                                                          :state="$v.shifttime.code.$dirty? !$v.shifttime.code.$error : null">
                                            </b-form-input>
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.code }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Detail.Form.ShiftCodeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.StartCheckIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_name" style="display: none;"
                                                          v-model="$v.shifttime.startCheckIn.$model"
                                                          :state="$v.shifttime.startCheckIn.$dirty? !$v.shifttime.startCheckIn.$error : null">
                                            </b-form-input>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startCheckIn"></vue-timepicker>
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatStartCheckIn }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Detail.Form.StartCheckInRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.ShiftName')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input v-if="editing" type="text" id="txt_name"
                                                          v-model="$v.shifttime.name.$model"
                                                          :state="$v.shifttime.name.$dirty? !$v.shifttime.name.$error : null">
                                            </b-form-input>
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.name }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Detail.Form.ShiftNameRequire')}}
                                            </b-form-invalid-feedback>

                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.EndCheckIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_name" style="display: none;"
                                                          v-model="$v.shifttime.endCheckIn.$model"
                                                          :state="$v.shifttime.endCheckIn.$dirty? !$v.shifttime.endCheckIn.$error : null">
                                            </b-form-input>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endCheckIn"></vue-timepicker>
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatEndCheckIn }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkingShiftTimes.Detail.Form.EndCheckInRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.StartTime')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startTime"></vue-timepicker>
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatStartTime }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.StartCheckOut')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatStartCheckOut }}</label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startCheckOut"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.EndTime')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatEndTime }}</label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.EndCheckOut')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatEndCheckOut }}</label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endCheckOut"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.StartBreak')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatStartBreak }}</label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startBreak"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.NotCheckIn')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.notCheckIn }} {{$t('Timesheet.WorkingShiftTimes.Detail.Form.Minute')}}</label>
                                            <b-form-input v-if="editing" type="number" id="txt_notcheckin"
                                                          v-model="shifttime.notCheckIn">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.EndBreak')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatEndBreak }} </label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endBreak"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.NotCheckOut')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.notCheckOut }} {{$t('Timesheet.WorkingShiftTimes.Detail.Form.Minute')}}</label>
                                            <b-form-input v-if="editing" type="number" id="txt_notcheckout"
                                                          v-model="shifttime.notCheckOut">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.TotalTime')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label">{{ totalTime }} {{$t('Timesheet.WorkingShiftTimes.Detail.Form.Minute')}}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.LateAccept')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.lateAccept }} {{$t('Timesheet.WorkingShiftTimes.Detail.Form.Minute')}}</label>
                                            <b-form-input v-if="editing" type="number" id="txt_lateaccept"
                                                          v-model="shifttime.lateAccept">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.WorkingShift')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.workingShift }} </label>
                                            <b-form-input v-if="editing" type="number" id="txt_workingShift" min="0" max="10000" step="0.01"
                                                          v-model="shifttime.workingShift">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.EarlyAccept')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.earlyAccept }} {{$t('Timesheet.WorkingShiftTimes.Detail.Form.Minute')}}</label>
                                            <b-form-input v-if="editing" type="number" id="txt_earlyaccept"
                                                          v-model="shifttime.earlyAccept">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.StartOT')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatStartCheckInOverTime }} </label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.startCheckInOverTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.Detail.Form.EndOT')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="!editing" class="col-form-label">{{ shifttime.formatEndCheckOutOverTime }}</label>
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="shifttime.endCheckOutOverTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group>
                                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageShiftTime'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
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
                    formatStartTime: null,
                    endTime: '00:00',
                    formatEndTime: null,
                    startBreak: '00:00',
                    formatStartBreak: null,
                    endBreak: '00:00',
                    formatEndBreak: null,
                    total: 0,
                    workingShift: null,
                    ot: null,
                    lateAccept: null,
                    earlyAccept: null,
                    startCheckIn: '00:00',
                    formatStartCheckIn: null,
                    startCheckOut: '00:00',
                    formatStartCheckOut: null,
                    endCheckIn: '00:00',
                    formatEndCheckIn: null,
                    endCheckOut: '00:00',
                    formatEndCheckOut: null,
                    notCheckIn: null,
                    notCheckOut: null,
                    type: null,
                    startCheckInOverTime: '00:00',
                    formatStartCheckInOverTime: null,
                    endCheckOutOverTime: '00:00',
                    formatEndCheckOutOverTime: null,
                },
                lstcompany: [],
                isAdmin: false,
                isExist: false,
                editing: false,
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
            shifttimeId() {
                return this.$route.params.shifttimeId;
            },
            totalTime: function () {
                var vm = this;
                var _startTime = vm.convertTimeStampToMinutes(vm.shifttime.startTime);
                var _endTime = vm.convertTimeStampToMinutes(vm.shifttime.endTime);
                var _startBreak = vm.convertTimeStampToMinutes(vm.shifttime.startBreak);
                var _endBreak = vm.convertTimeStampToMinutes(vm.shifttime.endBreak);
                var totalTime1 = 0;
                var _startCheckOut = vm.convertTimeStampToMinutes(vm.shifttime.startCheckOut);
                var _endCheckOut = vm.convertTimeStampToMinutes(vm.shifttime.endCheckOut);

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
            this.loadWorkingShifttimesDetail();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            loadWorkingShifttimesDetail() {
                var vm = this;
                return this.$services.get(`timesheet/working-shift-times/${vm.shifttimeId}`).done(shifttime => {
                    vm.shifttime = shifttime;
                    if (vm.shifttime.startCheckInOverTime == null) {
                        vm.shifttime.startCheckInOverTime = vm.shifttime.endTime;
                    }
                    if (vm.shifttime.endCheckOutOverTime == null) {
                        vm.shifttime.endCheckOutOverTime = vm.shifttime.endCheckOut;
                    }
                });
            },
            async loadShiftTimeByCompId() {
                var vm = this;
                return this.$services.get(`timesheet/working-shift-times/loadShiftTimeByCompId/${vm.companyId}`).done(shifttime => {
                    if (shifttime) {
                        vm.isExist = true;
                    } else {
                        vm.isExist = false;
                    }
                });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.shifttime.total = vm.totalTime;
                    this.$services.put(`timesheet/working-shift-times/${this.shifttimeId}`, this.shifttime).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/timesheet/working-shift-times/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadWorkingShifttimesDetail();
                vm.editing = false;
            },
            convertTimeStampToMinutes(strTime) {
                var times = strTime.split(":");
                if (!times || times.length <= 0) return 0;
                var hours = parseInt(times[0]);
                var minutes = parseInt(times[1]);
                var result = hours * 60 + minutes;
                return result;
            }
        }
    }
</script>
<style scoped>
    div {
        width: 100%;
    }
</style>
