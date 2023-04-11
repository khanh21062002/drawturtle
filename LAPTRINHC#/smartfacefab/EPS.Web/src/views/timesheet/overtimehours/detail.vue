<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Chi tiết làm thêm ngoài giờ</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12" offset="1">
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Đơn vị:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 placeholder="Chọn giá trị" :disabled="true" v-if="editing"
                                                     v-model="overtime.comP_ID" :options="lstcompany">
                                            </select2>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.comP_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Phòng ban:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 :options="lstdepartment" placeholder="Chọn giá trị" :settings="{allowClear: true}"
                                                     @change="loadPersonByDept($event)" v-if="editing"
                                                     v-model="overtime.departmenT_ID">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.departmenT_ID.$model"
                                                          :state="$v.overtime.departmenT_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn phòng ban
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.departmenT_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Nhân viên:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 placeholder="Chọn giá trị" v-if="editing"
                                                     v-model="overtime.persoN_ID" :options="lstperson">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.persoN_ID.$model"
                                                          :state="$v.overtime.persoN_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn nhân viên
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.persoN_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Ngày:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <date-picker v-if="editing" style="width: 100%;" v-model="overtime.datE_OT" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.datE_OT.$model"
                                                          :state="$v.overtime.datE_OT.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn ngày
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.formaT_DATE_OT }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group label="Ca:"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 placeholder="Chọn giá trị" style="margin-left: 5px;" :settings="{allowClear: true}" v-if="editing" @change="loadOtInOut($event)"
                                                     v-model="overtime.shifT_ID" :options="lstshiftime">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.overtime.shifT_ID.$model"
                                                          :state="$v.overtime.shifT_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn ca
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" style="margin-left: 5px;" v-if="!editing">{{ overtime.shifT_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group label="Số giờ thực tế:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" :label-class="editing? 'required' : ''">
                                            <label class="col-form-label" v-if="!editing">{{ overtime.suM_OT }}</label>
                                            <b-form-input type="number" min="0" step="0.01" v-if="editing"
                                                          v-model="$v.overtime.suM_OT.$model"
                                                          :state="$v.overtime.suM_OT.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng nhập số giờ
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4" offset="1">
                                        <b-form-group label="Từ giờ:"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <vue-timepicker v-if="editing" input-width="100%; " style="width: 100%;margin-left: 5px;" advanced-keyboard hide-clear-button v-model="overtime.oT_IN"></vue-timepicker>
                                            <label class="col-form-label" style="margin-left: 5px;" v-if="!editing">{{ overtime.formaT_OT_IN }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group label="Đến giờ:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <vue-timepicker v-if="editing" input-width="100%" style="width: 100%" advanced-keyboard hide-clear-button v-model="overtime.oT_OUT"></vue-timepicker>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.formaT_OT_OUT }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>

                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Ghi chú:"
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
                                                Số ký tự không được vượt quá 250
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ overtime.note }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label-cols="1" >
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
            }
        },
        computed: {
            overtimeId() {
                return this.$route.params.overtimeId;
            },
            totalTime: function () {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(vm.overtime.oT_IN);
                var _oT_OUT = vm.convertTimeStamp(vm.overtime.oT_OUT);

                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24;
                }

                return totalTime;
            },
            totalTimeDraff: function () {
                var vm = this;
                var _oT_IN = vm.convertTimeStamp(vm.overtime.oT_IN);
                var _oT_OUT = vm.convertTimeStamp(vm.overtime.oT_OUT);

                var totalTime = 0;
                if (_oT_OUT >= _oT_IN) {
                    totalTime = _oT_OUT - _oT_IN;
                } else {
                    totalTime = _oT_OUT - _oT_IN + 24;
                }

                var rounded = Math.round(totalTime * 100) / 100;
                return rounded;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.overtime.compId = accessToken.comId;
            this.loadDetails();
            this.loadCompany();
            this.loadDepartment();
            this.loadWorkingShiftTime();
        },
        methods: {
            async loadDetails() {
                var vm = this;
                return this.$services.get(`timesheet/overtime/${vm.overtimeId}`).done(overtime => {
                    vm.overtime = overtime;
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
                    //vm.overtime.suM_OT = vm.totalTime;
                    this.$services.put(`timesheet/overtime/${this.overtimeId}`, this.overtime).done(() => {
                        vm.$toastr.s('Cập nhật OT ngoài giờ thành công');
                        vm.cancel();
                    });
                }
            },
            edit() {
                this.editing = true;
            },
            back() {
                var vm = this;
                vm.$router.push({ path: '/timesheet/overtimehours/list' });
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
                return this.$services.get(`timesheet/working-shift-times/${shiftTimeId}`).done(shifttime => {
                    if (shifttime.startCheckInOverTime != null) {
                        vm.overtime.oT_IN = shifttime.startCheckInOverTime;
                    } else {
                        vm.overtime.oT_IN = shifttime.endTime;
                    }
                    if (shifttime.endCheckOutOverTime != null) {
                        vm.overtime.oT_IN = shifttime.endCheckOutOverTime;
                    } else {
                        vm.overtime.oT_IN = shifttime.endCheckOut;
                    }
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
