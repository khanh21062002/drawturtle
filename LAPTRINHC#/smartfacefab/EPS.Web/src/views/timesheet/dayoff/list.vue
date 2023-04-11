<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.Dayoff.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DeptName')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :autoDeselectDescendants="true" :limit="3" :multiple="true" :flat="true" :autoSelectDescendants="true"
												:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="deptId" />

                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DateFrom')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.dateFrom" id="dateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DateTo')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.dateTo" id="dateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.PersonCode')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.personCode">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.PersonName')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.personName">
                                    </b-form-input>
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
                            :multiLanguage="true"
                            dataUrl="timesheet/dayoff"
                            gridName="Timesheet.Dayoff.List.Table.TableName"
                            ref="holidaysTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/dayoff/create" tag="button" class="btn btn-primary" size="md" v-if=" authorize(['ManageDayOff'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/timesheet/dayoff/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1"
                                          v-if="authorize(['ManageDayOff']) && row.item.isOutDate" :title="$t('TitleDetail.Delete')">
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
                dayoff: {
                    id:null
                },
                deptId: null,

                searchForm: {
                    deptId: null,
                    compId: null,
                    dateFrom: null,
                    dateTo: null,
                    personName: null,
                    personCode: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'personCode', label: this.$t('Timesheet.Dayoff.List.Table.PersonCode'), sortable: false, },
                        { key: 'personName', label: this.$t('Timesheet.Dayoff.List.Table.PersonName'), sortable: false, },
                        { key: 'deptName', label: this.$t('Timesheet.Dayoff.List.Table.DeptName'), sortable: false, },
                        { key: 'formatDateFrom', label: this.$t('Timesheet.Dayoff.List.Table.DateFrom'), sortable: false, },
                        { key: 'formatDateTo', label: this.$t('Timesheet.Dayoff.List.Table.DateTo'), sortable: false, },
                        { key: 'totalDay', label: this.$t('Timesheet.Dayoff.List.Table.TotalDay'), sortable: false, },
                        { key: 'reason', label: this.$t('Timesheet.Dayoff.List.Table.Reason'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                lstdepartment: [],
                isAdmin: false,
                isEmpty: false,
                lstYear: [],
                datepickerLang: null,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.loadYear();
            await this.loadCompany();
            await this.loadDepartment();
            this.loadDepartmentTree();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.dayoff.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/dayoff/${this.dayoff.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            loadDepartment() {
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
            loadYear() {
                var vm = this;
                vm.lstYear = [];
                const currentYear = new Date().getFullYear();
                const year = new Date().getFullYear() + 100;
                for (var item = 1971; item < year; item++) {
                    let object = {};
                    object.id = item + "";
                    object.value = item + "";
                    object.text = item + "";
                    vm.lstYear.push(object);
                }
                vm.searchForm.year = currentYear;
            },
            search() {
                if (this.deptId != null) {
                    var deptStr = this.deptId.join(',');
                    this.searchForm.deptId = deptStr;
                } else {
                    this.searchForm.deptId = null;
                }
                this.$refs.holidaysTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                var deleteStr = i18n.t("Messages.ConfirmDelete");
                if (confirm(deleteStr)) {
                    this.$services.delete(`timesheet/dayoff/${item.id}`).done(() => {
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
