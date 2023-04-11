<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Guess.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Name")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.fullName }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Identification")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.passport }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Birthday")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.birthday | formatDate }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Phone")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.phoneNumber }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Gender")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label" v-if="guess.gender === 0">{{$t("Guess.Detail.Form.Male")}}</label>
                                        <label class="col-form-label" v-if="guess.gender === 1">{{$t("Guess.Detail.Form.Female")}}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">Email:</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.email }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Address")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.address }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Status")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <span data-v-5f7284b9="" class="badge badge-secondary" v-if="guess.approve == 0">{{$t("Guess.Detail.Form.NotApproved")}}</span>
                                        <span data-v-5f7284b9="" class="badge badge-success" v-if="guess.approve == 1">{{$t("Guess.Detail.Form.Approved")}}</span>
                                        <span data-v-5f7284b9="" class="badge badge-danger" v-if="guess.approve == 2">{{$t("Guess.Detail.Form.Refuse")}}</span>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label :class="editing? 'col-form-label required' : 'col-form-label'">{{$t("Guess.Detail.Form.Machine")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-select style="display: none;" v-if="editing" :options="lstmachine" v-model="$v.guess.lstMachineId.$model" :state="$v.guess.lstMachineId.$dirty? !$v.guess.lstMachineId.$error : null">
                                        </b-form-select>
                                        <select2 v-if="editing" :placeholder="this.$t('PlaceHolder.Select')" :options="lstmachine" :settings="{ multiple: true }"
                                                 v-model="$v.guess.lstMachineId.$model" :state="$v.guess.lstMachineId.$dirty? !$v.guess.lstMachineId.$error : null">
                                        </select2>
                                        <b-form-invalid-feedback>
                                            {{$t("Guess.Detail.Form.MachineRequire")}}
                                        </b-form-invalid-feedback>
                                        <label class="col-form-label" v-if="!editing">{{ guess.strMachine }}</label>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.TimeIn")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input type="text" style="display:none"
                                                      v-model="$v.guess.startTime.$model"
                                                      :state="$v.guess.startTime.$dirty? !$v.guess.startTime.$error : null">
                                        </b-form-input>
                                        <date-picker v-if="editing" :disabled-date="disabledBeforeToday" style="width: 100%;" type="datetime" v-model="guess.startTime" id="ID" valueType="YYYY-MM-DD HH:mm" format="DD/MM/YYYY HH:mm"></date-picker>
                                        <label class="col-form-label" v-if="!editing">{{ guess.startTime | formatDateTime}}</label>
                                        <b-form-invalid-feedback v-if="!$v.guess.startTime.required">
                                            {{$t("Guess.Detail.Form.TimeInRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.TimeOut")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input type="text" style="display:none"
                                                      v-model="$v.guess.endTime.$model"
                                                      :state="$v.guess.endTime.$dirty? !$v.guess.endTime.$error : null">
                                        </b-form-input>
                                        <date-picker v-if="editing" style="width: 100%;" type="datetime" v-model="guess.endTime" id="ID" valueType="YYYY-MM-DD HH:mm" format="DD/MM/YYYY HH:mm"></date-picker>
                                        <label class="col-form-label" v-if="!editing">{{ guess.endTime | formatDateTime}}</label>
                                        <b-form-invalid-feedback v-if="!$v.guess.endTime.required">
                                            {{$t("Guess.Detail.Form.TimeOutRequire")}}
                                        </b-form-invalid-feedback>
                                        <b-form-invalid-feedback v-if="!$v.guess.endTime.isUnique">
                                            {{$t("Guess.Detail.Form.TimeUnique")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Detail.Form.Reason")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <label class="col-form-label">{{ guess.jobDuties }}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col sm="5">
                                        <img v-bind:src="guess.imageUrl" class="img-fluid" alt="No Image">
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col sm="12">
                                <strong> <label class="col-form-label">{{$t("Guess.Detail.Form.IsTravel")}}</label></strong>
                                &nbsp;<b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.isTravel" name="isTravel-radios" value="true">{{$t("Guess.Detail.Form.Yes")}}</b-form-radio>
                                &nbsp;<b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.isTravel" name="isTravel-radios" value="false">{{$t("Guess.Detail.Form.No")}}</b-form-radio>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col offset-sm="2" sm="10">
                                <b-form-textarea id="textarea"
                                                 :disabled="true"
                                                 v-model="guess.travelDec"
                                                 placeholder="..."
                                                 rows="3"
                                                 max-rows="6">
                                </b-form-textarea>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col sm="12">
                                <strong> <label class="col-form-label">{{$t("Guess.Detail.Form.IsFever")}}</label></strong>
                                &nbsp;<b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.isFever" name="isFever-radios" value="true">{{$t("Guess.Detail.Form.Yes")}}</b-form-radio>
                                &nbsp;<b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.isFever" name="isFever-radios" value="false">{{$t("Guess.Detail.Form.No")}}</b-form-radio>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col offset-sm="2" sm="10">
                                <b-form-textarea id="textarea"
                                                 :disabled="true"
                                                 v-model="guess.feverDec"
                                                 placeholder="..."
                                                 rows="3"
                                                 max-rows="6">
                                </b-form-textarea>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col sm="12">
                                <strong> <label class="col-form-label">{{$t("Guess.Detail.Form.IsTangent")}}</label></strong>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col offset-sm="2" sm="10">
                                <b-table-simple hover small caption-top responsive>
                                    <b-thead head-variant="dark">
                                        <b-tr>
                                            <b-th></b-th>
                                            <b-th>{{$t("Guess.Detail.Form.Yes")}}</b-th>
                                            <b-th>{{$t("Guess.Detail.Form.No")}}</b-th>
                                        </b-tr>
                                    </b-thead>
                                    <b-tbody>
                                        <b-tr>
                                            <b-td><label class="col-form-label">{{$t("Guess.Detail.Form.CovidNN")}}</label></b-td>
                                            <b-td><b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.nCovid_NN" name="nCovid_NN-radios" value="true"></b-form-radio></b-td>
                                            <b-td><b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.nCovid_NN" name="nCovid_NN-radios" value="false"></b-form-radio></b-td>
                                        </b-tr>
                                        <b-tr>
                                            <b-td><label class="col-form-label">{{$t("Guess.Detail.Form.CovidNCB")}}</label></b-td>
                                            <b-td><b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.nCovid_NCB" name="nCovid_NCB-radios" value="true"></b-form-radio></b-td>
                                            <b-td><b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.nCovid_NCB" name="nCovid_NCB-radios" value="false"></b-form-radio></b-td>
                                        </b-tr>
                                        <b-tr>
                                            <b-td><label class="col-form-label">{{$t("Guess.Detail.Form.CovidBH")}}</label></b-td>
                                            <b-td><b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.nCovid_BH" name="nCovid_BH-radios" value="true"></b-form-radio></b-td>
                                            <b-td><b-form-radio :disabled="true" style="display: inline-block;" v-model="guess.nCovid_BH" name="nCovid_BH-radios" value="false"></b-form-radio></b-td>
                                        </b-tr>
                                    </b-tbody>
                                </b-table-simple>
                            </b-col>
                        </b-row>
                        <br />
                        <div class="text-center">
                            <b-button type="button" variant="success" @click="guessApprove" v-if="guess.approve == 0 && editing && authorize(['ManageGuess'])"><i class="fa fa-gavel"></i> {{$t("Button.Approve")}}</b-button>
                            <b-button type="button" variant="danger" @click="guessBlock" v-if="guess.approve == 0 && editing && authorize(['ManageGuess'])"><i class="fa fa-hand-paper-o"></i> {{$t("Button.Refuse")}}</b-button>
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageGuess'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing && guess.approve == 1"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
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
    import moment from 'moment'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GuessDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                guess: {
                    fullName: null,
                    passport: null,
                    birthday: null,
                    phoneNumber: null,
                    lstMachineId: null,
                    approve: 0,
                    startTime: null,
                    endTime: null,
                },
                lstmachine: [],
                editing: false,
                approveValue: null
            }
        },
        validations: {
            guess: {
                lstMachineId: { required },
                startTime: { required },
                endTime: {
                    required,
                    isUnique(value) {
                        if (this.guess.startTime > this.guess.endTime) {
                            return false
                        }
                        else
                            return true;
                    }
                },
            }
        },
        computed: {
            guessId() {
                return this.$route.params.guessId;
            },
        },
        created() {
            this.loadMachine();
            this.loadGuessDetail();
        },
        methods: {
            loadGuessDetail() {
                var vm = this;
                return this.$services.get(`guess/${this.guessId}`).done(guess => {
                    vm.guess = guess;
                    vm.guess.startTime = moment(vm.guess.startTime).format('YYYY-MM-DD HH:mm');
                    vm.guess.endTime = moment(vm.guess.endTime).format('YYYY-MM-DD HH:mm');
                });
            },
            loadMachine() {
                var vm = this;
                return this.$services.get(`lookup/machines`).done(lstmachine => {
                    vm.lstmachine = lstmachine;
                })
            },
            edit() {
                this.editing = true;
            },
            cancel() {
                var vm = this;
                this.loadGuessDetail().done(() => vm.editing = false);
            },
            back() {
                this.$router.push({ path: '/guess/list' });
            },
            guessApprove() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    if (confirm(i18n.t("Messages.Approve.Confirm"))) {
                        var vm = this;
                        this.guess.approve = 1;
                        this.$services.put(`guess/${this.guessId}`, this.guess).done(() => {
                            vm.$toastr.s(i18n.t("Messages.Approve.Success"));
                            vm.cancel();
                        });
                    }
                }
            },
            guessBlock() {
                if (confirm(i18n.t("Messages.Refuse.Confirm"))) {
                    var vm = this;
                    this.approveValue = 2;
                    this.$services.put(`guess/${this.guessId}/approve${this.approveValue}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.Refuse.Success"));
                        //Set trạng thái về từ chối
                        vm.guess.approve = 2;
                        vm.cancel();
                    });
                }
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`guess/${this.guessId}`, this.guess).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            disabledBeforeToday(date) {
                var today = new Date();
                today.setDate(today.getDate() - 1);
                return date < today;
            },
        }
    }
</script>
<style scoped>
</style>
