<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Person.List.Header")}}</strong>
                        <!--<strong>Quản lý khách</strong>-->
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <!--<b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" id="compId"
                                             :settings="{allowClear: true}" :disabled="true"
                                             v-model="searchForm.compId" :options="lstcompany">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Person.List.SearchForm.PersonName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>-->
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.PersonName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Guess.Create.Form.Phone')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.phoneNumber">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <!--<b-row>
                            <b-col md="6">
                                <b-form-group label="Loại khách"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.filterType"
                                             :options="lstpersonType">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label="Địa chỉ"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name" style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.address">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>-->
                        <b-row>
                            <b-col md="7" class="text-right">
                                <b-form-group style="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>

                    <!--gridName="Categories.Person.List.Table.TableName"-->
                    <b-grid :searchForm="searchForm" class="grid-border-top"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="person/listEmployee"
                            gridName="Categories.Person.List.Table.TableName"
                            ref="personTable">
                        <template v-slot:cell(avatar)="row">
                            <img v-bind:src="row.item.file1" alt="No Image" width="60" height="60" style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                        </template>
                        <template v-slot:cell(vaccineStr)="row">
                            <span class="col-form-label " style=" display: inline-block; width: 140px; " v-if="row.item.vaccine == null || row.item.vaccine == 0"><i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}</span>
                            <span class=" bg-danger text-white p-8" style="padding: 8px; display: inline-block; width: 140px; " v-if="row.item.vaccine == -1"><i class="fa fa-times"></i> {{  $t(row.item.vaccineStr) }}</span>
                            <span class=" bg-warning text-white p-8" style="padding: 8px;display: inline-block; width: 140px;" v-if="row.item.vaccine == 1"><i class="fa fa-check"></i> {{  $t(row.item.vaccineStr) }}</span>
                            <span class=" bg-success text-white p-8" style="padding: 8px;display: inline-block; width: 140px;" v-if="row.item.vaccine >= 2"><i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}</span>
                        </template>
                        <template v-slot:cell(genderName)="row">
                            <span class="col-form-label "> {{ $t(row.item.genderName) }}</span>

                        </template>
                        <template v-slot:cell(personTypeStr)="row">
                            <span class="col-form-label">
                                {{ $t(row.item.personTypeStr) }}
                            </span>
                        </template>
                        <!--<template v-slot:cell(statusName)="row">
                            <span class="col-form-label "> {{ $t(row.item.statusName) }}</span>
                        </template>-->
                        <template v-slot:actions>
                            <router-link to="/categories/employee/create"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md" 
                                         v-if="authorize(['ManageEmployee'])">
                                {{$t("Button.Create")}}
                            </router-link>
                            <router-link to="/categories/employee/import"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManageEmployee'])">
                                {{$t("Button.ImportExcel")}}
                            </router-link>
                            <!--<router-link to="/categories/person/import" tag="button" class="btn btn-primary" size="md" v-if="authorize(['ManageEmployee'])">
                                {{$t("Button.ImportExcel")}}
                            </router-link>-->
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm" :to="{ path: `/categories/employee/detail/${row.item.personId}` }" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm" @click.stop="doDelete(row.item.personId)" class="mr-1" :title="$t('Button.Delete')" v-if="authorize(['ManageEmployee'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                                <!--<b-button size="sm" @click="loadPersonDetail(row.item.personId)" :title="$t('Button.Desplay')">
                                    <i class="fa fa-street-view" aria-hidden="true"></i>
                                </b-button>-->
                            </b-button-group>
                        </template>
                    </b-grid>
                    <confirmModal ref="confirmModal">
                    </confirmModal>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListPerson',
        mixins: [authorizationMixin],
        data() {
            return {
                format: {
                    double: 0.01
                },
                searchFormMachine: {
                    groupId: null
                },
                gridOptionsMachine: {
                    fields: [
                        { key: 'deviceName', label: this.$t('Reporting.Event.List.Table.Machine'), sortable: false },
                        { key: 'time2', label: this.$t('Common.DayOfWeek.Mon'), sortable: false, thAttr: { width: 120 } },
                        { key: 'time3', label: this.$t('Common.DayOfWeek.Tue'), sortable: false, thAttr: { width: 120 } },
                        { key: 'time4', label: this.$t('Common.DayOfWeek.Wed'), sortable: false, thAttr: { width: 120 } },
                        { key: 'time5', label: this.$t('Common.DayOfWeek.Thu'), sortable: false, thAttr: { width: 120 } },
                        { key: 'time6', label: this.$t('Common.DayOfWeek.Fri'), sortable: false, thAttr: { width: 120 } },
                        { key: 'time7', label: this.$t('Common.DayOfWeek.Sat'), sortable: false, thAttr: { width: 120 } },
                        { key: 'time8', label: this.$t('Common.DayOfWeek.Sun'), sortable: false, thAttr: { width: 120 } },
                    ],
                    sortBy: 'groupId',
                    sortDesc: true,
                },
                searchForm: {
                    filterText: null,
                    phoneNumber: null,
                    filterType: null,
                    gender: null,
                    file1: null,
                    fileData: null,
                    imageBase64: null,
                    value: 78,
                    valueThreshold: 0.7,
                    //statusPicture: null,
                },
                lstpersonType: [
                    { id: '0', text: this.$t('Reporting.Event.List.Table.CustomerVip') },
                    { id: '1', text: this.$t('Reporting.Event.List.Table.Employee') },
                    { id: '2', text: this.$t('Reporting.Event.List.Table.Customer') },
                    { id: '4', text: this.$t('Reporting.Event.List.Table.CustomerBlack') },
                ],
                lstStatusPicture: [
                    { id: '1', text: this.$t('Categories.Person.List.SearchForm.AlreadyHavePictures') },
                    { id: '0', text: this.$t('Categories.Person.List.SearchForm.NoPhotosYet') }
                ],
                gridOptions: {
                    fields: [
                        { key: 'personCode', label: this.$t('Categories.Person.List.Table.Code'), sortable: false },
                        { key: 'fullName', label: this.$t('Categories.Person.List.Table.Name'), sortable: false, },
                        { key: 'phoneNumber', label: this.$t('School.Parent.List.Table.NumberPhone'), sortable: false },
                        { key: 'genderName', label: this.$t('Categories.Person.List.Table.Gender'), sortable: false },
                        { key: 'dobStr', label: this.$t('School.Parent.List.Table.Birthday'), sortable: false },
                        { key: 'department', label: this.$t('Categories.Person.List.Table.Department'), sortable: false },
                        //{ key: 'personTypeStr', label: "Loại khách", sortable: false },
                        /*{ key: 'numberOfTimes', label: "Số lượt", sortable: false },*/
                        { key: 'avatar', label: this.$t('Guess.List.Table.Image'), sortable: false, tdClass: 'text-center' },
                        //{ key: 'vaccineStr', label: this.$t('Categories.Person.List.Table.vaccine'), sortable: false },
                    ],
                    sortBy: 'fullName',
                    sortDesc: false,
                },
                person: {
                    vaccine: null,
                    personId: null,
                    compId: null,
                    AvatarPath: null,
                    AvatarPath1: null,
                    compid: null,
                    personcode: null,
                    fullname: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    file1: null,
                    fileData: null,
                    imageBase64: null,
                    id: null,
                    personCode: null,
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
                    groupId: null,
                },
                lstcompany: [],
                lstgroup: [],
                lstdepartment: [],
                lstGender: [
                    { id: '0', text: this.$t("Guess.Detail.Form.Male") },
                    { id: '1', text: this.$t("Guess.Detail.Form.Female") }
                ],

                isCameraOpen: false,
                isPhotoTaken: false,
                isShotPhoto: false,
                isLoading: false,
                link: '#',
                personisCameraOpen: false,
                avatar: null,
                isAdmin: false,
                darkModal: false,
                isSearch: false,
                treeselect: {
                    value: null,
                    options: [],
                },
                editing: false,
                acceptCamera: false,//Hiện camera

            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            this.loadCompany();
            this.loadDepartment();
            this.loadDepartmentTree();
            this.loadGroup();
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                this.person.id = item;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`person/${this.person.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                } else {

                    this.$refs.popup.close();
                }
            },
            toggleSelected(value, id) {
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
                tracks.forEach(track => { track.stop(); });
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
                vm.searchForm.fileData = jpeg.split(';base64,')[1];
                //vm.searchForm.fileData = jpeg;
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
                        vm.searchForm.fileData = window.btoa(binaryData);
                    };
                })(f);
                //vm.searchForm.fileData = URL.createObjectURL(f);
                vm.avatar = URL.createObjectURL(f);
                reader.readAsBinaryString(f, vm);
                vm.person.file1 = f.name;
            },
            setImage: function (output) {
                this.hasImage = true;
                this.image = output;
            },
            async loadPersonDetail(personId) {
                var vm = this;
                return this.$services.get(`person/${personId}`).done(person => {
                    vm.person = person;
                    if (vm.person.vaccine == null) {
                        vm.person.vaccine = 0;
                    }
                    vm.searchFormMachine.groupId = person.groupId;
                    vm.darkModal = true;
                    vm.searchmachine();
                });
            },
            //Danh sách nhóm người
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(lstgroup => {
                    vm.lstgroup = lstgroup;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done(lstdepartment => {
                    vm.treeselect.options = lstdepartment;
                });
            },
            search() {
                if (this.avatar != null) {
                    if (this.avatar.includes(';base64,') == true) {
                        var base64 = this.avatar.split(",");
                        this.searchForm.fileData = base64[1];
                    }
                }

                var vm = this;
                //vm.searchForm.hiddenParentField = null;
                //if (vm.searchForm.deptId) {
                //    var dept = vm.lstdepartment.filter(x => x.id == vm.searchForm.deptId);
                //    if (dept.length > 0) {
                //        vm.searchForm.hiddenParentField = dept[0].hiddenParentField;
                //    }
                //}

                this.$refs.personTable.refresh();
            },
            searchmachine() {
                this.$refs.personTableMachine.refresh();
            },
            //remove(item, index, button) {
            //    var vm = this;
            //    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
            //        this.$services.delete(`person/${item.personId}`).done(() => {
            //            vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
            //            vm.search();
            //        });
            //    }
            //},
            showModal() {
                this.darkModal = true;
            },
            isSearch2(checked) {
                if (checked == "false") {
                    this.isSearch = false;
                    this.searchForm.filterText = null;
                    this.searchForm.deptId = null;
                    this.searchForm.gender = null;
                    this.searchForm.fileData = null;
                    this.avatar = null;
                    this.valueThreshold = 0.7;
                    this.$refs.personTable.refresh();
                }
                else {
                    this.isSearch = true;
                }
            },
            removeSelected() {
                var vm = this;
                var selectedRows = this.$refs.personTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                        var data = selectedRows.map(x => x.personId).join(',');
                        this.$services.delete(`person?ids=${data}`).done(() => {
                            vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                            vm.search();
                        });
                    }
                }
                else {
                    vm.$toastr.e(i18n.t("Messages.ToastrDelete_e"));
                }
            }
        }
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

    .slider-target {
        margin-top: 15px;
    }
</style>
