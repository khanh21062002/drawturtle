<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.WorkCalendar.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save" @submit="$v.$touch()">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Create.Form.Company')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="workcalendar.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Create.Form.CalendarName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text"
                                                          v-model="$v.workcalendar.name.$model"
                                                          :state="$v.workcalendar.name.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkCalendar.Create.Form.CalendarNameRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Create.Form.DateFrom')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workcalendar.dateFrom.$model"
                                                          :state="$v.workcalendar.dateFrom.$error ? false : null">
                                            </b-form-input>
                                            <date-picker style="width: 100%;" type="date"
                                                         v-model="workcalendar.dateFrom"
                                                         id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY">
                                            </date-picker>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.WorkCalendar.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.WorkCalendar.Create.Form.DateTo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.workcalendar.dateTo.$model"
                                                          :state="$v.workcalendar.dateTo.$error ? false : null">
                                            </b-form-input>
                                            <date-picker style="width: 100%;" type="date" v-model="workcalendar.dateTo" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback v-if="!$v.workcalendar.dateTo.required">
                                                {{$t('Timesheet.WorkCalendar.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                            <b-form-invalid-feedback v-if="!$v.workcalendar.dateTo.greaterFrom">
                                                {{$t('Timesheet.WorkCalendar.Create.Form.DateToRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <br />
                        <b-row>
                            <b-col md="7">
                                <h2>{{$t("Timesheet.WorkCalendar.Create.Table.Header")}}</h2>
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
                                        <b-form-checkbox size="lg"
                                                         v-model="row.item.isWorkDay"
                                                         value="1"
                                                         unchecked-value="0">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(isBreakDay)="row">
                                        <b-form-checkbox size="lg"
                                                         v-model="row.item.isWorkDay"
                                                         value="0"
                                                         unchecked-value="1">
                                        </b-form-checkbox>
                                    </template>
                                    <template v-slot:cell(number)="row">
                                        <b-form-input required type="number" min="0" max="100" step="0.01" v-model="row.item.number" />
                                    </template>
                                </b-table>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageWorkCalendar'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
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
                    { key: 'index', label: this.$t('Timesheet.WorkCalendar.Create.Table.Index'), sortable: false },
                    { key: 'name', label: this.$t('Timesheet.WorkCalendar.Create.Table.DayOfWeek'), sortable: false },
                    { key: 'isWorkDay', label: this.$t('Timesheet.WorkCalendar.Create.Table.Workday'), sortable: false, },
                    { key: 'isBreakDay', label: this.$t('Timesheet.WorkCalendar.Create.Table.Breakday') },
                    { key: 'number', label: this.$t('Timesheet.WorkCalendar.Create.Table.Coef') },
                ],
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
        computed: {},
        async created() {
            var accessToken = Services.getAccessToken();
            this.workcalendar.compId = accessToken.comId;
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
            initDetails() {
                var vm = this;
                for (let i = 0; i < 7; i++) {
                    let item = {};
                    item.isWorkDay = 1;
                    item.number = 1;
                    let name = "";
                    let dayOfWeek = i + 1;
                    switch (i) {
                        case 6:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Sun");
                            dayOfWeek = 0;
                            break;
                        case 0:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Mon");
                            break;
                        case 1:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Tue");
                            break;
                        case 2:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Wed");
                            break;
                        case 3:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Thu");
                            break;
                        case 4:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Fri");
                            break;
                        case 5:
                            // code block
                            name = i18n.t("Common.DayOfWeek.Sat");
                            break;
                        default:
                        // code block
                    }

                    item.dayOfWeek = dayOfWeek;
                    item.name = name;
                    vm.listDetails.push(item);
                }
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
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.workcalendar.listDetails = [];
                    vm.workcalendar.listDetails = vm.listDetails;
                    this.$services.post('timesheet/workcalendar', this.workcalendar).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/timesheet/workcalendar/list' });
                    });
                }
            },
            disabledBeforeToday(date) {
                const dateTo = new Date();
                dateTo.setHours(0, 0, 0, 0);
                return date < dateTo
            },
            cancel() {
                this.$router.push({ path: '/timesheet/workcalendar/list' });
            },
        }
    }
</script>

<style scoped>
    div {
        width: 100%;
    }
</style>
