<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>
                            {{$t("ReportWorkingTime.Header")}}
                        </strong>
                    </div>
                    <b-form @submit.prevent="search">

                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.ExportExcel.HeaderExcel.Company')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :multiple="false"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                style="width:100%"
                                                v-model="searchForm.compId" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.ExportExcel.HeaderExcel.DateFrom')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  id="filterDateFrom"
                                                  disabled
                                                  style="display: none"
                                                  v-model="$v.searchForm.filterDateFrom.$model"
                                                  :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error: null">
                                    </b-form-input>
                                    <date-picker style="width: 100%"
                                                 v-model="$v.searchForm.filterDateFrom.$model"
                                                 :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error: null"
                                                 id="filterDateFrom"
                                                 valueType="MM-DD-YYYY"
                                                 format="DD-MM-YYYY"></date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t("Reporting.Event.List.SearchForm.DateRequire")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback>
                                        {{ $t('Reporting.Event.List.SearchForm.DateFromRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.ExportExcel.HeaderExcel.DateTo')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  id="filterDateTo"
                                                  disabled
                                                  style="display: none"
                                                  v-model="$v.searchForm.filterDateTo.$model"
                                                  :state="$v.searchForm.filterDateTo.$dirty? !$v.searchForm.filterDateTo.$error: null ">
                                    </b-form-input>
                                    <date-picker style="width: 100%"
                                                 v-model="$v.searchForm.filterDateTo.$model"
                                                 :disabled-date="disabledBeforeTodayAndAfterAMonth"
                                                 :state="$v.searchForm.filterDateTo.$dirty? !$v.searchForm.filterDateTo.$error: null"
                                                 id="filterDateTo"
                                                 valueType="MM-DD-YYYY"
                                                 format="DD-MM-YYYY"></date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t("Reporting.Event.List.SearchForm.DateRequire")}}
                                    </b-form-invalid-feedback>
                                    <!--<b-form-invalid-feedback>
                                        {{ $t('Reporting.Event.List.SearchForm.DateFromRequire') }}
                                    </b-form-invalid-feedback>-->
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <div style="text-align: center">
                                    <b-button size="md" class="btn btn-success" @click="report">
                                        <i class="fa fa-file-excel-o"></i>
                                        {{ $t("Button.Report") }}
                                    </b-button>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" style="height: 1000px; padding-top: 20px;" v-show="showReport">
                                <WebDocumentViewer ref="refSummaryOfArrivals"
                                                   :reportUrl="reportUrl"
                                                   style="height: 1000px"></WebDocumentViewer>
                            </b-col>
                        </b-row>

                    </b-form>
                    <br />
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import downloadexcel from "vue-json-excel";
    import { required } from "vuelidate/lib/validators";
    import Services from "@/utils/services";
    import i18n from "@/libs/i18n";

    export default {
        name: "Report",
        components: {
            downloadexcel,
        },
        data() {
            return {
                searchForm: {
                    filterDateFrom: null,
                    filterDateTo: null,
                    compId: null,
                },
                headerExcel: [],
                lstmachineStation: [],
                lstArea: [],
                lstcompany: [],
                reportUrl: null,
                calculateDto: {
                    dateFrom: null,
                    dateTo: null
                },
                showReport: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            };
        },
        validations: {
            searchForm: {
                filterDateFrom: { required },
                filterDateTo: { required },
            },
        },
        created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            //this.reportUrl = 'Reporttemptest?p_lang=' + this.locale;
            if (this.locale == "vi") {
                this.reportUrl = 'SummaryOfArrivals?p_lang=' + this.locale;
                //    this.reportUrl = 'ReportInOutEmployee?p_company=6 ';
            } else {
                this.reportUrl = 'SummaryOfArrivals?p_lang=' + this.locale;
            }
            var currentDate = new Date();
            //this.searchForm.filterDateFrom =
            //    String(currentDate.getMonth() + 1).padStart(2, "0")
            //    + "-" +
            //    "01" + "-" + currentDate.getFullYear();
            this.searchForm.filterDateFrom = String(currentDate.getMonth() + 1).padStart(2, "0") +
                "-" +
                String(currentDate.getDate()).padStart(2, "0")
                + "-" + currentDate.getFullYear();
            this.searchForm.filterDateTo =
                String(currentDate.getMonth() + 1).padStart(2, "0") +
                "-" +
                String(currentDate.getDate()).padStart(2, "0")
                + "-" + currentDate.getFullYear();
            this.loadCompany();
            this.loadCompanyTree();
            //this.loadMachineStation();
            /*+ this.searchForm.filterDateFrom + '&EndDate=' + this.searchForm.filterDateTo + 'ShiftId=' + (this.searchForm.shiftId) + 'DepartId=' + this.searchForm.deptId;*/
        },
        computed: {
            locale: function () {
                let lang = localStorage.getItem("lang");
                return lang;
            },

        },
        methods: {
            report() {
                var vm = this;
                vm.showReport = true;
                var comName = null;
                for (var i = 0; i < vm.lstcompany.length; i++) {
                    var item = vm.lstcompany[i];
                    if (item.value == vm.searchForm.compId) {
                        comName = item.text;
                        break;
                    } else {
                        comName = vm.$t("Reporting.Event.List.ExportExcel.All");
                    }
                }
                if (comName == null) {
                    comName = vm.$t("Reporting.Event.List.ExportExcel.All");
                }
                
                var clientParameter = this.$refs.refSummaryOfArrivals.getClientParameter();

                clientParameter["p_toDate"](this.searchForm.filterDateTo);
                clientParameter["p_compId"](this.searchForm.compId);
                clientParameter["p_fromDate"](this.searchForm.filterDateFrom);
                clientParameter["p_comName"](comName);
                clientParameter["p_lang"](this.locale);
                this.$refs.refSummaryOfArrivals.refresh();

            },
            search() {
                this.$refs.refSummaryOfArrivals.refresh();
            },
            disabledBeforeTodayAndAfterAMonth(date) {
                const dateTo = new Date(this.searchForm.filterDateFrom);
                dateTo.setHours(0, 0, 0, 0);
                return (
                    date < dateTo ||
                    date >= new Date(dateTo.getTime() + 30 * 24 * 3600 * 1000)
                );
            },
            //Danh sách khu vực
            loadArea() {
                var vm = this;
                return this.$services.get(`lookup/areas`).done((res) => {
                    vm.lstArea = res;
                });
            },
            //danh sách đơn vị
            async loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done((res) => {
                    vm.treeselect.options = res;
                });
            },
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companyas`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách phòng ban
            loadMachineStation() {
                var vm = this;
                return this.$services.get(`lookup/machineStation`).done((lstmachineStation) => {
                    vm.lstmachineStation = lstmachineStation;
                });
            },
        },
    };
</script>
