<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.PreOrder.Crud.HeaderDetail") }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <div class="row" v-if="editing">
                            <div class="col-md-6 offset-3">
                                <div class="form-row form-group">
                                    <div class="col-4 bv-no-focus-ring col-form-label text-md-left required">
                                        {{ $t('Categories.PreOrder.List.Table.TimeOrder') }}
                                    </div>
                                    <date-picker type="date"
                                                 class="col-4"
                                                 :disabled-date="disabledBeforeToday"
                                                 v-model="preOrder.dateOrder"
                                                 valueType="YYYY-MM-DD"
                                                 format="DD-MM-YYYY">
                                    </date-picker>
                                    <vue-timepicker type="datetime"
                                                    class="col-4"
                                                    input-width=100%
                                                    hide-clear-button
                                                    advanced-keyboard
                                                    v-model="preOrder.timeOrder"
                                                    valueType="HH:mm"
                                                    format="HH:mm">
                                    </vue-timepicker>
                                    <div class="col-4"></div>
                                    <div class="col-4">
                                        <b-form-input style="display: none"
                                                      v-model="$v.preOrder.dateOrder.$model"
                                                      :state="$v.preOrder.dateOrder.$dirty ? !$v.preOrder.dateOrder.$error : null">
                                        </b-form-input>
                                        <b-form-invalid-feedback>
                                            {{ $t("Categories.PreOrder.Crud.Form.Require.Date") }}
                                        </b-form-invalid-feedback>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="d-flex" v-if="!editing">
                            <b-form-group :label="this.$t('Categories.PreOrder.List.Table.TimeOrder')"
                                          :label-cols="6"
                                          :horizontal="true"
                                          style="margin-left: 25%; flex: 42%; padding-right: 15px; padding-left: 8px;"
                                          label-align-md="left">
                                <label class="col-form-label" >{{ preOrder.dateOrder | formatDate }}</label>
                            </b-form-group>
                            <div style="flex: 50%;">
                                <label class="col-form-label" >{{ preOrder.timeOrder | formatTimeHour }}</label>
                            </div>
                        </div>
                        <!--<b-row>
        <b-col md="6" offset="3">
            <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.Date')"
                          :label-cols="4"
                          :horizontal="true"
                          label-align-md="left"
                          :label-class="editing ? 'required' : ''">
                <b-form-input style="display:none"
                              v-model="$v.preOrder.dateOrder.$model"
                              :state="$v.preOrder.dateOrder.$dirty? !$v.preOrder.dateOrder.$error : null">
                </b-form-input>
                <date-picker type="date"
                             :disabled-date="disabledBeforeToday"
                             v-if="editing"
                             style="width: 100%;"
                             v-model="preOrder.dateOrder"
                             valueType="YYYY-MM-DD"
                             format="DD-MM-YYYY">
                </date-picker>
                <label class="col-form-label" v-if="!editing">{{ preOrder.dateOrder | formatDate }}</label>
                <b-form-invalid-feedback v-if="!$v.preOrder.timeOrder.required">
                    {{ $t("Categories.PreOrder.Crud.Form.Require.Date") }}
                </b-form-invalid-feedback>
            </b-form-group>
        </b-col>
    </b-row>-->
                        <!--<b-row>
        <b-col md="6" offset="3">
            <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.Time')"
                          :label-cols="4"
                          :horizontal="true"
                          label-align-md="left"
                          :label-class="editing ? 'required' : ''">
                <b-form-input style="display:none"
                              v-model="$v.preOrder.timeOrder.$model"
                              :state="$v.preOrder.timeOrder.$dirty? !$v.preOrder.timeOrder.$error : null">
                </b-form-input>
                <vue-timepicker type="datetime"
                                v-if="editing"
                                style="width: 100%;"
                                hide-clear-button
                                v-model="preOrder.timeOrder"
                                valueType="HH:mm"
                                format="HH:mm">
                </vue-timepicker>
                <label class="col-form-label" v-if="!editing">{{ preOrder.timeOrder | formatTimeHour }}</label>
                <b-form-invalid-feedback v-if="!$v.preOrder.timeOrder.required">
                    {{ $t("Categories.PreOrder.Crud.Form.Require.Date") }}
                </b-form-invalid-feedback>
            </b-form-group>
        </b-col>
    </b-row>-->
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.Guest')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  style="display:none"
                                                  v-model="$v.preOrder.idGuest.$model"
                                                  :state="$v.preOrder.idGuest.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="listGuest"
                                             :settings="{allowClear: true}"
                                             :state="$v.preOrder.idGuest.$dirty ? !$v.preOrder.idGuest.$error : null"
                                             v-if="editing"
                                             v-model="$v.preOrder.idGuest.$model">
                                    </select2>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.idGuest.required">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.Guest") }}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ preOrder.nameGuest }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.PhoneNumber')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.preOrder.phoneNumber.$model"
                                                  :state="$v.preOrder.phoneNumber.$dirty ? !$v.preOrder.phoneNumber.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.phoneNumber.numeric">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.VehicleLoadPositive") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.phoneNumber.maxLength">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.Max") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.phoneNumber.minLength">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.Min") }}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ preOrder.phoneNumber }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col>
                                <b-button type="button"
                                          v-if="editing"
                                          variant="secondary"
                                          @click="findByPhone(preOrder.phoneNumber)">
                                    <i class="fa fa-search"></i>
                                </b-button>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.AmountPeople')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="number"
                                                  v-model="$v.preOrder.amountPeople.$model"
                                                  v-if="editing"
                                                  :state="$v.preOrder.amountPeople.$dirty ? !$v.preOrder.amountPeople.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.amountPeople.required">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.AmountPeople") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if=" !$v.preOrder.amountPeople.numeric && $v.preOrder.amountPeople.required ">
                                        {{  $t('Categories.PreOrder.Crud.Form.Require.VehicleLoadPositive' )}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ preOrder.amountPeople }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.RequestOrder')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-textarea type="text"
                                                     v-if="editing"
                                                     v-model="preOrder.requestOrder">
                                    </b-form-textarea>
                                    <label class="col-form-label" v-if="!editing">{{ preOrder.requestOrder }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.AnotherOrder')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-textarea type="text"
                                                     v-if="editing"
                                                     v-model="preOrder.anotherOrder">
                                    </b-form-textarea>
                                    <label class="col-form-label" v-if="!editing">{{ preOrder.anotherOrder }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.DownPayment')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="number"
                                                  v-if="editing"
                                                  v-model="$v.preOrder.downPayment.$model"
                                                  :state="$v.preOrder.downPayment.$dirty ? !$v.preOrder.downPayment.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.downPayment.numeric">
                                        {{  $t('Categories.PreOrder.Crud.Form.Require.VehicleLoadPositive' )}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ preOrder.downPaymentStr }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button type="button"
                                              variant="primary"
                                              @click="edit"
                                              v-if="!editing && authorize(['ManagePreOrder'])">
                                        <i class="fa fa-pencil-square-o"></i> {{ $t("Button.Edit") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="back"
                                              v-if="!editing"
                                              style="width: 120px">
                                        <i class="fa fa-ban"></i> {{ $t("Button.Back") }}
                                    </b-button>
                                    <b-button type="submit"
                                              variant="primary"
                                              v-if="editing">
                                        <i class="fa fa-floppy-o"></i> {{ $t("Button.Submit") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel"
                                              style="width: 120px"
                                              v-if="editing">
                                        <i class="fa fa-ban"></i> {{ $t("Button.Cancel") }}
                                    </b-button>
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
    import { validationMixin } from "vuelidate";
    import { authorizationMixin } from "@/shared/mixins";
    import { required, numeric, maxLength, minLength } from 'vuelidate/lib/validators'
    import Services from "@/utils/services";
    import i18n from "@/libs/i18n";
    import moment from 'moment'

    export default {
        name: "PreOrderDetail",
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                preOrder: {
                    dateOrder: null,
                    timeOrder: null,
                    idGuest: null,
                    phoneNumber: null,
                    amountPeople: null,
                    requestOrder: null,
                    anotherOrder: null,
                    downPayment: 0,
                    compId: null,
                    isDelete: false,
                },
                editing: false,
                listGuest: [],
            };
        },
        validations: {
            preOrder: {
                timeOrder: { required },
                dateOrder: { required },
                idGuest: { required },
                amountPeople: {
                    required,
                    numeric,
                },
                phoneNumber: {
                    numeric,
                    maxLength: maxLength(11),
                    minLength: minLength(10)
                },
                downPayment: { numeric },
            },
        },
        computed: {
            preOrderId() {
                return this.$route.params.preOrderId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.preOrder.compId = accessToken.comId;
            await this.loadPreOrderDetail();
            this.loadGuest();
        },
        methods: {
            async loadPreOrderDetail() {
                return this.$services.get(`preOrder/${this.preOrderId}`).done((preOrder) => {
                    this.preOrder = preOrder;
                });
            },
            formatPhone(e) {
                return String(e).substring(0, 10);
            },
            findByPhone(val) {
                if (val != null && val != "") {
                    return this.$services.get(`preOrder/getInfoByPhone/${val}`).done(res => {
                        if (res.personId != '00000000-0000-0000-0000-000000000000' && res.personId != null) {
                            this.preOrder.idGuest = res.personId;
                        } else {
                            this.preOrder.idGuest = null;
                        }
                    });
                }
                else {
                    alert(i18n.t("Guess.Create.Form.PhoneRequire"));
                }
            },
            disabledBeforeToday(date) {
                var today = new Date();
                today.setDate(today.getDate() - 1);
                return date < today;
            },
            loadGuest() {
                this.$services.get(`lookup/guest`).done(res => {
                    this.listGuest = res;
                });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    let time = moment(this.preOrder.dateOrder).format("YYYY-MM-DD").toString() + ' ' + this.preOrder.timeOrder.toString();
                    this.preOrder.timeOrder = moment(time).format("YYYY-MM-DD HH:mm");
                    if (this.preOrder.downPayment == "" || this.preOrder.downPayment == null) {
                        this.preOrder.downPayment = 0;
                    }
                    this.$services.put(`preOrder/${this.preOrderId}`, this.preOrder).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            edit() {
                this.editing = true;
            },
            cancel() {
                this.loadPreOrderDetail();
                this.editing = false;
            },
            back() {
                this.$router.push({ path: "/categories/preOrder/list" });
            },
        },
    };
</script>
<style scoped></style>
