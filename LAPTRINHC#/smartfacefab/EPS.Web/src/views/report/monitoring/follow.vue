<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <b-row>
                        <b-col md="3">
                            <b-row>
                                <table class="table border mb-0">
                                    <thead class="table-light fw-semibold">
                                        <tr class="align-middle">
                                            <th style="width: 20%;"></th>
                                            <th> {{ $t("Monitor.Blacklist") }}</th>
                                        </tr>
                                    </thead>
                                    <tbody style="display: block; height: 34.7vh; overflow: auto; width: 100%;" id="style-scroll">
                                        <tr class="align-middle" v-for="item in listEventBlackList">
                                            <td class="text-center">
                                                <div class="avatar avatar-md"><img class="avatar-img" style=" margin-top: 10px;" :src="item.getFaceUrl"><span class="avatar-status bg-success"></span></div>
                                            </td>
                                            <td>
                                                <div>{{ $t("Monitor.Name") }} {{ item.person }}</div>
                                                <div class="small text-medium-emphasis" style="color: red;">{{ $t("Reporting.Event.List.SearchForm.PersonType") }} {{ $t(item.personTypeStr)  }}</div>
                                                <div class="small text-medium-emphasis">{{ $t("Monitor.Time") }} {{ item.accessDateDayFormat }}</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </b-row>
                            <b-row>
                                <table class="table border mb-0">
                                    <thead class="table-light fw-semibold">
                                        <tr class="align-middle">
                                            <th></th>
                                            <th>{{ $t("Monitor.Unregistered") }}</th>
                                        </tr>
                                    </thead>
                                    <tbody style="display: block; height: 34vh; overflow: auto; width: 100%;" id="style-scroll">
                                        <tr class="align-middle" v-for="item in listEventGuess">
                                            <td class="text-center">
                                                <div class="avatar avatar-md"><img class="avatar-img" :src="item.getFaceUrl"><span class="avatar-status bg-success"></span></div>
                                            </td>
                                            <td>
                                                <div> {{ $t("Monitor.Name") }} {{ $t("Monitor.Unregistered") }} </div>
                                                <div class="small text-medium-emphasis">{{ $t("Monitor.Time") }} {{ item.accessDateDayFormat }}</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </b-row>
                            <!--<b-row>
                                <b-col style="border: solid 1px; border-color: darkgray; overflow: hidden; overflow-y: scroll; height: 325px;" id="style-scroll">
                                    <b-row class="headerBox">
                                        {{ $t("Monitor.Blacklist") }}
                                    </b-row>
                                    <b-row v-for="item in listEventBlackList" style="border-bottom: solid 1px; border-color: darkgray; ">
                                        <b-row class="mydivouter" style="padding: 5px; min-width: 100%;">
                                            <b-button class="mybuttonoverlap"
                                                      size="sm"
                                                      :title="$t('Monitor.FollowDetail')"
                                                      @click="openEventDetailModal(item)">
                                                <i class="fa fa-info" aria-hidden="true"></i>
                                            </b-button>
                                            <b-col md="4">
                                                <img alt="No Image" style="width: 80px; height: 80px;" :src="item.getFaceUrl" />
                                            </b-col>
                                            <b-col md="8" class="text-left" style="font-size:10px;">
                                                <div> {{ $t("Monitor.Name") }} {{ item.person }} </div>
                                                <div style="color: red;"> {{ $t("Reporting.Event.List.SearchForm.PersonType") }} {{ $t(item.personTypeStr)  }} </div>
                                                <div> {{ $t("Monitor.Time") }} {{ item.accessDateDayFormat }} </div>
                                            </b-col>
                                        </b-row>
                                    </b-row>
                                </b-col>
                            </b-row>-->
                            <!--<b-row>
                                <b-col style="border: solid 1px; border-color: darkgray; overflow: hidden; overflow-y: scroll; height: 325px;" id="style-scroll">
                                    <b-row class="headerBox">
                                        {{ $t("Monitor.Unregistered") }}
                                    </b-row>
                                    <b-row v-for="item in listEventGuess" style="border-bottom: solid 1px; border-color: darkgray; ">
                                        co
                                        <b-row class="mydivouter" style="padding: 5px; min-width: 100%;">
                                            <b-col md="4">
                                                <img alt="No Image" style="width: 80px; height: 80px;" :src="item.getFaceUrl" />
                                            </b-col>
                                            <b-col md="8" class="text-left" style="font-size:10px;">
                                                <div> {{ $t("Monitor.Name") }} {{ $t("Monitor.Unregistered") }} </div>
                                                <div> {{ $t("Monitor.Time") }} {{ item.accessDateDayFormat }} </div>
                                            </b-col>
                                        </b-row>
                                    </b-row>
                                </b-col>
                            </b-row>-->
                        </b-col>
                        <b-col md="6" style="border: solid 1px; border-color: darkgray;">
                            <!--<b-row class="headerBox">
                                {{$t('Monitor.FollowCam')}}
                            </b-row>-->

                            <table class="table border mb-0">
                                <thead class="table-light fw-semibold">
                                    <tr class="align-middle">
                                        <th>Theo dõi giám sát</th>
                                    </tr>
                                </thead>
                            </table>
                            <div style="height: 36vh; border-bottom: solid 1px; border-color: darkgray">
                                <Live ref="device7_1"
                                      :systemCode="systemCode"
                                      :handleShowModal="handleShowModal"
                                      :videoIndex="1" />
                            </div>
                            <div style="height: 36vh; border-bottom: solid 1px; border-color: darkgray">
                                <Live ref="device7_2"
                                      :systemCode="systemCode"
                                      :handleShowModal="handleShowModal"
                                      :videoIndex="2" />
                            </div>
                        </b-col>
                        <b-col md="3">
                            <!--<b-row class="headerBox">
                                {{ $t("Monitor.Live") }}
                            </b-row>
                            <b-row v-for="item in listEvent" style="border-bottom: solid 1px; border-color: darkgray; ">
                                <b-row style="padding: 5px; min-width:100%" class="mydivouter">
                                    <b-button class="mybuttonoverlap"
                                              size="sm"
                                              :title="$t('Monitor.FollowDetail')"
                                              @click="openEventDetailModal(item)">
                                        <i class="fa fa-info" aria-hidden="true"></i>
                                    </b-button>
                                    <b-col md="4">
                                        <img alt="No Image" style="width: 80px; height: 80px;" :src="item.getFaceUrl" />
                                    </b-col>
                                    <b-col md="8" class="text-left" style="font-size:10px">
                                        <div> {{ $t("Monitor.Name") }} {{ $t(item.person) }} </div>
                                        <div> {{ $t("Timesheet.Dayoff.List.SearchForm.DeptName") }} {{ item.depName }} </div>
                                        <div> {{ $t("Monitor.Time") }} {{ item.accessDateDayFormat }} </div>
                                    </b-col>
                                </b-row>
                            </b-row>-->
                            <b-row>
                                <table class="table border mb-0">
                                    <thead class="table-light fw-semibold">
                                        <tr class="align-middle">
                                            <th style="width: 20%;"></th>
                                            <th>{{ $t("Monitor.Live") }}</th>
                                        </tr>
                                    </thead>
                                    <tbody style="display: block; height: 74vh; overflow: auto; width: 100%;" id="style-scroll">
                                        <tr class="align-middle" v-for="item in listEvent">
                                            <td class="text-center">
                                                <div class="avatar avatar-md"><img class="avatar-img"  style=" margin-top: 10px;" :src="item.getFaceUrl"><span class="avatar-status bg-success"></span></div>
                                            </td>
                                            <td>
                                                <div>{{ $t("Monitor.Name") }} {{ $t(item.person) }}</div>
                                                <div class="small text-medium-emphasis">{{ $t("Timesheet.Dayoff.List.SearchForm.DeptName") }} {{ item.depName }}</div>
                                                <div class="small text-medium-emphasis"> {{ $t("Monitor.Time") }} {{ item.accessDateDayFormat }}</div>
                                            </td>
                                        </tr>
                                        
                                    </tbody>
                                </table>
                            </b-row>
                        </b-col>
                    </b-row>
                </b-card>
                <CModal :show.sync="eventDetailModal"
                        :no-close-on-backdrop="true"
                        :title="$t('Monitor.Detail')"
                        size="lg"
                        color="dark">
                    <b-row v-if="personCard">
                        <b-col md="6">
                            <img alt="No Image" style="width: 300px; height: 300px;" :src="personCard.getFaceUrl" />
                        </b-col>
                        <b-col md="6" class="text-left">
                            <b-row style="border-bottom: solid 1px; border-color: darkgray">
                                <b-col md="6"> {{ $t("Monitor.Name") }} </b-col>
                                <b-col md="6"> {{ personCard.person }} </b-col>
                            </b-row>
                            <b-row style="border-bottom: solid 1px; border-color: darkgray">
                                <b-col md="6"> {{ $t("Monitor.Gender") }} </b-col>
                                <b-col md="6"> {{ $t(personCard.genderStr) }} </b-col>
                            </b-row>
                            <b-row style="border-bottom: solid 1px; border-color: darkgray">
                                <b-col md="6"> {{ $t("Timesheet.Dayoff.List.SearchForm.DeptName") }} </b-col>
                                <b-col md="6"> {{ $t(personCard.depName) }} </b-col>
                            </b-row>
                            <b-row style="border-bottom: solid 1px; border-color: darkgray">
                                <b-col> {{ $t("Monitor.Time") }} </b-col>
                            </b-row>
                            <b-row>
                                <b-col md="6"> {{ $t('Reporting.Event.List.SearchForm.Day') }} </b-col>
                                <b-col md="6"> {{ $t('Reporting.Event.List.SearchForm.Hours') }} </b-col>
                            </b-row>
                            <b-row v-for="item in listEventTimeDetail">
                                <b-col md="6">
                                    <div> {{ item | formatDate }} </div>
                                </b-col>
                                <b-col md="6">
                                    <div> {{ item | formatTime }} </div>
                                </b-col>
                            </b-row>
                        </b-col>
                    </b-row>
                    <template v-slot:cell(genderStr)="row">
                        <span> {{ $t(personCard.genderStr) }} </span>
                    </template>
                    <template #header>
                        <h6 class="modal-title"> {{ $t('Monitor.Detail') }} </h6>
                        <CButtonClose @click="eventDetailModal = false" class="text-white" />
                    </template>
                    <template #footer>
                        <div></div>
                    </template>
                </CModal>
                <CModal :show.sync="chooseCameraModal"
                        :no-close-on-backdrop="false"
                        :title="$t('Monitor.FollowSelect')"
                        size="lg"
                        color="dark">
                    <b-form-group>
                        <b-row>
                            <b-col md="12">
                                <b-form-group :label="this.$t('Monitor.FollowDevice')"
                                              :label-cols="3"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="deviceId"
                                             :options="lstDeviceClone">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form-group>
                    <template #footer>
                        <div>
                            <b-form-group>
                                <b-button type="button"
                                          @click="loadStream"
                                          variant="primary"
                                          style="width: 120px">
                                    <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="cancelCamConfig"
                                          style="width: 120px">
                                    <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
                                </b-button>
                            </b-form-group>
                        </div>
                    </template>
                </CModal>
                <CModal :show.sync="updatePersonModal"
                        :no-close-on-backdrop="true"
                        :title="$t('Monitor.FollowChangeCustomer')"
                        size="lg"
                        color="dark">
                    <b-form-group @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="6" v-if="personCard">
                                        <img alt="No Image" style="width: 300px; height: 300px;" :src="personCard.getFaceUrl" />
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Name')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <b-form-input type="text"
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
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Phone')"
                                                      :label-cols="4"
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
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: true}"
                                                     v-model="person.gender"
                                                     :options="lstGender">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Birthday')"
                                                      :label-cols="4"
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
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-input type="text"
                                                          v-model="person.homeAddress">
                                            </b-form-input>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.PersonType')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     :settings="{allowClear: false}"
                                                     v-model="person.personType"
                                                     :options="lstpersonType">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                </b-row>

                            </b-col>
                        </b-row>
                    </b-form-group>
                    <template #footer>
                        <div>
                            <b-form-group>
                                <b-button type="submit"
                                          @click="save"
                                          variant="primary"
                                          style="width: 120px">
                                    <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="cancel"
                                          style="width: 120px">
                                    <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
                                </b-button>
                            </b-form-group>
                        </div>
                    </template>
                </CModal>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { authorizationMixin } from '@/shared/mixins'
    import moment from 'moment'
    import i18n from '@/libs/i18n'
    import { required } from 'vuelidate/lib/validators'
    import Services from '@/utils/services'

    export default {
        name: 'FilterMonitoring',
        mixins: [authorizationMixin],
        data() {
            return {
                listEvent: [
                    {
                        getFaceUrl: null,
                        accessDateDayFormat: null,
                        accessDateTimeFormat: null,
                        person: null,
                        personCode: null,
                        personPosition: null,
                        deptName: null,
                        errorCodeName: null,
                        accessTime: null,
                        countNumber: null,
                    }
                ],
                listEventGuess: [
                    {
                        getFaceUrl: null,
                        accessDateDayFormat: null,
                        accessDateTimeFormat: null,
                        person: null,
                        personCode: null,
                        personPosition: null,
                        deptName: null,
                        errorCodeName: null,
                        accessTime: null,
                        countNumber: null,
                    }
                ],
                listEventBlackList: [
                    {
                        getFaceUrl: null,
                        accessDateDayFormat: null,
                        accessDateTimeFormat: null,
                        person: null,
                        personCode: null,
                        personPosition: null,
                        deptName: null,
                        errorCodeName: null,
                        accessTime: null,
                        countNumber: null,
                    }
                ],
                //listPreOrder: [
                //    {
                //        guest: null,
                //        phoneNumber: null,
                //        amountPeople: null,
                //        timeOrder: null,
                //        timeInOut: null,
                //    }
                //],
                personCard: null,
                eventDetailModal: false,
                listEventTimeDetail: null,
                chooseCameraModal: false,
                updatePersonModal: false,
                person: {
                    homeAddress: null,
                    vaccine: null,
                    personId: null,
                    compId: null,
                    AvatarPath: null,
                    AvatarPath1: null,
                    compid: null,
                    personcode: null,
                    fullname: null,
                    phoneNumber: null,
                    personType: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    departmenT_ID: null,
                    file1: null,
                    fileData: null,
                    gender: null,
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
                    fromDate: null,
                    toDate: null,
                },
                lstGender: [
                    { id: '0', text: this.$t("Guess.Detail.Form.Male") },
                    { id: '1', text: this.$t("Guess.Detail.Form.Female") }
                ],
                lstpersonType: [
                    { id: '1', text: this.$t('Reporting.Event.List.Table.Employee') },
                    { id: '2', text: this.$t('Reporting.Event.List.Table.Customer') },
                    { id: '4', text: this.$t('Reporting.Event.List.Table.BlackFollow') },
                    { id: '5', text: this.$t('Reporting.Event.List.Table.Steal') },
                    { id: '6', text: this.$t('Reporting.Event.List.Table.Destructive') },
                ],
                deviceId: null,
                lstDevice: [],
                lstDeviceClone: [],
                indexCam: null,
                systemCode: 'BOTO',
                arrayOnline: [],
            };
        },
        validations: {
            person: {
                personCode: { required },
                fullName: { required },
                file1: { required },
            }
        },
        async created() {
            this._uid = 2000000002;
            this.loadFirst();
            this.loadDevice();
            await this.loadCamViewConfig();
        },
        methods: {
            loadFirst() {
                this.$services.get(`event/list5RealTime`).done((res) => {
                    this.listEvent = res.data;
                    // nhan vien
                });
                this.$services.get(`event/listGuessRealTime`).done((res) => {
                    this.listEventGuess = res.data;
                    // black list
                });
                this.$services.get(`event/listBlackListRealTime`).done((res) => {
                    this.listEventBlackList = res.data;
                });
                //this.$services.get(`event/listPreOrderRealTime`).done((res) => {
                //    this.listPreOrder = res.data;
                //    // chua dky
                //});
            },
            openEventDetailModal(item) {
                console.log(item);
                this.eventDetailModal = true;
                this.personCard = item;
                this.$services.get(`event/timeDetailPerson/` + item.personId).done((res) => {
                    this.listEventTimeDetail = res;
                });
            },
            closeEventDetailModal() {
                this.eventDetailModal = false;
            },
            handleShowModal(isShowModal, videoIndex) {
                this.chooseCameraModal = isShowModal;
                this.indexCam = videoIndex;
            },
            updatePerson(item) {
                this.updatePersonModal = true;
                this.personCard = item;
                this.$services.get(`person/${item.personId}`).done(res => {
                    this.person = res;
                });
            },
            formatPhone(e) {
                return String(e).substring(0, 10);
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`person/${this.person.personId}`, this.person).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            cancel() {
                this.updatePersonModal = false;
            },
            async loadDevice() {
                this.$services.get(`lookup/device`).done((res) => {
                    this.lstDevice = res;
                    this.lstDeviceClone = res;
                });
            },
            //Lấy danh sách các thiết bị chưa chọn
            reloadDevices() {
                var vm = this;
                this.lstDeviceClone = [...this.lstDevice];
                this.arrayOnline.forEach(function (value) {
                    let removeItem = vm.lstDeviceClone.find(
                        (x) => x.id === value.deviceId.toString()
                    );
                    if (removeItem) vm.lstDeviceClone.remove(removeItem);
                });
            },
            loadStream() {
                var deviceId = this.deviceId;
                if (deviceId != null) {
                    this.$services.get(`device/getDeviceOnCam/${deviceId}`).done(async (res) => {
                        if (this.indexCam == 1) {
                            const data = {
                                streamLink: res.rstpLink,
                                areaCode: res.areaCode,
                                cameraCode: res.deviceCode,
                            }
                            this.$refs.device7_1.submitSelected(data);

                            var idConfig = await this.findIdCamConfig(1);
                            var accessToken = Services.getAccessToken();
                            const dtoConfig = {
                                id: idConfig,
                                userId: accessToken.userId,
                                deviceId: deviceId,
                                position: 1,
                            }
                            let uid = {
                                deviceId: this.deviceId,
                                indexCam: this.indexCam,
                            };
                            this.arrayOnline.splice(this.arrayOnline.findIndex(x => x.indexCam = this.indexCam), 1);
                            this.arrayOnline.push(uid);
                            if (idConfig == 0) {
                                this.saveCreateCamConfig(dtoConfig);
                            } else {
                                this.saveCamConfig(dtoConfig.id, dtoConfig);
                            }
                            this.reloadDevices();
                        }
                        if (this.indexCam == 2) {
                            const data = {
                                streamLink: res.rstpLink,
                                areaCode: res.areaCode,
                                cameraCode: res.deviceCode,
                            }
                            this.$refs.device7_2.submitSelected(data);

                            var idConfig = await this.findIdCamConfig(2);
                            var accessToken = Services.getAccessToken();
                            const dtoConfig = {
                                id: idConfig,
                                userId: accessToken.userId,
                                deviceId: deviceId,
                                position: 2,
                            }
                            let uid = {
                                deviceId: this.deviceId,
                                indexCam: this.indexCam,
                            };
                            this.arrayOnline.splice(this.arrayOnline.findIndex(x => x.indexCam = this.indexCam), 1);
                            this.arrayOnline.push(uid);
                            if (idConfig == 0) {
                                this.saveCreateCamConfig(dtoConfig);
                            } else {
                                this.saveCamConfig(dtoConfig.id, dtoConfig);
                            }
                            this.reloadDevices();
                        }
                    });
                } else {
                    alert("Vui lòng chọn thiết bị");
                    //this.loadCamViewConfig();
                }
            },
            async loadCamViewConfig() {
                this.$services.get(`device/camViewConfigByUser`).done((res) => {
                    if (res[0]) {
                        const data = {
                            streamLink: res[0].rstpLink,
                            areaCode: res[0].areaCode,
                            cameraCode: res[0].deviceCode,
                        }
                        let uid = {
                            deviceId: res[0].deviceId,
                            indexCam: 1,
                        };
                        this.arrayOnline.push(uid);
                        this.$refs.device7_1.submitSelected(data);
                    }
                    if (res[1]) {
                        const data = {
                            streamLink: res[1].rstpLink,
                            areaCode: res[1].areaCode,
                            cameraCode: res[1].deviceCode,
                        }
                        let uid = {
                            deviceId: res[1].deviceId,
                            indexCam: 2,
                        };
                        this.arrayOnline.push(uid);
                        this.$refs.device7_2.submitSelected(data);
                    }
                    this.reloadDevices();
                });
            },
            findIdCamConfig(position) {
                return this.$services.get(`device/getIdViewConfigByUser/${position}`).done((res) => {
                    console.log(res);
                });
            },
            saveCreateCamConfig(dtoConfig) {
                this.$services.post(`device/createCamViewConfigByUser`, dtoConfig).done(() => {
                    this.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                    this.cancelCamConfig();
                });
            },
            saveCamConfig(idConfig, dtoConfig) {
                this.$services.put(`device/updateCamViewConfigByUser/${idConfig}`, dtoConfig).done(() => {
                    this.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                    this.cancelCamConfig();
                });
            },
            cancelCamConfig() {
                this.chooseCameraModal = false;
                this.deviceId = null;
            },
        },
    };
