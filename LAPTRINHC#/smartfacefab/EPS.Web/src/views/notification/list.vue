<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Warning.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="5">
                                <b-form-group :label="this.$t('Warning.List.Form.Company')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')"
                                             id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="5">
                                <b-form-group :label="this.$t('Warning.List.Form.Machine')"
                                              label-for="dpl_compId"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstmachine" :placeholder="this.$t('PlaceHolder.Select')"
                                             id="dpl_departmentid" :settings="{allowClear: true}"
                                             v-model="searchForm.machineId">
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
                            dataUrl="notification"
                            :multiLanguage="true"
                            gridName="Warning.List.HeaderTable"
                            ref="notitable">
                        <template v-slot:actions>
                            <router-link to="/notification/create" tag="button" class="btn btn-primary" size="md" v-if=" authorize(['ManageNotification'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(typeName)="row">
                            <span class="col-form-label "> {{ $t(row.item.typeName) }}</span>

                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/notification/detail/${row.item.id}` }" :title="$t('TitleDetail.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('TitleDetail.Delete')" v-if="authorize(['ManageNotification'])">
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
                notification: {
                    id: null
                },
                searchForm: {
                    compId: null,
                    machineId: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'typeName', label: this.$t('Warning.List.Table.Type'), sortable: false, },
                        { key: 'machineName', label: this.$t('Warning.List.Table.Machine'), sortable: false, },
                        { key: 'scoreMatch', label: this.$t('Warning.List.Table.Thresholds'), sortable: false, },

                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                lstmachine: [],
                isAdmin: false,
                isEmpty: false,
            }
        },

        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.loadCompany();
            this.loadMachine();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.notification.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`notification/${this.notification.id}`).done(() => {
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
            loadMachine() {
                var vm = this;
                return this.$services.get(`lookup/machines`).done(lstmachine => {
                    vm.lstmachine = lstmachine;
                })
            },
            search() {
                this.$refs.notitable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`notification/${item.id}`).done(() => {
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
