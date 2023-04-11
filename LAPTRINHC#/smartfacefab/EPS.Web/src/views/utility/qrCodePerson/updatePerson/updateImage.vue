<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("QRCodePerson.Update.Header")}}</strong>
                    </div>
                    <b-row v-if="checkValid">
                        <b-col>
                            <div>
                                <h1 style="color: red !important">{{$t("QRCodePerson.Find.Expire")}}</h1>
                            </div>
                        </b-col>
                    </b-row>
                    <b-form v-if="!checkValid">
                        <b-row offset="3">
                            <b-col md="6">
                                <b-button @click="takePhotoCam" style="margin-left: 5px;">{{$t("Guess.Create.Form.BtnTakePhoto")}}</b-button>
                                <v-easy-camera ref="refTakePhoto" v-if="acceptCamera" :stop="true" v-model="avatar" :autoDetectMobile="true" loading="true" :fullscreen="fullscreen" :fullscreenZIndex="9999" :mustApprove="true" @close="closeOrApproveEvent" @approve="closeOrApproveEvent"></v-easy-camera>
                                <div class="form-group">
                                    <b-row class="my-1">
                                        <b-col>
                                            <div class="d-flex justify-content-center"
                                                 style="margin-left: 5px; position: relative; width: 100%; height: 300px; border: 0.5px dotted">
                                                <img class="img-fluid" style="max-height: 100%; max-width: 100%" v-bind:src="avatar" />
                                            </div>
                                        </b-col>
                                    </b-row>
                                    <div style="width:100%; margin-left: 5px;">
                                        <b-form-file @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                        </b-form-file>
                                    </div>
                                    <b-form-group style="width:100%; margin-left: 5px;"
                                                  :label-cols="4"
                                                  :horizontal="true"
                                                  label-align-md="left">
                                        <b-form-input type="text" style="display: none;"
                                                      v-model="$v.person.fileData.$model"
                                                      :state="$v.person.fileData.$error ? false : null">
                                        </b-form-input>
                                        <b-form-invalid-feedback>
                                            {{$t('Categories.Person.Detail.Label.ImageRequire')}}
                                        </b-form-invalid-feedback>
                                    </b-form-group>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row offset="3">
                            <b-col md="6">
                                <b-form-group :label="this.$t('QRCodePerson.Update.Form.Note')"
                                              :label-cols="4"
                                              style="margin-left: 5px;"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-textarea type="text"
                                                     v-model="qrUpdate.note">
                                    </b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row offset="3">
                            <div id="captcha" class="col-md-3" style="width: auto; margin-left: 5px;"></div>
                            <div class="col-md-3" style="width:auto; float:right">
                                <b-button @click="createCaptcha"><i class="fa fa-refresh" aria-hidden="true"></i></b-button>
                            </div>
                        </b-row>
                        <b-row offset="3">
                            <b-col md="6" style="margin-left: 5px;">
                                <b-form-group :label="this.$t('QRCodePerson.Update.Form.Captcha')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.captchaRequire.$model"
                                                  :state="$v.captchaRequire.$error ? false : null">
                                    </b-form-input>
                                    <b-form-input v-model="captchaRequire" type="text" :placeholder="this.$t('PlaceHolder.Captcha')" id="captchaTextBox" />
                                    <b-form-invalid-feedback>
                                        {{$t('QRCodePerson.Update.Form.CaptchaRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row offset="3">
                            <b-col md="6" style="margin-left: 5px;">
                                <b-form-group>
                                    <b-button type="button" variant="primary" @click="save"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
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
<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
<script src="https://unpkg.com/vue-multiselect@2.1.0"></script>
<script defer src="https://use.fontawesome.com/releases/v5.3.1/js/all.js"></script>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'

    const toBase64 = file1 => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file1);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });

    export default {
        name: 'PersonDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                captchaRequire: null,
                code: null,
                checkValid: false,
                value: [],
                backgroundVaccine: '',
                textVaccine: '',
                qrUpdate: {
                    personId: null,
                    dateUpdate: null,
                    imageUpdate: null,
                    status: 1,
                    note: null,
                    fileData: null,
                },
                person: {
                    personId: null,
                    AvatarPath: null,
                    AvatarPath1: null,
                    file1: null,
                    fileData: null,
                    imageBase64: null,
                    file1: null,
                    listFace: [{
                        file1: null,
                        filedata: null,

                    }],
                    listFace1: [{
                        file1: null,
                        filedata: null,
                    }],
                    groupId: null,

                },
                listFace: [{
                    file1: null,
                    filedata: null
                }],
                listFace1: [{
                    file1: null,
                    filedata: null
                }],
                isCameraOpen: false,
                isPhotoTaken: false,
                isShotPhoto: false,
                isLoading: false,
                link: '#',
                personisCameraOpen: false,
                isAdmin: false,
                avatar: null,
                treeselect: {
                    value: null,
                    options: [],
                },
                acceptCamera: false,//Hiện camera
                now: null,
            }
        },
        validations: {
            person: {
                fileData: { required },
            },
            captchaRequire: { required },
        },
        mounted: function () {
            this.createCaptcha();
        },
        computed: {
            pid() {
                return this.$route.params.pid;
            },
            compId() {
                return this.$route.params.compId;
            },
            qrId() {
                return this.$route.params.qrId;
            },
        },
        created() {
            this.now = new Date();
            this.loadQRCodeUsing();
        },
        methods: {
            loadQRCodeUsing() {
                var current;
                this.$services.get(`lookup/currentDateTime`).done(res => {
                    current = res;
                });
                var vm = this;
                return this.$services.get(`qrCodePerson/detailQR/${this.qrId}`).done(res => {
                    if (current <= res.dateTo && current >= res.dateFrom && this.compId == res.compId) {
                        vm.checkValid = false;
                    } else {
                        vm.checkValid = true;
                    }
                });
            },
            save() {
                if (this.avatar != null) {
                    if (this.avatar.includes(';base64,') == true) {
                        var base64 = this.avatar.split(",");
                        this.person.fileData = base64[1];
                    }
                }
                this.turnOffCamera();
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    event.preventDefault();
                    if (document.getElementById("captchaTextBox").value == this.code) {
                        var vm = this;
                        this.qrUpdate.personId = this.pid;
                        this.qrUpdate.fileData = this.person.fileData;
                        this.qrUpdate.dateUpdate = [this.now.getFullYear(), this.now.getMonth() + 1, this.now.getDate()].join('-') + ' ' + [this.now.getHours(), this.now.getMinutes(), this.now.getSeconds()].join(':');
                        this.$services.post('qrCodePerson/updateImage', this.qrUpdate).done((id) => {
                            vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                            vm.$router.push({ path: '/searchPersonByCode/' + this.compId + '/' + this.qrId });
                            //window.location.reload();
                        });
                    } else {
                        alert(i18n.t("QRCodePerson.Update.Alert.Invalid"));
                    }
                }
            },
            cancel() {
                this.turnOffCamera();
                this.$router.push({ path: '/searchPersonByCode/' + this.compId + '/' + this.qrId });
            },
            takePhotoCam() {
                this.fullscreen = true;
                this.acceptCamera = true;
            },
            //Approve ảnh tự sướng
            closeOrApproveEvent() {
                this.acceptCamera = false;
                this.fullscreen = false;
                this.$refs.refTakePhoto.stop();
            },
            toggleCamera() {
                if (this.isCameraOpen) {
                    this.isCameraOpen = false;
                    this.isPhotoTaken = false;
                    this.isShotPhoto = false;
                    this.stopCameraStream();
                } else {
                    this.isCameraOpen = true;
                    this.createCameraElement();
                }
            },
            turnOffCamera() {
                if (this.isCameraOpen) {
                    this.isCameraOpen = false;
                    this.isPhotoTaken = false;
                    this.isShotPhoto = false;
                    this.stopCameraStream();
                }
            },
            createCameraElement() {
                this.isLoading = true;
                $("#avatar").hide();
                const constraints = (window.constraints = {
                    audio: false,
                    video: true
                });
                navigator.mediaDevices
                    .getUserMedia(constraints)
                    .then(stream => {
                        this.isLoading = false;
                        this.$refs.camera.srcObject = stream;
                    })
                    .catch(error => {
                        this.isLoading = false;
                        alert("May the browser didn't support or there is some errors.");
                    });
            },
            stopCameraStream() {
                let tracks = this.$refs.camera.srcObject.getTracks();
                tracks.forEach(track => {
                    track.stop();
                });
                $("#avatar").show();
            },
            takePhoto() {
                var vm = this;
                if (!this.isPhotoTaken) {
                    this.isShotPhoto = true;
                    const FLASH_TIMEOUT = 50;
                    setTimeout(() => {
                        this.isShotPhoto = false;
                    }, FLASH_TIMEOUT);
                }
                this.isPhotoTaken = !this.isPhotoTaken;

                const context = this.$refs.canvas.getContext('2d');
                context.drawImage(this.$refs.camera, 0, 0, 300, 225);
                var jpeg = document.getElementById("photoTaken").toDataURL("image/jpeg");
                vm.person.fileData = jpeg.split(';base64,')[1];
                vm.avatar = jpeg;
            },
            downloadImage() {
                const download = document.getElementById("downloadPhoto");
                const canvas = document.getElementById("photoTaken").toDataURL("image/jpeg")
                    .replace("image/jpeg", "image/octet-stream");
                download.setAttribute("href", canvas);
            },
            onChange: function (evt) {
                this.turnOffCamera();
                var vm = this;
                var f = evt.target.files[0]; // FileList object
                var acceptExtensions = ["jpg", "png", "PNG", "JPG", "jpeg", "JPEG"];
                let fileExtensions = (/[.]/.exec(f.name)) ? /[^.]+$/.exec(f.name) : undefined;

                if (fileExtensions && !acceptExtensions.includes(fileExtensions[0])) {
                    alert(i18n.t("Categories.Person.Detail.Label.Fileinput"));
                    return;
                }
                if (f.size > 5000000) {
                    alert(i18n.t("Timesheet.TimeKeeping.List.ImportExcel.FileInput50Mb"));
                    f.value = "";
                    return;
                };
                var reader = new FileReader();
                // Closure to capture the file information.
                reader.onload = (function (theFile) {
                    return function (e) {
                        var binaryData = e.target.result;
                        //Converting Binary Data to base 64
                        vm.person.fileData = window.btoa(binaryData);
                    };
                })(f);
                vm.avatar = URL.createObjectURL(f);;
                reader.readAsBinaryString(f, vm);
                vm.person.file1 = f.name;
            },
            setImage: function (output) {
                this.hasImage = true;
                this.image = output;
            },
            filesChange(e) {
                var yyy;
                var f = e.target.files[0];
                if (f) {
                    var r = new FileReader();
                    r.onload = function (e) {
                        yyy = e.target.result;
                        alert("name: " + f.name + "n"
                            + "type: " + f.type + "n"
                            + "size: " + f.size + " bytesn"
                            + "starts with: " + yyy
                        );
                    }
                    var xxx = r.readAsText(f);
                }
            },
            createCaptcha() {
                //clear the contents of captcha div first
                document.getElementById('captcha').innerHTML = "";
                var charsArray = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@!#$%^&*";
                var lengthOtp = 6;
                var captcha = [];
                for (var i = 0; i < lengthOtp; i++) {
                    //below code will not allow Repetition of Characters
                    var index = Math.floor(Math.random() * charsArray.length + 1); //get the next character from the array
                    if (captcha.indexOf(charsArray[index]) == -1)
                        captcha.push(charsArray[index]);
                    else i--;
                }
                var canv = document.createElement("canvas");
                canv.id = "captcha";
                canv.width = 100;
                canv.height = 50;
                var ctx = canv.getContext("2d");
                ctx.font = "25px Georgia";
                ctx.strokeText(captcha.join(""), 0, 30);
                //storing captcha so that can validate you can save it somewhere else according to your specific requirements
                this.code = captcha.join("");
                document.getElementById("captcha").appendChild(canv); // adds the canvas to the body element
            },
            validateCaptcha() {
                event.preventDefault();
                if (document.getElementById("captchaTextBox").value == this.code) {
                    alert("Valid Captcha")
                } else {
                    alert("Invalid Captcha. Try Again");
                    this.createCaptcha();
                }
            }
        }
    }
</script>
<style scoped>
    input[type=text] {
        padding: 12px 20px;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }

    canvas {
        /*prevent interaction with the canvas*/
        pointer-events: none;
    }
</style>
