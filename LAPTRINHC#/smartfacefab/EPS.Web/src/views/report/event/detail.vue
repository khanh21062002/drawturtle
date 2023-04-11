<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('Reporting.Event.Detail.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="4">
                                <b-form-group label=""
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <div class="form-group">
                                        <img v-bind:src="event.facePath" alt="No Image" width="250" height="250">
                                    </div>
                                </b-form-group>
                            </b-col>
                            <b-col md="8">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Day')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="event.accessDateDayFormat">
                                            </b-form-input>
                                            <!--<label class="col-form-label">{{ event.accessDateDayFormat }}</label>-->
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.PersonName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="event.personNameOther">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.PersonType')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input :value="this.lstpersonType.filter((personType)=>
                                                event.personType == personType.id).map((element) => element.text).join(',')"
                                                :disabled="!editing"
                                                v-if="!editing">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Create.Form.PerCode')"
                                                      label-for="code"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="event.personCode">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Create.Form.AreaName')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="event.areaName">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                                      label-for="code"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input :value="this.lstGender.filter((gender)=>
                                                event.gender == gender.id).map((element) => element.text).join(',')"
                                                :disabled="!editing"
                                                v-if="!editing">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Birthday')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="event.birthdayStr">
                                            </b-form-input>
                                            <!--<label class="col-form-label">{{ event.birthdayStr }}</label>-->
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.Table.Deparment') + ':'"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="event.depName">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <div style="text-align: center">
                            <b-form-group>
                                <b-button type="button"
                                          style="width: 120px"
                                          variant="secondary"
                                          @click="back"
                                          v-if="!editing">
                                    <i class="fa fa-ban"></i> {{$t("Button.Back")}}
                                </b-button>
                            </b-form-group>
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

    export default {
        name: 'EventDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                event: {
                    id: null,
                    code: null,
                    name: null,
                    note: null,
                    status: null,
                    supId: null,
                    emloyeeOther: null
                },
                editing: false,
                lstpersonType: [
                    { id: '1', text: this.$t('Reporting.Event.List.Table.Employee') },
                    { id: '2', text: this.$t('Reporting.Event.List.Table.Unregistered') },
                    { id: '4', text: this.$t('Reporting.Event.List.Table.Suspect') },
                    { id: '5', text: this.$t('Reporting.Event.List.Table.Thief') },
                    { id: '6', text: this.$t('Reporting.Event.List.Table.Vandalize') },
                    { id: '7', text: this.$t('Reporting.Event.List.Table.Suspect') },
                ],
                lstGender: [
                    { id: '0', text: this.$t("Guess.Detail.Form.Male") },
                    { id: '1', text: this.$t("Guess.Detail.Form.Female") }
                ],
            }
        },
        computed: {
            eventId() {
                return this.$route.params.eventId;
            },
            fromHistory() {
                return this.$route.query.fromHistory;
            },
        },
        validations: {},
        created() {
            this.loadEventDetail();
        },
        methods: {
            loadEventDetail() {
                var vm = this;
                return this.$services.get(`event/${this.eventId}`).done(event => {
                    vm.event = event;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                if (this.fromHistory) {
                    this.$router.push({ path: '/report/event/list' });
                } else {
                    this.$router.push({ path: '/report/event/list' });
                }
            },
            cancel() {
                var vm = this;
                this.loadEventDetail().done(() => vm.editing = false);
            },
        }
    }
</script>
<style scoped>
    .form-control:disabled, .form-control[readonly] {
        background-color: #e2ebeae8;
        font-weight: bold;
        color: #47494a;
        opacity: 1;
    }

    .form-control {
        color: #444;
    }
</style>
