<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{ $t('System.User.List.Header') }} </strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :multiple="false"
                                                :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="searchForm.compId" />
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('System.User.List.SearchForm.UserName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.username">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="" :label-cols="4">
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
                            dataUrl="users/list"
                            gridName="System.User.List.Table.TableName"
                            ref="userTable">
                        <template v-slot:actions v-if="authorize(['ManageUser'])">
                            <router-link to="/system/users/create"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(roles)="row">
                            <span class="col-form-label "> {{ $t(row.item.roles[0]) }}</span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          v-if="authorize(['ViewUser'])"
                                          :to="{ path: `/system/users/detail/${row.item.id}` }"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye"></i>
                                </b-button>
                                <b-button v-if="authorize(['ManageUser'])"
                                          size="sm"
                                          :to="{ path: `/system/users/privileges/${row.item.id}` }"
                                          :title="$t('Button.Privileges')">
                                    <i class="fa fa-cogs" aria-hidden="true"></i>
                                </b-button>
                                <b-button v-if="authorize(['ManageUser'])"
                                          size="sm"
                                          @click.stop="doDelete(row.item.id)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')">
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
        name: 'ListUser',
        mixins: [authorizationMixin],
        data() {
            return {
                user: {
                    id: null
                },
                searchForm: {
                    username: null,
                    unitId: null,
                    unitTraversalOption: 0,
                    compId: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: 'company',
                            label: this.$t('System.User.List.Table.Company'),
                            sortable: false,
                        },
                        {
                            key: 'username',
                            label: this.$t('System.User.List.Table.Name'),
                            sortable: false,
                        },
                        {
                            key: 'roles[0]',
                            label: this.$t('System.User.List.Table.RolesName'),
                            sortable: false,
                        },
                        {
                            key: 'phoneNumber',
                            label: this.$t('System.User.List.Table.Phone'),
                            sortable: false,
                        },
                    ],
                    sortBy: 'id',
                    sortDesc: true,
                },
                lstcompany: [],
                treeselect: {
                    value: null,
                    options: [],
                }
            }
        },
        created() {
            this.loadCompanyTree();
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.user.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`users/${this.user.id}`).done(() => {
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
            //Danh sách công ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done(lstcompany => {
                    vm.treeselect.options = lstcompany;
                });
            },
            search() {
                this.$refs.userTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`users/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            deletes() {
                var vm = this;
                var selectedRows = this.$refs.userTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`users?ids=${data}`).done(() => {
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
