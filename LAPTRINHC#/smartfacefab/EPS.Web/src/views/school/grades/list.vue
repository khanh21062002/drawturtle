<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('School.Grades.List.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="unit"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" :disabled="true" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                    <!--<unit-tree v-model="searchForm.compId" ref="unitTree"></unit-tree>-->
                                    <!--<b-form-radio-group v-model="searchForm.unitTraversalOption" name="radio-sub-component">
                        <b-form-radio value="0">Trực thuộc đơn vị</b-form-radio>
                        <b-form-radio value="1">Cả đơn vị con</b-form-radio>
                        <b-form-radio value="2">Toàn bộ đơn vị cấp dưới</b-form-radio>
                    </b-form-radio-group>-->
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('School.Grades.List.SearchForm.UnitName')"
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
                            dataUrl="grades"
                            :multiLanguage="true"
                            gridName="School.Grades.List.Table.TableName"
                            ref="gradesTable">
                        <template v-slot:actions>
                            <router-link to="/school/grades/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageGrades'])">
                                {{$t("Button.Create")}}
                            </router-link>

                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/school/grades/detail/${row.item.id}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageGrades'])">
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
        name: 'ListDepartment',
        mixins: [authorizationMixin],
        data() {
            return {
                grades: {
                    id:null
                },
                searchForm: {
                    filterText: null,
                    username: null,
                    Id: null,
                    departmentTraversalOption: 0,
                    compId: null
                },
                gridOptions: {
                    fields: [
                        { key: 'compName', label: this.$t('Categories.Accesstimeseg.List.Table.Company'), sortable: false, },
                        { key: 'name', label: this.$t('School.Grades.List.Table.NameClass'), sortable: false, },
                        { key: 'gradeNumber', label: this.$t('School.Grades.List.Table.Unit'), sortable: false, },
                        { key: 'descriptions', label: this.$t('School.Grades.List.Table.Descriptions'), sortable: false },
                    ],

                    sortBy: 'id',
                    sortDesc: true,
                }, lstcompany: [],
                isAdmin: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            //this.searchForm.department_ID = accessToken.unitId;
            await this.loadCompany();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.grades.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`grades/${this.grades.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                   
                });
            },
            search() {
                this.$refs.gradesTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`grades/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            refresh() {
                var vm = this;
                vm.searchForm = {
                    filterText: null,
                    username: null,
                    Id: null,
                    departmentTraversalOption: 0,
                    compId: null
                };
                var accessToken = Services.getAccessToken();
                this.searchForm.compId = accessToken.comId;
                this.$refs.gradesTable.refresh();
            },
           
        }
    }
</script>
<style scoped>
</style>
