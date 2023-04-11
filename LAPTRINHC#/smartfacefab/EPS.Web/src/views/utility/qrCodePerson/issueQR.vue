<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("QRCodePerson.Issue.Header")}}</strong>
                    </div>
                    <b-form>
                        <b-row>
                            <b-col>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label="this.$t('QRCodePerson.Issue.Form.DateFrom')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" disabled style="display: none;"
                                                          v-model="$v.qrCode.dateFrom.$model"
                                                          :state="$v.qrCode.dateFrom.$dirty? !$v.qrCode.dateFrom.$error : null">
                                            </b-form-input>
                                            <date-picker type="datetime" v-if="updateForm || createForm"
                                                         style="width: 100%;" v-model="$v.qrCode.dateFrom.$model"
                                                         :state="$v.qrCode.dateFrom.$dirty? !$v.qrCode.dateFrom.$error : null"
                                                         valueType="YYYY-MM-DDTHH:mm:ss" format="DD-MM-YYYY HH:mm:ss">
                                            </date-picker>
                                            <b-form-invalid-feedback>
                                                {{$t("QRCodePerson.Issue.Form.DateRequire")}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!updateForm && !createForm">{{ qrCode.dateFromStr }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label="this.$t('QRCodePerson.Issue.Form.DateTo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" disabled style="display: none;"
                                                          v-model="$v.qrCode.dateTo.$model"
                                                          :state="$v.qrCode.dateTo.$dirty? !$v.qrCode.dateTo.$error : null">
                                            </b-form-input>
                                            <date-picker type="datetime" v-if="updateForm || createForm"
                                                         style="width: 100%;" v-model="$v.qrCode.dateTo.$model"
                                                         :state="$v.qrCode.dateTo.$dirty? !$v.qrCode.dateTo.$error : null"
                                                         valueType="YYYY-MM-DDTHH:mm:ss" format="DD-MM-YYYY HH:mm:ss">
                                            </date-picker>
                                            <b-form-invalid-feedback v-if="!$v.qrCode.dateTo.required">
                                                {{$t("QRCodePerson.Issue.Form.DateRequire")}}
                                            </b-form-invalid-feedback>
                                            <b-form-invalid-feedback v-if="!$v.qrCode.dateTo.greaterFrom">
                                                {{$t("QRCodePerson.Issue.Form.DateGreaterFrom")}}
                                            </b-form-invalid-feedback>
                                            <b-form-invalid-feedback v-if="!$v.qrCode.dateTo.greaterCurrent">
                                                {{$t("QRCodePerson.Issue.Form.DateGreaterCurrent")}}
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!updateForm && !createForm">{{ qrCode.dateToStr }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row v-if="!updateForm && !createForm">
                                    <b-col offset="3">
                                        <b-button type="button" style="width: 120px; background-color: #207dd9 !important; color: white; " variant="outline-primary" @click="updateBtn" v-if="authorize(['ManageIssueCodeQR'])">{{$t("QRCodePerson.Issue.Button.Update")}}</b-button>
                                        <b-button type="button" style="width: 120px; background-color: #207dd9; color: white; " variant="outline-primary" @click="createBtn" v-if="authorize(['ManageIssueCodeQR'])">{{$t("QRCodePerson.Issue.Button.Create")}}</b-button>
                                        <b-button type="button" style="width: 120px;" variant="success" @click="openQR">QR Code</b-button>
                                    </b-col>
                                </b-row>
                                <b-row v-if="updateForm">
                                    <b-col offset="3">
                                        <b-button type="button" style="width: 120px; background-color: #207dd9 !important; color: white; " variant="outline-primary" @click="saveUpdate">{{$t("QRCodePerson.Issue.Button.Save")}}</b-button>
                                        <b-button type="button" style="width: 120px" variant="secondary" @click="cancel">{{$t("QRCodePerson.Issue.Button.Cancel")}}</b-button>
                                    </b-col>
                                </b-row>
                                <b-row v-if="createForm">
                                    <b-col offset="3">
                                        <b-button type="button" style="width: 120px; background-color: #207dd9 !important; color: white; " variant="outline-primary" @click="saveCreate">{{$t("QRCodePerson.Issue.Button.Save")}}</b-button>
                                        <b-button type="button" style="width: 120px" variant="secondary" @click="cancel">{{$t("QRCodePerson.Issue.Button.Cancel")}}</b-button>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <CModal :show.sync="qrcodeModal"
                                    :no-close-on-backdrop="true"
                                    :title="$t('Guess.List.Modal.QRcode')"
                                    size="lg"
                                    color="dark">
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-row>
                                            <template>
                                                <div style="text-align: center;" id="printMe">
                                                    <h1 class="pt-1">{{ titleQR }}</h1>
                                                    <vue-qrcode :value="qrcodeValue" :width="350" />
                                                </div>
                                            </template>
                                        </b-row>
                                        <b-row style="display: none">
                                            <b-col style="margin-top: 10px;">
                                                <b-form-group :label="this.$t('QRCodePerson.Issue.Form.Link')"
                                                              :label-cols="4"
                                                              :horizontal="true"
                                                              label-align-md="left">
                                                    <b-form-input type="text" disabled
                                                                  v-model="qrcodeValue">
                                                    </b-form-input>
                                                </b-form-group>
                                            </b-col>
                                        </b-row>
                                    </b-col>
                                </b-row>
                                <template #header>
                                    <h6 class="modal-title">QR Code</h6>
                                    <CButtonClose @click="qrcodeModal = false" class="text-white" />
                                </template>
                                <template #footer v-if="!updateForm && !createForm">
                                    <b-button style="width: 150px !important" variant="primary" @click="copy(qrcodeValue)">{{$t("QRCodePerson.Issue.Button.Copy")}}</b-button>
                                    <b-button type="button" variant="primary" @click="download">{{$t("QRCodePerson.Issue.Button.Download")}}</b-button>
                                    <b-button type="button" v-print="printObj" variant="primary">{{$t("QRCodePerson.Issue.Button.Print")}}</b-button>
                                    <b-button type="button" style="width: 120px" variant="secondary" @click="closeQRCode">{{$t("QRCodePerson.Issue.Button.Cancel")}}</b-button>
                                </template>
                            </CModal>
                        </b-row>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators';
    import i18n from '@/libs/i18n'
    import VueQrcode from 'vue-qrcode'
    import Services from '@/utils/services'

    export default {
        name: 'QRCodePerson',
        mixins: [authorizationMixin],
        components: {
            VueQrcode,
        },
        data() {
            return {
                printObj: {
                    id: "printMe",
                    popTitle: ' ',
                    beforeOpenCallback(vue) { },
                    openCallback(vue) { },
                    closeCallback(vue) { }
                },
                qrCode: {
                    personId: null,
                    dateFrom: null,
                    dateTo: null,
                    imageUpdate: null,
                    status: null,
                },
                updateForm: false,
                createForm: false,
                qrcodeValue: 'https://www.google.com.vn/',
                current: null,
                compCreatorId: null,
                idQR: null,
                titleQR: this.$t('QRCodePerson.Issue.TitleQR'),
                qrcodeModal: false,
            }
        },
        validations: {
            qrCode: {
                dateFrom: { required },
                dateTo: {
                    required,
                    greaterFrom(value) {
                        if (this.qrCode.dateFrom > this.qrCode.dateTo) {
                            return false
                        }
                        else
                            return true;
                    },
                    greaterCurrent(value) {
                        if (this.current > this.qrCode.dateTo) {
                            return false
                        }
                        else
                            return true;
                    }
                },
            }
        },
        computed: {
            Id() {
                return this.$route.params.Id;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.compCreatorId = accessToken.comId;
            this.loadCurrentDateTime();
            await this.loadQRCodeUsing();
            this.viewQRCode();
        },
        methods: {
            loadCurrentDateTime() {
                var vm = this;
                return this.$services.get(`lookup/currentDateTime`).done(res => {
                    vm.current = res;
                });
            },
            openQR() {
                this.qrcodeModal = true;
            },
            closeQRCode() {
                this.qrcodeModal = false;
            },
            async loadQRCodeUsing() {
                var vm = this;
                return this.$services.get(`qrCodePerson/QRCode`).done(res => {
                    vm.qrCode = res;
                    vm.idQR = res.id;
                    vm.viewQRCode();
                });
            },
            async copy(s) {
                await navigator.clipboard.writeText(s);
                alert(i18n.t("Guess.List.Success"));
            },
            createBtn() {
                this.qrCode.dateFrom = null;
                this.qrCode.dateTo = null;
                this.createForm = true;
            },
            updateBtn() {
                if (new Date() > Date.parse(this.qrCode.dateTo)) {
                    alert(i18n.t("QRCodePerson.Issue.Alert.QRExpire"));
                }
                else if (this.qrCode.dateTo == null) {
                    alert(i18n.t("QRCodePerson.Issue.Alert.NoQR"));
                }
                else {
                    this.updateForm = true;
                }
            },
            saveCreate() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('qrCodePerson/QRCode', this.qrCode).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.cancel();
                        vm.loadQRCodeUsing();
                    });
                }
            },
            saveUpdate() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put('qrCodePerson/QRCode', this.qrCode).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                        vm.loadQRCodeUsing();
                    });
                }
            },
            cancel() {
                this.createForm = false;
                this.updateForm = false;
                this.loadQRCodeUsing();
            },
            download() {
                var a = document.createElement("a"); //Create <a>
                a.href = document.getElementById('printMe').getElementsByTagName('img')[0].currentSrc;
                a.download = "CodeQR.png"; //File name Here
                a.click();
            },
            viewQRCode() {
                this.qrcodeValue = window.location.protocol + '//' + window.location.host + '/#/searchPersonByCode/' + this.compCreatorId + '/' + this.idQR;
            },
        },
    }
</script>
<style scoped>
</style>
