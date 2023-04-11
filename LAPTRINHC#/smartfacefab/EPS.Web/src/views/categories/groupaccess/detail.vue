<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.GroupAccess.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.groupaccess.compId.$model"
                                                  :state="$v.groupaccess.compId.$error ? false : null">
                                    </b-form-input>
                                    <select2 class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                             v-model="groupaccess.compId" :options="lstcompany">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Group.Detail.Label.CompanyRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.Group')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.groupaccess.groupId.$model"
                                                  :state="$v.groupaccess.groupId.$error ? false : null">
                                    </b-form-input>

                                    <select2 class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="!editing"
                                             v-model="groupaccess.groupId" :options="lstgroup">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Accesstimeseg.Create.Require.GroupNameRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>

                        </b-row>

                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.GroupAccess.List.SearchForm.TimeSeg')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.groupaccess.timeSegId.$model"
                                                  :state="$v.groupaccess.timeSegId.$error ? false : null">
                                    </b-form-input>
                                    <select2 class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="!editing"
                                             v-model="groupaccess.timeSegId" :options="lstaccesstimeseg">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Group.Detail.Label.DateRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">

                                <b-form-group :label="this.$t('Categories.GroupAccess.List.SearchForm.Machine')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-select type="text" style="display: none"
                                                   v-model="$v.groupaccess.lstMachineId.$model"
                                                   :state="$v.groupaccess.lstMachineId.$error ? false : null">
                                    </b-form-select>
                                    <select2 class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="!editing" :settings="{ multiple: true }"
                                             v-model="groupaccess.lstMachineId" :options="lstmachine">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.GroupAccess.Detail.Label.MachineRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>

                        </b-row>
                        <!--<b-row>
        <b-col md="6">
            <b-form-group label="Trạng thái:"
                          :label-cols="4"
                          :horizontal="true"
                          label-align-md="right">
                <b-form-select class="col-sm-12" placeholder="Chọn giá trị" v-if="editing"
                               v-model="groupaccess.status">
                    <b-form-select-option value="1">Hoạt động</b-form-select-option>
                    <b-form-select-option value="0">Ngừng hoạt động</b-form-select-option>
                </b-form-select>
                <label class="col-form-label" v-if="!editing">{{ groupaccess.statusName }}</label>
            </b-form-group>
        </b-col>
    </b-row>-->



                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageGroupAccess'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                        </div>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>

    </div>
</template>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'
   
    export default {
        name: 'GroupAccessDetail',
        mixins: [validationMixin, authorizationMixin],
       
        data() {
            return {
                groupaccess: {
                    groupId: null,
                    machineId: null,
                    timeSegId: null,
                    compId: null,
                    deptId: 0,
                    status: null,
                    lstMachineId: []
                }, lstgroup: [],
                lstmachine: [],
                lstcompany: [],
                lstdepartment: [],
                lstaccesstimeseg: [],

                editing: false,
                isAdmin: false,
            }
        },
        computed: {
            groupaccessId() {
                return this.$route.params.groupaccessId;
            },
        },
        validations: {
            groupaccess: {
                compId: { required },
                timeSegId: { required },
                groupId: { required },
                deptId: { required },
                lstMachineId: { required }
            }
        },
        async created() {
            await this.loadCompany();
            await this.loadAllDepartment();
            await this.loadGroup();
            await this.loadAccessTimeSeg();
            await this.loadGroupAccessDetail();
            await this.loadMachine();

        },
        methods: { //Danh sách công ty
            async loadAllDepartment() {
                var vm = this;
                return this.$services.get(`lookup/all-departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;

                })
            },
            async loadMachine() {
                var vm = this;

                return this.$services.get(`lookup/machines`).done(lstmachine => {
                    vm.lstmachine = lstmachine;
                })
            },

            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },

            async loadAccessTimeSeg() {
                var vm = this;
                return this.$services.get(`lookup/accesstimesegs`).done(lstaccesstimeseg => {
                    vm.lstaccesstimeseg = lstaccesstimeseg;
                });
            },
            //Danh sách nhom nguoi
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(lstgroup => {
                    vm.lstgroup = lstgroup;
                });
            },

            async loadGroupAccessDetail() {
                var vm = this;
                return this.$services.get(`groupaccess/${this.groupaccessId}`).done(groupaccess => {
                    vm.groupaccess = groupaccess;
                })
             },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/groupaccess/list' });
            },
            async cancel() {
                var vm = this;
                this.loadGroupAccessDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`groupaccess/${this.groupaccessId}`, this.groupaccess).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.back();
                    });
                }
            },


        }
    }
</script>
<style scoped>
</style>
