<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.GroupAccess.List.Header")}}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>

                            <b-col md="5" offset="1">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.Group')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstgroup" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_groupId" :settings="{allowClear: true}"
                                             v-model="searchForm.groupId">
                                    </select2>

                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.GroupAccess.List.SearchForm.Machine')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">

                                    <select2 :options="lstmachine" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_machineId" :settings="{allowClear: true}"
                                             v-model="searchForm.machineId">
                                    </select2>

                                </b-form-group>
                            </b-col>
                            <b-col md="5" offset="1">
                                <b-form-group :label="this.$t('Categories.GroupAccess.List.SearchForm.TimeSeg')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstaccesstimeseg" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_timeSegId" :settings="{allowClear: true}"
                                             v-model="searchForm.timeSegId">
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
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="groupaccess"
                            gridName="Categories.GroupAccess.List.Table.TableName"
                            ref="groupaccessTable">
                        <template v-slot:actions>
                            <router-link to="/categories/groupaccess/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageGroupAccess'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <!--không cho phép xóa nhiều-->
                            <!--<b-button size="md" @click.stop="removeSelected" variant="danger" v-if="authorize(['ManageGroupAccess'])">
                Xóa
            </b-button>-->
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/categories/groupaccess/detail/${row.item.id}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageGroupAccess'])">
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
                groupaccess: {
                    id:null
                },
                searchForm: {
                    filterText: null,
                    compId: null,
                    timeSegId: null,
                    groupId: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'company', label: this.$t('Categories.Accesstimeseg.List.Table.Company'), sortable: false, },
                        { key: 'group', label: this.$t('Categories.GroupAccess.List.Table.Group'), sortable: false, },
                        { key: 'accessTimeSeg', label: this.$t('Categories.GroupAccess.List.Table.Timeseg'), sortable: false, },
                        { key: 'machine', label: this.$t('Categories.GroupAccess.List.Table.MachineName'), sortable: false },
                        //{ key: 'statusName', label: 'Trạng thái', sortable: false },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                lstmachine: [],
                lstdepartment: [],
                lstgroup: [],
                lstaccesstimeseg: [],
                isAdmin: false,
            }
        },

        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            //this.searchForm.department_ID = accessToken.unitId;
            await this.loadCompany();
            await this.loadDepartment();
            await this.loadMachine();
            await this.loadAccessTimeSeg();
            await this.loadGroup();
        },
        methods: {
            async doDelete(item) {
                debugger;
                var vm = this;
                this.groupaccess.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`groupaccess/${this.groupaccess.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/all-departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;

                })
            },
            async loadMachine() {
                var vm = this;
                return this.$services.get(`lookup/machines`).done(lstmachine => {
                    vm.lstmachine = lstmachine;
                })
            },

            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },

            async loadAccessTimeSeg() {
                var vm = this;
                return this.$services.get(`lookup/accesstimesegs`).done(lstaccesstimeseg => {
                    vm.lstaccesstimeseg = lstaccesstimeseg;
                })
            },
            //Danh sách nhom nguoi
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(lstgroup => {
                    vm.lstgroup = lstgroup;
                })
            },
            search() {
                this.$refs.groupaccessTable.refresh();
            },
            //remove(item, index, button) {
            //    var vm = this;
            //    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
            //        this.$services.delete(`groupaccess/${item.id}`).done(() => {
            //            vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //            vm.search();
            //        });
            //    }
            //},
            //removeSelected() {
            //    var vm = this;
            //    var selectedRows = this.$refs.groupaccessTable.selectedRows;
            //    if (selectedRows.length > 0) {
            //        if (confirm(i18n.t("Messages.ConfirmDelete"))) {
            //            var data = selectedRows.map(x => x.id).join(',');
            //            this.$services.delete(`groupaccess?ids=${data}`).done(() => {
            //                vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //                vm.search();
            //            });
            //        }
            //    }
            //    else {
            //        vm.$toastr.e(i18n.t("Messages.ToastrDelete_e"));
            //    }
            //},


        }
    }
</script>
<style scoped>
</style>
