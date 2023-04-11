<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Person.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="4">
                                <!--<b-row>
                                    <b-col md="10">
                                        <b-button @click="takePhotoCam" style="margin-left: -5px;">{{$t("Guess.Create.Form.BtnTakePhoto")}}</b-button>
                                        <v-easy-camera ref="refTakePhoto" v-if="acceptCamera" :stop="true" v-model="avatar" :autoDetectMobile="true" loading="true" :fullscreen="fullscreen" :fullscreenZIndex="9999" :mustApprove="true" @close="closeOrApproveEvent" @approve="closeOrApproveEvent"></v-easy-camera>
                                        <div class="form-group">
                                            <b-row class="my-1">
                                                <b-col>
                                                    <div class="d-flex justify-content-center"
                                                         style="margin-left: -5px; position: relative; width: 300px; height: 300px; border: 0.5px dotted">
                                                        <img class="img-fluid" style="max-height: 100%; max-width: 100%" v-bind:src="avatar" />
                                                    </div>
                                                </b-col>
                                            </b-row>
                                            <div style="width: 300px; height: 300px; margin-left: -5px;">
                                                <b-form-file @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                                </b-form-file>
                                            </div>
                                            <b-form-group :label-cols="4"
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
                                </b-row>-->
                                <b-row>
                                    <b-col md="10" offset="2">
                                        <div class="form-group">
                                            <div style="width:auto; margin-bottom: 20px; ">
                                                <img v-if="avatar != null"
                                                     v-bind:src="avatar"
                                                     alt="No Image"
                                                     style="width:100%" />
                                            </div>
                                            <div style="width:auto">
                                                <b-form-file @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                                </b-form-file>
                                            </div>
                                        </div>
                                        <b-form-group :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          style="display: none;"
                                                          v-model="$v.person.fileData.$model"
                                                          :state="$v.person.fileData.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Categories.Person.Detail.Label.ImageRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="8">
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Name')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text"
                                                          v-model="$v.person.fullname.$model"
                                                          :state="$v.person.fullname.$dirty? !$v.person.fullname.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Categories.Person.Detail.Label.NameRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.PersonType')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: true}"
                                                     v-model="$v.person.personType.$model"
                                                     state="$v.person.personType.$dirty? !$v.person.personType.$error : null"
                                                     :options="lstpersonType">
                                            </select2>
                                            <b-form-input type="text"
                                                          style="display: none;"
                                                          v-model="$v.person.personType.$model"
                                                          :state="$v.person.personType.$dirty? !$v.person.personType.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn loại khách
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Phone')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="number"
                                                          :formatter="formatPhone"
                                                          v-model="person.phoneNumber">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-select class="col-sm-12"
                                                           :placeholder="this.$t('PlaceHolder.Select')"
                                                           v-model="person.gender">
                                                <b-form-select-option value="0">{{$t("Guess.Detail.Form.Male")}}</b-form-select-option>
                                                <b-form-select-option value="1">{{$t("Guess.Detail.Form.Female")}}</b-form-select-option>
                                            </b-form-select>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Birthday')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <date-picker style="width: 100%;"
                                                         v-model="person.birthday"
                                                         valueType="YYYY-MM-DD"
                                                         format="DD-MM-YYYY">
                                            </date-picker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Address')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          v-model="person.homeAddress">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="7" offset="4">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManagePerson'])"
                                              type="submit"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel"
                                              style="width: 120px">
                                        <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
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

<script src="https://cdn.jsdelivr.net/npm/vue/dist/vue.js"></script>
<script src="https://unpkg.com/vue-multiselect@2.1.0"></script>
<script defer src="https://use.fontawesome.com/releases/v5.3.1/js/all.js"></script>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'
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
                value: [],
                backgroundVaccine: '',
                textVaccine: '',
                lstpersonType: [
                    //{ id: '0', text: this.$t('Reporting.Event.List.Table.CustomerVip') },
                    //{ id: '1', text: this.$t('Reporting.Event.List.Table.Employee') },
                    //{ id: '2', text: this.$t('Reporting.Event.List.Table.Customer') },
                    { id: '4', text: this.$t('Reporting.Event.List.Table.Suspect') },
                    { id: '5', text: this.$t('Reporting.Event.List.Table.Thief') },
                    { id: '6', text: this.$t('Reporting.Event.List.Table.Vandalize') }                    
                ],
                person: {
                    homeAddress: null,
                    phoneNumber: null,
                    personType: null,
                    vaccine: null,
                    AvatarPath: null,
                    AvatarPath1: null,
                    compid: null,
                    personcode: null,
                    fullname: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    file1: null,
                    card: null,
                    fileData: null,
                    imageBase64: null,
                    deptId: null,
                    deptcode: null,
                    note: null,
                    status: null,
                    deleted: null,
                    gendername: null,
                    status: null,
                    statuS_NAME: null,
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
                    fromDate: null,
                    toDate: null,
                },
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
                lstgroup: [],
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
                current: null,
            }
        },
        validations: {
            person: {
                fullname: { required },
                fileData: { required },
                personType: { required },
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.person.status = 1;
            await this.loadCompany();
            this.person.compid = accessToken.comId;
        },
        methods: {
            loadCurrentDateTime() {
                var vm = this;
                return this.$services.get(`lookup/currentDateTime`).done(res => {
                    vm.current = res;
                });
            },
            toggleSelected(value, id) { },
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
                }
                else {
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
                vm.avatar = URL.createObjectURL(f);;
                reader.readAsBinaryString(f, vm);
                vm.person.file1 = f.name;
            },
            setImage: function (output) {
                this.hasImage = true;
                this.image = output;
            },
            //Danh sách face
            loadFace() {
                var vm = this;
                return this.$services.get(`lookup/faces`).done(res => {
                    vm.lstcompany = res;
                });
            },
            //Danh sách department
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(res => {
                    vm.lstdepartment = res;
                })
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(res => {
                    vm.treeselect.options = res;
                });
            },
            //Danh sách nhóm người
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(res => {
                    vm.lstgroup = res;
                })
            },
            //Danh sách company
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            back() {
                this.turnOffCamera();
                this.$router.push({ path: '/categories/person/list' });
            },
            formatPhone(e) {
                return String(e).substring(0, 10);
            },
            save() {
                var accessToken = Services.getAccessToken();
                if (this.avatar != null) {
                    if (this.avatar.includes(';base64,') == true) {
                        var base64 = this.avatar.split(",");
                        this.person.fileData = base64[1];
                    }
                }
                //this.turnOffCamera();
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.person.compId = accessToken.comId;
                    this.person.status = 1;
                    this.person.listFace = this.listFace;
                    this.$services.post('person', this.person).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/categories/person/list' });
                    });
                }
            },
            cancel() {
                this.turnOffCamera();
                this.$router.push({ path: '/categories/person/list' });
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
        }
    }
</script>
<style scoped>
</style>
