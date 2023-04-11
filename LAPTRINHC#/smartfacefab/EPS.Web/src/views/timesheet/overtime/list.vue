<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.OverTime.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTime.List.Form.CompanyName')"
                                              label-for="dpl_compId"
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
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTime.List.Form.DepartmentName')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :autoDeselectDescendants="true" :limit="3" :multiple="true" :flat="true" :autoSelectDescendants="true" :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="deptId" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTime.List.Form.EmployeeCode')"
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
                                <b-form-group :label="this.$t('Timesheet.OverTime.List.Form.EmployeeName')"
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
                                <b-form-group :label="this.$t('Timesheet.OverTime.List.Form.DateFrom')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                    <b-form-input style="display: none;" v-model="$v.searchForm.filterDateFrom.$model" :state="$v.searchForm.filterDateFrom.$dirty? !$v.searchForm.filterDateFrom.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t("Timesheet.OverTime.List.Form.DateFromRequire")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.OverTime.List.Form.DateTo')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            dataUrl="timesheet/overtime"
                            :multiLanguage="true"
                            gridName="Timesheet.OverTime.List.HeaderTable"
                            ref="overtimeTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/overtime/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageOverTime'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/timesheet/overtime/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')" v-if="authorize(['ManageOverTime'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                    <confirmModal ref="confirmModal">
                    </confirmModal>
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
                overtime: {
                    id:null
                },
                deptId: null,
                searchForm: {
                    compId: null,
                    deptId: null,
                    filterCodePerson: null,
                    filterNamePerson: null,
                    filterDateFrom: null,
                    filterDateTo: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'persoN_CODE', label: this.$t('Timesheet.OverTime.List.Table.EmployeeCode'), sortable: false, },
                        { key: 'persoN_NAME', label: this.$t('Timesheet.OverTime.List.Table.EmployeeName'), sortable: false, },
                        { key: 'departmenT_NAME', label: this.$t('Timesheet.OverTime.List.Table.DepartmentName'), sortable: false, },
                        { key: 'formaT_DATE_OT', label: this.$t('Timesheet.OverTime.List.Table.DateWorking'), sortable: false, },
                        { key: 'shifT_NAME', label: this.$t('Timesheet.OverTime.List.Table.Shift'), sortable: false, },
                        { key: 'formaT_SUM_OT', label: this.$t('Timesheet.OverTime.List.Table.Hour'), sortable: false, },
                        { key: 'note', label: this.$t('Timesheet.OverTime.List.Table.Note'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                lstdepartment: [],
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
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
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
            this.formaT_SUM_OT;
            this.loadDepartmentTree();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.overtime.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/overtime/${this.overtime.id}`).done(() => {
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
                this.$refs.overtimeTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`timesheet/overtime/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            }
        }
    }
</script>
<style scoped>
    td {
        text-align: center;
    }
</style>
