<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.GroupAccess.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>

                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">

                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.groupaccess.compId.$model"
                                                  :state="$v.groupaccess.compId.$error ? false : null">
                                    </b-form-input>
                                    <select2 placeholder="Chọn giá trị" :options="lstcompany" :disabled="true"
                                             v-model="$v.groupaccess.compId.$model" :state="$v.groupaccess.compId.$dirty? !$v.groupaccess.compId.$error : null">
                                    </select2>

                                    <b-form-invalid-feedback>
                                        Vui lòng chọn đơn vị
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
                                              label-class="required">

                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.groupaccess.groupid.$model"
                                                  :state="$v.groupaccess.groupid.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstgroup"
                                             v-model="$v.groupaccess.groupid.$model" :state="$v.groupaccess.groupid.$dirty? !$v.groupaccess.groupid.$error : null">
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
                                              label-class="required">

                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.groupaccess.timesegId.$model"
                                                  :state="$v.groupaccess.timesegId.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstaccesstimeseg"
                                             v-model="$v.groupaccess.timesegId.$model" :state="$v.groupaccess.timesegId.$dirty? !$v.groupaccess.timesegId.$error : null">
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
                                              label-class="required">

                                    <b-form-select type="text" style="display: none"
                                                   v-model="$v.groupaccess.LstMachineId.$model"
                                                   :state="$v.groupaccess.LstMachineId.$error ? false : null">
                                    </b-form-select>

                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstmachine" :settings="{ multiple: true }"
                                             v-model="$v.groupaccess.LstMachineId.$model" :state="$v.groupaccess.LstMachineId.$dirty? !$v.groupaccess.LstMachineId.$error : null">
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
                <b-form-select class="col-sm-12" placeholder="Hoạt động"
                               v-model="groupaccess.status">
                    <b-form-select-option value="1">Hoạt động</b-form-select-option>
                    <b-form-select-option value="0">Ngừng hoạt động</b-form-select-option>
                </b-form-select>
            </b-form-group>
        </b-col>
    </b-row>-->

                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageGroupAccess'])" @click="save" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
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
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GroupAccessDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                groupaccess: {
                    groupid: null,
                    machineId: null,
                    timesegId: null,
                    compId: null,
                    deptId: 0,
                    status: null,
                    LstMachineId: []
                },
                isAdmin: false,
                lstmachine: []
                , lstgroup: []
                , lstcompany: []
                , lstdepartment: []
                , lstaccesstimeseg: []
                ,
            }
        },
        validations: {
            groupaccess: {
                compId: { required },
                timesegId: { required },
                groupid: { required },
                deptId: { required },
                LstMachineId: { required }
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.groupaccess.departmenT_ID = accessToken.unitId;
            this.groupaccess.compId = accessToken.comId;
            await this.loadCompany();
            await this.loadMachine();
            await this.loadGroup();
            await this.loadAccessTimeSeg();
            await this.loadAllDepartmentAndClass();
            this.groupaccess.status = 1;

        },

        methods: {
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;

                })
            },
            async loadAllDepartmentAndClass() {
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
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(lstgroup => {
                    vm.lstgroup = lstgroup;

                });
            },
            async loadAccessTimeSeg() {
                var vm = this;
                return this.$services.get(`lookup/accesstimesegs`).done(lstaccesstimeseg => {
                    vm.lstaccesstimeseg = lstaccesstimeseg;

                });
            },
            back() {
                this.$router.push({ path: '/categories/groupaccess/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('groupaccess', this.groupaccess).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/categories/groupaccess/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/groupaccess/list' });
            },
            
        }
    }
</script>
<style scoped>
</style>
