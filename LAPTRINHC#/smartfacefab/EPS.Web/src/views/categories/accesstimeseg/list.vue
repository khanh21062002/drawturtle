<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Accesstimeseg.List.Header")}}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 class="col-10" :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
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

                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.List.SearchForm.DateName')"
                                              label-for="filterText"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="filterText"
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

                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="accesstimeseg"
                            gridName="Categories.Accesstimeseg.List.Table.TableName"
                            ref="accesstimesegTable">
                        <template v-slot:actions>
                            <router-link to="/categories/accesstimeseg/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageAccessTimeSeg'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <!--<b-button size="md" @click.stop="removeSelected" variant="danger" v-if="authorize(['ManageAccessTimeSeg'])">
                {{$t("Button.Delete")}}
            </b-button>-->
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/categories/accesstimeseg/detail/${row.item.id}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageAccessTimeSeg'])">
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
        name: 'ListAccessTimeSeg',
        mixins: [authorizationMixin],
        data() {
            return {
                accesstimeseg: {
                    id: null,
                },
                searchForm: {
                    filterText: null,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'company', label: this.$t('Categories.Accesstimeseg.List.Table.Company'), sortable: false },
                        { key: 'timeSegName', label: this.$t('Categories.Accesstimeseg.List.Table.TimeSegName'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                lstdepartment: [],
                isAdmin: false,
                modalDl: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            await this.loadCompany();
            await this.loadDepartment();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.accesstimeseg.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`accesstimeseg/${this.accesstimeseg.id}`).done(() => {
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
            },  //Danh sách công ty
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/all-departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            search() {
                this.$refs.accesstimesegTable.refresh();
            },
            //remove() {
            //    var vm = this;
            //    this.$services.delete(`accesstimeseg/${this.accesstimeseg.id}`).done(() => {
            //        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //        vm.search();
            //    });
            //    this.$refs.popup.close();
            //},
            //removeSelected() {
            //    var vm = this;
            //    var selectedRows = this.$refs.accesstimesegTable.selectedRows;
            //    var deleteStr = i18n.t("Messages.ConfirmDelete");
            //    if (selectedRows.length > 0) {
            //        if (confirm(deleteStr)) {
            //            var data = selectedRows.map(x => x.id).join(',');
            //            this.$services.delete(`accesstimeseg?ids=${data}`).done(() => {
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
