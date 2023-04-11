<template>
    <div id="app" style="margin-top: 0;">
        <b-card>
            <div slot="header" style="text-align: left;">
                <strong>{{$t('System.Company.List.Header')}}</strong>
            </div>
            <vue-ads-table-tree :columns="columns"
                                :rows="rows"
                                :page="page"
                                :styling="styles"
                                :filter="filter"
                                @filter-change="filterChanged"
                                @page-change="pageChanged"
                                @selection-change="selectionChanged"
                                ref="treeTable">
                <template slot="top" slot-scope="topper">
                    <b-row>
                        <b-col md="6" class="text-left" style="margin-right: 18px; margin-left: 3px;">
                            <b-form-group :label="$t('System.Company.List.SearchForm.CompanyName')"
                                          label-for="companyFilter"
                                          :label-cols="5"
                                          :horizontal="true"
                                          label-align-md="left">
                                <b-form-input type="tel"
                                              maxlength="20"
                                              v-model="filter"
                                              @change="filterChanged">
                                </b-form-input>
                            </b-form-group>
                        </b-col>
                        <b-col md="2">
                            <b-form-group label="">
                                <b-button type="submit"
                                          @click="filterChanged"
                                          style="width: 120px"
                                          variant="primary">
                                    {{$t("Button.Search")}}
                                </b-button>
                            </b-form-group>
                        </b-col>
                    </b-row>
                    <b-row class="grid-border-top" style="margin-left: 0px; margin-right: 0px;">
                        <b-col md="4" class="text-left" :label-cols="5" style="text-align: left; margin-bottom: 5px; margin-left: -14px; ">
                            <h2>{{$t('System.Company.List.Header')}}</h2>
                        </b-col>
                        <b-col md="8" class="text-right" :label-cols="12" style="text-align: right; margin-bottom: 5px; margin-right: -26px; margin-left: 34px;">
                            <router-link to="/system/company/create/"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManageCompany'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </b-col>
                    </b-row>
                </template>
                <template slot="index" slot-scope="props">
                    {{props.row._meta.index + 1}}
                </template>
                <template slot="action" slot-scope="props">
                    <b-button size="sm"
                              :to="{ path: `/system/company/detail/${props.row.id}` }"
                              :title="$t('Button.View')">
                        <i class="fa fa-eye" aria-hidden="true"></i>
                    </b-button>
                    <b-button size="sm"
                              @click.stop="doDelete(props.row.id)"
                              class="mr-1"
                              :title="$t('Button.Delete')"
                              v-if="authorize(['ManageCompany'])">
                        <i class="fa fa-trash-o" aria-hidden="true"></i>
                    </b-button>
                    <b-button size="sm"
                              :to="{ path: `/system/company/create/${props.row.id}` }"
                              :title="$t('Button.Adda')"
                              v-if="authorize(['ManageCompany'])">
                        <i class="fa fa-plus" aria-hidden="true"></i>
                    </b-button>
                </template>
            </vue-ads-table-tree>
            <confirmModal ref="confirmModal">
            </confirmModal>
        </b-card>
    </div>
</template>

<script>
    import { authorizationMixin } from '@/shared/mixins'
    import i18n from '@/libs/i18n'

    export default {
        name: 'TableContainerApp',
        mixins: [authorizationMixin],
        data() {
            let rows = [];
            let columns = [
                {
                    property: 'action',
                    direction: null,
                    filterable: false,
                    groupable: false,
                },
                {
                    property: 'index',
                    title: this.$t('System.Company.List.Table.Index'),
                    direction: null,
                    filterable: false,
                    groupable: false,
                    collapseIcon: true,
                },
                {
                    property: 'code',
                    title: this.$t('System.Company.List.Table.Code'),
                    direction: null,
                    filterable: true,
                    groupable: false,
                },
                {
                    property: 'label',
                    title: this.$t('System.Company.List.Table.Name'),
                    direction: null,
                    filterable: true,
                    groupable: false,
                    groupCollapsable: false,
                    hideOnGroup: true,
                },
            ];
            let styles = {
                group: {
                    'vue-ads-font-bold': true,
                    'vue-ads-border-b': true,
                    'vue-ads-italic': true,
                },
                '0/all': {
                    'vue-ads-py-2': true,
                    'vue-ads-px-1': true,
                },
                'even/': {
                    'vue-ads-bg-blue-lighter': true,
                },
                'odd/': {
                    'vue-ads-bg-blue-lightest': true,
                },
                '0/': {
                    'vue-ads-bg-blue-lighter': false,
                    'vue-ads-bg-blue-dark': true,
                    'vue-ads-text-white': true,
                    'vue-ads-font-bold': true,
                },
                '1_/': {
                    'hover:vue-ads-bg-red-lighter': true,
                },
                '1_/0': {
                    'leftAlign': true
                }
            };
            return {
                company:
                {
                    id: null
                },
                rows,
                columns,
                styles,
                filter: '',
                page: 0,
                selectedRowIds: [],
            };
        },
        created() { },
        computed: {
            nothingSelected() {
                return this.selectedRowIds.length === 0;
            },
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.company.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`company/${this.company.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.addRows();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            sleep(ms) {
                return new Promise(resolve => setTimeout(resolve, ms));
            },
            filterChanged(filter) {
                let filterTrim = "";
                if (filter) filterTrim = filter.trim();
                this.filter = filterTrim;
            },
            pageChanged(page) {
                this.page = page;
            },
            selectionChanged(selectedRows) {
                this.selectedRowIds = selectedRows.map(row => row.id);
            },
            async callTempRows() {
                await this.sleep(1000);
                return {
                    rows: [],
                    total: 0,
                };
            },
            deleteRows() { },
            async addRows() {
                var vm = this;
                return this.$services.get(`lookup/company-tree-view`).done(lstcompany => {
                    vm.rows = lstcompany;
                });
            },
            remove(item, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`company/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.addRows();
                    });
                }
            },
        },
        mounted() {
            // Add rows after component is created to illustrate
            // how the change is recognized and reflected in the table.
            this.addRows();
        },
    };
</script>
<style>
    tbody {
        text-align: left !important;
    }

    .fa-sort {
        display: none;
    }

    thead {
        background: aliceblue;
    }

    th.vue-ads-cursor-pointer.vue-ads-border-r.vue-ads-px-4.vue-ads-py-2.vue-ads-text-sm.vue-ads-text-left:nth-child(1) {
        width: 141px;
    }

    th.vue-ads-cursor-pointer.vue-ads-border-r.vue-ads-px-4.vue-ads-py-2.vue-ads-text-sm.vue-ads-text-left:nth-child(2) {
        width: 85px;
    }

    tr.hover\:vue-ads-bg-gray-200.vue-ads-bg-white.vue-ads-border-b {
        background-color: #f2f2f2;
    }

    tr.hover\:vue-ads-bg-gray-200.vue-ads-bg-gray-100.vue-ads-border-b {
        background-color: #f5f6f775;
    }
</style>