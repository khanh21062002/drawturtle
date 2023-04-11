<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.Areas.List.Header") }}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Areas.List.SearchForm.Text')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.FilterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group style="margin-bottom: 0px !important"
                                              label=""
                                              :label-cols="3">
                                    <b-button type="submit"
                                              variant="primary"
                                              class="btn-search">
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
                            dataUrl="areas"
                            gridName="Categories.Areas.List.Table.TableName"
                            ref="areasTable">
                        <template v-slot:cell(statusName)="row">
                            <span>{{ $t(row.item.statusName) }}</span>
                        </template>
                        <template v-slot:actions>
                            <router-link to="/categories/areas/create"
                                         tag="button"
                                         class="btn btn-primary btn-save"
                                         size="md"
                                         v-if="authorize(['ManageAreas'])">
                                {{ $t("Button.Create") }}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{path: `/categories/areas/detail/${row.item.id}`,}"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="doDelete(row.item.id)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManageAreas'])">
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
        name: "ListAreas",
        mixins: [authorizationMixin],
        data() {
            return {
                area: {
                    id: null,
                },
                searchForm: {
                    FilterText: null,
                    FilterStatus: null,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: "code",
                            label: this.$t("Categories.Areas.List.Table.Code"),
                            sortable: false,
                        },
                        {
                            key: "name",
                            label: this.$t("Categories.Areas.List.Table.Name"),
                            sortable: false,
                        },
                        {
                            key: "note",
                            label: this.$t("Categories.Areas.List.Table.Note"),
                            sortable: false,
                        },
                    ],
                    sortBy: "id",
                    sortDesc: true,
                },
                lstStatus: [
                    { id: "1", text: this.$t("Guess.Detail.Form.Active") },
                    { id: "0", text: this.$t("Guess.Detail.Form.Stop") },
                    { id: 2, text: this.$t("TimeKeepService.Message.All") },
                ],
                isAdmin: false,
                modalDl: false,
            };
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.FilterStatus = 2;
            this.searchForm.compId = accessToken.comId;
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.area.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`areas/${this.area.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();

                    });
                } else {
                    this.$refs.popup.close();
                }
            },
            search() {
                this.$refs.areasTable.refresh();
            },
            refresh() {
                this.searchForm.FilterText = null;
                this.searchForm.FilterStatus = 2;
                this.$refs.areasTable.refresh();
            },
            remove() {
                var vm = this;
                this.$services.delete(`areas/${this.area.id}`).done(() => {
                    vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                    vm.search();
                });
                this.$refs.popup.close();
            },
            Delete(item) {
                this.$refs.popup.open();
                this.machine.id = item;
            },
        },
    };
</script>
<style scoped></style>
