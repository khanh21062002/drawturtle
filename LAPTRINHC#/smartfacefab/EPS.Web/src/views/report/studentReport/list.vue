<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong> {{ $t('Reporting.StudentReport.List.Header') }} </strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row style="margin-left: 10px;">
                            <b-col md="4">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstcompany"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :disabled="true"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.compId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('School.Student.Detail.Label.Class')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstdepartment"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.deptId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Categories.GroupAccess.List.SearchForm.Machine')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstmachine"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.machineId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row style="margin-left: 10px;">
                            <b-col md="4">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DateFrom')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;"
                                                 type="datetime"
                                                 v-model="searchForm.filterDateRequestFrom"
                                                 valueType="YYYY-MM-DD HH:mm:ss"
                                                 format="DD-MM-YYYY HH:mm:ss">
                                    </date-picker>
                                    <b-form-input style="display: none;"
                                                  v-model="$v.searchForm.filterDateRequestFrom.$model"
                                                  :state="$v.searchForm.filterDateRequestFrom.$dirty? !$v.searchForm.filterDateRequestFrom.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t("Timesheet.OverTime.List.Form.DateFromRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.DateTo')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 100%;"
                                                 type="datetime"
                                                 v-model="searchForm.filterDateRequestTo"
                                                 valueType="YYYY-MM-DD HH:mm:ss"
                                                 format="DD-MM-YYYY HH:mm:ss">
                                    </date-picker>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Categories.Person.Detail.Label.Status')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="lstStatus"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="searchForm.status">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row style="margin-left: 10px;">
                            <b-col md="4">
                                <b-form-group :label="this.$t('School.Parent.Detail.Label.StudentName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.person">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('School.Parent.Detail.Label.Studentcode')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.personCode">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="4">
                                <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Temp')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <div class="range-wrap" style="top: 10px;">
                                        <input type="range"
                                               v-model="searchForm.filterTemp"
                                               class="range"
                                               min="35"
                                               max="45"
                                               step="0.1">
                                        <output class="bubble"></output>
                                    </div>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="7" offset="3">
                                <b-form-group :label-cols="3"
                                              style="margin-bottom: 0px !important;"
                                              label=""
                                              label-align-md="left">
                                    <b-button type="submit"
                                              variant="primary">
                                        {{ $t("Button.Search") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="warning"
                                              @click="refresh">
                                        {{ $t("Button.Refresh") }}
                                    </b-button>
                                    <b-button size="md"
                                              class="btn btn-success">
                                        <download-excel :fetch="exportData"
                                                        :fields="export_fields"
                                                        name="Điểm danh học sinh">
                                            {{ $t("Button.ExportExcel") }}
                                        </download-excel>
                                    </b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
                    </b-form>
                    <b-grid :searchForm="searchForm"
                            :gridOptions="gridOptions"
                            class="grid-border-top"
                            dataUrl="event/student"
                            gridName="Reporting.StudentReport.List.Table.TableName"
                            ref="eventTable">
                        <template v-slot:cell(personCode)="row">
                            <router-link :to="{ path: '/school/student/detail/' + row.item.personId}" target="_blank">
                                {{row.item.personCode}}
                            </router-link>
                        </template>
                        <template v-slot:cell(person)="row">
                            <router-link :to="{ path: '/school/student/detail/' + row.item.personId}" target="_blank">
                                {{row.item.person}}
                            </router-link>
                        </template>
                        <template v-slot:cell(genderName)="row">
                            <span class="col-form-label "> {{ $t(row.item.genderName) }}</span>
                        </template>
                        <template v-slot:cell(statusName)="row">
                            <span class="col-form-label "> {{ $t(row.item.statusName) }}</span>
                        </template>
                        <template v-slot:cell(avatar)="row">
                            <img v-bind:src="row.item.getFaceUrl"
                                 alt="No Image"
                                 width="60"
                                 height="60"
                                 style="max-width: 80px; max-height: 80px;">
                        </template>
                        <template v-slot:cell(temperatureFormat)="row">
                            <span class="col-form-label"
                                  v-if="row.item.isHighTemp"
                                  style="font-weight: bolder; color: red;">
                                {{ row.item.temperatureFormat }}
                            </span>
                            <span class="col-form-label"
                                  v-if="!row.item.isHighTemp">
                                {{ row.item.temperatureFormat }}
                            </span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <b-button size="sm"
                                          @click="loadEventDetail(row.item.eventId)"
                                          :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </b-grid>
                    <CModal :show.sync="darkModal"
                            :no-close-on-backdrop="true"
                            title="Thông tin chi tiết"
                            size="xl"
                            color="dark">
                        <b-row>
                            <b-col md="3">
                                <b-form-group label=""
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <div class="form-group">
                                        <img v-bind:src="event.getFaceUrl"
                                             alt="No Image"
                                             width="250"
                                             height="250"
                                             style="max-width: 250px; max-height: 250px;">
                                    </div>
                                </b-form-group>
                            </b-col>
                            <b-col md="8">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Student.Detail.Label.Class')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.deptName }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Parent.Detail.Label.Studentcode')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.personCode }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Parent.Detail.Label.Name')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.personName }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Day')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.accessDateDayFormat }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Hours')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.accessDateTimeFormat }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.IdentificationPont')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.scoreMatchFormat }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Temp')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"
                                                   v-if="event.isHighTemp"
                                                   style="font-weight: bolder; color: red;">
                                                {{ event.temperatureFormat }}
                                            </label>
                                            <label class="col-form-label"
                                                   v-if="!event.isHighTemp">
                                                {{ event.temperatureFormat }}
                                            </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.FaceMask')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ $t(event.wearMaskName) }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Status')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ $t(event.statusName) }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <template #footer>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="darkModal = false">
                                <i class="fa fa-ban"></i> {{ $t("Button.Cancel") }}
                            </b-button>
                        </template>
                    </CModal>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { authorizationMixin } from '@/shared/mixins'
    import moment from 'moment';
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'ListEvent',
        mixins: [authorizationMixin],
        data() {
            return {
                export_fields: {
                    'School.Parent.Detail.Label.Class': 'deptName',
                    'School.Class.List.Table.Code': 'personCode',
                    'School.Parent.Detail.Label.Namestudent': 'person',
                    'Categories.GroupAccess.List.Table.MachineName': 'machine',
                    'Reporting.Event.List.Table.Day': 'accessDateDayFormat',
                    'Reporting.Event.List.Table.Hours': 'accessDateTimeFormat',
                    'Reporting.Event.List.Table.FaceMask': 'wearMaskName',
                    'Reporting.Event.List.Table.Temp': 'temperatureFormat',
                    'Guess.List.Table.Status': 'statusName',
                },
                searchForm: {
                    personCode: null,
                    person: null,
                    filterDateRequestFrom: null,
                    filterDateRequestTo: null,
                    status: null,
                    acessType: null,
                    machineId: null,
                    groupId: null,
                    deptId: null,
                    personId: null,
                    filterTimeRequestFrom: null,
                    filterTimeRequestTo: null,
                    sortBy: "AccessTime",
                    filterTemp: 35,
                    personType: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: 'deptName',
                            label: this.$t('School.Parent.Detail.Label.Class'),
                            sortable: false,
                        },
                        {
                            key: 'personCode',
                            label: this.$t('Reporting.StudentReport.List.Table.Code'),
                            sortable: false,
                        },
                        {
                            key: 'person',
                            label: this.$t('School.Parent.Detail.Label.Namestudent'),
                            sortable: false,
                        },
                        {
                            key: 'machine',
                            label: this.$t('Categories.GroupAccess.List.Table.MachineName'),
                            sortable: false,
                        },
                        {
                            key: 'accessDateDayFormat',
                            label: this.$t('Reporting.Event.List.Table.Day'),
                            sortable: false,
                        },
                        {
                            key: 'accessDateTimeFormat',
                            label: this.$t('Reporting.Event.List.Table.Hours'),
                            sortable: false,
                        },
                        {
                            key: 'wearMaskName',
                            label: this.$t('Reporting.Event.List.Table.FaceMask'),
                            sortable: false,
                        },
                        {
                            key: 'temperatureFormat',
                            label: this.$t('Reporting.Event.List.Table.Temp'),
                            sortable: false,
                        },
                        {
                            key: 'statusName',
                            label: this.$t('Guess.List.Table.Status'),
                            sortable: false,
                        },
                        {
                            key: 'avatar',
                            label: this.$t('Guess.List.Table.Image'),
                            sortable: false,
                        },
                    ],
                    sortBy: 'machineId',
                    sortDesc: true,
                },
                lstPesonType: [],
                lstmachine: [],
                lstcompany: [],
                lstgroup: [],
                lstdepartment: [],
                lstperson: [],
                lstHD: [
                    {
                        id: '1001',
                        text: this.$t('Reporting.Event.List.Table.ListHD1'),
                    },
                    {
                        id: '1002',
                        text: this.$t('Reporting.Event.List.Table.ListHD2'),
                    },
                    {
                        id: '1003',
                        text: this.$t('Reporting.Event.List.Table.ListHD3'),
                    },
                    {
                        id: '1004',
                        text: this.$t('Reporting.Event.List.Table.ListHD4'),
                    },
                    {
                        id: '3001',
                        text: this.$t('Reporting.Event.List.Table.ListHD5'),
                    },
                    {
                        id: '0002',
                        text: this.$t('Reporting.Event.List.Table.ListHD6'),
                    },
                ],
                lstStatus: [
                    {
                        id: '1',
                        text: this.$t('Reporting.Event.List.Table.Status0'),
                    },
                    {
                        id: '0',
                        text: this.$t('Reporting.Event.List.Table.Status1'),
                    }
                ],
                isAdmin: false,
                darkModal: false,
                event: {
                    id: null,
                    code: null,
                    name: null,
                    note: null,
                    status: null,
                    deleted: null,
                    statuS_NAME: null,
                    supId: null
                },
                searchTemp: true,
            }
        },
        validations: {
            searchForm: {
                filterDateRequestFrom: {
                    checkDate(value) {
                        if (value && this.searchForm.filterDateRequestTo) {
                            var a = moment(value, 'YYYY-MM-DD HH:mm:ss');
                            var b = moment(this.searchForm.filterDateRequestTo, 'YYYY-MM-DD HH:mm:ss');
                            if (a > b) {
                                return false;
                            }
                            return true;
                        }
                        return true;
                    }
                }
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            var startWeek = moment().startOf('isoWeek').format('YYYY-MM-DD HH:mm:ss');
            this.searchForm.filterDateRequestFrom = startWeek;
            this.generateListPersonType();
            this.loadCompany();
            this.loadMachine();
            this.loadGroup();
            this.loadDepartment();
            this.loadPerson();
        },
        mounted() {
            this.initFormRangeInput();
        },
        methods: {
            generateListPersonType() {
                var vm = this;
                vm.lstPesonType = [];
                let item = {};
                item.id = 1;
                item.value = 1;
                item.text = this.$t('Reporting.Event.List.Table.Employee');
                vm.lstPesonType.push(item);
                let item2 = {};
                item2.id = 2;
                item2.value = 2;
                item2.text = this.$t('Reporting.Event.List.Table.Customer');
                vm.lstPesonType.push(item2);
            },
            async exportData() {
                var vm = this;
                var pagination = {
                    page: 1,
                    itemsPerPage: 99999,
                    sortBy: 'accessDate',
                    sortDesc: true
                };
                var formData = $.extend({}, pagination, vm.searchForm);
                var response = await this.$services.get(this.$refs.eventTable.dataUrl, formData);
                return response.data;
            },
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/classes`).done(res => {
                    vm.lstdepartment = res;
                })
            },
            async loadPerson() {
                var vm = this;
                return this.$services.get(`lookup/persons`).done(res => {
                    vm.lstperson = res;
                })
            },
            //Danh sách thiết bị
            async loadMachine() {
                var vm = this;
                return this.$services.get(`lookup/machines`).done(res => {
                    vm.lstmachine = res;
                })
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;

                });
            },
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done(res => {
                    vm.lstgroup = res;
                })
            },
            search() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    this.$refs.eventTable.refresh();
                }
            },
            remove(item, index, button) {
                var vm = this;
                if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                    this.$services.delete(`event/${item.id}`).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                        vm.search();
                    });
                }
            },
            removeSelected() {
                var vm = this;
                var selectedRows = this.$refs.eventTable.selectedRows;
                if (selectedRows.length > 0) {
                    if (confirm(i18n.t("Messages.ConfirmDelete"))) {
                        var data = selectedRows.map(x => x.id).join(',');
                        this.$services.delete(`event?ids=${data}`).done(() => {
                            vm.$toastr.s(i18n.t("Messages.ToastrDelete_s"));
                            vm.search();
                        });
                    }
                }
                else {
                    vm.$toastr.e(i18n.t("Messages.ToastrDelete_e"));
                }
            },
            async loadEventDetail(eventId) {
                var vm = this;
                vm.darkModal = true;
                return this.$services.get(`event/${eventId}`).done(event => {
                    vm.event = event;
                });
            },
            refresh() {
                var vm = this;
                vm.searchForm = {
                    personCode: null,
                    person: null,
                    filterDateRequestFrom: null,
                    filterDateRequestTo: null,
                    status: null,
                    acessType: null,
                    machineId: null,
                    groupId: null,
                    deptId: null,
                    personId: null,
                    filterTimeRequestFrom: null,
                    filterTimeRequestTo: null,
                    sortBy: "AccessTime",
                    filterTemp: 35,
                    personType: null,
                };
                var accessToken = Services.getAccessToken();
                this.searchForm.compId = accessToken.comId;
                var startWeek = moment().startOf('isoWeek').format('YYYY-MM-DD HH:mm:ss');
                this.searchForm.filterDateRequestFrom = startWeek;
                this.$refs.eventTable.refresh();
                this.initFormRangeInput();
            },
            initFormRangeInput() {
                const allRanges = document.querySelectorAll(".range-wrap");
                allRanges.forEach(wrap => {
                    const range = wrap.querySelector(".range");
                    const bubble = wrap.querySelector(".bubble");
                    range.addEventListener("input", () => {
                        setBubble(range, bubble);
                    });
                    setBubble(range, bubble);
                });
                function setBubble(range, bubble) {
                    const val = range.value;
                    const min = range.min ? range.min : 0;
                    const max = range.max ? range.max : 100;
                    const newVal = Number(((val - min) * 100) / (max - min));
                    bubble.innerHTML = val;
                    // Sorta magic numbers based on size of the native UI thumb
                    bubble.style.left = `calc(${newVal}% + (${8 - newVal * 0.15}px))`;
                }
            }
        }
    }
</script>

<style scoped>
    .range-wrap {
        position: relative;
        margin: 0 auto 3rem;
    }

    .range {
        width: 100%;
    }

    .bubble {
        background: red;
        color: white;
        padding: 4px 12px;
        position: absolute;
        border-radius: 4px;
        left: 50%;
        transform: translateX(-50%);
        top: 20px;
    }

        .bubble::after {
            content: "";
            position: absolute;
            width: 2px;
            height: 2px;
            background: red;
            left: 50%;
        }
</style>

