<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Báo cáo chấm công</strong>
                    </div>
                    <b-form @submit.prevent="search">
                        <b-row style="margin-left: 30px;">
                            <b-col md="3">
                                <b-form-group label="Đơn vị :"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany" @change="loadShiftTimeByCompId($event)" placeholder="Chọn giá trị" id="dpl_compId" :disabled="!isAdmin"
                                             v-model="searchForm.filterCompId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="3" offset="1">
                                <b-form-group label="Phòng ban:"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstdepartment" placeholder="Chọn giá trị" id="dpl_departmentid" :settings="{allowClear: true}"
                                             v-model="searchForm.filterDeptId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="3" offset="1">
                                <b-form-group label="Tên/ Mã nhân sự:"
                                              label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="name"
                                                  placeholder="Tìm kiếm tên/mã nhân sự ..."
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row style="margin-left: 30px;">
                            <b-col md="3">
                                <b-form-group label="Từ ngày:"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateFrom" id="filterDateFrom" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3" offset="1">
                                <b-form-group label="Đến ngày:"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;" v-model="searchForm.filterDateTo" id="filterDateTo" valueType="YYYY-MM-DD" format="DD-MM-YYYY"></date-picker>
                                </b-form-group>

                            </b-col>

                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group label="" :label-cols="4">
                                    <b-button type="submit" variant="primary">Tìm kiếm</b-button>
                                    <b-button size="md" class="btn btn-success">
                                        <download-excel :fetch="exportData" :fields="export_fields" name="Bao_cao_cham_cong">
                                            Xuất Excel
                                        </download-excel>
                                    </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <br />
                    <div id="shift-time">
                        <label style="font-weight:bold; font-size: 18px">Giờ làm việc:</label>
                        <label style=" margin-left: 30px;font-size: 18px"> {{ shifttime.beginShiftTime }}  </label>
                        <label style=" margin-left: 10px;font-size: 18px"> ~  </label>
                        <label style=" margin-left: 10px;font-size: 18px"> {{ shifttime.endShiftTime }}  </label>
                        <label style="margin-left: 30px; font-weight: bold; font-size: 18px">Giờ giải lao:</label>
                        <label style=" margin-left: 30px;font-size: 18px"> {{ shifttime.beginFreeShiftTime }}  </label>
                        <label style=" margin-left: 10px;font-size: 18px"> ~  </label>
                        <label style=" margin-left: 10px;font-size: 18px"> {{ shifttime.endFreeShiftTime }}  </label>
                    </div>
                   
                    <b-grid :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            gridName="Danh sách"
                            dataUrl="event/getReportWorkingTime"
                            ref="eventTable">
                  
                    </b-grid>

                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins'
    import moment from 'moment'
    import Services from '@/utils/services'
    export default {
        name: 'ListWorkingTime',
        mixins: [authorizationMixin],
        data() {
            return {
                export_fields: {
                    'Họ và tên': 'fullName',
                    'Phòng ban': 'deptName',
                    'Ngày': 'dayFormat',
                    'Thứ': 'dayOfWeekInVietnamese',
                    'Giờ vào': 'startAccessTimeFormat',
                    'Giờ ra': 'endAccessTimeFormat',
                    'Muộn': 'lateTime',
                    'Sớm': 'earlyTime', 
                    'Tổng giờ ': 'totalTimeFormat', 
                },
                searchForm: {
                    filterCompId: null,
                    filterDeptId: null,
                    filterText: null,
                    filterDateFrom: null,
                    filterDateTo: null,
                },
                gridOptions: {
                    fields: [
                        { key: 'fullName', label: 'Họ và tên', sortable: false },
                        { key: 'deptName', label: 'Phòng ban', sortable: false },
                        { key: 'dayFormat', label: 'Ngày', sortable: false },
                        { key: 'dayOfWeekInVietnamese', label: 'Thứ', sortable: false },
                        { key: 'startAccessTimeFormat', label: 'Giờ vào', sortable: false },
                        { key: 'endAccessTimeFormat', label: 'Giờ ra', sortable: false },
                        { key: 'lateTime', label: 'Muộn', sortable: false },
                        { key: 'earlyTime', label: 'Sớm ', sortable: false },
                        { key: 'overTime', label: 'Ngoài giờ ', sortable: false },
                        { key: 'totalTimeFormat', label: 'Tổng giờ ', sortable: false },

                    ],
                    sortDesc: true,
                },
                shifttime: {
                    beginShiftTime: '',

                    endShiftTime: '',

                    beginFreeShiftTime: '',

                    endFreeShiftTime: '',
                },
                lstmachine: [],
                lstcompany: [],
                lstgroup: [],
                lstdepartment: [],
                lstperson: [],

                isAdmin: false,
                darkModal: false,
                isDefaultTime: false,
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            //this.searchForm.department_ID = accessToken.unitId;
            this.searchForm.filterCompId  = accessToken.comId;
            var startWeek = moment().startOf('isoWeek').format('YYYY-MM-DD');
            this.searchForm.filterDateFrom = startWeek;
            //this.search();

            await this.loadCompany();
            await this.loadDepartment();
            await this.loadShiftTimeByCompId(this.searchForm.filterCompId);
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                   
                });
            },
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;
                })
            },
            async loadShiftTimeByCompId(compId) {
                var vm = this;
                return this.$services.get(`categories/shifttime/loadShiftTimeByCompId/${compId}`).done(shifttime => {
                    if (shifttime) {
                        vm.shifttime = shifttime;
                    } else {
                        vm.isDefaultTime = true;
                        vm.shifttime = {
                            beginShiftTime: '',

                            endShiftTime: '',

                            beginFreeShiftTime: '',

                            endFreeShiftTime: '',
                        };
                    }
                   
                });
            },
            async exportData() {
                var vm = this;
                var pagination = {
                    page: 1,
                    itemsPerPage: 99999,
                    sortBy: '',
                    sortDesc: false
                };

                var formData = $.extend({}, pagination, vm.searchForm);

                var response = await this.$services.get(this.$refs.eventTable.dataUrl, formData);
                return response.data;
                //return this.$refs.eventTable._data.sourceTable;
            },
            search() {
                this.$refs.eventTable.refresh();
            },
        }
    }
</script>
<style scoped>
</style>




