<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.RegisterWorkingShift.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Form.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Form.DepartmentName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :multiple="true" :flat="true" :limit="3"
                                                :autoSelectDescendants="true"
                                                :autoDeselectDescendants="true"
												:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="deptId" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Form.EmployeeCode')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="personCode"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.personCode">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Form.EmployeeName')"
                                              label-for="inp_year"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="personName"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.personName">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Form.DateFrom')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.dateFrom" id="dateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.List.Form.DateTo')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.dateTo" id="dateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
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
                            dataUrl="timesheet/register-working-shift"
                            :multiLanguage="true"
                            gridName="Timesheet.RegisterWorkingShift.List.HeaderTable"
                            ref="registerWorkingShiftTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/register-working-shift/create" tag="button" class="btn btn-primary" size="md" v-if=" authorize(['ManageRegisterWorkingShift'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(typeName)="row">
                            <span class="col-form-label "> {{ $t(row.item.typeName) }}</span>

                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/timesheet/register-working-shift/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')" v-if="authorize(['ManageRegisterWorkingShift'])">
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
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListGroupAccess',
        mixins: [authorizationMixin],
        data() {
            return {
                register: {
                    id:null
                },
                deptId: null,
                searchForm: {
                    compId: null,
                    deptId: null,
                    hiddenParentField: null,
                    filterText: null,
                    personCode: null,
                    personName: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'typeName', label: this.$t('Timesheet.RegisterWorkingShift.List.Table.RegisType'), sortable: false, },
                        { key: 'deptName', label: this.$t('Timesheet.RegisterWorkingShift.List.Table.DepartmentName'), sortable: false, },
                        { key: 'personCode', label: this.$t('Timesheet.RegisterWorkingShift.List.Table.EmployeeCode'), sortable: false, },
                        { key: 'personName', label: this.$t('Timesheet.RegisterWorkingShift.List.Table.EmployeeName'), sortable: false, },
                        { key: 'dateFromFormat', label: this.$t('Timesheet.RegisterWorkingShift.List.Table.DateFrom'), sortable: false, },
                        { key: 'dateToFormat', label: this.$t('Timesheet.RegisterWorkingShift.List.Table.DateTo'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstdepartment: [],
                lstcompany: [],
                isAdmin: false,
                isEmpty: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            await this.loadCompany();
            await this.loadDepartment();
            this.loadDepartmentTree();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.register.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/register-working-shift/${this.register.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                });
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            search() {
                if (this.deptId != null) {
                    var deptStr = this.deptId.join(',');
                    this.searchForm.deptId = deptStr;
                } else {
                    this.searchForm.deptId = null;
                }
                this.$refs.registerWorkingShiftTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`timesheet/register-working-shift/${item.id}`).done(() => {
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
