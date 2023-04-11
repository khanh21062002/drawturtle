<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.OverTimeHours.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.DepartmentName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :autoDeselectDescendants="true" :limit="3" :multiple="true" :flat="true" :autoSelectDescendants="true" :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options" 
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="deptId" />
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.EmployeeGroup')"
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
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.EmployeeCode')"
                                              label-for="code"
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
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.EmployeeName')"
                                              label-for="name"
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
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.DateFrom')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                    <b-form-input style="display: none;" v-model="$v.searchForm.filterDateFrom.$model" :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t("Timesheet.OverTimeHours.List.Form.DateFromRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Form.DateTo')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="9" class="text-center">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                    <b-button size="md" class="btn btn-success" @click="exportExcel">
                                        {{$t("Button.ExportExcel")}}
                                    </b-button>

                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            dataUrl="timesheet/overtimehours"
                            :multiLanguage="true"
                            gridName="Timesheet.OverTimeHours.List.HeaderTable"
                            ref="overtimeTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/overtimehours/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageOverTimeHours'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <b-button type="button" variant="info" v-if="lang == 'vi'" style="width: 120px !important" @click="darkModal = true">{{$t("Button.Recalculation")}}</b-button>
                            <b-button type="button" variant="info" v-if="lang == 'en'" style="width: 180px !important" @click="darkModal = true">{{$t("Button.Recalculation")}}</b-button>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')"
                                          v-if="authorize(['ManageOverTimeHours']) && row.item.type == 5">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                    <confirmModal ref="confirmModal">
                    </confirmModal>
                    <CModal :show.sync="darkModal"
                            :no-close-on-backdrop="true"
                            :title="this.$t('Timesheet.OverTimeHours.List.Recalculation.Title')"
                            size="xl"
                            color="dark">
                        <b-form @submit.prevent="save" @submit="$v.$touch()">
                            <b-row>
                                <b-col md="11">
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Recalculation.Company')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                         v-model="calculateDto.compId" :options="lstcompany">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Recalculation.Type')"
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
                                            <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Recalculation.Department')"
                                                          v-if="showDept"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <treeselect :multiple="false" @select="loadListPersonByDeptReport($event.id)"
                                                            :options="treeselect.options"
															:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                            :placeholder="this.$t('PlaceHolder.Select')"
                                                            v-model="calculateDto.deptId" />
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Recalculation.Employee')"
                                                          v-if="showPerson"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                         v-model="calculateDto.personId" :options="lstperson">
                                                </select2>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Recalculation.DateFrom')"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="left" label-class="required">
                                                <date-picker style="width: 100%;" type="date"
                                                             v-model="calculateDto.dateFrom"
                                                             :editable="false"
                                                             id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                                </date-picker>
                                            </b-form-group>
                                        </b-col>
                                        <b-col md="6">
                                            <b-form-group :label="this.$t('Timesheet.OverTimeHours.List.Recalculation.DateTo')"
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
                            <b-button v-if="authorize(['ManageTimeKeeping'])" style="width: 200px !important" type="button" @click="calculateOT" variant="primary"> {{$t("Button.Recalculation")}}</b-button>
                            <b-button type="button" variant="secondary" @click="darkModal = false"><i class="fa fa-ban"></i>{{$t("Button.Close")}}</b-button>
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
    import moment from 'moment';
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListOverTime',
        mixins: [authorizationMixin],
        data() {
            return {
                overtimehours: {
                    id: null
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
                export_fields: {
                    'Timesheet.OverTimeHours.List.Table.EmployeeCode': 'personCode',
                    'Timesheet.OverTimeHours.List.Table.EmployeeName': 'personName',
                    'Timesheet.OverTimeHours.List.Table.DepartmentName': 'deptName',
                    'Timesheet.OverTimeHours.List.Table.DateWorking': 'dayFormat',
                    'Timesheet.OverTimeHours.List.Table.Shift': 'workingShiftName',
                    'Timesheet.OverTimeHours.List.Table.TimeFrom': 'timeFromFormat',
                    'Timesheet.OverTimeHours.List.Table.TimeTo': 'timeToFormat',
                    'Timesheet.OverTimeHours.List.Table.BreakTime': 'breakTime',
                    'Timesheet.OverTimeHours.List.Table.RealTime': 'totalReal',
                    'Timesheet.OverTimeHours.List.Table.RegisterTime': 'totalRegister',
                    'Timesheet.OverTimeHours.List.Table.Coefficient': 'numberPartion',
                    'Timesheet.OverTimeHours.List.Table.TotalHour': 'totalFormat',
                    'Timesheet.OverTimeHours.List.Table.Note': 'note'
                },
                gridOptions: {
                    fields: [
                        { key: 'personCode', label: this.$t('Timesheet.OverTimeHours.List.Table.EmployeeCode'), sortable: false, },
                        { key: 'personName', label: this.$t('Timesheet.OverTimeHours.List.Table.EmployeeName'), sortable: false, },
                        { key: 'deptName', label: this.$t('Timesheet.OverTimeHours.List.Table.DepartmentName'), sortable: false, },
                        { key: 'groupName', label: this.$t('Timesheet.OverTimeHours.List.Table.EmployeeGroup'), sortable: false, },
                        { key: 'dayFormat', label: this.$t('Timesheet.OverTimeHours.List.Table.DateWorking'), sortable: false, },
                        { key: 'timeFromFormat', label: this.$t('Timesheet.OverTimeHours.List.Table.TimeFrom'), sortable: false, },
                        { key: 'timeToFormat', label: this.$t('Timesheet.OverTimeHours.List.Table.TimeTo'), sortable: false, },
                        { key: 'breakTime', label: this.$t('Timesheet.OverTimeHours.List.Table.BreakTime'), sortable: false, },
                        { key: 'totalReal', label: this.$t('Timesheet.OverTimeHours.List.Table.RealTime'), sortable: false, },
                        { key: 'totalRegister', label: this.$t('Timesheet.OverTimeHours.List.Table.RegisterTime'), sortable: false, },
                        { key: 'workingShiftName', label: this.$t('Timesheet.OverTimeHours.List.Table.Shift'), sortable: false, },
                        { key: 'numberPartion', label: this.$t('Timesheet.OverTimeHours.List.Table.Coefficient'), sortable: false, },
                        { key: 'totalFormat', label: this.$t('Timesheet.OverTimeHours.List.Table.TotalHour'), sortable: false, },
                        { key: 'note', label: this.$t('Timesheet.OverTimeHours.List.Table.Note'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                calculateDto: {
                    dateFrom: null,
                    dateTo: null,
                    deptId: null,
                    personId: null,
                    type: 0,
                    compId: null,
                },
                lstcompany: [],
                lstdepartment: [],
                lstperson: [],
                headerExcel: [],
                darkModal: false,
                lsttype: [],
                lstgroup: [],
                exportDetailDto: {
                    dateFrom: null,
                    dateTo: null,
                    deptId: null,
                    personId: null,
                    type: 0,
                    compId: null,
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
                            var b = moment(this.searchForm.filterDateTo, 'YYYY-MM-DD ');
                            if (a > b) {
                                return false;
                            }
                            return true;
                        }
                        return true;
                    }
                }
            }
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
            this.loadDepartmentTree();
            this.loadGroup();
            this.formaT_SUM_OT;
            this.initDetails();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.overtimehours.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/overtimehours/${this.overtimehours.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(res => {
                    vm.lstgroup = res;
                })
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                })
            },
            search() {
                if (this.deptId != null) {
                    var deptStr = this.deptId.join(',');
                    this.searchForm.deptId = deptStr;
                } else {
                    this.searchForm.deptId = null;
                }
                this.$refs.overtimeTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`timesheet/overtimehours/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            calculateOT() {
                var vm = this;
                return this.$services.post(`timesheet/overtimehours/calculate-ot`, vm.calculateDto).done((id) => {
                    vm.$toastr.s(i18n.t("Messages.CalculateSuccess"));
                    vm.darkModal = false;
                    this.$refs.overtimeTable.refresh();
                })
            },
            initDetails() {
                var vm = this;
                //load list type
                vm.lsttype = [];
                let item0 = {};
                item0.id = 0;
                item0.value = 0;
                item0.text = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.InitDetail.Company");
                vm.lsttype.push(item0);
                let item = {};
                item.id = 1;
                item.value = 1;
                item.text = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.InitDetail.Department");
                vm.lsttype.push(item);
                let item2 = {};
                item2.id = 2;
                item2.value = 2;
                item2.text = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.InitDetail.Employee");
                vm.lsttype.push(item2);
            },
            loadListPersonByDeptReport(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.calculateDto.personId = null;
                })
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
                        compName = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.All");
                    }
                }
                for (var i = 0; i < vm.lstdepartment.length; i++) {
                    var item = vm.lstdepartment[i];
                    if (item.value == vm.searchForm.deptId) {
                        deptName = item.text;
                        break;
                    } else {
                        deptName = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.All");
                    }
                }
                for (var i = 0; i < vm.lstgroup.length; i++) {
                    var item = vm.lstgroup[i];
                    if (item.value == vm.searchForm.groupId) {
                        groupName = item.text;
                        break;
                    } else {
                        groupName = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.All");
                    }
                }
                if (groupName == null) {
                    groupName = i18n.t("Timesheet.OverTimeHours.List.ExportExcel.All");
                }
                var dateFrom = moment(this.searchForm.filterDateFrom).format('DD/MM/YYYY');
                var dateTo = moment(this.searchForm.filterDateTo).format('DD/MM/YYYY');
                this.headerExcel = [
                    i18n.t("Timesheet.OverTimeHours.List.ExportExcel.HeaderExcel.Title"),
                    i18n.t("Timesheet.OverTimeHours.List.ExportExcel.HeaderExcel.Company") + compName,
                    i18n.t("Timesheet.OverTimeHours.List.ExportExcel.HeaderExcel.Department") + deptName,
                    i18n.t("Timesheet.OverTimeHours.List.ExportExcel.HeaderExcel.GroupName") + groupName,
                    i18n.t("Timesheet.OverTimeHours.List.ExportExcel.HeaderExcel.DateFrom") + dateFrom + i18n.t("Timesheet.OverTimeHours.List.ExportExcel.HeaderExcel.DateTo") + dateTo,
                ];
                vm.searchForm.isExportRequest = 1;
                var formData = $.extend({}, pagination, vm.searchForm);
                var response = await this.$services.get(this.$refs.overtimeTable.dataUrl, formData);
                return response.data;
            },
            async changeGroupByDept(deptId) {
                var vm = this;
                if (deptId == null) {
                    vm.lstgroup = [];
                    vm.searchForm.groupId = null;
                    vm.searchForm.deptId = null;
                    return;
                }
                return this.$services.get(`lookup/group-by-dept/${deptId}`).done(lstgroup => {
                    vm.lstgroup = lstgroup;
                    vm.searchForm.groupId = null;
                })
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
                return this.$services.get(`timesheet/timekeeping/exportExcelForOT`, formData).done(response => {
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
</style>
