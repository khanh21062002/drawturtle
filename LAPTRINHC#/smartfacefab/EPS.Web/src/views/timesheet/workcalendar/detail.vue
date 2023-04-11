<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.WorkCalendar.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save" @submit="$v.$touch()">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Detail.Form.Company')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label class="col-form-label">{{ workcalendar.compName }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Detail.Form.CalendarName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" v-if="editing"
                                                          v-model="$v.workcalendar.name.$model"
                                                          :state="$v.workcalendar.name.$error ? false : null">
                                            </b-form-input>
                                            <label class="col-form-label" v-if="!editing">{{ workcalendar.name }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkCalendar.Detail.Form.CalendarNameRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Detail.Form.DateFrom')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workcalendar.dateFrom.$model"
                                                          :state="$v.workcalendar.dateFrom.$error ? false : null">
                                            </b-form-input>
                                            <date-picker v-if="editing" style="width: 100%;" type="date"
                                                         v-model="workcalendar.dateFrom"
                                                         id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                            </date-picker>
                                            <label class="col-form-label" v-if="!editing">{{ workcalendar.dateFromFormat }}</label>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkCalendar.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Detail.Form.DateTo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workcalendar.dateTo.$model"
                                                          :state="$v.workcalendar.dateTo.$error ? false : null">
                                            </b-form-input>
                                            <label class="col-form-label" v-if="!editing">{{ workcalendar.dateToFormat }}</label>
                                            <date-picker v-if="editing" style="width: 100%;" type="date" v-model="workcalendar.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback v-if="!$v.workcalendar.dateTo.required">
                                                {{$t('Timesheet.WorkCalendar.Detail.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                            <b-form-invalid-feedback v-if="!$v.workcalendar.dateTo.greaterFrom">
                                                {{$t('Timesheet.WorkCalendar.Detail.Form.DateToRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="7">
                                <h2>{{$t("Timesheet.WorkCalendar.Detail.Table.Header")}}</h2>
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
                                    <template v-slot:cell(isWorkDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.isWorkDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(isBreakDay)="row">
                                        <b-form-checkbox size="lg" :disabled="!editing"
                                                         v-model="row.item.isWorkDay"
                                                         value="0"
                                                         unchecked-value="1">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(number)="row">
                                        <b-form-input required v-if="editing" type="number" min="0" max="100" step="0.01" v-model="row.item.number" />
                                        <label class="col-form-label" v-if="!editing">{{ row.item.number }}</label>
                                    </template>
                                </b-table>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group >
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
                workcalendar: {
                    id: 0,
                    compId: null,
                    name: null,
                    dateFrom: null,
                    dateTo: null,
                    listDetails: [],
                },
                lstcompany: [],
                listDetails: [],
                detailsField: [
                    { key: 'index', label: this.$t('Timesheet.WorkCalendar.Detail.Table.Index'), sortable: false },
                    { key: 'name', label: this.$t('Timesheet.WorkCalendar.Detail.Table.DayOfWeek'), sortable: false },
                    { key: 'isWorkDay', label: this.$t('Timesheet.WorkCalendar.Detail.Table.Workday'), sortable: false, },
                    { key: 'isBreakDay', label: this.$t('Timesheet.WorkCalendar.Detail.Table.Breakday') },
                    { key: 'number', label: this.$t('Timesheet.WorkCalendar.Detail.Table.Coef') },
                ],
                editing: false,
                compareDate: false,
            }
        },
        validations: {
            workcalendar: {
                compId: { required },
                name: { required },
                dateFrom: { required },
                dateTo: {
                    required,
                    greaterFrom(value) {
                        if (this.workcalendar.dateFrom > this.workcalendar.dateTo) {
                            return false
                        }
                        else
                            return true;
                    }
                },
            }
        },
        computed: {
            workcalendarId() {
                return this.$route.params.workcalendarId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.workcalendar.compId = accessToken.comId;
            this.loadDetail();
            this.loadCompany();
            this.initDetails();
        },
        methods: {
            loadPerson() {
                var vm = this;
                return this.$services.get(`lookup/persons`).done(lstperson => {
                    vm.lstperson = lstperson;
                })
            },
            async loadDetail() {
                var vm = this;
                return this.$services.get(`timesheet/workcalendar/${this.workcalendarId}`).done(workcalendar => {
                    vm.workcalendar = workcalendar;
                    let dayFrom = new Date(this.workcalendar.dateFrom);
                    const currentDate = new Date();
                    currentDate.setHours(0, 0, 0, 0);
                    if (dayFrom >= currentDate)
                        this.compareDate = true;
                    else
                        this.compareDate = false;
                });
            },
            initDetails() {
                var vm = this;
                return this.$services.get(`timesheet/workcalendar/getListDetails/${this.workcalendarId}`).done(listDetails => {
                    //todo: get the english for this
                    vm.listDetails = listDetails;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;

                });
            },
            back() {
                this.$router.push({ path: '/timesheet/workcalendar/list' });
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
                    vm.workcalendar.listDetails = vm.listDetails;
                    this.$services.put(`timesheet/workcalendar/${this.workcalendarId}`, this.workcalendar).done(() => {
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
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
