<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.Holidays.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Holidays.List.Form.CompanyName')"
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
                                <b-form-group :label="this.$t('Timesheet.Holidays.List.Form.Year')"
                                              label-for="inp_year"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstYear" class="col-10" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_year" :settings="{allowClear: true}"
                                             v-model="searchForm.year">
                                    </select2>
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
                            dataUrl="timesheet/holidays"
                            :multiLanguage="true"
                            gridName="Timesheet.Holidays.List.Header"
                            ref="holidaysTable">
                        <template v-slot:actions>
                            <router-link to="/timesheet/holidays/create" tag="button" class="btn btn-primary" size="md" v-if=" authorize(['ManageHolidays'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/timesheet/holidays/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')" v-if="authorize(['ManageHolidays'])">
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
                holidays: {
                    id:null
                },
                searchForm: {
                    year: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'formatDate', label: this.$t('Timesheet.Holidays.List.Table.Date'), sortable: false, },
                        { key: 'description', label: this.$t('Timesheet.Holidays.List.Table.Description'), sortable: false, },
                        { key: 'coEf', label: this.$t('Timesheet.Holidays.List.Table.Coefficient'), sortable: false, },
                    ],
                    sortBy: 'date',
                    sortDesc: false,
                },
                lstcompany: [],
                isAdmin: false,
                isEmpty: false,
                lstYear: [],
            }
        },

        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.loadYear();
            await this.loadCompany();

        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.holidays.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`timesheet/holidays/${this.holidays.id}`).done(() => {
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
                this.$refs.holidaysTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`timesheet/holidays/${item.id}`).done(() => {
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
