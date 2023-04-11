<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.RegisterWorkingShift.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save" @submit="$v.$touch()">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.Company')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label class="col-form-label">{{ registerworkingshift.compName }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.Type')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" v-if="editing"
                                                     v-model="registerworkingshift.type" :options="lsttype">
                                            </select2>
                                            <label class="col-form-label" v-if="!editing">{{ $t(registerworkingshift.typeName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.DepartmentName')"
                                                      v-if="showDept"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none;"
                                                          v-model="$v.registerworkingshift.deptId.$model"
                                                          :state="$v.registerworkingshift.deptId.$error ? false : null">
                                            </b-form-input>
                                            <!--<select2 :placeholder="this.$t('PlaceHolder.Select')" @change="loadListPersonByDept($event)" v-if="editing"
                                                     v-model="registerworkingshift.deptId" :options="lstdepartment">
                                            </select2>-->
                                            <treeselect :multiple="false" @select="loadListPersonByDept($event.id)" v-if="editing"
                                                        :options="treeselect.options"
														:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="registerworkingshift.deptId" />
                                            <label class="col-form-label" v-if="!editing">{{ registerworkingshift.deptName }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.RegisterWorkingShift.Detail.Form.DepartmentRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.EmployeeName')"
                                                      v-if="showPerson"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none;" v-if="editing"
                                                          v-model="$v.registerworkingshift.personId.$model"
                                                          :state="$v.registerworkingshift.personId.$error ? false : null">
                                            </b-form-input>
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" v-if="editing"
                                                     v-model="registerworkingshift.personId" :options="lstperson">
                                            </select2>
                                            <label class="col-form-label" v-if="!editing">{{ registerworkingshift.personName }}</label>
                                            <b-form-invalid-feedback v-if="editing">
                                                {{$t('Timesheet.RegisterWorkingShift.Detail.Form.EmployeeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.DateFrom')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none" v-if="editing"
                                                          v-model="$v.registerworkingshift.dateFrom.$model"
                                                          :state="$v.registerworkingshift.dateFrom.$error ? false : null">
                                            </b-form-input>
                                            <date-picker v-if="editing " style="width: 100%;" type="date"
                                                         v-model="registerworkingshift.dateFrom"
                                                         :editable="false"
                                                         id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                            </date-picker>
                                            <label class="col-form-label" v-if="!editing">{{ registerworkingshift.dateFromFormat }}</label>
                                            <b-form-invalid-feedback v-if="editing">
                                                {{$t('Timesheet.RegisterWorkingShift.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.DateTo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.registerworkingshift.dateTo.$model"
                                                          :state="$v.registerworkingshift.dateTo.$error ? false : null">
                                            </b-form-input>
                                            <date-picker v-if="editing" style="width: 100%;" type="date" v-model="registerworkingshift.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback v-if="!$v.registerworkingshift.dateTo.required">
                                                {{$t('Timesheet.RegisterWorkingShift.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ registerworkingshift.dateToFormat }}</label>
                                            <b-form-invalid-feedback v-if="!$v.registerworkingshift.dateTo.greaterFrom">
                                                {{$t('Timesheet.RegisterWorkingShift.Detail.Form.DateToRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.RegisterWorkingShift.Detail.Form.Note')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-textarea id="textarea" v-if="editing"
                                                             v-model="registerworkingshift.note"
                                                             :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                             rows="3"
                                                             max-rows="6">
                                            </b-form-textarea>
                                            <label class="col-form-label" v-if="!editing">{{ registerworkingshift.note }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        </br>
                        <b-row>
                            <b-col md="7">
                                <h2>{{$t("Timesheet.RegisterWorkingShift.Detail.HeaderTable")}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12">
                                <b-table striped hover :items="listDetails" :fields="detailsField">
                                    <template v-slot:cell(index)="row">
                                        {{ row.index + 1 }}
                                    </template>

                                    <template v-slot:cell(name)="row">
                                        {{ row.item.name }}
                                    </template>
                                    <template v-slot:cell(monday)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.monday"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(tueDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.tueDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(wedDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.wedDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(thuDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.thuDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(friDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.friDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(satDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.satDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(sunDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.sunDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                </b-table>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageWorkCalendar'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                                    <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                registerworkingshift: {
                    ID: 0,
                    compId: null,
                    type: null,
                    deptId: null,
                    personId: null,
                    dateFrom: null,
                    dateTo: null,
                    note: null,
                    timeCycle: null,
                    listDetails: [],
                },
                lstcompany: [],
                lstdepartment: [],
                listDetails: [],
                listDetailsDB: [],
                lstperson: [],
                lsttype: [],
                detailsField: [
                    { key: 'index', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Index'), sortable: false },
                    { key: 'name', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.NameShift'), sortable: false },
                    { key: 'monday', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Monday'), sortable: false },
                    { key: 'tueDay', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Tuesday'), sortable: false, },
                    { key: 'wedDay', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Wednesday') },
                    { key: 'thuDay', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Thursday') },
                    { key: 'friDay', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Friday') },
                    { key: 'satDay', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Saturday') },
                    { key: 'sunDay', label: this.$t('Timesheet.RegisterWorkingShift.Detail.Field.Sunday') },
                ],
                editing: false,
                compareDate: true,
                treeselect: {
                    value: null,
                    options: [],
                },
            }
        },
        validations: {
            registerworkingshift: {
                compId: { required },
                deptId: {
                    customValidate(value) {
                        if (this.registerworkingshift.type != 0 && (this.registerworkingshift.deptId == null || this.registerworkingshift.deptId == "")) {
                            return false
                        }
                        else
                            return true;
                    }
                },
                personId: {
                    customValidate(value) {
                        if (this.registerworkingshift.type == 2 && (this.registerworkingshift.personId == null || this.registerworkingshift.personId == "")) {
                            return false
                        }
                        else
                            return true;
                    }
                },
                dateFrom: { required },
                dateTo: {
                    required,
                    greaterFrom(value) {
                        if (this.registerworkingshift.dateFrom > this.registerworkingshift.dateTo) {
                            return false
                        }
                        else
                            return true;
                    }
                },
            }
        },
        watch: {
            // whenever typeWatch changes, this function will run
            typeWatch: function (newType, oldType) {
                var vm = this;
                if (newType != 2) {
                    vm.registerworkingshift.personId = null;
                }
                if (newType == 0) {
                    vm.registerworkingshift.deptId = null;
                    vm.registerworkingshift.personId = null;
                }
            },
        },
        computed: {
            showPerson: function () {
                var vm = this;
                if (vm.registerworkingshift.type == 2) {
                    return true;
                }
                return false;
            },
            showDept: function () {
                var vm = this;
                if (vm.registerworkingshift.type != 0) {
                    return true;
                }
                return false;
            },
            typeWatch: function () {
                var vm = this;
                if (vm.registerworkingshift.type) return vm.registerworkingshift.type;

                return 1;
            },
            registerWorkingShiftId() {
                return this.$route.params.registerworkingshiftId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.registerworkingshift.compId = accessToken.comId;
            this.loadDetail();
            this.loadCompany();
            this.loadDepartment();
            this.loadDepartmentTree();
            this.initDetails();
        },
        methods: {
            async loadDetail() {
                var vm = this;
                return this.$services.get(`timesheet/register-working-shift/${this.registerWorkingShiftId}`).done(registerworkingshift => {
                    vm.registerworkingshift = registerworkingshift;
                    if (registerworkingshift.personId != null) {
                        vm.registerworkingshift.personId = registerworkingshift.personId.toUpperCase();
                    }
                    vm.loadListPersonByDeptFirst(vm.registerworkingshift.deptId);
                    let dayFrom = new Date(this.registerworkingshift.dateFrom);
                    const currentDate = new Date();
                    currentDate.setHours(0, 0, 0, 0);
                    if (dayFrom >= currentDate)
                        this.compareDate = true;
                    else
                        this.compareDate = false;
                });
            },
            loadPerson() {
                var vm = this;
                return this.$services.get(`lookup/persons`).done(lstperson => {
                    vm.lstperson = lstperson;
                })
            },
            initDetails() {
                var vm = this;
                //load list type
                vm.lsttype = [];
                let item0 = {};
                item0.id = 0;
                item0.value = 0;
                item0.text = i18n.t("Timesheet.RegisterWorkingShift.Detail.InitDetail.Company");
                vm.lsttype.push(item0);
                let item = {};
                item.id = 1;
                item.value = 1;
                item.text = i18n.t("Timesheet.RegisterWorkingShift.Detail.InitDetail.Department");
                vm.lsttype.push(item);
                let item2 = {};
                item2.id = 2;
                item2.value = 2;
                item2.text = i18n.t("Timesheet.RegisterWorkingShift.Detail.InitDetail.Employee");
                vm.lsttype.push(item2);
                return this.$services.get(`timesheet/register-working-shift/getListDetails/${this.registerWorkingShiftId}`).done(listDetailsDB => {
                    vm.listDetailsDB = listDetailsDB;
                    // call init List details
                    vm.initListDetails();
                });
            },
            initListDetails() {
                var vm = this;
                return this.$services.get(`timesheet/working-shift-times/all`).done(listShiftTimes => {
                    vm.listDetails = [];
                    listShiftTimes.forEach((shift) => {
                        let item = {};
                        item.workingShiftId = shift.id;
                        item.name = shift.name;
                        let isInitFromDB = false;
                        for (let i = 0; i < vm.listDetailsDB.length; i++) {
                            let detailsDB = vm.listDetailsDB[i];
                            if (detailsDB.workingShiftId == shift.id) {
                                item.monday = detailsDB.monday;
                                item.tueDay = detailsDB.tueDay;
                                item.wedDay = detailsDB.wedDay;
                                item.thuDay = detailsDB.thuDay;
                                item.friDay = detailsDB.friDay;
                                item.satDay = detailsDB.satDay;
                                item.sunDay = detailsDB.sunDay;
                                isInitFromDB = true;
                                break;
                            }
                        }
                        if (!isInitFromDB) {
                            item.monday = 0;
                            item.tueDay = 0;
                            item.wedDay = 0;
                            item.thuDay = 0;
                            item.friDay = 0;
                            item.satDay = 0;
                            item.sunDay = 0;
                        }
                        vm.listDetails.push(item);
                    });
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                });
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(lstdepartment => {
                    vm.treeselect.options = lstdepartment;
                });
            },
            back() {
                this.$router.push({ path: '/timesheet/register-working-shift/list' });
            },
            edit() {
                this.editing = true;
            },
            async cancel() {
                var vm = this;
                await this.loadDetail();
                vm.editing = false;
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.registerworkingshift.listDetails = vm.listDetails;
                    this.$services.put(`timesheet/register-working-shift/${this.registerWorkingShiftId}`, this.registerworkingshift).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            disabledBeforeToday(date) {
                const dateTo = new Date();
                dateTo.setHours(0, 0, 0, 0);
                return date < dateTo
            },
            remove(item, index, button) {
                this.listDetails.splice(index, 1);
            },
            loadListPersonByDept(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                    vm.registerworkingshift.personId = null;
                })
            },
            loadListPersonByDeptFirst(deptId) {
                var vm = this;
                if (deptId == null) return;
                return this.$services.get(`lookup/persons-by-dept/${deptId}`).done(lstperson => {
                    vm.lstperson = lstperson;
                })
            },
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
