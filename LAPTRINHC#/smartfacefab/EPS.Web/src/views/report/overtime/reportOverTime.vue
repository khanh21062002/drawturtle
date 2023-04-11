<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Báo cáo làm thêm ngoài giờ</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Từ ngày:"
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
                                        Vui lòng nhập ngày làm thêm ngoài giờ
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Đến ngày:"
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
                                        Vui lòng nhập ngày làm thêm ngoài giờ
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group label="Phòng ban:"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 placeholder="Chọn giá trị" id="lstdepartment" :settings="{ allowClear: true }"
                                             v-model="searchForm.deptId" :options="lstdepartment">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Nhân viên:"
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
                                    <b-button size="md" class="btn btn-success" @click="report"><i class="fa fa-file-excel-o"></i> Tạo báo cáo</b-button>
                                </div>
                            </b-col>
                        </b-row>
                        <WebDocumentViewer ref="refReportOverTime" :reportUrl="reportUrl" style="height: 800px;"></WebDocumentViewer>
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
                    deptId: null,
                    filterNamePerson: null,
                },
                headerExcel: [],
                lstdepartment: [],
                reportUrl: null,
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
            this.reportUrl = 'ReportOverTime?p_company=' + accessToken.comId;

            var currentDate = new Date();
            this.searchForm.filterDateFrom = currentDate.getFullYear() + '-' + String(currentDate.getMonth() + 1).padStart(2, '0') + '-' + "01";
            this.searchForm.filterDateTo = currentDate.getFullYear() + '-' + String(currentDate.getMonth() + 1).padStart(2, '0') + '-' + String(currentDate.getDate() - 1).padStart(2, '0');
            this.loadDepartments();
        },
        methods: {
            report() {
                var departmentName = null;
                var vm = this;
                for (var i = 0; i < vm.lstdepartment.length; i++) {
                    var item = vm.lstdepartment[i];
                    if (item.value == vm.searchForm.deptId) {
                        departmentName = item.text;
                        break;
                    } else {
                        departmentName = 'Tất cả';
                    }
                }
                var clientParameter = this.$refs.refReportOverTime.getClientParameter();

                clientParameter["p_filterDateFrom"](this.searchForm.filterDateFrom);
                clientParameter["p_filterDateTo"](this.searchForm.filterDateTo);
                clientParameter["p_departmentId"](this.searchForm.deptId);
                clientParameter["p_filterNamePerson"](this.searchForm.filterNamePerson);
                clientParameter["p_departmentName"](departmentName);

                this.$refs.refReportOverTime.refresh();
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
        }
    };
</script>