</script>

<style scoped>
    .headerBox {
        justify-content: center;
        background-color: white;
        font-size: 15px;
        font-weight: bold;
        position: sticky;
        top: 0;
        z-index: 999;
        background-color: cornflowerblue;
        color: white;
    }

    #style-scroll::-webkit-scrollbar-track {
        -webkit-box-shadow: inset 0 0 6px rgba(0,0,0,0.3);
        background-color: #F5F5F5;
    }

    #style-scroll::-webkit-scrollbar {
        width: 4px;
        background-color: #FFFFFF;
    }

    #style-scroll::-webkit-scrollbar-thumb {
        background-color: #727572;
    }

    .mydivouter {
        position: relative;
        /*background: #f90;
        width: 200px;
        height: 120px;
        margin: 0 auto;*/
    }

    .mydivoverlap {
        position: absolute;
        visibility: hidden;
        z-index: 99;
        bottom: 0px;
        left: 18px;
    }

    .mybuttonoverlap {
        position: absolute;
        visibility: hidden;
        z-index: 99;
        bottom: 0px;
        left: 50px;
    }

    .mydivouter:hover .mydivoverlap {
        visibility: visible;
    }

    .mydivouter:hover .mybuttonoverlap {
        visibility: visible;
    }

    .avatar-md {
        width: 3.5rem !important;
        height: 2.8rem !important;
        font-size: 1rem !important;
    }

    .avatar {
        position: relative;
        display: inline-flex;
        align-items: center;
        justify-content: center;
        vertical-align: middle;
        border-radius: 50em;
        transition: margin .15s;
        width: 2rem;
        height: 2rem;
        font-size: .8rem;
    }

    .avatar-img {
        width: 100%;
        height: auto;
    }

    img, svg {
        vertical-align: middle;
    }

    thead, tbody tr {
        display: table;
        width: 100%;
    }
</style>
