<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('System.Company.List.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.Company.List.SearchForm.CompanyName')"
                                              label-for="name"
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
                            <b-col md="6" offset="3">
                                <b-form-group label=""
                                              :label-cols="4">
                                    <b-button type="submit" variant="primary"> {{ $t("Button.Search") }} </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            class="grid-border-top"
                            dataUrl="company"
                            gridName="System.Company.List.Table.TableName"
                            ref="companyTable">
                        <template v-slot:actions>
                            <router-link to="/system/company/create"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManageCompany'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <b-button size="md"
                                      @click.stop="removeSelected"
                                      variant="danger"
                                      v-if="authorize(['ManageCompany'])">
                                {{$t("Button.Delete")}}
                            </b-button>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{ path: `/system/company/detail/${row.item.id}` }"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="remove(row.item, row.index, $event.target)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManageCompany'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { authorizationMixin } from '@/shared/mixins'
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListCompany',
        mixins: [authorizationMixin],
        data() {
            return {
                searchForm: {
                    filterText: null
                },
                gridOptions: {
                    fields: [
                        {
                            key: 'code',
                            label: this.$t('System.Company.List.Table.Code'),
                            sortable: false,
                        },
                        {
                            key: 'name',
                            label: this.$t('System.Company.List.Table.Name'),
                            sortable: false,
                        },
                    ],
                    sortBy: 'position',
                    sortDesc: false,
                },
            }
        },
        created() { },
        methods: {
            search() {
                this.$refs.companyTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`company/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            removeSelected() {
                var vm = this;
                var selectedRows = this.$refs.companyTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`company?ids=${data}`).done(() => {
                            vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                            vm.search();
                        });
                    }
                }
                else {
                    vm.$toastr.e(i18n.t("Messages.ToastrDelete_e"));
                }
            }
        }
    }
</script>
<style scoped>
</style>
