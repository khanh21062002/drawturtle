<template>
    <CContainer class="d-flex content-center min-vh-100">
        <CRow>
            <CCol>
                <CCardGroup>
                    <CCard class="p-4">
                        <CCardBody>
                            <b-card>
                                <div slot="header">
                                    <strong>{{$t("QRCodePerson.Find.Header")}}</strong>
                                </div>
                                <b-form>
                                    <b-row v-if="checkValid">
                                        <b-col>
                                            <div>
                                                <h1 style="color: red !important">{{$t("QRCodePerson.Find.Expire")}}</h1>
                                            </div>
                                        </b-col>
                                    </b-row>
                                    <b-row v-if="!checkValid">
                                        <b-col>
                                            <b-form-group :label="this.$t('QRCodePerson.Find.Form.Code')"
                                                          :label-cols="5"
                                                          :horizontal="true"
                                                          label-align-md="left"
                                                          label-class="required">
                                                <b-form-input type="text"
                                                              v-model.trim="qrCode.code">
                                                </b-form-input>
                                                <b-form-input type="text" disabled style="display: none;"
                                                              v-model="$v.qrCode.code.$model"
                                                              :state="$v.qrCode.code.$dirty? !$v.qrCode.code.$error : null">
                                                </b-form-input>
                                                <b-form-invalid-feedback>
                                                    {{$t("QRCodePerson.Find.Form.CodeRequire")}}
                                                </b-form-invalid-feedback>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>
                                    <b-row v-if="!checkValid">
                                        <b-col offset="3">
                                            <b-button type="button" variant="primary" @click="next">{{$t("QRCodePerson.Find.Button.Next")}}</b-button>
                                        </b-col>
                                    </b-row>
                                </b-form>
                                <CModal :show.sync="modalDetailPerson"
                                        :no-close-on-backdrop="true"
                                        :title="this.$t('QRCodePerson.Find.Modal.Title')"
                                        size="xl"
                                        color="dark">
                                    <b-form class="form-horizontal">
                                        <b-row>
                                            <b-col md="4">
                                                <b-row>
                                                    <b-col md="10">
                                                        <div class="form-group">
                                                            <b-button v-if="editing" @click="takePhotoCam" style="margin-left: -5px;">{{$t("Guess.Create.Form.BtnTakePhoto")}}</b-button>
                                                            <v-easy-camera ref="refTakePhoto" v-if="acceptCamera" :stop="true" v-model="avatar" :autoDetectMobile="true" loading="true" :fullscreen="fullscreen" :fullscreenZIndex="9999" :mustApprove="true" @close="closeOrApproveEvent" @approve="closeOrApproveEvent"></v-easy-camera>
                                                            <div class="form-group">
                                                                <b-row class="my-1">
                                                                    <b-col>
                                                                        <div class="d-flex justify-content-center"
                                                                             style="margin-left: -5px; position: relative; width: 300px; height: 300px; border: 0.1px dotted gray;">
                                                                            <img class="img-fluid" style="max-height: 100%; max-width: 100%" v-bind:src="avatar" />
                                                                            <img v-bind:src="person.file1" v-if="person.file1 != null && (!editing || avatar == null)" alt="No Image" style="max-height: 100%; max-width: 100%">
                                                                        </div>
                                                                    </b-col>
                                                                </b-row>
                                                                <div style="width:auto; margin-left: -5px;">
                                                                    <b-form-file v-if="editing" @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                                                    </b-form-file>
                                                                </div>
                                                                <b-form-group :label-cols="4"
                                                                              :horizontal="true"
                                                                              label-align-md="left">
                                                                    <b-form-input type="text" style="display: none;" v-if="editing"
                                                                                  v-model="$v.person.file1.$model"
                                                                                  :state="$v.person.file1.$error ? false : null">
                                                                    </b-form-input>
                                                                    <b-form-invalid-feedback>
                                                                        {{$t('Categories.Person.Detail.Label.ImageRequire')}}
                                                                    </b-form-invalid-feedback>
                                                                </b-form-group>
                                                            </div>
                                                        </div>
                                                    </b-col>
                                                </b-row>
                                            </b-col>
                                            <b-col md="8">
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left"
                                                                      label-class="required">
                                                            <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                                           v-model="person.compId" :options="lstcompany">
                                                            </b-form-select>
                                                            <b-form-invalid-feedback>
                                                                {{$t('Categories.Group.Detail.Label.CompanyRequire')}}
                                                            </b-form-invalid-feedback>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Work')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-input type="text" id="txt_jobDuties" v-if="editing"
                                                                          v-model="person.jobDuties">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.jobDuties }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.CodeName')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-input type="text" v-if="editing"
                                                                          v-model="person.personCode">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.personCode }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Birthday')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <date-picker style="width: 100%;" v-model="person.birthday" v-if="editing" id="dpk_birthday" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                                            <label class="col-form-label" v-if="!editing">{{ person.birthday | formatDate }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Name')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left"
                                                                      label-class="required">
                                                            <b-form-input type="text" id="txt_fullName" v-if="editing"
                                                                          v-model="$v.person.fullName.$model"
                                                                          :state="$v.person.fullName.$dirty? !$v.person.fullName.$error : null">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.fullName }}</label>
                                                            <b-form-invalid-feedback>
                                                                {{$t('Categories.Person.Detail.Label.NameRequire')}}
                                                            </b-form-invalid-feedback>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" v-if="editing"
                                                                           v-model="person.gender">
                                                                <b-form-select-option value="0">{{$t("Guess.Detail.Form.Male")}}</b-form-select-option>
                                                                <b-form-select-option value="1">{{$t("Guess.Detail.Form.Female")}}</b-form-select-option>
                                                            </b-form-select>
                                                            <label class="col-form-label" v-if="!editing">{{ $t(person.genderName) }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Position')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-input type="text" id="txt_position" v-if="editing"
                                                                          v-model="person.position">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.position }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Guess.Detail.Form.Phone')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-input type="text" id="txt_PhoneNumber" v-if="editing"
                                                                          v-model="person.phoneNumber">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.phoneNumber }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Card')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-input type="text" id="txt_card" v-if="editing"
                                                                          v-model="person.card">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.card }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group label="Email:"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-input type="text" id="txt_email" v-if="editing"
                                                                          v-model="person.email">
                                                            </b-form-input>
                                                            <label class="col-form-label" v-if="!editing">{{ person.email }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DeptName')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left"
                                                                      label-class="required">
                                                            <treeselect :multiple="false" :disabled="!editing"
                                                                        :options="treeselect.options"
																		:noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                                        v-model="person.deptId" />
                                                            <b-form-invalid-feedback>
                                                                {{$t('Timesheet.Dayoff.Detail.Form.DepartmentRequire')}}
                                                            </b-form-invalid-feedback>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Status')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" v-if="editing"
                                                                           v-model="person.status">
                                                                <b-form-select-option value="1">{{$t('Categories.Person.Detail.Label.Activate')}}</b-form-select-option>
                                                                <b-form-select-option value="0">{{$t('Categories.Person.Detail.Label.Stop')}}</b-form-select-option>
                                                            </b-form-select>
                                                            <label class="col-form-label" v-if="!editing">{{ $t(person.statusName) }}</label>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                                <b-row>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.Group')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left"
                                                                      label-class="required">
                                                            <div>
                                                                <!--<b-form-select style="display: none;" v-model="$v.person.groupId.$model" :state="$v.person.groupId.$dirty? !$v.person.groupId.$error : null">
                                                                </b-form-select>-->
                                                                <select2 style="width: 100%;" :placeholder="this.$t('PlaceHolder.Select')"
                                                                         :options="lstgroup" :settings="{ allowClear: true }" :disabled="!editing"
                                                                         v-model="person.groupId">
                                                                </select2>
                                                            </div>
                                                        </b-form-group>
                                                    </b-col>
                                                    <b-col md="6">
                                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.StatusVaccine')"
                                                                      :label-cols="4"
                                                                      :horizontal="true"
                                                                      label-align-md="left">
                                                            <b-form-select class="col-sm-12" :disabled="!editing" @change="changeClassVaccine($event)" v-bind:class="[backgroundVaccine, textVaccine]" placeholder="Chọn giá trị"
                                                                           v-model="person.vaccine">
                                                                <b-form-select-option class="bg-light text-dark" value="0">{{$t("Categories.Person.Detail.Label.StatusVaccine1")}}</b-form-select-option>
                                                                <b-form-select-option class="bg-danger text-white" value="-1">{{$t("Categories.Person.Detail.Label.StatusVaccine2")}}</b-form-select-option>
                                                                <b-form-select-option class="bg-warning  text-dark" value="1">{{$t("Categories.Person.Detail.Label.StatusVaccine3")}}</b-form-select-option>
                                                                <b-form-select-option class="bg-success text-white" value="2">{{$t("Categories.Person.Detail.Label.StatusVaccine4")}}</b-form-select-option>
                                                                <b-form-select-option class="bg-success text-white" value="3">{{$t("Categories.Person.Detail.Label.StatusVaccine5")}}</b-form-select-option>
                                                                <b-form-select-option class="bg-success text-white" value="4">{{$t("Categories.Person.Detail.Label.StatusVaccine6")}}</b-form-select-option>
                                                            </b-form-select>
                                                        </b-form-group>
                                                    </b-col>
                                                </b-row>
                                            </b-col>
                                        </b-row>
                                    </b-form>
                                    <template #footer>
                                        <b-button @click="toUpdate" style="width: 170px; background-color: #207dd9 !important; color: white; " variant="outline-primary">{{$t("QRCodePerson.Find.Button.ToUpdate")}}</b-button>
                                        <b-button type="button" variant="secondary" @click="cancel">{{$t("QRCodePerson.Find.Button.Cancel")}}</b-button>
                                    </template>
                                </CModal>
                            </b-card>
                        </CCardBody>
                    </CCard>
                </CCardGroup>
            </CCol>
        </CRow>
    </CContainer>
</template>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'

    export default {
        name: 'GuessDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                qrCode: {
                    code: null,
                    compId: null,
                },
                checkValid: false,
                backgroundVaccine: '',
                textVaccine: '',
                person: {
                    vaccine: null,
                    compId: null,
                    groupId: null,
                    personcode: null,
                    file1: null,
                    fileData: null,
                    personCode: null,
                    fullName: null,
                    card: null,
                    file1: null,
                    listFace: [{
                        file1: null,
                        filedata: null,
                    }],
                    listFace1: [{
                        file1: null,
                        filedata: null,
                    }],
                },
                lstgroup: [],
                listFace: [{
                    file1: null,
                    filedata: null
                }],
                listFace1: [{
                    file1: null,
                    filedata: null
                }],
                lstcompany: [],
                lstdepartment: [],
                isCameraOpen: false,
                isPhotoTaken: false,
                isShotPhoto: false,
                isLoading: false,
                link: '#',
                avatar: null,
                treeselect: {
                    value: null,
                    options: [],
                },
                acceptCamera: false,//Hiện camera
                modalDetailPerson: false,
                editing: false,
            }
        },
        validations: {
            qrCode: {
                code: { required },
            },
        },
        computed: {
            compId() {
                return this.$route.params.compId;
            },
            qrId() {
                return this.$route.params.qrId;
            },
        },
        created() {
            this.loadCompany();
            this.loadDepartmentTree();
            this.loadQRCodeUsing();
            this.loadGroup();
        },
        methods: {
            next() {
                var vm = this;
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var pid = null;
                    this.$services.get(`qrCodePerson/findByCode/${encodeURIComponent(this.qrCode.code)}/${this.compId}`).done(res => {
                        if (res.personCode != "00000") {
                            pid = res.personId;
                            vm.modalDetailPerson = true;
                            this.$services.get(`qrCodePerson/person/${pid}`).done(res => {
                                vm.person = res;
                                if (vm.person.vaccine == null) {
                                    vm.person.vaccine = 0;
                                }
                                vm.changeClassVaccine(vm.person.vaccine);
                            });
                        } else {
                            alert(i18n.t("QRCodePerson.Find.Alert.NotExistCode"));
                        }
                    });
                }
            },
            toUpdate() {
                var pid = null;
                this.$services.get(`qrCodePerson/findByCode/${encodeURIComponent(this.qrCode.code)}/${this.compId}`).done(res => {
                    if (res.personCode != "00000") {
                        pid = res.personId;
                        this.$router.push({ path: '/updateImagePerson/' + this.compId + '/' + this.qrId + '/' + pid });
                    } else {
                        alert(i18n.t("QRCodePerson.Find.Alert.NotExistCode"));
                    }
                });
            },
            cancel() {
                this.modalDetailPerson = false;
            },
            changeClassVaccine(vaccine) {
                var vm = this;
                if (vaccine == null || vaccine == 0) {
                    vm.backgroundVaccine = '';
                    vm.textVaccine = '';
                    return;
                }
                if (vaccine == -1) {
                    vm.backgroundVaccine = 'bg-danger';
                    vm.textVaccine = 'text-white';
                    return;
                }
                if (vaccine == 1) {
                    vm.backgroundVaccine = 'bg-warning';
                    vm.textVaccine = 'text-dark';
                    return;
                }
                if (vaccine >= 2) {
                    vm.backgroundVaccine = 'bg-success';
                    vm.textVaccine = 'text-white';
                    return;
                }
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
            createCameraElement() {
                this.isLoading = true;
                const constraints = (window.constraints = {
                    audio: false,
                    video: true
                });
                navigator.mediaDevices.getUserMedia(constraints)
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
            },
            takePhoto() {
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
                var vm = this;
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
                var acceptExtensions = ["jpg", "png", "PNG", "JPG"];
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
                reader.readAsBinaryString(f, vm);
                vm.person.file1 = f.name;
                vm.avatar = URL.createObjectURL(f);
            },
            turnOffCamera() {
                if (this.isCameraOpen) {
                    this.isCameraOpen = false;
                    this.isPhotoTaken = false;
                    this.isShotPhoto = false;
                    this.stopCameraStream();
                }
            },
            //Danh sách phòng ban - tree view
            loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`qrCodePerson/department-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            //Danh sách công ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`qrCodePerson/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            //Danh sách nhóm người
            loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groupAllowAnonymous`).done(res => {
                    vm.lstgroup = res;
                })
            },
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
        }
    }
</script>
<style scoped>
    .content-center {
        text-align: left;
    }
</style>
