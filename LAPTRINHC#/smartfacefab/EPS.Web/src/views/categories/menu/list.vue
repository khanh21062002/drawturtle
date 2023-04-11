<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.Menu.List.Header") }}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Menu.List.SearchForm.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterName">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Menu.List.SearchForm.Price')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="number"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterPrice">
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
                            dataUrl="menu"
                            gridName="Categories.Menu.List.Table.TableName"
                            ref="menuTable">
                        <template v-slot:cell(typeStr)="row">
                            <span>{{ $t(row.item.typeStr) }}</span>
                        </template>
                        <template v-slot:cell(setFoodId)="row">
                            <span>{{ getMon(row.item.setFoodId) }}</span>
                        </template>
                        <template v-slot:actions>
                            <router-link to="/categories/menu/create"
                                         tag="button"
                                         class="btn btn-primary btn-save"
                                         size="md"
                                         v-if="authorize(['ManageMenu'])">
                                {{ $t("Button.Create") }}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{path: `/categories/menu/detail/${row.item.id}`,}"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="doDelete(row.item.id)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManageMenu'])">
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
        name: "ListMenu",
        mixins: [authorizationMixin],
        data() {
            return {
                lstMon: [],
                searchForm: {
                    filterName: null,
                    filterPrice: null,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: "typeStr",
                            label: this.$t("Categories.Menu.List.Table.Type"),
                            sortable: false,
                        },
                        {
                            key: "name",
                            label: this.$t("Categories.Menu.List.Table.Name"),
                            sortable: false,
                        },
                        {
                            key: "priceStr",
                            label: this.$t("Categories.Menu.List.Table.Price"),
                            sortable: false,
                        },
                        {
                            key: "setFoodId",
                            label: this.$t("Categories.Menu.List.Table.SetFood"),
                            sortable: false,
                        },
                        {
                            key: "note",
                            label: this.$t("Categories.Menu.List.Table.Note"),
                            sortable: false,
                        },
                    ],
                    sortBy: "id",
                    sortDesc: true,
                },
            };
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.loadMon();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`menu/${item}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {
                    this.$refs.popup.close();
                }
            },
            getMon(idMon) {
                if (idMon != null) {
                    const vm = this;
                    const listId = idMon.split(",");
                    const listName = [];
                    listId.forEach(x => {
                        const name = vm.lstMon.filter(item => item.id == x) ? vm.lstMon.filter(item => item.id == x).map(x => x.text)[0] : '';
                        listName.push(name);
                    })
                    return listName.join(", ");
                }
                return '';
            },
            loadMon() {
                var vm = this;
                return this.$services.get(`lookup/mon`).done((res) => {
                    vm.lstMon = res;
                });
            },
            search() {
                this.$refs.menuTable.refresh();
            },
            refresh() {
                this.searchForm.FilterName = null;
                this.searchForm.FilterPrice = null;
                this.$refs.menuTable.refresh();
            },
        },
    };
</script>
<style scoped></style>
