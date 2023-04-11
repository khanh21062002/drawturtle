<template>

    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("School.Student.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save" class="form-search">
                        <b-row>
                            <b-col md="3">
                                <b-row>
                                    <b-col md="10" offset="2">
                                        <b-form-group label="Camera:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <div id="app" style="text-align:left;" class="web-camera-container">
                                                <div class="camera-button">
                                                    <button type="button" class="button is-rounded" :class="{ 'is-primary' : !isCameraOpen, 'is-danger' : isCameraOpen}" @click="toggleCamera">
                                                        <span v-if="!isCameraOpen">Open Camera</span>
                                                        <span v-else>Close Camera</span>
                                                    </button>
                                                </div>
                                                <div v-show="isCameraOpen && isLoading" class="camera-loading">
                                                    <ul class="loader-circle">
                                                        <li></li>
                                                        <li></li>
                                                        <li></li>
                                                    </ul>
                                                </div>
                                                <div v-if="isCameraOpen" v-show="!isLoading" class="camera-box" :class="{ 'flash' : isShotPhoto }">
                                                    <div class="camera-shutter" :class="{'flash' : isShotPhoto}"></div>
                                                    <video v-show="!isPhotoTaken" ref="camera" :width="300" :height="225" autoplay></video>
                                                    <canvas v-show="isPhotoTaken" id="photoTaken" ref="canvas" :width="300" :height="225"></canvas>
                                                </div>
                                                <div v-if="isCameraOpen && !isLoading" class="camera-shoot">
                                                    <button type="button" class="button" @click="takePhoto">
                                                        <img src="https://img.icons8.com/material-outlined/50/000000/camera--v2.png">
                                                    </button>
                                                </div>
                                                <div v-if="isPhotoTaken && isCameraOpen" class="camera-download">
                                                    <a id="downloadPhoto" download="my-photo.jpg" class="button" role="button" @click="downloadImage">
                                                        Download
                                                    </a>
                                                </div>
                                            </div>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="10" offset="2">
                                        <div class="form-group">
                                            <div style="width:auto; margin-bottom: 20px; ">
                                                <img v-if="avatar != null" v-bind:src="avatar" alt="No Image" width="100%">
                                            </div>
                                            <div style="width:auto">
                                                <b-form-file @change="onChange($event)" id="file-input" accept=".jpg, .png">
                                                </b-form-file>
                                            </div>
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
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.person.compid.$model"
                                                          :state="$v.person.compid.$error ? false : null">
                                            </b-form-input>
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstcompany" :disabled="true"
                                                     v-model="$v.person.compid.$model" :state="$v.person.compid.$dirty? !$v.person.compid.$error : null">
                                            </select2>
                                            <b-form-invalid-feedback>
                                                {{$t('Categories.Group.Detail.Label.CompanyRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.StatusVaccine')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-select class="col-sm-12" @change="changeClassVaccine($event)" v-bind:class="[backgroundVaccine, textVaccine]" placeholder="Chọn giá trị"
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
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Student.Detail.Label.Code')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_personcode"
                                                          v-model.trim="$v.person.personcode.$model"
                                                          :state="$v.person.personcode.$dirty? !$v.person.personcode.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback v-if="!$v.person.personcode.required">
                                                {{$t('School.Student.Detail.Label.CodeRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Birthday')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <date-picker style="width: 100%;" v-model="person.birthday" id="ID" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Student.Detail.Label.Name')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">

                                            <b-form-input type="text" id="txt_fullname"
                                                          v-model="$v.person.fullname.$model"
                                                          :state="$v.person.fullname.$dirty? !$v.person.fullname.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t('School.Student.Detail.Label.NameRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')"
                                                           v-model="person.gender">
                                                <b-form-select-option value="0">{{$t("Guess.Detail.Form.Male")}}</b-form-select-option>
                                                <b-form-select-option value="1">{{$t("Guess.Detail.Form.Female")}}</b-form-select-option>
                                            </b-form-select>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Student.Detail.Label.Class')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.person.deptId.$model"
                                                          :state="$v.person.deptId.$error ? false : null">
                                            </b-form-input>
                                            <b-form-select :placeholder="this.$t('PlaceHolder.Select')" :options="lstdepartment"
                                                           v-model="$v.person.deptId.$model" :state="$v.person.deptId.$dirty? !$v.person.deptId.$error : null">
                                            </b-form-select>
                                            <b-form-invalid-feedback>
                                                {{$t('School.Student.Detail.Label.ClassRequire')}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Phone')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_PhoneNumber"
                                                          v-model="person.phoneNumber">
                                            </b-form-input>
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
                                                <b-form-select style="display: none;" v-model="$v.person.listPersonGroup.$model" :state="$v.person.listPersonGroup.$dirty? !$v.person.listPersonGroup.$error : null">
                                                </b-form-select>
                                                <select2 style="width: 100%;" :placeholder="this.$t('PlaceHolder.Select')" :options="lstgroup" :settings="{ multiple: true }"
                                                         v-model="$v.person.listPersonGroup.$model" :state="$v.person.listPersonGroup.$dirty? !$v.person.listPersonGroup.$error : null">
                                                </select2>
                                                <b-form-invalid-feedback>
                                                    {{$t('Categories.Accesstimeseg.Create.Require.GroupNameRequire')}}
                                                </b-form-invalid-feedback>
                                                <b-form-invalid-feedback v-if="!$v.person.listPersonGroup.checkUnique">
                                                    {{$t('Categories.Accesstimeseg.Create.Require.GroupNameUnique')}}
                                                </b-form-invalid-feedback>
                                            </div>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Status')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')"
                                                           v-model="person.status">
                                                <b-form-select-option value="1">{{$t('Categories.Person.Detail.Label.Activate')}}</b-form-select-option>
                                                <b-form-select-option value="0">{{$t('Categories.Person.Detail.Label.Stop')}}</b-form-select-option>
                                            </b-form-select>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Email:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text" id="txt_email"
                                                          v-model="person.email">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <!--Mối quan hệ với học sinh-->
                        <b-row>
                            <b-col md="11" style="margin-left: 20px;">
                                <b-row>
                                    <b-col md="7">
                                        <h2>{{$t("School.Parent.List.Header")}}</h2>
                                    </b-col>
                                    <b-col md="5" class="text-right">
                                        <b-button color="dark" @click="chooseParents">{{$t('School.Student.Detail.Label.ChooseParent')}}</b-button>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-table striped hover :items="lstparents" :fields="parentFields" show-empty>
                                            <template #empty="scope">
                                                <div class="invalid-feedback" style="display: block; text-align:center;">{{$t('School.Parent.Detail.Label.Student')}}</div>
                                            </template>
                                            <template v-slot:cell(index)="row">
                                                {{ row.index + 1 }}
                                            </template>
                                            <template v-slot:cell(avatar)="row">
                                                <img v-bind:src="row.item.file1" alt="No Image" width="60" height="60" style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                                            </template>
                                            <template v-slot:cell(note)="row">
                                                <b-form-input required type="text" id="note" style="width: 100%"
                                                              v-model="row.item.note">
                                                </b-form-input>
                                            </template>
                                            <template v-slot:cell(statusName)="row">
                                                <span class="col-form-label "> {{ $t(row.item.statusName) }}</span>
                                            </template>
                                            <template v-slot:cell(action)="row">
                                                <b-button-group>
                                                    <b-button size="sm" @click.stop="remove(row.item, row.index, $event.target)" class="mr-1" title="xóa">
                                                        <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                    </b-button>
                                                </b-button-group>
                                            </template>
                                            <template v-slot:cell(action)="row">
                                                <b-button-group>
                                                    <b-button size="sm" @click.stop="remove(row.item, row.index, $event.target)" class="mr-1" title="xóa">
                                                        <i class="fa fa-trash-o" aria-hidden="true"></i>
                                                    </b-button>
                                                </b-button-group>
                                            </template>
                                        </b-table>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <b-form-group>
                                            <b-button v-if="authorize(['ManageStudent'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>

                        </b-row>
                    </b-form>
                    <CModal :show.sync="showListParents"
                            :no-close-on-backdrop="true"
                            title="this.$t('School.Parent.List.Table.TableName')"
                            size="xl"
                            color="dark">
                        <b-form @submit.prevent="search" class="form-search">
                            <b-row>
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
                                    <b-form-group :label="this.$t('School.Parent.List.SearchForm.ParentName')"
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
                            </b-row>
                            <b-row>
                                <b-col md="6">
                                    <b-form-group :label="this.$t('Guess.Detail.Form.Phone')"
                                                  label-for="code"
                                                  :label-cols="4"
                                                  :horizontal="true"
                                                  label-align-md="left">
                                        <b-form-input type="number" min="0" max="999999999999" step="1" id="name" style="width: 100%"
                                                      :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                      v-model="searchForm.phoneNumber">
                                        </b-form-input>
                                    </b-form-group>
                                </b-col>
                                <b-col md="6">
                                    <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                                  label-for="code"
                                                  :label-cols="4"
                                                  :horizontal="true"
                                                  label-align-md="left">
                                        <select2 :placeholder="this.$t('PlaceHolder.Select')" id="gender" :settings="{allowClear: true}"
                                                 v-model="searchForm.gender" :options="lstGender">
                                        </select2>
                                    </b-form-group>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col md="6" offset="3">
                                    <b-form-group lstyle="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                        <b-button type="submit" variant="primary">{{$t("Button.Search")}}</b-button>
                                        <b-button type="button" variant="warning" @click="refresh">{{$t("Button.Refresh")}}</b-button>
                                    </b-form-group>
                                </b-col>
                            </b-row>
                        </b-form>
                        <b-grid :searchForm="searchForm" class="grid-border-top"
                                :gridOptions="gridOptions"
                                dataUrl="person/parent"
                                :actionable="false"
                                gridName="School.Parent.List.Table.TableName"
                                ref="parentTable">
                            <template v-slot:cell(vaccineStr)="row">
                                <span class="col-form-label " style=" display: inline-block; width: 140px; " v-if="row.item.vaccine == null || row.item.vaccine == 0"><i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}</span>
                                <span class=" bg-danger text-white p-8" style="padding: 8px; display: inline-block; width: 140px; " v-if="row.item.vaccine == -1"><i class="fa fa-times"></i> {{  $t(row.item.vaccineStr) }}</span>
                                <span class=" bg-warning text-white p-8" style="padding: 8px;display: inline-block; width: 140px;" v-if="row.item.vaccine == 1"><i class="fa fa-check"></i> {{  $t(row.item.vaccineStr) }}</span>
                                <span class=" bg-success text-white p-8" style="padding: 8px;display: inline-block; width: 140px;" v-if="row.item.vaccine >= 2"><i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}</span>
                            </template>
                            <template v-slot:cell(genderName)="row">
                                <span class="col-form-label "> {{ $t(row.item.genderName) }}</span>
                            </template>
                            <template v-slot:cell(statusName)="row">
                                <span class="col-form-label "> {{ $t(row.item.statusName) }}</span>
                            </template>
                            <template v-slot:cell(avatar)="row">
                                <img v-bind:src="row.item.file1" alt="No Image" width="60" height="60" style="width: 60px; height: 80px; object-fit: cover; border-radius: 0.3em;">
                            </template>
                        </b-grid>
                        <template #header>
                            <h6 class="modal-title">{{$t('School.Student.Detail.Label.ChooseParent')}}</h6>
                            <CButtonClose @click="showListParents = false" class="text-white" />
                        </template>
                        <template #footer>
                            <b-col md="11" offset="12">
                                <b-form-group lstyle="margin-bottom: 0px !important;" label="" :label-cols="4" label-align-md="right">
                                    <b-button variant="primary" @click="submitSelected"><i class="fa fa-floppy-o"></i> {{$t('School.Parent.Detail.Label.Confirm')}}</b-button>
                                    <b-button type="button" variant="secondary" @click="showListParents = false"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </template>
                    </CModal>
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
                person: {
                    vaccine: null,
                    AvatarPath: null,
                    AvatarPath1: null,
                    compid: null,
                    groupId: null,
                    personcode: null,
                    fullname: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    file1: null,
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
                    listPersonGroup: [],
                    compType: 1,

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
                lstparents: [],
                parentFields: [
                    { key: 'action', label: this.$t('School.Parent.Detail.Label.Action') },
                    { key: 'personCode', label: this.$t('School.Parent.List.Table.Code'), sortable: false },
                    { key: 'fullName', label: this.$t('School.Parent.List.Table.Name'), sortable: false, },
                    { key: 'dobStr', label: this.$t('School.Parent.List.Table.Birthday'), sortable: false, },
                    { key: 'phoneNumber', label: this.$t('School.Parent.List.Table.NumberPhone'), sortable: false, },
                    { key: 'email', label: 'Email', sortable: false },
                    { key: 'statusName', label: this.$t('Guess.List.Table.Status'), sortable: false },
                    { key: 'avatar', label: this.$t('Guess.List.Table.Image'), sortable: false },
                    { key: 'note', label: this.$t('School.Parent.Detail.Label.Note'), sortable: false },
                ],
                showListParents: false,
                searchForm: {
                    filterText: null,
                    compId: null,
                    gradeId: null,
                    deptId: null,
                    gender: null,
                    phoneNumber: null,
                    lstpersonidsExcept: '',
                },
                gridOptions: {
                    fields: [
                        { key: 'personCode', label: this.$t('School.Parent.List.Table.Code'), sortable: false },
                        { key: 'fullName', label: this.$t('School.Parent.List.Table.Name'), sortable: false, },
                        { key: 'dobStr', label: this.$t('School.Parent.List.Table.Birthday'), sortable: false, },
                        { key: 'genderName', label: this.$t('Categories.Person.List.Table.Gender'), sortable: false },
                        { key: 'phoneNumber', label: this.$t('School.Parent.List.Table.NumberPhone'), sortable: false, },
                        { key: 'email', label: 'Email', sortable: false },
                        { key: 'statusName', label: this.$t('Guess.List.Table.Status'), sortable: false },
                        { key: 'avatar', label: this.$t('Guess.List.Table.Image'), sortable: false },
                        { key: 'vaccineStr', label: this.$t('Categories.Person.List.Table.vaccine'), sortable: false },
                    ],
                    sortBy: 'fullName',
                    sortDesc: false,
                },
                lstGender: [
                    { id: '1', text: this.$t("Guess.Detail.Form.Female") },
                    { id: '0', text: this.$t("Guess.Detail.Form.Male") }
                ],
            }
        },
        validations: {
            person: {
                personcode: {
                    required,
                },
                fullname: { required },
                deptId: { required },
                compid: { required },
                //groupId: { required },
                listPersonGroup: {
                    required, checkUnique() {
                        if (this.person.listPersonGroup && this.person.listPersonGroup.length == 1) {
                            return true;
                        }
                        return false;
                    },
                },
                fileData: { required },
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.person.departmenT_ID = accessToken.unitId;
            this.searchForm.compId = accessToken.comId;
            this.person.status = 1;
            await this.loadCompany();
            this.person.compid = accessToken.comId;
            await this.loadDepartment();
            this.loadGroup();
        },
        methods: {
            toggleSelected(value, id) {
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
                return this.$services.get(`lookup/faces`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách department
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/classes`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            //Danh sách company
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách nhóm người
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(lstgroups => {
                    vm.lstgroup = lstgroups;
                })
            },
            back() {
                this.turnOffCamera();
                this.$router.push({ path: '/school/student/list' });
            },
            save() {
                this.turnOffCamera();
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.person.listFace = this.listFace;
                    this.person.parents = this.lstparents;
                    this.$services.post('person/student', this.person).done((id) => {
                        vm.$toastr.s('Tạo mới học sinh thành công');
                        vm.$router.push({ path: '/school/student/list' });
                    });
                }
            },
            cancel() {
                this.turnOffCamera();
                this.$router.push({ path: '/school/student/list' });
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
            remove(item, index, button) {
                this.lstparents.splice(index, 1);
            },
            search() {
                this.$refs.parentTable.refresh();
            },
            refresh() {
                var vm = this;
                vm.searchForm = {
                    compId: null,
                    filterText: null,
                    gradeId: null,
                    deptId: null,
                    gender: null,
                    filterTextStudent: null,
                    phoneNumber: null,
                };
                var accessToken = Services.getAccessToken();
                this.searchForm.compId = accessToken.comId;
                vm.$refs.parentTable.refresh();
            },
            chooseParents() {
                // remove personid existed in list students chosen
                var vm = this;
                let lstparentids = '';
                $.each(vm.lstparents, function (index, value) {
                    if (index === 0)
                        lstparentids += value.personId;
                    else
                        lstparentids += ',' + value.personId;
                });
                vm.searchForm.lstpersonidsExcept = lstparentids;
                this.$refs.parentTable.refresh();
                this.showListParents = true;
                this.$refs.parentTable.isCheckAll = false;
            },
            submitSelected() {
                var vm = this;
                var selectedRows = this.$refs.parentTable.selectedRows;
                if (selectedRows.length > 0) {
                    $.each(selectedRows, function (key, value) {
                        let item = {
                            'parentId': value.personId,
                            'personId': value.personId,
                            'personCode': value.personCode,
                            'fullName': value.fullName,
                            'dobStr': value.dobStr,
                            'genderName': value.genderName,
                            'phoneNumber': value.phoneNumber,
                            'email': value.email,
                            'statusName': value.statusName,
                            'avatar': value.avatar,
                            'file1': value.file1,
                            'vaccineStr': value.vaccineStr,
                            'note': '',
                        }
                        var cloneItem = JSON.parse(JSON.stringify(item));
                        vm.lstparents.push(cloneItem);
                    });
                }
                this.showListParents = false;
            },
        }
    }
</script>
<style scoped>
</style>
