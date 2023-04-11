<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('Nav.InOutStudentList.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('OverTimeHoursReport.FromDateReport')"
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
                                <b-form-group :label="this.$t('School.Parent.Detail.Label.StudentName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('School.Parent.Detail.Label.StudentPlaceholder')"
                                                  v-model="searchForm.personNameCode">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('School.Grades.List.SearchForm.UnitName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :settings="{multiple: true}"
                                             @change="loadClassByGrade((searchForm.gradeId))"
                                             v-model="searchForm.gradeId" :options="lstgrades">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('School.Class.List.SearchForm.ClassName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :settings="{multiple: true}"
                                             v-model="searchForm.deptId" :options="lstclass">
                                    </select2>
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
                        <WebDocumentViewer ref="refReportInOutStudent" :reportUrl="reportUrl" style="height: 800px;"></WebDocumentViewer>
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
                    gradeId: null,
                    deptId: null,
                    personNameCode: null,
                },
                headerExcel: [],
                lstdepartment: [],
                lstgrades: [],
                lstclass: [],
                reportUrl: null,
            }
        },
        validations: {
            searchForm: {
                filterDateFrom: { required },
            }
        },
        created() {
            var accessToken = Services.getAccessToken();
            var currentDate = new Date();
            this.searchForm.filterDateFrom = this.searchForm.filterDateTo = currentDate.getFullYear() + '-' + String(currentDate.getMonth() + 1).padStart(2, '0') + '-' + String(currentDate.getDate()).padStart(2, '0');
            this.reportUrl = 'ReportInOutStudent?p_lang=' + this.locale + '&p_company=' + accessToken.comId;
            this.loadDepartments();
            this.loadGrades();
            this.loadClass();
        },
        computed: {
            locale: function () {
                let lang = localStorage.getItem("lang");
                return lang;
            }
        },
        methods: {
            report() {
                var gradesName = '';
                var gradeId = '';
                var vm = this;
                if (vm.searchForm.gradeId == null) {
                    let locale = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                    if (locale == "vi") {
                        gradesName = 'Tất cả';
                    } else {
                        gradesName = 'All';
                    }
                } else {
                    for (var i = 0; i < vm.lstgrades.length; i++) {
                        var item = vm.lstgrades[i];
                        for (var j = 0; j < vm.searchForm.gradeId.length; j++) {
                            if (item.value == vm.searchForm.gradeId[j]) {
                                gradesName += item.text + ', ';
                                gradeId += item.value + ',';
                            }
                        }
                    }
                    gradesName = gradesName.substring(0, gradesName.length - 1);
                }
                var className = '';
                var classId = '';
                var vm = this;
                if (vm.searchForm.deptId == null) {
                    let locale = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                    if (locale == "vi") {
                        className = 'Tất cả';
                    } else {
                        className = 'All';
                    }
                } else {
                    for (var i = 0; i < vm.lstclass.length; i++) {
                        var item = vm.lstclass[i];
                        for (var j = 0; j < vm.searchForm.deptId.length; j++) {
                            if (item.value == vm.searchForm.deptId[j]) {
                                className += item.text + ', ';
                                classId += item.value + ',';
                            }
                        }
                    }
                    className = className.substring(0, className.length - 1);
                }
                var clientParameter = this.$refs.refReportInOutStudent.getClientParameter();
                clientParameter["p_filterDateFrom"](this.searchForm.filterDateFrom);
                clientParameter["p_grades"](gradeId);
                clientParameter["p_gradesName"](gradesName);
                clientParameter["p_class"](classId);
                clientParameter["p_className"](className);
                clientParameter["p_personNameCode"](this.searchForm.personNameCode);
                clientParameter["p_lang"](this.locale);
                this.$refs.refReportInOutStudent.refresh();
            },
            search() {
                this.$refs.overtimeTable.refresh();
            },
            //Danh sách khối
            loadGrades() {
                var vm = this;
                return this.$services.get(`lookup/grades`).done(res => {
                    vm.lstgrades = res;
                })
            },
            loadClass() {
                var vm = this;
                return this.$services.get(`lookup/classes`).done(res => {
                    vm.lstclass = res;
                })
            },
            loadClassByGrade(id) {
                var vm = this;
                var ids = String(id) + ',';
                if (id == null) return;
                return this.$services.get(`lookup/classes/${ids}`).done(res => {
                    vm.lstclass = res;
                })
            },
            //Danh sách phòng ban
            loadDepartments() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                })
            },
        }
    };
</script>
