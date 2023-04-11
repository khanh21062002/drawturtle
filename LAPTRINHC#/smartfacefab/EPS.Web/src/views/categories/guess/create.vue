<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Guess.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Phone")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input id="txt_phoneNumber" type="tel" v-model="$v.guess.phoneNumber.$model" :state="$v.guess.phoneNumber.$dirty? !$v.guess.phoneNumber.$error : null"></b-form-input>
                                        <b-form-invalid-feedback v-if="!$v.guess.phoneNumber.required">
                                            {{$t("Guess.Create.Form.PhoneRequire")}}
                                        </b-form-invalid-feedback>
                                        <b-form-invalid-feedback v-if="!$v.guess.phoneNumber.maxLength">
                                            {{$t("Guess.Create.Form.PhoneMaxLength")}}
                                        </b-form-invalid-feedback>
                                        <b-form-invalid-feedback v-if="!$v.guess.phoneNumber.minLength">
                                            {{$t("Guess.Create.Form.PhoneMinLength")}}
                                        </b-form-invalid-feedback>
                                        <b-form-invalid-feedback v-if="!$v.guess.phoneNumber.numeric">
                                            {{$t("Guess.Create.Form.PhoneNumeric")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Name")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input type="text" v-model="$v.guess.fullName.$model"
                                                      :state="$v.guess.fullName.$error ? false : null">
                                        </b-form-input>
                                        <b-form-invalid-feedback v-if="!$v.guess.fullName.required">
                                            {{$t("Guess.Create.Form.NameRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Identification")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input type="text" v-model="$v.guess.passport.$model"
                                                      :state="$v.guess.passport.$error ? false : null">
                                        </b-form-input>
                                        <b-form-invalid-feedback v-if="!$v.guess.passport.required">
                                            {{$t("Guess.Create.Form.IdentificationRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Birthday")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input id="txt_birthday" style="display: none;" v-model="$v.guess.birthday.$model" :state="$v.guess.birthday.$dirty? !$v.guess.birthday.$error : null"></b-form-input>
                                        <date-picker style="width: 100%;" type="date" v-model="guess.birthday" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                        <b-form-invalid-feedback v-if="!$v.guess.birthday.required">
                                            {{$t("Guess.Create.Form.BirthdayRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label">{{$t("Guess.Create.Form.Gender")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')"
                                                       v-model="guess.gender">
                                            <b-form-select-option value="1">{{$t("Guess.Create.Form.Male")}}</b-form-select-option>
                                            <b-form-select-option value="0">{{$t("Guess.Create.Form.Female")}}</b-form-select-option>
                                        </b-form-select>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label">Email:</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input id="txt_email" v-model="guess.email"></b-form-input>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Address")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input id="input-none" v-model="$v.guess.address.$model" :state="$v.guess.address.$dirty? !$v.guess.address.$error : null"></b-form-input>
                                        <b-form-invalid-feedback v-if="!$v.guess.address.required">
                                            {{$t("Guess.Create.Form.AddressRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Machine")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-select style="display: none;" :options="lstmachine" v-model="$v.guess.lstMachineId.$model" :state="$v.guess.lstMachineId.$dirty? !$v.guess.lstMachineId.$error : null">
                                        </b-form-select>
                                        <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstmachine" :settings="{ multiple: true }"
                                                 v-model="$v.guess.lstMachineId.$model" :state="$v.guess.lstMachineId.$dirty? !$v.guess.lstMachineId.$error : null">
                                        </select2>
                                        <b-form-invalid-feedback>
                                            {{$t("Guess.Create.Form.MachineRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <b-button type="button" variant="secondary" @click="findByPhone(guess.phoneNumber)"><i class="fa fa-search"></i></b-button>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.TimeIn")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input style="display:none;" v-model="$v.guess.startTime.$model" :state="$v.guess.startTime.$dirty? !$v.guess.startTime.$error : null"></b-form-input>
                                        <date-picker :disabled-date="disabledBeforeToday" style="width: 100%;" type="datetime" v-model="guess.startTime" id="ID" valueType="YYYY-MM-DD HH:mm" format="DD/MM/YYYY HH:mm"></date-picker>
                                        <b-form-invalid-feedback v-if="!$v.guess.startTime.required">
                                            {{$t("Guess.Create.Form.TimeInRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.TimeOut")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input style="display:none;" v-model="$v.guess.endTime.$model" :state="$v.guess.endTime.$dirty? !$v.guess.endTime.$error : null"></b-form-input>
                                        <date-picker style="width: 100%;" type="datetime" v-model="guess.endTime" id="ID" valueType="YYYY-MM-DD HH:mm" format="DD/MM/YYYY HH:mm"></date-picker>
                                        <b-form-invalid-feedback v-if="!$v.guess.endTime.required">
                                            {{$t("Guess.Create.Form.TimeOutRequire")}}
                                        </b-form-invalid-feedback>
                                        <b-form-invalid-feedback v-if="!$v.guess.endTime.isUnique">
                                            {{$t("Guess.Create.Form.TimeUnique")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Reason")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <b-form-input id="input-none" v-model="$v.guess.jobDuties.$model" :state="$v.guess.jobDuties.$dirty? !$v.guess.jobDuties.$error : null"></b-form-input>
                                        <b-form-invalid-feedback v-if="!$v.guess.jobDuties.required">
                                            {{$t("Guess.Create.Form.ReasonRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col sm="4">
                                        <label class="col-form-label required">{{$t("Guess.Create.Form.Image")}}</label>
                                    </b-col>
                                    <b-col sm="8">
                                        <button type="button" @click="takePhoto" class="btn btn-primary">{{$t("Guess.Create.Form.BtnTakePhoto")}}</button>
                                        <button type="button" @click="scanQRCode" class="btn btn-primary">{{$t("Guess.Create.Form.BtnScanQR")}}</button>
                                        <b-form-input style="display:none;" id="txtFullName" v-model="$v.guess.base64Image.$model" :state="$v.guess.base64Image.$dirty? !$v.guess.base64Image.$error : null"></b-form-input>
                                        <v-easy-camera v-if="acceptCamera" :stop="true" v-model="guess.base64Image" :autoDetectMobile="true" loading="true" :fullscreen="fullscreen" :fullscreenZIndex="9999" :mustApprove="true" @close="closeOrApproveEvent" @approve="closeOrApproveEvent"></v-easy-camera>
                                        <div :class="{ 'fullscreen': qrcodefullscreen }" :style="{ 'display': qrcodedisplay }" ref="wrapper">
                                            <qrcode-stream @decode="onDecode" :camera="qrcamera" :track="paintBoundingBox">
                                                <a class="close-button" role="button" @click="closeQRCode">x</a>
                                            </qrcode-stream>
                                        </div>
                                        <b-form-invalid-feedback v-if="!$v.guess.base64Image.required">
                                            {{$t("Guess.Create.Form.ImageRequire")}}
                                        </b-form-invalid-feedback>
                                    </b-col>
                                </b-row>
                                <b-row class="my-1">
                                    <b-col offset-sm="4" sm="8">
                                        <img class="img-fluid" style="min-width: 100%;" v-bind:src="guess.base64Image" />
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col sm="12">
                                <strong> <label class="col-form-label">{{$t("Guess.Create.Form.IsTravel")}}</label></strong>
                                &nbsp;<b-form-radio style="display: inline-block;" v-model="guess.isTravel" name="isTravel-radios" value="true">{{$t("Guess.Create.Form.Yes")}}</b-form-radio>
                                &nbsp;<b-form-radio style="display: inline-block;" v-model="guess.isTravel" name="isTravel-radios" value="false">{{$t("Guess.Create.Form.No")}}</b-form-radio>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col offset-sm="2" sm="10">
                                <b-form-textarea id="textarea"
                                                 v-model="guess.travelDec"
                                                 placeholder="..."
                                                 rows="3"
                                                 max-rows="6">
                                </b-form-textarea>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col sm="12">
                                <strong> <label class="col-form-label">{{$t("Guess.Create.Form.IsFever")}}</label></strong>
                                &nbsp;<b-form-radio style="display: inline-block;" v-model="guess.isFever" name="isFever-radios" value="true">{{$t("Guess.Create.Form.Yes")}}</b-form-radio>
                                &nbsp;<b-form-radio style="display: inline-block;" v-model="guess.isFever" name="isFever-radios" value="false">{{$t("Guess.Create.Form.No")}}</b-form-radio>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col offset-sm="2" sm="10">
                                <b-form-textarea id="textarea"
                                                 v-model="guess.feverDec"
                                                 placeholder="..."
                                                 rows="3"
                                                 max-rows="6">
                                </b-form-textarea>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col sm="12">
                                <strong> <label class="col-form-label">{{$t("Guess.Create.Form.IsTangent")}}</label></strong>
                            </b-col>
                        </b-row>
                        <b-row class="my-1">
                            <b-col offset-sm="2" sm="10">
                                <b-table-simple hover small caption-top responsive>
                                    <b-thead head-variant="dark">
                                        <b-tr>
                                            <b-th></b-th>
                                            <b-th>{{$t("Guess.Create.Form.Yes")}}</b-th>
                                            <b-th>{{$t("Guess.Create.Form.No")}}</b-th>
                                        </b-tr>
                                    </b-thead>
                                    <b-tbody>
                                        <b-tr>
                                            <b-td><label class="col-form-label">{{$t("Guess.Create.Form.CovidNN")}}</label></b-td>
                                            <b-td><b-form-radio style="display: inline-block;" v-model="guess.nCovid_NN" name="nCovid_NN-radios" value="true"></b-form-radio></b-td>
                                            <b-td><b-form-radio style="display: inline-block;" v-model="guess.nCovid_NN" name="nCovid_NN-radios" value="false"></b-form-radio></b-td>
                                        </b-tr>
                                        <b-tr>
                                            <b-td><label class="col-form-label">{{$t("Guess.Create.Form.CovidNCB")}}</label></b-td>
                                            <b-td><b-form-radio style="display: inline-block;" v-model="guess.nCovid_NCB" name="nCovid_NCB-radios" value="true"></b-form-radio></b-td>
                                            <b-td><b-form-radio style="display: inline-block;" v-model="guess.nCovid_NCB" name="nCovid_NCB-radios" value="false"></b-form-radio></b-td>
                                        </b-tr>
                                        <b-tr>
                                            <b-td><label class="col-form-label">{{$t("Guess.Create.Form.CovidBH")}}</label></b-td>
                                            <b-td><b-form-radio style="display: inline-block;" v-model="guess.nCovid_BH" name="nCovid_BH-radios" value="true"></b-form-radio></b-td>
                                            <b-td><b-form-radio style="display: inline-block;" v-model="guess.nCovid_BH" name="nCovid_BH-radios" value="false"></b-form-radio></b-td>
                                        </b-tr>
                                    </b-tbody>
                                </b-table-simple>
                            </b-col>
                        </b-row>
                        <br />
                        <div class="text-center">
                            <b-button type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import { required, numeric, maxLength, minLength } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'
    import moment from 'moment';
    import { QrcodeStream } from 'vue-qrcode-reader'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GuessCreate',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                guess: {
                    fullName: null,
                    passport: null,
                    birthday: null,
                    phoneNumber: null,
                    lstMachineId: null,
                    jobDuties: null,
                    startTime: null,
                    endTime: null,
                    address: null,
                    gender: 1,
                    isTravel: false,
                    travelDec: null,
                    isFever: false,
                    feverDec: null,
                    nCovid_NN: false,
                    nCovid_NCB: false,
                    nCovid_BH: false,
                    comId: null,
                    base64Image: null,
                    approve: 1// gán mặc định là đã duyệt
                },
                lstmachine: [],
                editing: false,
                approveValue: null,
                acceptCamera: false,//Hiện camera
                qrcodefullscreen: false,
                qrcamera: 'off',
                qrcodedisplay: 'none',
                //tracking qr code
                paintBoundingBox(detectedCodes, ctx) {
                    for (const detectedCode of detectedCodes) {
                        const { boundingBox: { x, y, width, height } } = detectedCode
                        ctx.lineWidth = 2
                        ctx.strokeStyle = '#007bff'
                        ctx.strokeRect(x, y, width, height)
                    }
                },
            }
        },
        validations: {
            guess: {
                lstMachineId: { required },
                fullName: { required },
                passport: { required },
                birthday: { required },
                phoneNumber: {
                    required,
                    numeric,
                    maxLength: maxLength(10),
                    minLength: minLength(10)
                },
                jobDuties: { required },
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
                address: { required },
                base64Image: { required }
            }
        },
        created() {
            var accessToken = Services.getAccessToken();
            this.guess.comId = accessToken.comId;
            this.guess.startTime = moment().format('YYYY-MM-DD HH:mm');//set giờ vào là giờ hiện tại
            this.guess.endTime = moment().format('YYYY-MM-DD 17:30');
            this.loadMachine();
        },
        methods: {
            //Quét QR code
            onDecode(decodedString) {
                var arrIden = decodedString.split('|');
                if (arrIden && arrIden.length === 7) {
                    this.guess.passport = arrIden[0];
                    this.guess.fullName = arrIden[2];
                    this.guess.birthday = arrIden[3].substr(4, 4) + '-' + arrIden[3].substr(2, 2) + '-' + arrIden[3].substr(0, 2);
                    if (arrIden[4] === 'Nam' || arrIden[4] === 'Male')
                        this.guess.gender = 1;
                    else
                        this.guess.gender = 0;
                    this.guess.address = arrIden[5];
                }
                this.qrcodefullscreen = false;
                this.qrcodedisplay = 'none';
            },
            //Nhấn nút quét QR code CCCD
            scanQRCode() {
                this.qrcodefullscreen = true;
                this.qrcodedisplay = 'block';
                this.qrcamera = 'auto';
            },
            //Đóng màn hình quét qr code
            closeQRCode() {
                this.qrcodefullscreen = false;
                this.qrcodedisplay = 'none';
            },
            //Approve ảnh tự sướng
            closeOrApproveEvent() {
                this.acceptCamera = false;
                this.fullscreen = false;
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
            takePhoto() {
                this.fullscreen = true;
                this.acceptCamera = true;
            },
            back() {
                this.$router.push({ path: '/guess/list' });
            },
            save() {
                this.$v.guess.$touch();
                if (!this.$v.guess.$invalid) {
                    var vm = this;
                    this.$services.post('guess', this.guess).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.RegisterSuccess"));
                        vm.back();
                    });
                }
            },
            disabledBeforeToday(date) {
                var today = new Date();
                today.setDate(today.getDate() - 1);
                return date < today;
            },
            findByPhone(value) {
                var vm = this;
                if (value != null && value != "")
                    return this.$services.get(`guess/getInfo/${value}`).done(guess => {
                        vm.guess = guess;
                        vm.guess.startTime = moment().format('YYYY-MM-DD HH:mm');
                    });
                else {
                    alert(i18n.t("Guess.Create.Form.PhoneRequire"));
                }
            },
        },
        components: {
            QrcodeStream
        },
    }
</script>
<style scoped>
    .fullscreen {
        position: fixed;
        z-index: 1050;
        top: 0;
        bottom: 0;
        right: 0;
        left: 0;
    }

    .fullscreen-button {
        background-color: white;
        position: absolute;
        bottom: 0;
        right: 0;
        margin: 1rem;
    }

        .fullscreen-button img {
            width: 2rem;
        }

    .close-button {
        position: fixed;
        top: 5px;
        right: 15px;
        color: #fff;
        font-family: Roboto, Tahoma;
        font-size: 2.5rem;
        line-height: 40px;
        font-weight: 300 !important;
        z-index: 100;
    }
</style>
