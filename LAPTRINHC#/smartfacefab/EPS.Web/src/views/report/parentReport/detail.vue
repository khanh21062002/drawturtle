<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('Reporting.ParentReport.Detail.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="save">

                        <b-row>
                            <b-row>
                                <b-col md="10">
                                    <b-form-group label=""
                                                  :label-cols="4"
                                                  :horizontal="true"
                                                  label-align-md="left">
                                        <div class="form-group">
                                            <img v-bind:src="event.getFaceUrl"  alt="No Image" width="250" height="250">

                                        </div>
                                    </b-form-group>
                                </b-col>
                            </b-row>

                            <b-col md="8">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.ParentReport.List.SearchForm.PersonCode')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label">{{ event.personCode }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Parent.Detail.Label.Name')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                           
                                            <label class="col-form-label">{{ event.personName }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Day')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                           
                                            <label class="col-form-label">{{ event.accessDate }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Hours')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_accessTime"
                                                          v-model="event.accessTime">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.ErrorCode')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_errorCodeName"
                                                          v-model="event.errorCodeName">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.IdentificationPont')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_ScoreMatch"
                                                          v-model="event.scoreMatch">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Temp')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_Temperature"
                                                          v-model="event.temperature">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.FaceMask')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_WearMask"
                                                          v-model="event.wearMaskName">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Status')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_status"
                                                          v-model="event.statusName">
                                            </b-form-input>
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

    export default {
        name: 'EventDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                event: {
                    id: null,
                    projecT_ID: null,
                    buildinG_ID: null,
                    code: null,
                    name: null,
                    note: null,
                    status: null,
                    deleted: null,
                    statuS_NAME: null,
                    supId: null
                },
                editing: false
            }
        },
        computed: {
            eventId() {
                return this.$route.params.eventId;
            },
        },
        validations: {
            event: {
               
            }
        },
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
                this.$router.push({ path: '/system/event/list' });
            },
            cancel() {
                var vm = this;
                this.loadEventDetail().done(() => vm.editing = false);
            },


        }
    }
</script>
<style scoped>
</style>
