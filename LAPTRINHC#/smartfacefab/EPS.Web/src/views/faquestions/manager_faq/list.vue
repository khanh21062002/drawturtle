<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('ManagerFAQ.List.Header')}}</strong>
                    </div>

                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('ManagerFAQ.List.SearchForm.Question')"
                                              label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group label="" :label-cols="1">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="faquestions"
                            gridName="ManagerFAQ.List.Table.TableName"
                            ref="faquestionsTable">
                        <template v-slot:actions>
                            <router-link to="/faquestions/manager_faq/create" tag="button" class="btn btn-primary" size="md">
                                {{$t("Button.Create")}}
                            </router-link>
                            <!--<b-button size="md" @click.stop="removeSelected" variant="danger">
                {{$t("Button.Delete")}}
            </b-button>-->
                        </template>
                        <template v-slot:cell(typestr)="row">
                            <span class="col-form-label "> {{ $t(row.item.typestr) }}</span>

                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/faquestions/manager_faq/detail/${row.item.id}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('Button.Delete')">
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
    import i18n from '@/libs/i18n'

    export default {
        name: 'Listfaquestons',
        mixins: [authorizationMixin],
        data() {
            return {
                fqa: {
                    id:null
                },
                searchForm: {
                    filterText: null
                },
                gridOptions: {
                    fields: [
                        { key: 'name', label: this.$t('ManagerFAQ.List.Table.Name'), sortable: true, sortDirection: 'asc' },
                        { key: 'typestr', label: this.$t('ManagerFAQ.List.Table.Type'), sortable: true, sortDirection: 'asc' },
                    ],
                    sortBy: 'name',
                },
            }
        },
        created() {
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.fqa.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`faquestions/${this.fqa.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            search() {
                this.$refs.faquestionsTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`faquestions/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            removeSelected() {
                var vm = this;
                var selectedRows = this.$refs.faquestionsTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`faquestions?ids=${data}`).done(() => {
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
