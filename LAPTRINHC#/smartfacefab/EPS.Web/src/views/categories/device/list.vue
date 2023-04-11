<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{ $t("Categories.Device.List.Header") }} </strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.List.Table.Features')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstfeature"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{ allowClear: true }"
                                             v-model="searchForm.featuresId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.List.SearchForm.DeviceName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.List.Table.Area')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstArea"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{ allowClear: true }"
                                             v-model="searchForm.areaId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label-cols="4"
                                              label-align-md="right">
                                    <b-button type="submit" variant="primary">
                                        {{ $t("Button.Search") }}
                                    </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid class="grid-border-top"
                            :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="Device"
                            gridName="Categories.Device.List.Table.TableName"
                            ref="DeviceTable">
                        <template v-slot:actions>
                            <router-link to="/categories/device/create"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManageMachine'])">
                                {{ $t("Button.Create") }}
                            </router-link>
                        </template>
                        <template v-slot:cell(currentStatusName)="row">
                            <span style="color: green; font-weight: bolder"
                                  v-if="row.item.currentStatus == 1">
                                {{ row.item.currentStatusName }}
                            </span>
                            <span style="color: red; font-weight: bolder"
                                  v-if="row.item.currentStatus == 2 || row.item.currentStatus == null">
                                {{ row.item.currentStatusName }}
                            </span>
                        </template>
                        <template v-slot:cell(directionStr)="row">
                            <span> {{ $t(row.item.directionStr) }} </span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{ path: `/categories/Device/detail/${row.item.id}` }"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="doDelete(row.item)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManageMachine'])">
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
    import { authorizationMixin } from "@/shared/mixins";
    import Services from "@/utils/services";
    import i18n from "@/libs/i18n";

    export default {
        name: "ListDevice",
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    filterText: null,
                    featuresId: null,
                    areaId: null
                },
                gridOptions: {
                    fields: [
                        {
                            key: "deviceName",
                            label: this.$t("Categories.Device.List.Table.Name"),
                            sortable: false,
                        },
                        {
                            key: "deviceCode",
                            label: this.$t("Categories.Device.List.Table.Code"),
                            sortable: false,
                        },
                        {
                            key: "rstpLink",
                            label: this.$t("Categories.Device.List.Table.RstpLink"),
                            sortable: false,
                        },
                        {
                            key: "featuresName",
                            label: this.$t("Categories.Device.List.Table.Features"),
                            sortable: false,
                        },
                        {
                            key: "areaName",
                            label: this.$t("Categories.Device.List.Table.Area"),
                            sortable: false,
                        },
                        {
                            key: "currentStatusName",
                            label: this.$t("Categories.Group.List.Table.Status"),
                            sortable: false,
                        }
                    ],
                    sortBy: "id",
                    sortDesc: true,
                },
                lstfeature: [],
                lstArea: [],
                isAdmin: false,
                Device: [],
            };
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            await this.loadFeature();
            await this.loadArea();
        },
        methods: {
            async loadArea() {
                var vm = this;
                return this.$services.get("lookup/areas-l").done((res) => {
                    vm.lstArea = res;
                });
            },
            async loadFeature() {
                var vm = this;
                return this.$services.get(`lookup/features`).done((res) => {
                    vm.lstfeature = res;
                });
            },
            search() {
                this.$refs.DeviceTable.refresh();
            },
            synchronized(id) {
                var vm = this;
                this.$services.get(`Device/synchronized/${id}`).done(() => {
                    vm.$toastr.s(
                        i18n.t("Categories.Device.List.Table.LastTimeSynchronized")
                    );
                });
            },
            async doDelete(item) {
                var vm = this;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`Device/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {
                    this.$refs.popup.close();
                }
            },
        },
    };
</script>

<style scoped></style>
