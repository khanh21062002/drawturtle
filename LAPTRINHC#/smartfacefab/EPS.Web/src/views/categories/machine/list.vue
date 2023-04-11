<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Machine.List.Header")}}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')" :settings="{allowClear: true}" id="dpl_compId"
                                             v-model="searchForm.compId">
                                    </select2>

                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.List.SearchForm.MachineName')"
                                              label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AreaName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" id="areaId"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.areaId" :options="lstArea">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="7" class="text-right">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid class="grid-border-top" :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="machine"
                            gridName="Categories.Machine.List.Table.TableName"
                            ref="machineTable">
                        <template v-slot:actions>
                            <router-link to="/categories/machine/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageMachine'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <!--<b-button size="md" @click.stop="removeSelected" variant="danger" v-if="authorize(['ManageMachine'])">
                {{$t("Button.Delete")}}
                                
            </b-button>-->
                            </template>
                        <template v-slot:cell(statusOnOff)="row">
                            <span style="color: red; font-weight: bolder;" v-if="row.item.statusOnOff == 0">{{$t('Categories.Machine.List.SearchForm.StatusOff')}}</span>
                            <span style="color: green; font-weight: bolder;" v-if="row.item.statusOnOff == 1">{{$t('Categories.Machine.List.SearchForm.StatusOn')}}</span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" @click="requestReboot(row.item.id)" class="mr-1" :title="$t('Categories.Machine.List.Table.RequestRebootTable')" v-if="authorize(['ManageMachine'])">
                                    <i class="fa fa-caret-square-o-right" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click="synchronized(row.item.id)" :title="$t('Categories.Machine.List.Table.SynchronizedTable')" v-if="authorize(['ManageMachine'])">
                                    <i class="fa fa-refresh" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" :to="{ path: `/categories/machine/detail/${row.item.id}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageMachine'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                    <confirmModal ref="confirmModal">
                        <label>{{$t('Messages.ConfirmDelete')}}</label>
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
        name: 'ListMachine',
        mixins: [authorizationMixin],
        data() {
            return {
                machine: {
                    id: null,
                },
                searchForm: {
                    filterText: null,
                    compId: null,
                    areaId: null
                },
                gridOptions: {
                    fields: [
                        { key: 'company', label: this.$t('Categories.Accesstimeseg.List.Table.Company'), sortable: false },
                        //{ key: 'code', label: 'Mã', sortable: false },
                        { key: 'deviceName', label: this.$t('Categories.Machine.List.Table.Name'), sortable: false, },
                        { key: 'code', label: this.$t('Categories.Machine.List.Table.Code'), sortable: false, },
                        { key: 'areaName', label: this.$t('Categories.Machine.List.Table.AreaGrid'), sortable: false, },
                        { key: 'devicePerformance', label: this.$t('Categories.Machine.List.Table.Performance'), thAttr: { width: 120 }, },
                        //{ key: 'status', label: 'Trạng thái', sortable: false, },
                        //{ key: 'ipaddress', label: 'Địa chỉ ip', sortable: false, },
                        //{ key: 'port', label: 'Port', sortable: false, },
                        //{ key: 'mac', label: 'Địa chỉ mac', sortable: false, },

                        //{ key: 'statusName', label: 'Trạng thái', sortable: false },
                        { key: 'lastTimeSynchronized', label: this.$t('Categories.Machine.List.Table.LastTimeSynchronized'), sortable: false },
                        { key: 'statusOnOff', label: this.$t('Guess.List.Table.Status'), sortable: false },
                    ],

                    sortBy: 'id',
                    sortDesc: true,
                },
                lstArea: [],
                lstcompany: [],
                isAdmin: false,
                Machine: [],
                modalKD: false,
                modalDB: false,
                modalDl: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            //this.searchForm.department_ID = accessToken.unitId;
            await this.loadCompany();
            await this.loadArea();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.machine.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`machine/${this.machine.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            async synchronized(item) {
                var vm = this;
                this.machine.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Categories.Machine.List.Table.SynchronizedTable'),
                    message: this.$t('Categories.Machine.List.Table.SynchronizedRequire'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.get(`machine/synchronized/${this.machine.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Categories.Machine.List.Table.LastTimeSynchronized"));

                    });
                    this.$refs.popup.close();
                } else {
                    this.$refs.popup.close();
                }
            },
            async requestReboot(item) {
                var vm = this;
                this.machine.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Categories.Machine.List.Table.RequestRebootTable'),
                    message: this.$t('Categories.Machine.List.Table.RequestRebootRequire'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    var vm = this;
                    this.$services.get(`machine/request/${this.machine.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Categories.Machine.List.Table.RequestReboot"));
                    });
                } else {
                    this.$refs.popup.close();
                }
            },
            async loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done(res => {
                    vm.lstArea = res;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            search() {
                this.$refs.machineTable.refresh();
            },
            //remove() {
            //    var vm = this;
            //    this.$services.delete(`machine/${this.machine.id}`).done(() => {
            //        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //        vm.search();
            //    });
            //    this.$refs.popup.close();
            //},
            //removeSelected() {
            //    var vm = this;
            //    var selectedRows = this.$refs.machineTable.selectedRows;
            //    if (selectedRows.length > 0) {
            //        if (confirm(i18n.t("Messages.ConfirmDelete"))) {
            //            var data = selectedRows.map(x => x.id).join(',');
            //            this.$services.delete(`machine?ids=${data}`).done(() => {
            //                vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //                vm.search();
            //            });
            //        }
            //    }
            //    else {
            //        vm.$toastr.e(i18n.t("Messages.ToastrDelete_e"));
            //    }
            //},
            //synchronized() {
            //    debugger;
            //    var vm = this;
            //    this.$services.get(`machine/synchronized/${this.machine.id}`).done(() => {
            //        vm.$toastr.s(i18n.t("Categories.Machine.List.Table.LastTimeSynchronized"));

            //    });
            //    this.$refs.popup.close();
            //    /*this.modalDuyet1 = false;*/
            //},
            //requestReboot() {
            //    debugger;
            //    var vm = this;
            //    this.$services.get(`machine/request/${this.machine.id}`).done(() => {
            //        vm.$toastr.s(i18n.t("Categories.Machine.List.Table.RequestReboot"));
            //    });
            //    this.$refs.popup.close();
            //},
        }
    }
</script>
<style scoped>
</style>
