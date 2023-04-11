<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('Nav.ReportEmployeeInOut.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Area')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="lstArea"
                                             :settings="{ allowClear: true }"
                                             v-model="searchForm.areaId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Nav.Department')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="lstdepartment"
                                             :settings="{ allowClear: true }"
                                             v-model="searchForm.deptId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Table.EmployeeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.empName">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.Table.PersonCode')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.empCode">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('OverTimeHoursReport.FromDate')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  disabled
                                                  style="display: none;"
                                                  v-model="$v.searchForm.timeFrom.$model "
                                                  :state="$v.searchForm.timeFrom.$dirty? !$v.searchForm.timeFrom.$error : null">
                                    </b-form-input>
                                    <date-picker style="width: 100%;"
                                                 v-model="$v.searchForm.timeFrom.$model"
                                                 :state="$v.searchForm.timeFrom.$dirty? !$v.searchForm.timeFrom.$error : null"
                                                 valueType="YYYY-MM-DD"
                                                 format="DD-MM-YYYY">
                                    </date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t('OverTimeHoursReport.RequireDateMsg')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('OverTimeHoursReport.ToDate')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  disabled
                                                  style="display: none;"
                                                  v-model="$v.searchForm.timeTo.$model "
                                                  :state="$v.searchForm.timeTo.$dirty? !$v.searchForm.timeTo.$error : null">
                                    </b-form-input>
                                    <date-picker style="width: 100%;"
                                                 v-model="$v.searchForm.timeTo.$model"
                                                 :disabled-date="disabledBeforeTodayAndAfterAMonth"
                                                 :state="$v.searchForm.timeTo.$dirty? !$v.searchForm.timeTo.$error : null"
                                                 valueType="YYYY-MM-DD"
                                                 format="DD-MM-YYYY">
                                    </date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t('OverTimeHoursReport.RequireDateMsg')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <div style="text-align: center">
                                    <b-button size="md"
                                              class="btn btn-success"
                                              @click="report">
                                        <i class="fa fa-file-excel-o"></i> {{$t('OverTimeHoursReport.CreateButton')}}
                                    </b-button>
                                </div>
                            </b-col>
                        </b-row>
                        <WebDocumentViewer ref="refReport"
                                           :reportUrl="reportUrl"
                                           v-show="showReport"
                                           style="height: 800px;">
                        </WebDocumentViewer>
                    </b-form>
                    <br />
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { required } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'
    import moment from 'moment'

    export default {
        name: "App",
        data() {
            return {
                searchForm: {
                    compId: null,
                    areaId: null,
                    deptId: null,
                    empCode: null,
                    empName: null,
                    timeFrom: null,
                    timeTo: null,
                },
                lstdepartment: [],
                lstArea: [],
                reportUrl: null,
                showReport: false,
            }
        },
        validations: {
            searchForm: {
                timeFrom: { required },
                timeTo: { required },
            }
        },
        created() {
            this.reportUrl = 'ReportEmployeeInOut?p_lang=' + this.locale;

            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;

            var firstDay = moment().startOf('month');
            var currentDay = moment().subtract(0, 'days');
            if (currentDay < firstDay) {
                this.searchForm.timeFrom = moment(firstDay).format('YYYY-MM-DD');
                this.searchForm.timeTo = moment(firstDay).format('YYYY-MM-DD');
            } else {
                this.searchForm.timeFrom = moment(firstDay).format('YYYY-MM-DD');
                this.searchForm.timeTo = moment(currentDay).format('YYYY-MM-DD');
            }

            this.loadArea();
            this.loadDepartments();
        },
        computed: {
            locale: function () {
                let lang = localStorage.getItem("lang");
                return lang;
            }
        },
        methods: {
            report() {
                this.showReport = true;
                var departmentName = null;
                var areaName = null;
                var vm = this;

                if (vm.searchForm.areaId != null) {
                    var id = vm.searchForm.areaId;
                    var arName = vm.lstArea.filter(x => x.id == id);
                    areaName = arName[0].text;
                } else {
                    let locale = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                    if (locale == "vi") {
                        areaName = 'Tất cả';
                    } else {
                        areaName = 'All';
                    }
                }

                if (vm.searchForm.deptId != null) {
                    var id = vm.searchForm.deptId;
                    var deptName = vm.lstdepartment.filter(x => x.id == id);
                    departmentName = deptName[0].text;
                } else {
                    let locale = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                    if (locale == "vi") {
                        departmentName = 'Tất cả';
                    } else {
                        departmentName = 'All';
                    }
                }

                var clientParameter = this.$refs.refReport.getClientParameter();
                clientParameter["p_compId"](this.searchForm.compId);
                clientParameter["p_filterDateFrom"](this.searchForm.timeFrom);
                clientParameter["p_filterDateTo"](this.searchForm.timeTo);
                clientParameter["p_areaId"](this.searchForm.areaId);
                clientParameter["p_deptId"](this.searchForm.deptId);
                clientParameter["p_empCode"](this.searchForm.empCode);
                clientParameter["p_empName"](this.searchForm.empName);
                clientParameter["p_areaName"](areaName);
                clientParameter["p_deptName"](departmentName);

                clientParameter["p_lang"](this.locale);
                this.$refs.refReport.refresh();
            },
            disabledBeforeTodayAndAfterAMonth(date) {
                const dateTo = new Date(this.searchForm.timeFrom);
                dateTo.setHours(0, 0, 0, 0);
                return date < dateTo || date > new Date(dateTo.getTime() + 30 * 24 * 3600 * 1000);
            },
            loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done((res) => {
                    vm.lstArea = res;
                });
            },
            loadDepartments() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                })
            },
        }
    };
</script>
