<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.TimeKeeping.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.DepartmentName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :autoDeselectDescendants="true" :limit="3" :multiple="true" :flat="true" :autoSelectDescendants="true"
                                                :options="treeselect.options"
												:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="deptId" />
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.EmployeeGroup')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstgroup" :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.groupId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.EmployeeCode')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterCodePerson">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.EmployeeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterNamePerson">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.DateFrom')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                    <b-form-input style="display: none;" v-model="$v.searchForm.filterDateFrom.$model" :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t("Timesheet.TimeKeeping.List.Form.DateFromRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Form.DateTo')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="9" offset="2">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="2" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                    <b-button @click="exportExcel" size="md" class="btn btn-success">{{$t("Button.ExportExcel")}}</b-button>
                                    <b-button type="button" variant="info" style="width: 150px !important" @click="showDescriptionReport = true">{{$t("Button.DetailReport")}}</b-button>
                                    <b-button type="button" class="btn btn-success" variant="" @click="darkModal = true">{{$t("Button.Recalculation")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            dataUrl="timesheet/timekeeping"
                            :multiLanguage="true"
                            gridName="Timesheet.TimeKeeping.List.HeaderTable"
                            ref="timekeepingTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/timekeeping/create" tag="button" class="btn btn-primary btn-same-size" size="md" v-if="authorize(['ManageTimeKeeping'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <router-link to="/timesheet/timekeeping/import" style="background: cadetblue;" tag="button" class="btn btn-primary btn-same-size" size="md" v-if="authorize(['ManageTimeKeeping'])">
                                {{$t("Button.ImportExcel")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')"
                                          v-if="authorize(['ManageTimeKeeping']) && row.item.typeDay == 5">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                    <confirmModal ref="confirmModal">
                    </confirmModal>
                    <CModal :show.sync="darkModal"
                            :no-close-on-backdrop="true"
                            :title="this.$t('Timesheet.TimeKeeping.List.Recalculation.Title')"
                            size="xl"
                            color="dark">
                        <b-form @submit.prevent="save" @submit="$v.$touch()">
                            <b-row>
                                <b-col md="11">
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Recalculation.Company')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left"
                                                          label-class="required">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                         v-model="calculateDto.compId" :options="lstcompany">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Recalculation.Type')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left"
                                                          label-class="required">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                         v-model="calculateDto.type" :options="lsttype">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Recalculation.Department')"
                                                          v-if="showDept"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left"
                                                          label-class="required">
                                                <treeselect :multiple="false" @select="loadListPersonByDept($event.id)"
                                                            :options="treeselect.options"
															:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                            :placeholder="this.$t('PlaceHolder.Select')"
                                                            v-model="calculateDto.deptId" />
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Recalculation.Employee')"
                                                          v-if="showPerson"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left"
                                                          label-class="required">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                         v-model="calculateDto.personId" :options="lstperson">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Recalculation.DateFrom')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="right" label-class="required">
                                                <date-picker style="width: 100%;" type="date"
                                                             v-model="calculateDto.dateFrom"
                                                             :editable="false"
                                                             id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                                </date-picker>
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.Recalculation.DateTo')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <date-picker style="width: 100%;" type="date" v-model="calculateDto.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                </b-col>
                            </b-row>
                        </b-form>
                        <template #footer>
                            <b-button v-if="authorize(['ManageTimeKeeping'])" style="width: 200px !important" type="button" @click="calculateWorkingHours" variant="primary"> {{$t("Button.Recalculation")}}</b-button>
                            <b-button type="button" variant="secondary" @click="darkModal = false"><i class="fa fa-ban"></i>{{$t("Button.Close")}}</b-button>
                        </template>
                    </CModal>
                    <!--Báo cáo chi tiết-->
                    <CModal :show.sync="showDescriptionReport"
                            :no-close-on-backdrop="true"
                            :title="this.$t('Timesheet.TimeKeeping.List.DetailReport.Title')"
                            size="xl"
                            color="dark">
                        <b-form @submit.prevent="save" @submit="$v.$touch()">
                            <b-row>
                                <b-col md="11">
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.DetailReport.Company')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left"
                                                          label-class="required">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                         v-model="exportDetailDto.compId" :options="lstcompany">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.DetailReport.Department')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="right" label-class="required">
                                                <treeselect :multiple="false"
															:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                            :options="treeselect.options" @select="loadListPersonByDeptReport($event.id)"
                                                            :placeholder="this.$t('PlaceHolder.Select')"
                                                            v-model="exportDetailDto.deptId" />
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.DetailReport.Employee')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                         v-model="exportDetailDto.personId" :options="lstpersonRp">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.DetailReport.DateFrom')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <date-picker style="width: 100%;" type="date"
                                                             v-model="exportDetailDto.dateFrom"
                                                             :editable="false"
                                                             id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                                </date-picker>
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.TimeKeeping.List.DetailReport.DateTo')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <date-picker style="width: 100%;" type="date" v-model="exportDetailDto.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                </b-col>
                            </b-row>
                        </b-form>
                        <template #footer>
                            <b-button size="md" class="btn btn-success">
                                <download-excel :header="headerExcelDetail" :fetch="reportDetails" :fields="export_fields_details" name="Báo cáo chi tiết">
                                    {{$t("Button.ExportExcel")}}
                                </download-excel>
                            </b-button>
                            <b-button type="button" variant="secondary" @click="showDescriptionReport = false"><i class="fa fa-ban"></i>{{$t("Button.Close")}}</b-button>
                        </template>
                    </CModal>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'
    import Services from '@/utils/services'
    import { Group } from '@devexpress/analytics-core/analytics-internal';
    import moment from 'moment';
    import i18n from '@/libs/i18n';

    export default {
        name: 'ListTimeKeeping',
        mixins: [authorizationMixin],
        data() {
            return {
                timekeeping: {
                    id:null
                },
                export_fields: {
                    'Timesheet.TimeKeeping.List.Table.DepartmentName': 'deptName',
                    'Timesheet.TimeKeeping.List.Table.EmployeeCode': 'personCode',
                    'Timesheet.TimeKeeping.List.Table.EmployeeName': 'personName',
                    'Timesheet.TimeKeeping.List.Table.DateWorking': 'dayFormat',
                    'Timesheet.TimeKeeping.List.Table.Shift': 'workShiftName',
                    'Timesheet.TimeKeeping.List.Table.TimeIn': 'minCheckInFormat',
                    'Timesheet.TimeKeeping.List.Table.TimeLate': 'lateStr',
                    'Timesheet.TimeKeeping.List.Table.TimeOut': 'maxCheckOutFormat',
                    'Timesheet.TimeKeeping.List.Table.TimeEarly': 'earlyStr',
                    'Timesheet.TimeKeeping.List.Table.WorkDay': 'totalFormat',
                    'Timesheet.TimeKeeping.List.Table.Note': 'noteDay'
                },
                export_fields_details: {
                    'Timesheet.WorkCalendar.Create.Table.Index': 'index',
                    'Timesheet.TimeKeeping.List.Table.DateWorking': 'dayFormat',
                    'Timesheet.WorkCalendar.Create.Table.DayOfWeek': 'dayOfWeekInVietnamese',
                    'Timesheet.TimeKeeping.List.Table.Shift': 'workShiftName',
                    'Timesheet.TimeKeeping.List.Table.TimeIn': 'minCheckInFormat',
                    'Timesheet.TimeKeeping.List.Table.TimeLate': 'lateStr',
                    'Timesheet.TimeKeeping.List.Table.TimeOut': 'maxCheckOutFormat',
                    'Timesheet.TimeKeeping.List.Table.TimeEarly': 'earlyStr',
                    'Timesheet.TimeKeeping.List.Table.WorkDay': 'totalFormat',
                    'Timesheet.TimeKeeping.List.Table.Note': 'noteDay'
                },
                deptId: null,

                searchForm: {
                    compId: null,
                    deptId: null,
                    groupId: null,
                    filterCodePerson: null,
                    filterNamePerson: null,
                    filterDateFrom: null,
                    filterDateTo: null,
                    isExportRequest: null,
                },
                calculateDto: {
                    dateFrom: null,
                    dateTo: null,
                    deptId: null,
                    personId: null,
                    type: 0,
                    compId: null,
                },
                exportDetailDto: {
                    dateFrom: null,
                    dateTo: null,
                    deptId: null,
                    personId: null,
                    type: 0,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'deptName', label: this.$t('Timesheet.TimeKeeping.List.Table.DepartmentName'), sortable: false, },
                        { key: 'groupName', label: this.$t('Timesheet.TimeKeeping.List.Table.EmployeeGroup'), sortable: false, },
                        { key: 'personCode', label: this.$t('Timesheet.TimeKeeping.List.Table.EmployeeCode'), sortable: false, },
                        { key: 'personName', label: this.$t('Timesheet.TimeKeeping.List.Table.EmployeeName'), sortable: false, },
                        { key: 'dayFormat', label: this.$t('Timesheet.TimeKeeping.List.Table.DateWorking'), sortable: false, },
                        { key: 'workShiftName', label: this.$t('Timesheet.TimeKeeping.List.Table.Shift'), sortable: false, },
                        { key: 'minCheckInFormat', label: this.$t('Timesheet.TimeKeeping.List.Table.TimeIn'), sortable: false, },
                        { key: 'lateStr', label: this.$t('Timesheet.TimeKeeping.List.Table.TimeLate'), sortable: false, },
                        { key: 'maxCheckOutFormat', label: this.$t('Timesheet.TimeKeeping.List.Table.TimeOut'), sortable: false, },
                        { key: 'earlyStr', label: this.$t('Timesheet.TimeKeeping.List.Table.TimeEarly'), sortable: false, },
                        { key: 'totalFormat', label: this.$t('Timesheet.TimeKeeping.List.Table.WorkDay'), sortable: false, },
                        { key: 'noteDay', label: this.$t('Timesheet.TimeKeeping.List.Table.Note'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                headerExcel: [],
                lstdepartment: [],
                lstgroup: [],
                lstperson: [],
                darkModal: false,
                lsttype: [],
                lstpersonRp: [],
                showDescriptionReport: false,
                headerExcelDetail: [],
                headerObj: {
                    from: null,
                    to: null,
                    name: null,
                    comp: null,
                    dept: null,
                    late: null,
                    early: null,
                    totalEarlyLate: null,
                    notcheckin: null,
                    notcheckout: null,
                    totalinout: null,
                    total: null,
                },
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        validations: {
            searchForm: {
                filterDateFrom: {
                    checkDate(value) {
                        if (value && this.searchForm.filterDateTo) {
                            var a = moment(value, 'YYYY-MM-DD');
                            var b = moment(this.searchForm.filterDateTo, 'YYYY-MM-DD');
                            if (a > b) {
                                return false;
                            }
                            return true;
                        }
                        return true;
                    }
                }
            },
        },
        watch: {
            // whenever typeWatch changes, this function will run
            typeWatch: function (newType, oldType) {
                var vm = this;
                if (newType != 2) {
                    vm.calculateDto.personId = null;
                }
                if (newType != 1) {
                    vm.calculateDto.deptId = null;
                }
            },
        },
        computed: {
            showDept: function () {
                var vm = this;
                if (vm.calculateDto.type != 0) {
                    return true;
                }
                return false;
            },
            showPerson: function () {
                var vm = this;
                if (vm.calculateDto.type == 2) {
                    return true;
                }
                return false;
            },
            typeWatch: function () {
                var vm = this;
                if (vm.calculateDto.type) return vm.calculateDto.type;
                return 1;
            },
            lang: function () {
                let language = localStorage.getItem("lang") == null ? "vi" : localStorage.getItem("lang");
                return language;
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.calculateDto.compId = accessToken.comId;
            this.exportDetailDto.compId = accessToken.comId;
            var firstDay = moment().startOf('month');
            const yesterday = moment().subtract(1, 'days');
            if (yesterday < firstDay) {
                this.searchForm.filterDateFrom = moment(firstDay).format('YYYY-MM-DD');
                this.searchForm.filterDateTo = moment(firstDay).format('YYYY-MM-DD');
            } else {
                this.searchForm.filterDateFrom = moment(firstDay).format('YYYY-MM-DD');
                this.searchForm.filterDateTo = moment(yesterday).format('YYYY-MM-DD');
            }
            this.loadCompany();
            this.loadDepartment();
            this.loadGroup();
            this.formaT_SUM_OT;
            this.initDetails();
            this.initListDetails();
            this.loadDepartmentTree();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.timekeeping.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/timekeeping/${this.timekeeping.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách nhóm người
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(lstgroups => {
                    vm.lstgroup = lstgroups;

                })
            },
            //Danh sách phòng ban
            async loadDepartment() {
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
            search() {
                if (this.deptId != null) {
                    var deptStr = this.deptId.join(',');
                    this.searchForm.deptId = deptStr;
                } else {
                    this.searchForm.deptId = null;
                }
                this.$refs.timekeepingTable.refresh();
            },
            async exportData() {
                var vm = this;
                var pagination = {
                    page: 1,
                    itemsPerPage: 99999,
                    sortBy: 'accessDate',
                    sortDesc: true
                };
                var compName = null;
                var deptName = null;
                var groupName = null;
                var vm = this;

                for (var i = 0; i < vm.lstcompany.length; i++) {
                    var item = vm.lstcompany[i];
                    if (item.value == vm.searchForm.compId) {
                        compName = item.text;
                        break;
                    } else {
                        compName = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.All");
                    }
                }
                for (var i = 0; i < vm.lstdepartment.length; i++) {
                    var item = vm.lstdepartment[i];
                    if (item.value == vm.searchForm.deptId) {
                        deptName = item.text;
                        break;
                    } else {
                        deptName = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.All");
                    }
                }
                for (var i = 0; i < vm.lstgroup.length; i++) {
                    var item = vm.lstgroup[i];
                    if (item.value == vm.searchForm.groupId) {
                        groupName = item.text;
                        break;
                    } else {
                        groupName = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.All");
                    }
                }
                if (groupName == null) {
                    groupName = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.All");
                }
                var dateFrom = moment(this.searchForm.filterDateFrom).format('DD/MM/YYYY');
                var dateTo = moment(this.searchForm.filterDateTo).format('DD/MM/YYYY');
                this.headerExcel = [
                    i18n.t("Timesheet.TimeKeeping.List.ExportExcel.HeaderExcel.Title"),
                    i18n.t("Timesheet.TimeKeeping.List.ExportExcel.HeaderExcel.Company") + compName,
                    i18n.t("Timesheet.TimeKeeping.List.ExportExcel.HeaderExcel.Department") + deptName,
                    i18n.t("Timesheet.TimeKeeping.List.ExportExcel.HeaderExcel.GroupName") + groupName,
                    i18n.t("Timesheet.TimeKeeping.List.ExportExcel.HeaderExcel.DateFrom") + dateFrom + i18n.t("Timesheet.TimeKeeping.List.ExportExcel.HeaderExcel.DateTo") + dateTo,
                ];
                vm.searchForm.isExportRequest = 1;
                var formData = $.extend({}, pagination, vm.searchForm);

                var response = await this.$services.get(this.$refs.timekeepingTable.dataUrl, formData);
               
                return response.data;
            },
            showModal() {
                this.darkModal = true;
            },
            calculateWorkingHours() {
                var vm = this;
                return this.$services.post(`timesheet/working-hours/calculate-working-hours`, vm.calculateDto).done((id) => {
                    vm.$toastr.s(i18n.t("Messages.CalculateSuccess"));
                    vm.darkModal = false;
                    this.$refs.timekeepingTable.refresh();
                })
            },
            loadListPersonByDept(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.calculateDto.personId = null;
                })
            },
            loadListPersonByDeptReport(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstpersonRp = lstperson;
                    vm.exportDetailDto.personId = null;
                })
            },
            initDetails() {
                var vm = this;
                //load list type
                vm.lsttype = [];
                let item0 = {};
                item0.id = 0;
                item0.value = 0;
                item0.text = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.InitDetail.Company");
                vm.lsttype.push(item0);
                let item = {};
                item.id = 1;
                item.value = 1;
                item.text = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.InitDetail.Department");
                vm.lsttype.push(item);
                let item2 = {};
                item2.id = 2;
                item2.value = 2;
                item2.text = i18n.t("Timesheet.TimeKeeping.List.ExportExcel.InitDetail.Employee");
                vm.lsttype.push(item2);
            },
            initListDetails() {
                var vm = this;
                return this.$services.get(`timesheet/working-shift-times/all`).done(listShiftTimes => {
                    vm.listDetails = [];
                    listShiftTimes.forEach((shift) => {
                        let item = {};
                        item.workingShiftId = shift.id;
                        item.name = shift.name;
                        item.monday = 0;
                        item.tueDay = 0;
                        item.wedDay = 0;
                        item.thuDay = 0;
                        item.friDay = 0;
                        item.satDay = 0;
                        item.sunDay = 0;
                        vm.listDetails.push(item);
                    });
                });
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`timesheet/timekeeping/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            async reportDetails() {
                debugger;

                var vm = this;
                var lstdata = await this.$services.post(`timesheet/working-hours/report-details`, vm.exportDetailDto);
                let from = vm.exportDetailDto.dateFrom;
                let to = vm.exportDetailDto.dateTo;
                let name = "";
                let comp = "";
                let dept = "";
                let total = 0;
                let late = 0;
                let early = 0;
                let totalLateEarly = 0;
                let notcheckin = 0;
                let notcheckout = 0;
                let totalinout = 0;
                for (var i = 0; i < lstdata.length; i++) {
                    lstdata[i].dayOfWeekInVietnamese = i18n.t(lstdata[i].dayOfWeekInVietnamese);

                    var item = lstdata[i];
                    item.index = i + 1;
                    if (i == 0) {
                        name = item.personName;
                        comp = item.compName;
                        dept = item.deptName;
                    }

                    var num = new Number(item.totalFormat);
                    total = total + num;
                    if (item.typeDay && item.typeDay != 1 && item.typeDay != 2 && item.typeDay != 5 && item.workingShiftId != null) {
                        if (item.late && item.late != "" && item.late > 0) {
                            late = late + 1;
                        }
                        if (item.early && item.early != "") {
                            early = early + 1;
                        }
                        if (item.checkIn == null || item.checkIn == "") {
                            notcheckin = notcheckin + 1;
                        }
                        if (item.checkOut == null || item.checkOut == "") {
                            notcheckout = notcheckout + 1;
                        }
                    }
                }
                totalLateEarly = late + early;
                totalinout = notcheckin + notcheckout;
                let column1 = "";
                let column2 = "";
                let column3 = "";
                column1 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.Name") + name;
                while (column1.length < 80) {
                    column1 = column1 + "-";
                }
                column2 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.Late") + late;
                while (column2.length < 80) {
                    column2 = column2 + "-";
                }
                column3 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.NotCheckIn") + notcheckin;
                while (column3.length < 30) {
                    column3 = column3 + "-";
                }
                let row1 = column1 + column2 + column3;
                column1 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.Dept") + dept;
                while (column1.length < 80) {
                    column1 = column1 + "-";
                }
                column2 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.TotalLateEarly") + totalLateEarly;
                while (column2.length < 80) {
                    column2 = column2 + "-";
                }
                column3 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.TotalInOut") + totalinout;
                while (column3.length < 30) {
                    column3 = column3 + "-";
                }
                let row3 = column1 + column2 + column3 + "";
                column1 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.Comp") + comp;
                while (column1.length < 80) {
                    column1 = column1 + "-";
                }
                column2 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.Early") + early;
                while (column2.length < 80) {
                    column2 = column2 + "-";
                }
                column3 = i18n.t("Timesheet.TimeKeeping.List.ReportDetail.NotCheckOut") + notcheckout;
                while (column3.length < 30) {
                    column3 = column3 + "-";
                }
                let row2 = column1 + column2 + column3;
                var dateFrom = new Date(from);
                var dateTo = new Date(to);
                let fromFormat = moment(dateFrom).format('DD/MM/YYYY');;
                let toFormat = moment(dateTo).format('DD/MM/YYYY');
                row1 = row1.replace(/-/g, '&nbsp;');
                row2 = row2.replace(/-/g, '&nbsp;');
                row3 = row3.replace(/-/g, '&nbsp;');
                this.headerExcelDetail = [
                    i18n.t("Timesheet.TimeKeeping.List.ReportDetail.HeaderExcelDetail.Title"),
                    i18n.t("Timesheet.TimeKeeping.List.ReportDetail.HeaderExcelDetail.DateFrom") + fromFormat + i18n.t("Timesheet.TimeKeeping.List.ReportDetail.HeaderExcelDetail.DateTo") + toFormat,
                    '&nbsp;',
                    row1,
                    row2,
                    row3,
                    i18n.t("Timesheet.TimeKeeping.List.ReportDetail.HeaderExcelDetail.Total") + total,
                    '&nbsp;',
                ];

                return lstdata;
            },
            async exportExcel() {
                var vm = this;
                var pagination = {
                    page: 1,
                    itemsPerPage: 99999,
                    sortBy: 'accessDate',
                    sortDesc: true
                };
                vm.searchForm.isExportRequest = 1;
                var formData = $.extend({}, pagination, vm.searchForm);
                formData.Lang = vm.lang;

                return this.$services.get(`timesheet/timekeeping/exportExcel`, formData).done(response => {
                    //add download link
                    let a = document.createElement('a');
                  
                    a.setAttribute("href", response);
                    a.click();
                })
            },
        }
    }
</script>
<style scoped>
    td {
        text-align: center;
    }

    .btn-same-size {
        width: 120px;
        min-width: 105px;
    }
</style>
