<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.TimeKeeping.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12" offset="1">
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Company')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="workinghours.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Department')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <!--<select2 :options="lstdepartment" :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: true}"
                                                     @change="loadPersonByDept($event)"
                                                     v-model="workinghours.deptId">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadPersonByDept($event.id)"
                                                        :options="treeselect.options"
														:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="workinghours.deptId" />

                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workinghours.deptId.$model"
                                                          :state="$v.workinghours.deptId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.TimeKeeping.Create.Form.DepartmentRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Employee')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="workinghours.personId" :options="lstperson">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workinghours.personId.$model"
                                                          :state="$v.workinghours.personId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.TimeKeeping.Create.Form.EmployeeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Date')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <date-picker :disabled-date="disabledAfterToday" :editable="false"
                                                         style="width: 100%;" v-model="workinghours.day" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                            </date-picker>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workinghours.day.$model"
                                                          :state="$v.workinghours.day.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.TimeKeeping.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Workday')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      v-slot="{ ariaDescribedby }"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="number" min="0" max="10" step="0.01"
                                                          v-model="$v.workinghours.total.$model"
                                                          :state="$v.workinghours.total.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.TimeKeeping.Create.Form.WorkdayRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Shift')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :options="lstshiftime" :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: true}"
                                                     v-model="workinghours.workingShiftId">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workinghours.workingShiftId.$model"
                                                          :state="$v.workinghours.workingShiftId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.TimeKeeping.Create.Form.ShiftRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.TimeKeeping.Create.Form.Note')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-textarea id="textarea"
                                                             v-model="workinghours.noteDay"
                                                             :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                             rows="3" max-rows="6">
                                            </b-form-textarea>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label-cols="1">
                                            <b-button v-if="authorize(['ManageTimeKeeping'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
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
        name: 'TimeKeepingCreate',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                workinghours: {
                    compId: null,
                    deptId: null,
                    personId: null,
                    workingShiftId: null,
                    day: null,
                    checkIn: null,
                    checkOut: null,
                    total: null,
                    noteDay: null
                },
                lstcompany: [],
                lstdepartment: [],
                lstperson: [],
                lstshiftime: [],
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        validations: {
            workinghours: {
                deptId: { required },
                personId: { required },
                day: { required },
                total: { required },
                workingShiftId: { required },
            }
        },
        computed: {},
        async created() {
            var accessToken = Services.getAccessToken();
            this.workinghours.compId = accessToken.comId;
            this.loadCompany();
            this.loadDepartment();
            this.loadWorkingShiftTime();
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
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            //Danh sách nhân viên theo phòng ban
            loadPersonByDept(depT_ID) {
                var vm = this;
                if (depT_ID == null) return;
                return this.$services.get(`lookup/persons-by-dept/${depT_ID}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.workinghours.personId = null;
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
                this.$router.push({ path: '/timesheet/timekeeping/list' });
            },
            disabledAfterToday(date) {
                return date > new Date();
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('timesheet/timekeeping', this.workinghours).done((id) => {
                        if (id && id > 0) {
                            vm.$toastr.s(i18n.t("Messages.TimeKeeping.ExistEmployee"));
                        } else {
                            vm.$toastr.s(i18n.t("Messages.TimeKeeping.Success"));
                        }
                        vm.$router.push({ path: '/timesheet/timekeeping/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/timesheet/timekeeping/list' });
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
