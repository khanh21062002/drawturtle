<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Group.List.Header")}}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="5">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="!isAdmin" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                    <!--<unit-tree v-model="searchForm.compId" ref="unitTree"></unit-tree>-->
                                    <!--<b-form-radio-group v-model="searchForm.unitTraversalOption" name="radio-sub-component">
                        <b-form-radio value="0">Trực thuộc đơn vị</b-form-radio>
                        <b-form-radio value="1">Cả đơn vị con</b-form-radio>
                        <b-form-radio value="2">Toàn bộ đơn vị cấp dưới</b-form-radio>
                    </b-form-radio-group>-->
                                </b-form-group>
                            </b-col>

                            <b-col md="7">
                                <b-form-group :label="this.$t('Categories.Group.List.SearchForm.GroupNameCode')"
                                              label-for="name"
                                              :label-cols="5"
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
                            dataUrl="group"
                            gridName="Categories.Group.List.Table.TableName"
                            ref="groupTable">
                        <template v-slot:actions>
                            <router-link to="/categories/group/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageGroup'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <!--<b-button size="md" @click.stop="removeSelected" variant="danger" v-if="authorize(['ManageGroup'])">
                {{$t("Button.Delete")}}
            </b-button>-->
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/categories/group/detail/${row.item.groupId}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.groupId)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageGroup'])">
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
    import { authorizationMixin } from '@/shared/mixins';
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListGroup',
        mixins: [authorizationMixin],
        data() {
            return {
                group: {
                    id:null
                },
                searchForm: {
                    filterText: null,
                    compId: null,

                },
                gridOptions: {
                    fields: [
                        { key: 'company', label: this.$t('Categories.Accesstimeseg.List.Table.Company'), sortable: false },
                        { key: 'groupCode', label: this.$t('Categories.Group.List.Table.Code'), sortable: false },
                        { key: 'groupName', label: this.$t('Categories.Group.List.Table.NameGroup'), sortable: false, },
                        //{ key: 'statusName', label: 'Trạng thái', sortable: false },
                        
                    ],
                    sortBy: 'groupId',
                    sortDesc: true,
                }, lstcompany: [],
                lstdepartment: [],
                isAdmin: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            //this.searchForm.department_ID = accessToken.unitId;
            await this.loadCompany();
            await this.loadDepartment();

        },
        methods: {
            async doDelete(item) {
                debugger;
                var vm = this;
                this.group.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`group/${this.group.id}`).done(() => {
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
            //Danh sách công ty
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/all-departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;

                })
            },
            search() {
                this.$refs.groupTable.refresh();
            },
            //remove(item, index, button) {
            //    var vm = this;
            //    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
            //        this.$services.delete(`group/${item.groupId}`).done(() => {
            //            vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //            vm.search();
            //        });
            //    }
            //},
            //removeSelected() {
            //    var vm = this;
            //    var selectedRows = this.$refs.groupTable.selectedRows;
            //    if (selectedRows.length > 0) {
            //        if (confirm(i18n.t("Messages.ConfirmDelete"))) {
            //            var data = selectedRows.map(x => x.groupId).join(',');
            //            this.$services.delete(`group?ids=${data}`).done(() => {
            //                vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //                vm.search();
            //            });
            //        }
            //    }
            //    else {
            //        vm.$toastr.e(i18n.t("Messages.ToastrDelete_e"));
            //    }
            //}
        }
    }
</script>
<style scoped>
</style>
