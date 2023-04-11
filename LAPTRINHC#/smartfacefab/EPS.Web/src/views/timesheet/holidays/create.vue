<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Timesheet.Holidays.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="10" offset="1">
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.Holidays.Create.Form.Company')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="holidays.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.Holidays.Create.Form.Date')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" id="txt_name" style="display: none;"
                                                          v-model="$v.holidays.date.$model"
                                                          :state="$v.holidays.date.$dirty? !$v.holidays.date.$error : null">
                                            </b-form-input>
                                            <date-picker style="width: 100%;" v-model="holidays.date" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Holidays.Create.Form.DateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>

                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.Holidays.Create.Form.Description')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-textarea id="textarea"
                                                             v-model="holidays.description"
                                                             placeholder=""
                                                             rows="3"
                                                             max-rows="6"></b-form-textarea>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="8" offset="1">
                                        <b-form-group :label="this.$t('Timesheet.Holidays.Create.Form.Coefficient')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number" id="txt_coef" step="0.01"
                                                          v-model="holidays.coEf">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group>
                                            <b-button v-if="authorize(['ManageHolidays'])" type="submit" variant="primary">
                                                <i class="fa fa-floppy-o"></i>
                                                {{$t("Button.Submit")}}
                                            </b-button>
                                            <b-button type="button" variant="secondary" @click="cancel">
                                                <i class="fa fa-ban"></i>
                                                {{$t("Button.Cancel")}}
                                            </b-button>
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
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                holidays: {
                    id: 0,
                    compId: null,
                    year: null,
                    date: null,
                    description: null,
                    coEf: null,
                    isDelete: null,
                },
                lstcompany: [],
                isAdmin: false,
                isExist: false,
            }
        },
        validations: {
            holidays: {
                date: { required },
            }
        },
        computed: {
            year: function () {
                var vm = this;
                if (vm.holidays.date) {
                    let date = new Date(vm.holidays.date);
                    if (date) {
                        return date.getFullYear();
                    }
                }
                return "";
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.holidays.compId = accessToken.comId;
            await this.loadCompany();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            back() {
                this.$router.push({ path: '/timesheet/holidays/list' });
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.holidays.year = vm.year;
                    this.$services.post('timesheet/holidays', this.holidays).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/timesheet/holidays/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/timesheet/holidays/list' });
            },
        }
    }
</script>
<style scoped>
    div {
        width: 100%;
    }
</style>
