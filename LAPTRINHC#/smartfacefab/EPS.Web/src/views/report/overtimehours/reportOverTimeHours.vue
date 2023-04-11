<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('OverTimeHoursReport.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('OverTimeHoursReport.FromDate')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="filterDateFrom" disabled style="display: none;"
                                                  v-model="$v.searchForm.filterDateFrom.$model "
                                                  :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error : null">
                                    </b-form-input>
                                    <date-picker style="width: 100%;" v-model="$v.searchForm.filterDateFrom.$model"
                                                 :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error : null"
                                                 id="filterDateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
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
                                    <b-form-input type="text" id="filterDateTo" disabled style="display: none;"
                                                  v-model="$v.searchForm.filterDateTo.$model "
                                                  :state="$v.searchForm.filterDateTo.$dirty? !$v.searchForm.filterDateTo.$error : null">
                                    </b-form-input>
                                    <date-picker style="width: 100%;" v-model="$v.searchForm.filterDateTo.$model"
                                                 :disabled-date="disabledBeforeTodayAndAfterAMonth"
                                                 :state="$v.searchForm.filterDateTo.$dirty? !$v.searchForm.filterDateTo.$error : null"
                                                 id="filterDateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t('OverTimeHoursReport.RequireDateMsg')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('OverTimeHoursReport.Department')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <!--<select2 placeholder="Chọn giá trị" id="lstdepartment" :settings="{ allowClear: true }"
                                             v-model="searchForm.deptId" :options="lstdepartment">
                                    </select2>-->
                                    <treeselect :multiple="true"
                                                :flat="true"
                                                :limit="3"
                                                :autoSelectDescendants="true"
                                                :autoDeselectDescendants="true"
												:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="searchForm.deptId" />
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('OverTimeHoursReport.Employee')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-model="searchForm.filterNamePerson">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <div style="text-align: center">
                                    <b-button size="md" class="btn btn-success" @click="report"><i class="fa fa-file-excel-o"></i> {{$t('OverTimeHoursReport.CreateButton')}}</b-button>
                                </div>
                            </b-col>
                        </b-row>
                        <WebDocumentViewer ref="refReportOverTimeHours" :reportUrl="reportUrl" style="height: 800px;"></WebDocumentViewer>
                    </b-form>
                    <br />
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import downloadexcel from "vue-json-excel";
    import { required } from 'vuelidate/lib/validators';
    import Services from '@/utils/services'
    import moment from 'moment';
    export default {
        name: "App",
        components: {
            downloadexcel,
        },
        data() {
            return {
                searchForm: {
                    filterOdrNo: null,
                    filterDateFrom: null,
                    filterDateTo: null,
                    deptId: [],
                    filterNamePerson: null,
                },
                headerExcel: [],
                lstdepartment: [],
                reportUrl: null,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        }, //data
        validations: {
            searchForm: {
                filterDateFrom: { required },
                filterDateTo: { required },
            }
        },
        created() {
            var accessToken = Services.getAccessToken();
            if (this.locale == "vi") {
                this.reportUrl = 'ReportOverTimeHours?p_lang=' + this.locale + '&p_company=' + accessToken.comId + '&p_departmentName=Tất cả';
            } else {
                this.reportUrl = 'ReportOverTimeHours?p_lang=' + this.locale + '&p_company=' + accessToken.comId + '&p_departmentName=All';
            }

            var firstDay = moment().startOf('month');
            const yesterday = moment().subtract(1, 'days');
            if (yesterday < firstDay) {
                this.searchForm.filterDateFrom = moment(firstDay).format('YYYY-MM-DD');
                this.searchForm.filterDateTo = moment(firstDay).format('YYYY-MM-DD');
            } else {
                this.searchForm.filterDateFrom = moment(firstDay).format('YYYY-MM-DD');
                this.searchForm.filterDateTo = moment(yesterday).format('YYYY-MM-DD');
            }
            this.loadDepartments();
            this.loadDepartmentTree();
        },
        computed: {
            locale: function () {
                let lang = localStorage.getItem("lang");
                return lang;
            }
        },
        methods: {
            report() {
                var departmentName = null;
                var vm = this;
                var dptNames = [];
                if (vm.searchForm.deptId.length > 0) {

                    for (var i = 0; i < vm.searchForm.deptId.length; i++) {

                        var id = vm.searchForm.deptId[i];
                        // ...use `element`...
                        var deptName = vm.lstdepartment.filter(x => x.id == id);
                        if (deptName) {
                            dptNames.push(deptName[0].text);
                        }

                    }
                    departmentName = dptNames.join(',');

                } else {
                    let locale = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                    if (locale == "vi") {
                        departmentName = 'Tất cả';
                    } else {
                        departmentName = 'All';
                    }
                }
                var clientParameter = this.$refs.refReportOverTimeHours.getClientParameter();

                clientParameter["p_filterDateFrom"](this.searchForm.filterDateFrom);
                clientParameter["p_filterDateTo"](this.searchForm.filterDateTo);
                if (this.searchForm.deptId.length > 0) {
                    var deptIdsStr = this.searchForm.deptId.join(',');
                    //console.log(deptIdsStr);
                    clientParameter["p_departmentId"](deptIdsStr);
                } else {
                    clientParameter["p_departmentId"]('');
                }
                clientParameter["p_filterNamePerson"](this.searchForm.filterNamePerson);
                clientParameter["p_departmentName"](departmentName);
                clientParameter["p_lang"](this.locale);
                this.$refs.refReportOverTimeHours.refresh();
            },
            search() {
                this.$refs.overtimeTable.refresh();
            },
            disabledBeforeTodayAndAfterAMonth(date) {
                const dateTo = new Date(this.searchForm.filterDateFrom);
                dateTo.setHours(0, 0, 0, 0);
                return date < dateTo || date > new Date(dateTo.getTime() + 30 * 24 * 3600 * 1000);
            },
            //Danh sách phòng ban
            loadDepartments() {
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
        }
    };
</script>
