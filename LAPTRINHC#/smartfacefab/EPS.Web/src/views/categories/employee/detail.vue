<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Person.Detail.Header1")}}</strong>
                    </div>
                    <b-form-group @submit.prevent="save">
                        <b-row>
                            <b-col md="4">
                                <b-row>
                                    <b-col md="10">
                                        <div class="form-group">
                                            <!--<b-button v-if="editing" @click="takePhotoCam" style="margin-left: -5px;">{{$t("Guess.Create.Form.BtnTakePhoto")}}</b-button>-->
                                            <v-easy-camera ref="refTakePhoto" v-if="acceptCamera" :stop="true" v-model="avatar" :autoDetectMobile="true" loading="true" :fullscreen="fullscreen" :fullscreenZIndex="9999" :mustApprove="true" @close="closeOrApproveEvent" @approve="closeOrApproveEvent"></v-easy-camera>
                                            <div class="form-group">
                                                <b-row class="my-1">
                                                    <b-col>
                                                        <div class="d-flex justify-content-center"
                                                             style="margin-left: -5px; position: relative; width: 300px; height: 300px; border: 0.5px dotted ">
                                                            <img class="img-fluid" style="max-height: 100%; max-width: 100%" v-bind:src="avatar" />
                                                            <img v-bind:src="person.file1" v-if="person.file1 != null && (!editing || avatar == null)" alt="No Image" style="max-height: 100%; max-width: 100%" />
                                                        </div>
                                                    </b-col>
                                                </b-row>
                                                <div style="width:auto">
                                                    <b-form-file v-if="editing" @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                                    </b-form-file>
                                                </div>
                                                <b-form-group :label-cols="4"
                                                              :horizontal="true"
                                                              label-align-md="left">
                                                    <b-form-input type="text"
                                                                  style="display: none;"
                                                                  v-if="editing"
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
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.CodeName')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" 
                                                          :disabled="!editing"
                                                          v-model.trim="$v.person.personCode.$model"
                                                          :state="$v.person.personCode.$dirty? !$v.person.personCode.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback v-if="!$v.person.personCode.required">
                                                {{$t('Categories.Person.Detail.Label.CodeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Name')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      :label-class="editing ? 'required' : ''">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-model="$v.person.fullName.$model"
                                                          :state="$v.person.fullName.$dirty? !$v.person.fullName.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('Categories.Person.Detail.Label.NameRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>

                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DeptName')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" 
                                                          style="display: none"
                                                          v-model="$v.person.deptId.$model"
                                                          :state="$v.person.deptId.$error ? false : null">
                                            </b-form-input>
                                            <treeselect :multiple="false"
                                                        :disabled="!editing"
                                                        :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                        :options="treeselect.options"
                                                        :placeholder="this.$t('PlaceHolder.Select')"
                                                        v-model="person.deptId" />
                                            <b-form-invalid-feedback>
                                                {{$t('Timesheet.Dayoff.Detail.Form.DepartmentRequire')}}
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
                                                          :disabled="!editing"
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
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: true}"
                                                     v-if="editing"
                                                     v-model="person.gender"
                                                     :options="lstGender">
                                            </select2>
                                            <b-form-input :disabled="!editing"
                                                          v-if="!editing"
                                                          :value="this.lstGender.filter((gender)=>
                                                person.gender == gender.id).map((element) => element.text).join(',')">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Birthday')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="person.dobStr">
                                            </b-form-input>
                                            <date-picker style="width: 100%;"
                                                         v-model="person.birthday"
                                                         v-if="editing"
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
                                                          :disabled="!editing"
                                                          v-model="person.homeAddress">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.FromDate')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input style="display:none"
                                                          v-model="$v.person.fromDate.$model"
                                                          :state="$v.person.fromDate.$dirty? !$v.person.fromDate.$error : null">
                                            </b-form-input>
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="person.fromDateStr">
                                            </b-form-input>
                                            <date-picker type="date"
                                                         style="width: 100%;"
                                                         v-model="person.fromDate"
                                                         v-if="editing"
                                                         valueType="YYYY-MM-DD"
                                                         format="DD-MM-YYYY">
                                            </date-picker>
                                            <b-form-invalid-feedback>
                                                {{$t('Categories.Person.Detail.Label.FromDateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.ToDate')"
                                                      :label-cols="3"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          :disabled="!editing"
                                                          v-if="!editing"
                                                          v-model="person.toDateStr">
                                            </b-form-input>
                                            <b-form-input style="display:none"
                                                          v-model="$v.person.toDate.$model"
                                                          :state="$v.person.toDate.$dirty? !$v.person.toDate.$error : null">
                                            </b-form-input>
                                            <date-picker type="date" 
                                                         style="width: 100%;"
                                                         v-if="editing"
                                                         v-model="person.toDate"
                                                         valueType="YYYY-MM-DD"
                                                         format="DD-MM-YYYY">
                                            </date-picker>
                                            <b-form-invalid-feedback>
                                                {{$t('Categories.Person.Detail.Label.ToDateRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <div style="text-align:center">
                            <b-form-group>
                                <b-button type="button"
                                          variant="primary"
                                          @click="edit"
                                          style="width: 120px"
                                          v-if="!editing  && authorize(['ManagePerson'])">
                                    <i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="back"
                                          v-if="!editing"
                                          style="width: 120px">
                                    <i class="fa fa-ban"></i> {{$t("Button.Back")}}
                                </b-button>
                                <b-button type="submit"
                                          @click="save"
                                          variant="primary"
                                          v-if="editing"
                                          style="width: 120px">
                                    <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="cancel"
                                          v-if="editing"
                                          style="width: 120px">
                                    <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
                                </b-button>
                            </b-form-group>
                        </div>
                    </b-form-group>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'

    export default {
        name: 'PersonDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                lstpersonType: [
                    { id: '0', text: this.$t('Reporting.Event.List.Table.CustomerVip') },
                    { id: '1', text: this.$t('Reporting.Event.List.Table.Employee') },
                    { id: '2', text: this.$t('Reporting.Event.List.Table.Customer') },
                    { id: '4', text: this.$t('Reporting.Event.List.Table.CustomerBlack') },
                ],
                lstGender: [
                    { id: '0', text: this.$t("Guess.Detail.Form.Male") },
                    { id: '1', text: this.$t("Guess.Detail.Form.Female") }
                ],
                backgroundVaccine: '',
                textVaccine: '',
                person: {
                    homeAddress: null,
                    personId: null,
                    compId: null,
                    AvatarPath: null,
                    AvatarPath1: null,
                    compid: null,
                    personCode: null,
                    fullname: null,
                    phoneNumber: null,
                    personType: 1,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    file1: null,
                    fileData: null,
                    gender: null,
                    imageBase64: null,
                    id: null,
                    fullName: null,
                    deptcode: null,
                    note: null,
                    status: null,
                    deleted: null,
                    card: null,
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
                lstcompany: {},
                isShow: false,
                showHide: false,
                showUnHide: false,
                lstdepartment: {},
                lstgroup: [],
                editing: false,
                isCameraOpen: false,
                isPhotoTaken: false,
                isShotPhoto: false,
                isLoading: false,
                link: '#',
                personisCameraOpen: false,
                avatar: null,
                treeselect: {
                    value: null,
                    options: [],
                },
                acceptCamera: false,//Hiện camera
                current: null,
            }
        },
        computed: {
            personId() {
                return this.$route.params.personId;
            },
            fromHistory() {
                return this.$route.query.fromHistory;
            },
        },
        validations: {
            person: {
                personCode: { required },
                fullName: { required },
                file1: { required },
                deptId: { required },
                fromDate: { required },
                toDate: {
                    checkDate() {
                        const dateFrom = this.person.fromDate;
                        const dateTo = this.person.toDate;
                        if (dateFrom != null && dateTo != null) {
                            if (dateFrom > dateTo) {
                                return false
                            }
                            return true;
                        }
                        return true;
                    }
                },
            }
        },
        async created() {
            await this.loadPersonDetail();
            this.loadDepartmentTree();
        },
        methods: {
            loadCurrentDateTime() {
                var vm = this;
                return this.$services.get(`lookup/currentDateTime`).done(res => {
                    vm.current = res;
                });
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
            //Danh sách phòng ban
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
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            async loadPersonDetail() {
                var vm = this;
                return this.$services.get(`person/${this.personId}`).done(res => {
                    vm.person = res;
                });
            },
            edit() {
                this.editing = true;
                this.isShow = false;
            },
            back() {
                //this.turnOffCamera();
                if (this.fromHistory) {
                    this.$router.push({ path: '/report/event/list' });
                } else {
                    this.$router.push({ path: '/categories/employee/listEmployee' });
                }
            },
            showDept2() {
                this.showDept = false;
                this.showHide = true;
                this.showUnHide = true;
            },
            showDept() {
                this.isShow = true;
            },
            showDept1() {
                this.showDept = true;
                this.showHide = false;
                this.showUnHide = false;
            },
            showHide1() {
                this.showHide = true;
            },
            showUnHide1() {
                this.showUnHide = true;
            },
            async cancel() {
                var vm = this;
                this.turnOffCamera();
                await this.loadPersonDetail();
                vm.avatar = null;
                vm.editing = false;
            },
            search() {
                //this.$refs.personTable.refresh();
            },
            formatPhone(e) {
                return String(e).substring(0, 10);
            },
            save() {
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
                    this.person.personType = 1;
                    this.person.listFace = this.listFace;
                    this.person.listFace1 = this.listFace;
                    this.$services.put(`person/${this.personId}`, this.person).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
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
    .form-control:disabled, .form-control[readonly] {
        background-color: #e2ebeae8;
        font-weight: bold;
        color: #47494a;
        opacity: 1;
    }

    .form-control {
        color: #444;
    }
</style>

