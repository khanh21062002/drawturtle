<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.PreOrder.Crud.HeaderCreate") }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <div class="row">
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
                                                    class="col-4 display-time all-selected"
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
                                    <div class="col-4">
                                        <b-form-input style="display: none"
                                                      v-model="$v.preOrder.timeOrder.$model"
                                                      :state="$v.preOrder.timeOrder.$dirty ? !$v.preOrder.timeOrder.$error : null">
                                        </b-form-input>
                                        <b-form-invalid-feedback>
                                            {{ $t("Categories.PreOrder.Crud.Form.Require.Time") }}
                                        </b-form-invalid-feedback>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--<b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.PreOrder.Crud.Form.Date')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <date-picker style="width: 100%"
                                                 type="date"
                                                 :disabled-date="disabledBeforeToday"
                                                 v-model="preOrder.dateOrder"
                                                 valueType="YYYY-MM-DD"
                                                 format="DD-MM-YYYY">
                                    </date-picker>
                                    <b-form-input style="display: none"
                                                  v-model="$v.preOrder.dateOrder.$model"
                                                  :state="$v.preOrder.dateOrder.$dirty ? !$v.preOrder.dateOrder.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
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
                                              label-class="required">
                                    <vue-timepicker style="width: 100%"
                                                    type="datetime"
                                                    hide-clear-button
                                                    v-model="preOrder.timeOrder"
                                                    valueType="HH:mm"
                                                    format="HH:mm">
                                    </vue-timepicker>
                                    <b-form-input style="display: none"
                                                  v-model="$v.preOrder.timeOrder.$model"
                                                  :state="$v.preOrder.timeOrder.$dirty ? !$v.preOrder.timeOrder.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.Time") }}
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
                                              label-class="required">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             :options="listGuest"
                                             v-model="preOrder.idGuest">
                                    </select2>
                                    <b-form-input style="display: none"
                                                  v-model="$v.preOrder.idGuest.$model"
                                                  :state="$v.preOrder.idGuest.$dirty ? !$v.preOrder.idGuest.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.idGuest.required">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.Guest") }}
                                    </b-form-invalid-feedback>
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
                                </b-form-group>
                            </b-col>
                            <b-col>
                                <b-button type="button"
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
                                              label-class="required">
                                    <b-form-input type="number"
                                                  v-model="$v.preOrder.amountPeople.$model"
                                                  :state="$v.preOrder.amountPeople.$dirty ? !$v.preOrder.amountPeople.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.preOrder.amountPeople.required">
                                        {{ $t("Categories.PreOrder.Crud.Form.Require.AmountPeople") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if=" !$v.preOrder.amountPeople.numeric && $v.preOrder.amountPeople.required ">
                                        {{  $t('Categories.PreOrder.Crud.Form.Require.VehicleLoadPositive' )}}
                                    </b-form-invalid-feedback>
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
                                                     v-model="preOrder.requestOrder">
                                    </b-form-textarea>
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
                                                     v-model="preOrder.anotherOrder">
                                    </b-form-textarea>
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
                                                  v-model="$v.preOrder.downPayment.$model"
                                                  :state="$v.preOrder.downPayment.$dirty ? !$v.preOrder.downPayment.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if=" !$v.preOrder.downPayment.numeric">
                                        {{  $t('Categories.PreOrder.Crud.Form.Require.VehicleLoadPositive' )}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManagePreOrder'])"
                                              @click="save"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{ $t("Button.Submit") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel"
                                              style="width: 120px">
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
    import { validationMixin } from "vuelidate"
    import { authorizationMixin } from "@/shared/mixins"
    import { required, numeric, maxLength, minLength } from 'vuelidate/lib/validators'
    import Services from "@/utils/services"
    import i18n from "@/libs/i18n"
    import moment from 'moment'

    export default {
        name: "PreOrderCreate",
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                preOrder: {
                    dateOrder: null,
                    timeOrder: "00:00",
                    idGuest: null,
                    phoneNumber: null,
                    amountPeople: null,
                    requestOrder: null,
                    anotherOrder: null,
                    downPayment: null,
                    compId: null,
                    isDelete: false,
                },
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
                downPayment: {
                    numeric,
                },
                phoneNumber: {
                    numeric,
                    maxLength: maxLength(11),
                    minLength: minLength(10)
                },
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.preOrder.compId = accessToken.comId;
            this.preOrder.downPayment = 0;
            this.loadGuest();
        },
        methods: {
            loadGuest() {
                this.$services.get(`lookup/guest`).done(res => {
                    this.listGuest = res;
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
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    let a = this.preOrder.dateOrder.toString() + ' ' + this.preOrder.timeOrder.toString();//this.preOrder.timeOrder.HH.toString() + ":" + this.preOrder.timeOrder.mm.toString();
                    let b = moment(a).format("YYYY-MM-DD HH:mm");
                    this.preOrder.timeOrder = b;
                    this.$services.post("preOrder", this.preOrder).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: "/categories/preOrder/list" });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: "/categories/preOrder/list" });
            },
        },
    };
</script>

<style scoped>
</style>
