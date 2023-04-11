<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.PreOrder.List.Header") }}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.PreOrder.List.SearchForm.Name')"
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
                                <b-form-group :label="this.$t('Categories.PreOrder.List.SearchForm.PhoneNumber')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :formatter="formatPhone"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterPhoneNumber">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.PreOrder.List.SearchForm.DateFrom')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%"
                                                 type="datetime"
                                                 v-model="searchForm.filterDateFrom"
                                                 valueType="YYYY-MM-DD HH:mm"
                                                 format="DD-MM-YYYY HH:mm">
                                    </date-picker>
                                    <b-form-input style="display: none"
                                                  v-model="$v.searchForm.filterDateFrom.$model"
                                                  :state="$v.searchForm.filterDateFrom.$dirty ? !$v.searchForm.filterDateFrom.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t('Timesheet.OverTime.List.Form.DateFromRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.PreOrder.List.SearchForm.DateTo')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%"
                                                 type="datetime"
                                                 v-model="searchForm.filterDateTo"
                                                 valueType="YYYY-MM-DD HH:mm"
                                                 format="DD-MM-YYYY HH:mm">
                                    </date-picker>
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
                            dataUrl="preOrder"
                            gridName="Categories.PreOrder.List.Table.TableName"
                            ref="preOrderTable">
                        <template v-slot:cell(typeStr)="row">
                            <span>{{ $t(row.item.typeStr) }}</span>
                        </template>
                        <template v-slot:actions>
                            <router-link to="/categories/preOrder/create"
                                         tag="button"
                                         class="btn btn-primary btn-save"
                                         size="md"
                                         v-if="authorize(['ManagePreOrder'])">
                                {{ $t("Button.Create") }}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{path: `/categories/preOrder/detail/${row.item.id}`,}"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="doDelete(row.item.id)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManagePreOrder'])">
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
    import moment from 'moment';

    export default {
        name: "ListPreOrder",
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    filterName: null,
                    filterPrice: null,
                    filterDateFrom: null,
                    filterDateTo: null,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: "nameGuest",
                            label: this.$t("Categories.PreOrder.List.Table.Guest"),
                            sortable: false,
                        },
                        {
                            key: "phoneNumber",
                            label: this.$t("Categories.PreOrder.List.Table.PhoneNumber"),
                            sortable: false,
                        },
                        {
                            key: "amountPeople",
                            label: this.$t("Categories.PreOrder.List.Table.AmountPeople"),
                            sortable: false,
                        },
                        {
                            key: "timeOrderStr",
                            label: this.$t("Categories.PreOrder.List.Table.TimeOrder"),
                            sortable: false,
                        },
                        {
                            key: "downPaymentStr",
                            label: this.$t("Categories.PreOrder.List.Table.DownPayment"),
                            sortable: false,
                        },
                        {
                            key: "requestOrder",
                            label: this.$t("Categories.PreOrder.List.Table.RequestOrder"),
                            sortable: false,
                        },
                    ],
                    sortBy: "timeOrder",
                    sortDesc: true,
                },
            };
        },
        validations: {
            searchForm: {
                filterDateFrom: {
                    checkDate(value) {
                        if (value && this.searchForm.filterDateTo) {
                            var a = moment(value, 'YYYY-MM-DD HH:mm');
                            var b = moment(this.searchForm.filterDateTo, 'YYYY-MM-DD HH:mm');
                            if (a > b) {
                                return false;
                            }
                            return true;
                        }
                        return true;
                    },
                },
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.CompId = accessToken.comId;
            this.searchForm.filterDateFrom = moment().format('YYYY-MM-DD 00:00');
            this.searchForm.filterDateTo = moment().format('YYYY-MM-DD 23:59');

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
                    this.$services.delete(`preOrder/${item}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {
                    this.$refs.popup.close();
                }
            },
            formatPhone(e) {
                return String(e).substring(0, 10);
            },
            search() {
                this.$refs.preOrderTable.refresh();
            },
            refresh() {
                this.searchForm.FilterName = null;
                this.searchForm.FilterPrice = null;
                this.searchForm.filterDateFrom = moment().format('YYYY-MM-DD 00:00');
                this.searchForm.filterDateTo = null;
                this.$refs.preOrderTable.refresh();
            },
        },
    };
</script>
<style scoped></style>
