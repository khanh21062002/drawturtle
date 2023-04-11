<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('System.Role.List.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.Role.List.SearchForm.RoleName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col style="text-align:center">
                                <b-form-group>
                                    <b-button type="submit"
                                              style="width: 120px"
                                              variant="primary">
                                        {{ $t("Button.Search") }}
                                    </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            class="grid-border-top"
                            dataUrl="roles"
                            gridName="System.Role.List.Table.TableName"
                            ref="roleTable">
                        <template v-slot:actions>
                            <router-link to="/system/roles/create"
                                         style="width: 120px"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManageRole'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(name)="row">
                            <span class="col-form-label "> {{ $t(row.item.name) }}</span>
                        </template>
                        <template v-slot:cell(statusName)="row">
                            <span class="col-form-label "> {{ $t(row.item.statusName) }}</span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{ path: `/system/roles/detail/${row.item.id}` }"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          :to="{ path: `/system/roles/privileges/${row.item.id}` }"
                                          :title="$t('Button.Privileges')"
                                          v-if="authorize(['ManageRole'])">
                                    <i class="fa fa-cogs" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="doDelete(row.item.id)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManageRole'])">
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
        name: 'ListRole',
        mixins: [authorizationMixin],
        data() {
            return {
                roles: {
                    id: null
                },
                searchForm: {
                    filterText: null
                },
                gridOptions: {
                    fields: [
                        { key: 'name', label: this.$t('System.Role.List.Table.Name'), sortable: true, sortDirection: 'asc' },
                        { key: 'description', label: this.$t('System.Role.List.Table.Description'), sortable: false },
                        { key: 'statusName', label: this.$t('System.Role.List.Table.Status'), sortable: true },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                }, lstcompany: [],
            }
        },
        created() {
            this.loadCompany();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.roles.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`roles/${this.roles.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {
                    this.$refs.popup.close();
                }
            },
            //Danh sách công ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            search() {
                this.$refs.roleTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`roles/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            removeSelected() {
                var vm = this;
                var selectedRows = this.$refs.roleTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`roles?ids=${data}`).done(() => {
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
