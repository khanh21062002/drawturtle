<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("School.Parent.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="5">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" id="compId"
                                             :settings="{allowClear: true}" :disabled="true"
                                             v-model="searchForm.compId" :options="lstcompany">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="7">
                                <b-form-group :label="this.$t('School.Parent.List.SearchForm.ParentName')"
                                              label-for="code"
                                              :label-cols="5"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col md="5">
                                <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" id="gender" :settings="{allowClear: true}"
                                             v-model="searchForm.gender" :options="lstGender">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                    <b-button type="button" variant="warning" @click="refresh">{{$t("Button.Refresh")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="person/parent"
                            gridName="School.Parent.List.Table.TableName"
                            ref="personTable">
                        <template v-slot:cell(avatar)="row">
                            <img v-bind:src="row.item.file1" alt="No Image" width="60" height="60" style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                        </template>
                        <template v-slot:cell(vaccineStr)="row">
                            <span class="col-form-label " style=" display: inline-block; width: 140px; " v-if="row.item.vaccine == null || row.item.vaccine == 0"><i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}</span>
                            <span class=" bg-danger text-white p-8" style="padding: 8px; display: inline-block; width: 140px; " v-if="row.item.vaccine == -1"><i class="fa fa-times"></i> {{  $t(row.item.vaccineStr) }}</span>
                            <span class=" bg-warning text-white p-8" style="padding: 8px;display: inline-block; width: 140px;" v-if="row.item.vaccine == 1"><i class="fa fa-check"></i> {{  $t(row.item.vaccineStr) }}</span>
                            <span class=" bg-success text-white p-8" style="padding: 8px;display: inline-block; width: 140px;" v-if="row.item.vaccine >= 2"><i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}</span>

                        </template>
                        <template v-slot:cell(genderName)="row">
                            <span class="col-form-label "> {{ $t(row.item.genderName) }}</span>

                        </template>
                        <template v-slot:cell(statusName)="row">
                            <span class="col-form-label "> {{ $t(row.item.statusName) }}</span>

                        </template>


                        <template v-slot:actions>
                            <!--<router-link to="/school/student/import" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManagePerson'])">
                Import
            </router-link>-->
                            <router-link to="/school/parent/create" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageParent'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <router-link to="/school/parent/import" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageParent'])">
                                {{$t("Button.ImportExcel")}}
                            </router-link>


                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/school/parent/detail/${row.item.personId}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.id)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageParent'])">
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
        name: 'ListPerson',
        mixins: [authorizationMixin],
        data() {
            return {
                parent: {
                    id:null
                },
                searchForm: {
                    filterText: null,
                    compId: null,
                    gradeId: null,
                    deptId: null, 
                    gender: null,
                    filterTextStudent: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'personCode', label: this.$t('School.Parent.List.Table.Code'), sortable: false },
                        { key: 'fullName', label: this.$t('School.Parent.List.Table.Name'), sortable: false, },
                        { key: 'phoneNumber', label: this.$t('School.Parent.List.Table.NumberPhone'), sortable: false, },
                        { key: 'dobStr', label: this.$t('School.Parent.List.Table.Birthday'), sortable: false, },
                        { key: 'genderName', label: this.$t('Categories.Person.List.Table.Gender'), sortable: false },
                        { key: 'statusName', label: this.$t('Guess.List.Table.Status'), sortable: false },
                        { key: 'avatar', label: this.$t('Guess.List.Table.Image'), sortable: false },
                        { key: 'vaccineStr', label: this.$t('Categories.Person.List.Table.vaccine'), sortable: false },
                    ],
                    sortBy: 'fullName',
                    sortDesc: false,
                },
                lstcompany: [],
                lstgrades: [],
                lstclass:[],
                lstGender: [
                    { id: '0', text: this.$t("Guess.Detail.Form.Male") },
                    { id: '1', text: this.$t("Guess.Detail.Form.Female") }
                ],
                isAdmin: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.loadCompany();
           // this.loadGrades();
            //this.loadClass();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.parent.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`person/parent/${this.parent.id}`).done(() => {
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
            //Danh sách khối
            async loadGrades() {
                var vm = this;
                return this.$services.get(`lookup/grades`).done(lstgrades => {
                    vm.lstgrades = lstgrades;
                })
            },
            async loadClass() {
                var vm = this;
                return this.$services.get(`lookup/classes`).done(lstclass => {
                    vm.lstclass = lstclass;
                })
            },
            search() {
                this.$refs.personTable.refresh();
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`person/parent/${item.personId}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            refresh() {
                var vm = this;
                vm.searchForm = {
                    compId: null,
                    filterText: null,
                    gradeId: null,
                    deptId: null,
                    gender: null,
                    filterTextStudent: null,
                };
                var accessToken = Services.getAccessToken();
                this.searchForm.compId = accessToken.comId;
                vm.$refs.personTable.refresh();
            },
           
        }
    }
</script>
<style scoped>
</style>
