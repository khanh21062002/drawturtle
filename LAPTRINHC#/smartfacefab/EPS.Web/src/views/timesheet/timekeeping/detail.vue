<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Cập nhật chấm công thủ công</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="12" offset="1">
                                <b-row>
                                    <b-col md="8" offset="1" style="display: none">
                                        <b-form-group label="Đơn vị:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 placeholder="Chọn giá trị" :disabled="true" v-if="editing"
                                                     v-model="timekeeping.comP_ID" :options="lstcompany">
                                            </select2>
                                            <label class="col-form-label" v-if="!editing">{{ timekeeping.comP_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Phòng ban:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <!--<select2 :options="lstdepartment" placeholder="Chọn giá trị" :settings="{allowClear: true}"
                                                     @change="loadPersonByDept($event)" v-if="editing"
                                                     v-model="timekeeping.depT_ID">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadPersonByDept($event.id)" v-if="editing"
                                                        :options="treeselect.options"
														:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="workinghours.deptId" />
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.timekeeping.depT_ID.$model"
                                                          :state="$v.timekeeping.depT_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn phòng ban
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ timekeeping.departmenT_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Nhân viên:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <select2 placeholder="Chọn giá trị" v-if="editing"
                                                     v-model="timekeeping.persoN_ID" :options="lstperson">
                                            </select2>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.timekeeping.persoN_ID.$model"
                                                          :state="$v.timekeeping.persoN_ID.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn nhân viên
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ timekeeping.persoN_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Ngày:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing? 'required' : ''">
                                            <date-picker v-if="editing" style="width: 100%;" v-model="timekeeping.datE_WORKING" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.timekeeping.datE_WORKING.$model"
                                                          :state="$v.timekeeping.datE_WORKING.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn ngày
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ timekeeping.formaT_DATE_WORKING }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Số công:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      v-slot="{ ariaDescribedby }"
                                                      label-align-md="left">
                                            <b-form-radio-group id="radio-group-2" v-if="!editing" disabled
                                                                v-model="timekeeping.worK_DAY"
                                                                :aria-describedby="ariaDescribedby"
                                                                name="radio-sub-component">
                                                <b-form-radio value="1">Cả ngày</b-form-radio>
                                                <b-form-radio value="0.5">Nửa ngày</b-form-radio>
                                            </b-form-radio-group>
                                            <b-form-radio-group id="radio-group-2" v-if="editing"
                                                                v-model="timekeeping.worK_DAY"
                                                                :aria-describedby="ariaDescribedby"
                                                                name="radio-sub-component">
                                                <b-form-radio value="1">Cả ngày</b-form-radio>
                                                <b-form-radio value="0.5">Nửa ngày</b-form-radio>
                                            </b-form-radio-group>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Ca làm việc:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <select2 placeholder="Chọn giá trị" :settings="{allowClear: true}" v-if="editing"
                                                     v-model="timekeeping.shifT_ID" :options="lstshiftime">
                                            </select2>
                                            <label class="col-form-label" v-if="!editing">{{ timekeeping.shifT_NAME }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group label="Ghi chú:"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-textarea id="textarea" v-if="editing"
                                                             v-model="timekeeping.note"
                                                             placeholder=""
                                                             rows="3"
                                                             max-rows="6">
                                            </b-form-textarea>
                                            <label class="col-form-label" v-if="!editing">{{ timekeeping.note }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group :label-cols="1">
                                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageTimeKeeping'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i>{{$t("Button.Cancel")}}</b-button>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
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
        name: 'TimeKeepingDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                timekeeping: {
                    depT_ID: null,
                    persoN_ID: null,
                    datE_WORKING: null,
                    worK_DAY: null,
                    shifT_ID: null,
                    note: null,
                    comP_ID: null,
                },
                lstcompany: [],
                lstdepartment: [],
                lstperson: [],
                lstshiftime: [],
                editing: false,
                treeselect: {
                    value: null,
                    options: [],
                },

            }
        },
        validations: {
            timekeeping: {
                depT_ID: { required },
                persoN_ID: { required },
                datE_WORKING: { required },
            }
        },
        computed: {
            timekeepingId() {
                return this.$route.params.timekeepingId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.timekeeping.compId = accessToken.comId;
            this.loadDetails();
            this.loadCompany();
            this.loadDepartment();
            this.loadWorkingShiftTime();
            this.loadDepartmentTree();
        },
        methods: {
            async loadDetails() {
                var vm = this;
                return this.$services.get(`timesheet/timekeeping/${vm.timekeepingId}`).done(timekeeping => {
                    vm.timekeeping = timekeeping;
                    vm.initListPerson(vm.timekeeping.depT_ID);
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            //Danh sách nhân viên
            loadPersonByDept(depT_ID) {
                var vm = this;
                if (depT_ID == null) return;
                return this.$services.get(`lookup/persons-by-dept/${depT_ID}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.timekeeping.persoN_ID = null;
                })
            },
            initListPerson(depT_ID) {
                var vm = this;
                if (depT_ID == null) return;
                return this.$services.get(`lookup/persons-by-dept/${depT_ID}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.timekeeping.persoN_ID = vm.timekeeping.persoN_ID.toUpperCase();
                })
            },
            //Danh sách ca
            async loadWorkingShiftTime() {
                var vm = this;
                return this.$services.get(`lookup/working-shift-time`).done(lstshiftime => {
                    vm.lstshiftime = lstshiftime;
                })
            },

            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`timesheet/timekeeping/${this.timekeepingId}`, this.timekeeping).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/timesheet/timekeeping/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadDetails();
                vm.editing = false;
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(lstdepartment => {
                    vm.treeselect.options = lstdepartment;
                });
            },
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
