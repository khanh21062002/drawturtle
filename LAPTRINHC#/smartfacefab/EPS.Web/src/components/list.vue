<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <b-row>
                        <b-col md="2"></b-col>
                        <b-col md="4">
                            <b-form-group :label="this.$t('Functions.Monitoring.Form.SelectCompany')"
                                          :label-cols="4"
                                          :horizontal="true"
                                          label-align-md="right">
                                <treeselect :multiple="false"
                                            :options="treeselect.options"
                                            :placeholder="this.$t('PlaceHolder.Select')"
                                            v-model="companyId"
                                            @select="changeCompany($event)"
                                            :clearable="false" />
                            </b-form-group>
                        </b-col>
                        <b-col md="4">
                            <b-form-group :label="this.$t('Functions.Monitoring.Form.Select')"
                                          :label-cols="4"
                                          :horizontal="true"
                                          label-align-md="right">
                                <select2 :options="listCheckPoint"
                                         :placeholder="this.$t('PlaceHolder.Select')"
                                         :settings="{ allowClear: true }"
                                         @change="changeDisplay"
                                         v-model="checkPointId">
                                </select2>
                            </b-form-group>
                        </b-col>
                        <b-col md="2">
                            <b-button type="button" @click="save" variant="primary" :disabled="disableButton"><i class="fa fa-floppy-o"></i> {{ $t('Button.Submit') }}</b-button>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col md="4" style="padding: 0">
                            <div class="camera-list" style="display: flex; flex-flow: column">
                                <div style="height: calc(60% / 3); position: relative" class="flex-item">
                                    <Live ref="device7_1"
                                          :data="{device: lstDevices.deviceDoor,checkPointCode,compId: companyId,}"
                                          :videoIndex="1" />
                                </div>
                                <div style="height: calc(60% / 3); position: relative" class="flex-item">
                                    <Live ref="device7_2"
                                          :data="{device: lstDevices.deviceRight,checkPointCode,compId: companyId,}"
                                          :videoIndex="2" />
                                </div>
                                <div style="height: calc(60% / 3); position: relative" class="flex-item">
                                    <Live ref="device7_3"
                                          :data="{device: lstDevices.deviceTowHead,checkPointCode,compId: companyId,}"
                                          :videoIndex="3" />
                                </div>
                            </div>
                        </b-col>
                        <b-col md="4" style="padding: 0">
                            <div class="camera-list" style="display: flex; flex-flow: column">
                                <div style="height: calc(60% / 3); position: relative" class="flex-item">
                                    <Live ref="device7_4"
                                          :data="{device: lstDevices.deviceTop,checkPointCode,compId: companyId,}"
                                          :videoIndex="4" />
                                </div>
                                <div style="height: calc(60% / 3)" class="flex-item">
                                    <Live ref="device7_5"
                                          :data="{device: lstDevices.deviceLeft,checkPointCode,compId: companyId,}"
                                          :videoIndex="5" />
                                </div>
                                <div style="height: calc(60% / 3)" class="flex-item">
                                    <Live ref="device7_6"
                                          :data="{device: lstDevices.deviceRoomoc,checkPointCode,compId: companyId,}"
                                          :videoIndex="6" />
                                </div>
                            </div>
                        </b-col>
                        <b-col md="4" style="padding: 0">
                            <div class="camera-list" style="display: flex; flex-flow: column">
                                <div style="height: calc(60% / 3)" class="flex-item">
                                    <Live ref="device7_7"
                                          :data="{device: lstDevices.deviceFront,checkPointCode,compId: companyId,}"
                                          :videoIndex="7" />
                                </div>
                                <strong style="margin-left: 0.5rem">{{ this.$t('Functions.Monitoring.Table.Header') }}</strong>
                                <div style="
                                        margin: 0.5rem 0rem 0 0.3rem;
                                        border: 1px dashed #ccc;
                                        position: absolute;
                                        top: 36%;
                                        left: 0;
                                        bottom: 0;
                                        right: 2%;
                                        overflow: hidden;
                                        width: 99%;
                                    ">
                                    <b-table show-empty
                                             stacked="md"
                                             fixed
                                             responsive
                                             striped
                                             table-variant="light"
                                             hover
                                             bordered
                                             ref="eventTable"
                                             :items="gridOptions.items"
                                             :fields="gridOptions.fields"
                                             style="position: absolute;top: 15%; bottom: 0; left: 1%; right: 1%; height: 100%">
                                        <template v-slot:cell(trailerLicensePlate)="row">
                                            <b-link :to="{ path: `/functions/monitoring/detail/${row.item.id}`, }">
                                                <span :style="row.item.damagedCondition == true ? 'color: red' : 'color: #2eb85c'" class="col-form-label">
                                                    {{ $t(row.item.trailerLicensePlate) }}
                                                </span>
                                            </b-link>
                                        </template>
                                        <template v-slot:cell(tractorLicensePlate)="row">
                                            <span :style="row.item.damagedCondition == true ? 'color: red' : 'color: #2eb85c'"
                                                  class="col-form-label">
                                                {{ $t(row.item.tractorLicensePlate) }}
                                            </span>
                                        </template>
                                        <template v-slot:cell(containerSizeStr)="row">
                                            <span :style="row.item.damagedCondition == true ? 'color: red' : 'color: #2eb85c'">
                                                {{ $t(row.item.containerSizeStr) }}
                                            </span>
                                        </template>

                                        <template v-slot:cell(damagedConditionStr)="row">
                                            <span :style="row.item.damagedCondition == true ? 'color: red' : 'color: #2eb85c'"
                                                  class="col-form-label">
                                                {{ $t(row.item.damagedConditionStr) }}
                                            </span>
                                        </template>
                                    </b-table>
                                </div>
                            </div>
                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import Services from '@/utils/services';
    import i18n from '@/libs/i18n';

    const accessToken = Services.getAccessToken();
    const compId = accessToken.comId;
    export default {
        name: 'Functions.Monitoring.List',
        data: function () {
            return {
                searchForm: {
                    filterCompId: null,
                },
                disableButton: true,
                companyId: null,
                treeselect: {
                    value: null,
                    options: [],
                },
                checkPointId: null,
                checkPointCode: null,
                gridOptions: {
                    fields: [
                        {
                            key: 'timeInOutFormat',
                            label: this.$t('Functions.Monitoring.Table.TimeInOut'),
                            sortable: false,
                        },
                        { key: 'trailerLicensePlate', label: this.$t('Functions.Monitoring.Table.RoMooc'), sortable: false },
                        {
                            key: 'tractorLicensePlate',
                            label: this.$t('Functions.Monitoring.Table.TractorLicensePlate'),
                            sortable: false,
                        },
                        {
                            key: 'damagedConditionStr',
                            label: this.$t('Functions.Monitoring.Table.DamagedCondition'),
                            sortable: false,
                        },
                    ],
                    items: [],
                    sortBy: 'timeInOut',
                    sortDesc: true,
                    page: 1,
                    itemsPerPage: 10,
                },
                historyId: null,
                listCheckPoint: [],
                lstDevices: {
                    deviceDoor: null,
                    deviceTop: null,
                    deviceFront: null,
                    deviceRight: null,
                    deviceLeft: null,
                    deviceTowHead: null,
                    deviceRoomoc: null,
                },
                viewConfig: {
                    userId: 0,
                    currentView: 1,
                    strConfig: '',
                },
            };
        },
        beforeMount() { },
        mounted: function () { },
        async created() {
            this._uid = 2000000002;
            this.parent = this.$parent.$parent.$parent.$parent;
            var accessToken = this.$services.getAccessToken();
            this.companyId = accessToken.comId;
            this.loadConfig(accessToken.userId);
            this.loadCompanyTree();
            this.changeCompany({ id: accessToken.comId });
        },
        destroyed() {
            clearInterval(this.interval);
        },
        beforeDestroy() {
            // this.pauseVideoStream();
        },
        methods: {
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done((res) => {
                    vm.treeselect.options = res;
                });
            },
            changeCompany(value, checkPointId = null) {
                if (value && value.id) {
                    if (this.$refs.device7_1) {
                        //this.pauseVideoStream();
                    }
                    this.companyId = value.id;
                    this.checkPointId = checkPointId;
                    return this.$services.get(`lookup/checkPoint/${value.id}`).done((res) => {
                        this.listCheckPoint = res;
                        if (this.listCheckPoint) {
                            this.checkPointCode = this.listCheckPoint
                                .filter((x) => x.id == this.checkPointId)
                                .map((x) => x.code)
                                .join('');
                        }
                    });
                }
            },
            loadConfig(userId) {
                var vm = this;
                if (userId) {
                    return this.$services.get(`viewConfig/${userId}`).done((res) => {
                        if (res) {
                            vm.runConfig(res, vm);
                        }
                    });
                }
            },
            runConfig(item, vm) {
                this.checkPointId = null;
                this.companyId = item.compId;
                this.changeCompany({ id: item.compId }, item.checkPointId);
                this.loadDevicesByCheckPoint(item.checkPointId);
                this.loadHistoryInOutAreaList(item.checkPointId);
            },
            //Lấy danh sách thiết bị
            async loadDevicesByCheckPoint(id) {
                var vm = this;
                if (id) {
                    return this.$services.get(`lookup/device/checkPoint/${id}`).done((lstDevices) => {
                        if (lstDevices[0] !== null) {
                            vm.lstDevices.deviceFront = lstDevices.filter((x) => x.idDeviceFunction === 1)[[0]] || null;
                            vm.lstDevices.deviceDoor = lstDevices.filter((x) => x.idDeviceFunction === 2)[0] || null;
                            vm.lstDevices.deviceTop = lstDevices.filter((x) => x.idDeviceFunction === 3)[0] || null;
                            vm.lstDevices.deviceLeft = lstDevices.filter((x) => x.idDeviceFunction === 4)[0] || null;
                            vm.lstDevices.deviceRight = lstDevices.filter((x) => x.idDeviceFunction === 5)[0] || null;
                            vm.lstDevices.deviceTowHead = lstDevices.filter((x) => x.idDeviceFunction === 6)[0] || null;
                            vm.lstDevices.deviceRoomoc = lstDevices.filter((x) => x.idDeviceFunction === 7)[0] || null;
                            setTimeout(() => {
                                vm.playVideoStream();
                                this.disableButton = false;
                            }, 50);
                        }
                    });
                }
            },
            async changeDisplay(value) {
                //this.pauseVideoStream();
                if (value) {
                    this.checkPointCode = this.listCheckPoint
                        .filter((x) => x.value === value)
                        .map((x) => x.code)
                        .join('');
                    this.loadDevicesByCheckPoint(value);
                    await this.loadHistoryInOutAreaList(value);
                }
            },
            pauseVideoStream() {
                this.$refs.device7_1.stopVideoStream();
                this.$refs.device7_2.stopVideoStream();
                this.$refs.device7_3.stopVideoStream();
                this.$refs.device7_4.stopVideoStream();
                this.$refs.device7_5.stopVideoStream();
                this.$refs.device7_6.stopVideoStream();
                this.$refs.device7_7.stopVideoStream();
            },
            playVideoStream() {
                console.log('play video');
                this.$refs.device7_1.submitSelected();
                this.$refs.device7_2.submitSelected();
                this.$refs.device7_3.submitSelected();
                this.$refs.device7_4.submitSelected();
                this.$refs.device7_5.submitSelected();
                this.$refs.device7_6.submitSelected();
                this.$refs.device7_7.submitSelected();
            },
            save() {
                //this.srcTest = 'https://cam-live.atin.vn/CRD_1/DCTB_01.flv';
                var accessToken = this.$services.getAccessToken();
                this.viewConfig.userId = accessToken.userId;
                this.viewConfig.checkPointId = this.checkPointId;
                this.viewConfig.compId = this.companyId;
                const vm = this;
                this.$services.post('viewConfig', this.viewConfig).done((id) => {
                    vm.$toastr.s(i18n.t('Messages.CreateSuccess'));
                });
            },
            async loadHistoryInOutAreaList(checkPointId = null) {
                const vm = this;
                const checkPoint = checkPointId ? checkPointId : this.checkPointId;
                if (checkPoint) {
                    const filter = `?page=1&itemsPerPage=10&sortBy=timeInOut&sortDesc=true&filterCheckPoint=${checkPoint}`;
                    return this.$services.get(`historyInOutAreaPort/event${filter}`).done((res) => {
                        if (res.data.length > 0) {
                            vm.gridOptions.items = res.data;
                            vm.historyId = vm.gridOptions.items[0].id;
                        }
                    });
                }
            },
        },
    };
</script>
<style scoped>
    .modal-footer {
        justify-content: center !important;
    }

    .container {
        height: 200px;
        position: relative;
        margin-top: 50px;
    }

    .centerxxx {
        margin: 0;
        position: relative;
        top: 50%;
        left: 50%;
        -ms-transform: translate(-50%, -50%);
        transform: translate(-50%, -50%);
    }

    .table {
        width: 100%;
    }

    .center {
        margin: 0;
        position: absolute;
        top: 50%;
        left: 50%;
        -ms-transform: translate(-50%, -50%);
        transform: translate(-50%, -50%);
    }

    .vue-context-menu-list li {
        text-align: left;
        padding: 0px 30px 0px 3px;
        color: #fff;
        background: #4f6b99;
    }
</style>
