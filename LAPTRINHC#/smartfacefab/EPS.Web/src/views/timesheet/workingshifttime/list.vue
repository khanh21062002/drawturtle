<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.WorkingShiftTimes.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.List.Form.Company')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="false"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.List.Form.ShiftCode')"
                                              label-for="inp_code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="inp_code"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterCode">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Timesheet.WorkingShiftTimes.List.Form.ShiftName')"
                                              label-for="inp_name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="inp_name"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterName">
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
                            dataUrl="timesheet/working-shift-times"
                            :multiLanguage="true"
                            gridName="Timesheet.WorkingShiftTimes.List.HeaderTable"
                            ref="shifttimeTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/working-shift-times/create" tag="button" class="btn btn-primary" size="md" v-if=" authorize(['ManageShiftTime'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(formatLateAccept)="row">
                            <span class="col-form-label "> {{ row.item.formatLateAccept }}</span>
                        </template>
                        <template v-slot:cell(formatEarlyAccept)="row">
                            <span class="col-form-label "> {{ row.item.formatEarlyAccept }}</span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/timesheet/working-shift-times/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')" v-if="authorize(['ManageShiftTime'])">
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
                working: {
                    id:null
                },
                searchForm: {
                    filterCode: null,
                    compId: null,
                    filterName: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'code', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.ShiftCode'), sortable: false, },
                        { key: 'name', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.ShiftName'), sortable: false, },
                        { key: 'formatStartTime', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.StartTime'), sortable: false, },
                        { key: 'formatEndTime', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.EndTime'), sortable: false, },
                        { key: 'total', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.Total'), sortable: false, },
                        { key: 'workingShift', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.WorkingShift'), sortable: false, },
                        { key: 'ot', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.OT'), sortable: false, },
                        { key: 'formatLateAccept', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.LateAccept'), sortable: false, },
                        { key: 'formatEarlyAccept', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.EarlyAccept'), sortable: false, },
                        { key: 'formatStartCheckInOverTime', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.CheckInOT'), sortable: false, },
                        { key: 'formatEndCheckOutOverTime', label: this.$t('Timesheet.WorkingShiftTimes.List.Table.CheckOutOT'), sortable: false, },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                isAdmin: false,
                isEmpty: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            await this.loadCompany();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.working.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/working-shift-times/${this.working.id}`).done(() => {
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
            search() {
                this.$refs.shifttimeTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`timesheet/working-shift-times/${item.id}`).done(() => {
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
