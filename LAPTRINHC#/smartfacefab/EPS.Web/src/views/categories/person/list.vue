<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('Reporting.Event.List.SearchForm.Header')}}</strong>
                    </div>
                    <b-form class="form-search" @submit.prevent="search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.PersonName')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  style="width: 100%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.PersonType')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstpersonType"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{ allowClear: true, multiple: true }"
                                             style="width: 98%"
                                             v-model="searchForm.personTypeArray">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <!--<b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.SearchHight')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-checkbox size="lg"
                                                     type="checkbox"
                                                     @change="isSearch2($event)"
                                                     name="checkbox-usetemperature"
                                                     value="true"
                                                     unchecked-value="false">
                                    </b-form-checkbox>
                                </b-form-group>
                            </b-col>-->
                            <!--<b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Number')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <div style="width:100%; display:flex">
                                        <b-form-select :placeholder="this.$t('PlaceHolder.Select')"
                                                       style="width: 20%"
                                                       v-model="searchForm.countType">
                                            <b-form-select-option value="1"> > </b-form-select-option>
                                            <b-form-select-option value="2"> < </b-form-select-option>
                                            <b-form-select-option value="3"> = </b-form-select-option>
                                        </b-form-select>
                                        <b-form-input type="number"
                                                      style="width: 80%"
                                                      :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                      v-model="searchForm.countNumber">
                                        </b-form-input>
                                    </div>
                                </b-form-group>
                            </b-col>-->
                        </b-row>
                        <b-row v-if="isSearch">
                            <b-col md="6">
                                <b-row>
                                    <b-col md="4"></b-col>
                                    <b-col md="8">
                                        <!--<b-button @click="takePhotoCam"
                                                  style="margin-left: -5px;">
                                            {{$t("Guess.Create.Form.BtnTakePhoto")}}
                                        </b-button>-->
                                        <v-easy-camera ref="refTakePhoto"
                                                       v-if="acceptCamera"
                                                       :stop="true"
                                                       v-model="avatar"
                                                       :autoDetectMobile="true"
                                                       loading="true"
                                                       :fullscreen="fullscreen"
                                                       :fullscreenZIndex="9999"
                                                       :mustApprove="true"
                                                       @close="closeOrApproveEvent"
                                                       @approve="closeOrApproveEvent">
                                        </v-easy-camera>
                                        <div class="form-group">
                                            <b-row class="my-1">
                                                <b-col>
                                                    <div class="d-flex justify-content-center"
                                                         style="margin-left: -5px; position: relative; width: 300px; height: 300px; border: 0.1px dotted gray;">
                                                        <img class="img-fluid" style="max-height: 100%; max-width: 100%" v-bind:src="avatar" />
                                                    </div>
                                                </b-col>
                                            </b-row>
                                            <div style="width:auto; margin-left: -5px;">
                                                <b-form-file @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                                </b-form-file>
                                            </div>
                                        </div>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="4"></b-col>
                                    <b-col md="8">
                                        <b-form-group :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          style="display: none;"
                                                          v-model="searchForm.fileData">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UseTemperature1')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <Slider v-model="searchForm.valueThreshold" :step="0.01" :max="1" :min="0" :format="format" />
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="7" class="text-right">
                                <b-form-group style="margin-bottom: 0px !important;"
                                              label=""
                                              :label-cols="4"
                                              label-align-md="right">
                                    <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                    <b-button type="button" variant="success" style="width: 120px">
                                        <download-excel :header="headerExcelDetail"
                                                        :fetch="exportData"
                                                        :fields="export_fields"
                                                        name="Blacklist">
                                            {{ $t('Button.ExportExcel') }}
                                        </download-excel>
                                    </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <KPGrid :searchForm="searchForm"
                            class="grid-border-top"
                            :gridOptions="gridOptions"
                            :multiLanguage="true"
                            dataUrl="person/listPerson"
                            gridName="Categories.Person.List.Table.TableName1"
                            ref="personTable">
                        <template v-slot:cell(avatar)="row">
                            <img v-bind:src="row.item.file1"
                                 alt="No Image"
                                 width="60"
                                 height="60"
                                 style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                        </template>
                        <template v-slot:cell(genderName)="row">
                            <span class="col-form-label "> {{ $t(row.item.genderName) }}</span>
                        </template>
                        <template v-slot:cell(personTypeStr)="row">
                            <span class="col-form-label">
                                {{ $t(row.item.personTypeStr) }}
                            </span>
                        </template>
                        <template v-slot:actions>
                            <router-link to="/categories/person/create"
                                         tag="button"
                                         class="btn btn-primary"
                                         size="md"
                                         v-if="authorize(['ManagePerson'])">
                                {{$t("Button.Create")}}
                            </router-link>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          :to="{ path: `/categories/person/detail/${row.item.personId}` }"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click.stop="doDelete(row.item.personId)"
                                          class="mr-1"
                                          :title="$t('Button.Delete')"
                                          v-if="authorize(['ManagePerson'])">
                                    <i class="fa fa-trash-o" aria-hidden="true"></i>
                                </b-button>
                                <b-button size="sm"
                                          @click="loadPersonDetail(row.item.personId)"
                                          :title="$t('Button.Desplay')">
                                    <i class="fa fa-street-view" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </KPGrid>
                    <confirmModal ref="confirmModal">
                    </confirmModal>
                </b-card>
                <CModal :show.sync="darkModal"
                        :no-close-on-backdrop="true"
                        :title="this.$t('Timesheet.TimeKeeping.List.DetailReport.TitlePerson')"
                        size="xl"
                        color="dark">
                    <b-form @submit.prevent="save" @submit="$v.$touch()">
                        <b-row>
                            <b-col md="12">
                                <b-grid :searchForm="searchFormMachine"
                                        :gridOptions="gridOptionsMachine"
                                        :multiLanguage="true"
                                        :actionable="false"
                                        class="grid-border-top"
                                        dataUrl="person/PersonMachine"
                                        gridName=""
                                        ref="personTableMachine">
                                    <template v-slot:cell(avatar1)="row">
                                        <img v-bind:src="row.item.facePath"
                                             alt="No Image"
                                             width="60"
                                             height="60"
                                             style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                                    </template>
                                    <template v-slot:cell(genderStr)="row">
                                        <span class="col-form-label "> {{ $t(row.item.genderStr) }}</span>
                                    </template>
                                </b-grid>
                            </b-col>
                        </b-row>
                    </b-form>
                    <template #footer>
                        <b-button type="button"
                                  variant="secondary"
                                  @click="darkModal = false">
                            <i class="fa fa-ban"></i>{{$t("Button.Close")}}
                        </b-button>
                    </template>
                </CModal>
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
                    personId: null
                },
                gridOptionsMachine: {
                    fields: [
                        { key: 'avatar1', label: this.$t('Guess.List.Table.Image'), sortable: false },
                        { key: 'accessDateDayFormat', label: this.$t('Reporting.Event.List.Table.Day'), sortable: false, },
                        { key: 'phoneNumber', label: this.$t('Guess.List.Table.Phone'), sortable: false },
                        { key: 'fullName', label: this.$t('Categories.Person.List.Table.Name'), sortable: false, },
                        { key: 'genderStr', label: this.$t('Categories.Person.List.Table.Gender'), sortable: false, tdClass: 'text-center' },
                        { key: 'homeAddress', label: this.$t('Categories.Person.List.Table.Address'), sortable: false },
                        { key: 'companyName', label: this.$t('Reporting.Event.List.Table.Company'), sortable: false },
                    ],
                    sortBy: 'accessTime',
                    sortDesc: true,
                },
                searchForm: {
                    filterText: null,
                    phoneNumber: null,
                    filterType: null,
                    gender: null,
                    file1: null,
                    fileData: null,
                    valueThreshold: 0.7,
                    countType: 1,
                    countNumber: null,
                    personTypeArray: [],
                },
                lstpersonType: [
                    {
                        id: '4',
                        text: this.$t('Reporting.Event.List.Table.Suspect'),
                    },
                    {
                        id: '5',
                        text: this.$t('Reporting.Event.List.Table.Thief'),
                    },
                    {
                        id: '6',
                        text: this.$t('Reporting.Event.List.Table.Vandalize'),
                    }
                ],
                lstCountType: [
                    { id: '1', text: this.$t('Reporting.Event.List.SearchForm.Bigger') },
                    { id: '2', text: this.$t('Reporting.Event.List.SearchForm.Small') },
                ],
                lstStatusPicture: [
                    { id: '1', text: this.$t('Categories.Person.List.SearchForm.AlreadyHavePictures') },
                    { id: '0', text: this.$t('Categories.Person.List.SearchForm.NoPhotosYet') }
                ],
                gridOptions: {
                    fields: [
                        { key: 'fullName', label: this.$t('Categories.Person.List.Table.Name'), sortable: false, },
                        { key: 'personTypeStr', label: this.$t('Categories.Person.List.Table.PersonType'), sortable: false },
                        { key: 'phoneNumber', label: this.$t('Guess.List.Table.Phone'), sortable: false },
                        { key: 'genderName', label: this.$t('Categories.Person.List.Table.Gender'), sortable: false },
                        { key: 'dobStr', label: this.$t('Categories.Person.List.Table.Birthday'), sortable: false },
                        { key: 'homeAddress', label: this.$t('Categories.Person.List.Table.Address'), sortable: false },
                        //{ key: 'numberOfTimes', label: this.$t('Categories.Person.List.Table.Number'), sortable: false },
                        { key: 'avatar', label: this.$t('Guess.List.Table.Image'), sortable: false, tdClass: 'text-center' },
                    ],
                    sortBy: 'registerTime desc, updateTime',
                    sortDesc: true,
                },
                person: {
                    personId: null,
                    compId: null,
                    status: 1,
                    file1: null,
                    fileData: null,
                    fullName: null,
                },
                isCameraOpen: false,
                isPhotoTaken: false,
                isShotPhoto: false,
                isLoading: false,
                avatar: null,
                darkModal: false,
                isSearch: false,
                acceptCamera: false,//Hiện camera
                export_fields: {
                    'Categories.Person.List.Table.Name': 'fullName',
                    'Số điện thoại': 'phoneNumber',
                    'Categories.Person.List.Table.Gender': 'genderName',
                    'Ngày sinh': 'dobStr',
                    'Địa chỉ': 'homeAddress',
                    'Loại đối tượng': 'personTypeStr'
                },
                headerExcelDetail: [],
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
        },
        methods: {
            async doDelete(item) {
                var vm = this;
                const ok = await this.$refs.confirmModal.show({
                    title: this.$t('Button.Delete'),
                    message: this.$t('Messages.ConfirmDelete'),
                })
                // If you throw an error, the method will terminate here unless you surround it wil try/catch
                if (ok) {
                    this.$services.delete(`person/${item}`).done(() => {
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
                vm.searchFormMachine.personId = personId;
                vm.darkModal = true;
                vm.searchmachine();
            },
            search() {
                if (this.avatar != null) {
                    if (this.avatar.includes(';base64,') == true) {
                        var base64 = this.avatar.split(",");
                        this.searchForm.fileData = base64[1];
                    }
                }
                this.$refs.personTable.refresh();
            },
            searchmachine() {
                this.$refs.personTableMachine.refresh();
            },
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
            },
            async exportData() {
                var vm = this;
                var pagination = {
                    page: 1,
                    itemsPerPage: 99999,
                    sortBy: 'fullName',
                    sortDesc: false
                };
                var formData = $.extend({}, pagination, vm.searchForm);
                var response = await this.$services.post(this.$refs.personTable.dataUrl, formData);
                for (var i = 0; i < response.data.length; i++) {
                    response.data[i].personTypeStr = this.$t(response.data[i].personTypeStr);
                    response.data[i].genderName = this.$t(response.data[i].genderName);
                }
                return response.data;
            },
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
