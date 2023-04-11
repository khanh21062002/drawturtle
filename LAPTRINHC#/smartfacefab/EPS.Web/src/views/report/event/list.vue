<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('Reporting.Event.List.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="search" class="form-search">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.Table.Company')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <treeselect :multiple="false"
                                                :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :options="treeselect.options"
                                                style="width: 98%"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="searchForm.compId" />
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
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Person.List.Table.Code')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.personCode"
                                                  style="width: 98%">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.PersonName')"
                                              label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  style="width: 98%"
                                                  :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="searchForm.filterText">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.ExportExcel.HeaderExcel.DateFrom')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 98%"
                                                 type="datetime"
                                                 v-model="searchForm.filterDateRequestFrom"
                                                 valueType="YYYY-MM-DD HH:mm"
                                                 format="DD-MM-YYYY HH:mm">
                                    </date-picker>
                                    <b-form-input style="display: none"
                                                  v-model="$v.searchForm.filterDateRequestFrom.$model"
                                                  :state="$v.searchForm.filterDateRequestFrom.$dirty ? !$v.searchForm.filterDateRequestFrom.$error : null">
                                    </b-form-input>
                                    <b-form-input style="display: none"
                                                  v-model="$v.searchForm.filterDateRequestFrom.$model"
                                                  :state="$v.searchForm.filterDateRequestFrom.$dirty ? !$v.searchForm.filterDateRequestFrom.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.searchForm.filterDateRequestFrom.required">
                                        {{ $t("Reporting.Event.List.SearchForm.DateRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback>
                                        {{ $t('Timesheet.OverTime.List.Form.DateFromRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Reporting.Event.List.ExportExcel.HeaderExcel.DateTo')"
                                              label-for="code"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <date-picker style="width: 98%"
                                                 type="datetime"
                                                 v-model="searchForm.filterDateRequestTo"
                                                 valueType="YYYY-MM-DD HH:mm"
                                                 format="DD-MM-YYYY HH:mm">
                                    </date-picker>
                                    <b-form-input style="display: none"
                                                  v-model="$v.searchForm.filterDateRequestTo.$model"
                                                  :state="$v.searchForm.filterDateRequestTo.$dirty ? !$v.searchForm.filterDateRequestTo.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.searchForm.filterDateRequestTo.required">
                                        {{ $t("Reporting.Event.List.SearchForm.DateRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="8" offset="1">
                                <b-form-group style="margin-bottom: 0px !important"
                                              :label-cols="4"
                                              label-align-md="right">
                                    <b-button type="submit"
                                              variant="primary"
                                              style="width: 120px">
                                        {{ $t('Button.Search') }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="warning"
                                              style="width: 120px"
                                              @click="refresh">{{ $t('Button.Refresh') }}</b-button>
                                    <b-button type="button"
                                              variant="success"
                                              style="width: 120px">
                                        <download-excel :header="headerExcelDetail"
                                                        :fetch="exportData"
                                                        :fields="export_fields"
                                                        name="Lich_Su_Ra_Vao">
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
                            dataUrl="event/listEvent"
                            :multiLanguage="true"
                            gridName="Reporting.Event.List.Table.TableName"
                            ref="eventTable">
                        <template v-slot:cell(personNameOther)="row">
                            <div v-if="(row.item.personType == 0 || row.item.personType == 2 || row.item.personType == 4)">
                                <router-link :to="{ path: '/categories/person/detail/' + row.item.personId + '?fromHistory=true' }">
                                    {{ row.item.personNameOther }}
                                </router-link>
                            </div>
                            <div v-if="row.item.personType == 1">
                                <router-link :to="{ path: '/categories/employee/detail/' + row.item.personId + '?fromHistory=true' }">
                                    {{ row.item.personNameOther }}
                                </router-link>
                            </div>
                        </template>
                        <template v-slot:cell(emloyeeOther)="row">
                            <div v-if="row.item.personType == 1">
                                <router-link :to="{ path: '/categories/person/detail/' + row.item.personId + '?fromHistory=true' }">
                                    {{ row.item.emloyeeOther }}
                                </router-link>
                            </div>
                            <div v-if="row.item.personType == 2">
                                <span class="col-form-label">
                                    {{ row.item.emloyeeOther }}
                                </span>
                            </div>
                        </template>
                        <template v-slot:cell(avatar)="row">
                            <img v-bind:src="row.item.getFaceUrl" alt="No Image" width="60" height="60" style="max-width: 80px; max-height: 80px" />
                        </template>
                        <template v-slot:cell(temperatureFormat)="row">
                            <span class="col-form-label" v-if="row.item.isHighTemp" style="font-weight: bolder; color: red">
                                {{ row.item.temperatureFormat }}
                            </span>
                            <span class="col-form-label" v-if="!row.item.isHighTemp">{{ row.item.temperatureFormat }}</span>
                        </template>
                        <template v-slot:cell(vaccineStr)="row">
                            <span class="col-form-label"
                                  style="display: inline-block; width: 140px"
                                  v-if="row.item.vaccine == null || row.item.vaccine == 0">
                                <i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}
                            </span>
                            <span class="bg-danger text-white p-8"
                                  style="padding: 8px; display: inline-block; width: 140px"
                                  v-if="row.item.vaccine == -1">
                                <i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}
                            </span>
                            <span class="bg-warning text-white p-8"
                                  style="padding: 8px; display: inline-block; width: 140px"
                                  v-if="row.item.vaccine == 1">
                                <i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}
                            </span>
                            <span class="bg-success text-white p-8"
                                  style="padding: 8px; display: inline-block; width: 140px"
                                  v-if="row.item.vaccine >= 2">
                                <i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}
                            </span>
                        </template>
                        <template v-slot:cell(wearMaskName)="row">
                            <span class="col-form-label"> {{ $t(row.item.wearMaskName) }} </span>
                        </template>
                        <template v-slot:cell(statusName)="row">
                            <span class="col-form-label"> {{ $t(row.item.statusName) }} </span>
                        </template>
                        <template v-slot:cell(genderStr)="row">
                            <span class="col-form-label"> {{ $t(row.item.genderStr) }} </span>
                        </template>
                        <template v-slot:cell(errorCodeName)="row">
                            <span class="col-form-label"> {{ $t(row.item.errorCodeName) }} </span>
                        </template>
                        <template v-slot:cell(personTypeStr)="row">
                            <span class="col-form-label"> {{ $t(row.item.personTypeStr) }} </span>
                        </template>
                        <template v-slot:cell(_actions)="row">
                            <b-button-group>
                                <!--<b-button size="sm" @click="loadEventDetail(row.item.eventId)" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>-->
                                <b-button size="sm" @click="openDetail(row.item.eventId)" :title="$t('Button.View')">
                                    <i class="fa fa-eye" aria-hidden="true"></i>
                                </b-button>
                            </b-button-group>
                        </template>
                    </KPGrid>
                    <CModal :show.sync="darkModal" :no-close-on-backdrop="true" title="Thông tin truy cập" size="xl" color="dark">
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
                                             style="max-width: 250px; max-height: 250px" />
                                    </div>
                                </b-form-group>
                            </b-col>
                            <b-col md="8">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.PersonCode')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.personCode }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Person')"
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
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.ErrorCode')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ $t(event.errorCodeName) }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.IdentificationPont')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.scoreMatchFormat }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.Temp')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label" v-if="event.isHighTemp" style="font-weight: bolder; color: red">
                                                {{ event.temperatureFormat }}
                                            </label>
                                            <label class="col-form-label" v-if="!event.isHighTemp"> {{ event.temperatureFormat }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.FaceMask')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ $t(event.wearMaskName) }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Categories.Person.Detail.Label.Status')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ $t(event.statusName) }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.EmployeeOther')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ $t(event.emloyeeOther) }} </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row v-if="event.isCheckPCCovid == true || event.isCheckPCCovid == 'true'">
                            <b-col offset="3" md="7">
                                <h2> {{ $t('Reporting.Event.List.Table.Information') }} </h2>
                            </b-col>
                        </b-row>
                        <b-row v-if="event.isCheckPCCovid == true || event.isCheckPCCovid == 'true'">
                            <b-col md="3"></b-col>
                            <b-col md="8">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('School.Parent.Detail.Label.Name')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label"> {{ event.pcCovidInfo.fullName }} </label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Gender')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label v-if="event.pcCovidInfo.gender == 'male'" class="col-form-label"> {{ $t('Guess.Detail.Form.Male') }} </label>
                                            <label v-if="event.pcCovidInfo.gender == 'female'" class="col-form-label">
                                                {{ $t('Guess.Detail.Form.Female') }}
                                            </label>
                                            <label v-if="event.pcCovidInfo.gender != 'female' && event.pcCovidInfo.gender != 'male'" class="col-form-label"></label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Birthday')" :label-cols="4" :horizontal="true" label-align-md="left">
                                            <label class="col-form-label">{{ event.pcCovidInfo.yearOfBirthday }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Guess.Detail.Form.Phone')" :label-cols="4" :horizontal="true" label-align-md="left">
                                            <label class="col-form-label">{{ event.pcCovidInfo.phone }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.NumberInjection')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label">{{ event.pcCovidInfo.numberInjection }}</label>
                                            <b-button style="margin-left: 10px"
                                                      type="button"
                                                      variant="info"
                                                      v-if="event.pcCovidInfo.numberInjection > 0 && showHistoryInjection == false"
                                                      @click="showHistoryInjection = true">
                                                <i class="fa fa-arrow-down"></i>{{ $t('Reporting.Event.List.SearchForm.HistoryInjection') }}
                                            </b-button>
                                            <b-button style="margin-left: 10px"
                                                      type="button"
                                                      variant="info"
                                                      v-if="event.pcCovidInfo.numberInjection > 0 && showHistoryInjection == true"
                                                      @click="showHistoryInjection = false">
                                                <i class="fa fa-arrow-up"></i>
                                                {{ $t('Reporting.Event.List.SearchForm.HistoryInjection') }}
                                            </b-button>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row v-if="showHistoryInjection == true">
                                    <b-table striped hover head-variant="light" :items="tableInjectionOptions.items" :fields="tableInjectionOptions.fields">
                                    </b-table>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Reporting.Event.List.SearchForm.PcCovidInfo')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <label class="col-form-label text-warning" style="font-weight: bolder" v-if="event.pcCovidInfo.lastTime == 0">
                                                <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                                                {{ $t('Reporting.Event.List.SearchForm.PcCovidInfoLast') }}
                                            </label>
                                            <label class="col-form-label" style="color: green; font-weight: bolder" v-if="event.pcCovidInfo.lastTime > 0">
                                                <i class="fa fa-check" aria-hidden="true"></i>
                                                {{ $t('Reporting.Event.List.SearchForm.PcCovidInfoLastTime') + event.pcCovidInfo.lastTimeFormat }})
                                            </label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <template #footer>
                            <b-button type="button" variant="secondary" @click="darkModal = false"><i class="fa fa-ban"></i>{{ $t('Button.Cancel') }}</b-button>
                        </template>
                        <template v-slot:cell(vaccineStr)="row">
                            <span class="col-form-label" style="display: inline-block; width: 140px" v-if="row.item.vaccine == null || row.item.vaccine == 0"><i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}</span>
                            <span class="bg-danger text-white p-8" style="padding: 8px; display: inline-block; width: 140px" v-if="row.item.vaccine == -1"><i class="fa fa-times"></i> {{ $t(row.item.vaccineStr) }}</span>
                            <span class="bg-warning text-white p-8" style="padding: 8px; display: inline-block; width: 140px" v-if="row.item.vaccine == 1"><i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}</span>
                            <span class="bg-success text-white p-8" style="padding: 8px; display: inline-block; width: 140px" v-if="row.item.vaccine >= 2"><i class="fa fa-check"></i> {{ $t(row.item.vaccineStr) }}</span>
                        </template>
                        <template v-slot:cell(wearMaskName)="row">
                            <span class="col-form-label">
                                {{ $t(row.item.wearMaskName) }}
                            </span>
                        </template>
                        <template v-slot:cell(statusName)="row">
                            <span class="col-form-label"> {{ $t(row.item.statusName) }}</span>
                        </template>
                        <template v-slot:cell(errorCodeName)="row">
                            <span class="col-form-label">
                                {{ $t(row.item.errorCodeName) }}
                            </span>
                        </template>
                        <template v-slot:cell(personTypeStr)="row">
                            <span class="col-form-label">
                                {{ $t(row.item.personTypeStr) }}
                            </span>
                        </template>
                    </CModal>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { authorizationMixin } from '@/shared/mixins';
    import { required } from 'vuelidate/lib/validators';
    import moment from 'moment';
    import Services from '@/utils/services';
    import i18n from '@/libs/i18n';

    export default {
        name: 'ListEvent',
        mixins: [authorizationMixin],
        data() {
            return {
                export_fields: {
                    'Reporting.Event.List.Table.Day': 'accessDateDayFormat',
                    'Timesheet.Dayoff.List.Table.PersonName': 'person',
                    'Guess.List.Table.Phone': 'phoneNumber',
                    'Reporting.Event.List.Table.PersonType': 'personTypeStr',
                    'QRCodePerson.Approve.GridField.Gender': 'genderStr',
                    //'Reporting.Event.List.Table.Area': 'areaName',
                    'Reporting.Event.List.Table.Company': 'compName',
                },
                deptId: null,
                areaId: null,
                machineId: null,
                searchForm: {
                    compId: null,
                    filterText: null,
                    filterPhone: null,
                    filterDateRequestFrom: null,
                    filterDateRequestTo: null,
                    personCode: null,
                    personType: null,
                    personTypeArray: [],
                    areaId: [],
                    valueThreshold: 0.7,
                    fileData: null,
                },
                gridOptions: {
                    fields: [
                        {
                            key: 'accessDateDayFormat',
                            label: this.$t('Reporting.Event.List.Table.Day'),
                            sortable: false,
                        },
                        {
                            key: 'personCode',
                            label: this.$t('Categories.Person.List.Table.Code'),
                            sortable: false,
                        },
                        {
                            key: 'person',
                            label: this.$t('Categories.Person.List.Table.Name'),
                            sortable: false,
                        },
                        {
                            key: 'personTypeStr',
                            label: this.$t('Reporting.Event.List.Table.PersonType'),
                            sortable: false,
                        },
                        {
                            key: 'depName',
                            label: this.$t('Reporting.Event.List.Table.Deparment'),
                            sortable: false,
                        },
                        {
                            key: 'areaName',
                            label: this.$t('Reporting.Event.List.Table.Area'),
                            sortable: false,
                        },
                        {
                            key: 'avatar',
                            label: this.$t('Guess.List.Table.Image'),
                            sortable: false,
                            tdClass: 'text-center',
                        },
                    ],
                    sortBy: 'accessTime',
                    sortDesc: true,
                },
                lstpersonType: [
                    //{
                    //    id: '-1',
                    //    text: this.$t('Search.All'),
                    //},
                    {
                        id: '1',
                        text: this.$t('Reporting.Event.List.Table.Employee'),
                    },
                    {
                        id: '2',
                        text: this.$t('Reporting.Event.List.Table.Unregistered'),
                    },
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
                lstGender: [
                    {
                        id: '0',
                        text: this.$t("Guess.Detail.Form.Male"),
                    },
                    {
                        id: '1',
                        text: this.$t("Guess.Detail.Form.Female"),
                    },
                    {
                        id: '-1',
                        text: this.$t("Guess.Detail.Form.Unknown"),
                    }
                ],
                lstgender: [],
                lstPesonType: [],
                lstmachine: [],
                lstcompany: [],
                lstgroup: [],
                lstdepartment: [],
                lstperson: [],
                lstArea: [],
                lstHD: [
                    {
                        id: '0012',
                        text: this.$t('Reporting.Event.List.Table.ListHD'),
                    },
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
                    {
                        id: '0018',
                        text: this.$t('Reporting.Event.List.Table.List0018'),
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
                    },
                ],
                isAdmin: false,
                darkModal: false,
                showHistoryInjection: false,
                tableInjectionOptions: {
                    fields: [
                        {
                            key: 'order',
                            label: this.$t('Reporting.Event.List.Table.Order'),
                            sortable: true,
                        },
                        {
                            key: 'injectionName',
                            label: this.$t('Reporting.Event.List.Table.VaccineType'),
                            sortable: true,
                        },
                        {
                            key: 'injectionAddress',
                            label: this.$t('Reporting.Event.List.Table.InjectionAddress'),
                            sortable: false,
                        },
                        {
                            key: 'injectTimeFormat',
                            label: this.$t('Reporting.Event.List.Table.InjectTimeFormat'),
                            sortable: true,
                        },
                    ],
                    items: [],
                },
                event: {
                    id: null,
                    code: null,
                    name: null,
                    note: null,
                    status: null,
                    deleted: null,
                    statuS_NAME: null,
                    supId: null,
                    isCheckPCCovid: false,
                    pcCovidInfo: {
                        fullName: null,
                        phone: null,
                        gender: null,
                        phone: null,
                        KBCN: {},
                        checkDeclaration: null,
                        checkReason: null,
                        checkinId: null,
                        configDeclaration: null,
                        contactHistory: null,
                        covidTest: {
                            expiredTime: null,
                            lastTime: null,
                            locationTest: null,
                            order: null,
                            result: null,
                            state: null,
                            techTest: null,
                        },
                        healthStatus: null,
                        historyInjection: {
                            expiredTime: null,
                            injectionAddress: null,
                            injectionName: null,
                            lastTime: null,
                            lotNumber: null,
                            order: null,
                            state: null,
                            injectTimeFormat: '',
                        },
                        historyInjectionList: [],
                        inEpidemicArea: null,
                        lastTime: null,
                        phone: null,
                        travelHistory: null,
                        userId: null,
                        userStatus: null,
                        yearOfBirthday: null,
                        numberInjection: 0,
                        lastTimeFormat: '',
                    },
                },
                searchTemp: true,
                treeselect: {
                    value: null,
                    options: [],
                },
                headerExcelDetail: [],
                format: {
                    double: 0.01
                },
                avatar: null,
                isSearch: false,
            };
        },
        validations: {
            searchForm: {
                filterDateRequestFrom: {
                    required,
                    checkDate(value) {
                        if (value && this.searchForm.filterDateRequestTo) {
                            var a = moment(value, 'YYYY-MM-DD HH:mm');
                            var b = moment(this.searchForm.filterDateRequestTo, 'YYYY-MM-DD HH:mm');
                            if (a > b) {
                                return false;
                            }
                            return true;
                        }
                        return true;
                    },
                },
                filterDateRequestTo: { required },
            },
        },
        beforeCreate() {
            if (localStorage.getItem('pageSt') != null &&
                localStorage.getItem('pageSt') != 'null'
                && localStorage.getItem('pageSt') != 'NaN') {
                this.pageLocal = localStorage.getItem('pageSt');
            } else {
                this.pageLocal = 1;
            }
            if (localStorage.getItem('itemsPerPageSt') != null &&
                localStorage.getItem('itemsPerPageSt') != 'null' &&
                localStorage.getItem('pageSt') != 'NaN'
            ) {
                this.itemsPerPageLocal = localStorage.getItem('itemsPerPageSt');
            } else {
                this.itemsPerPageLocal = 10;
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.searchForm.compId = accessToken.comId;
            var startWeek = moment().startOf('isoWeek').format('YYYY-MM-DD HH:mm');
            this.searchForm.filterDateRequestFrom = startWeek;
            this.searchForm.filterDateRequestTo = moment().format('YYYY-MM-DD 23:59');
            this.generateListPersonType();
            this.generateListGender();
            this.loadCompanyTree();
            this.loadArea();
            this.loadCompany();
            this.lastSearch();
        },
        mounted() {
            this.initFormRangeInput();
        },
        methods: {
            convertMilliSecondsToDate(timeInMillis) {
                if (timeInMillis != null) {
                    return moment(timeInMillis).format('DD/MM/YYYY HH:mm'); //parse integer
                }
                return '';
            },
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
                let item3 = {};
                item3.id = 0;
                item3.value = 0;
                item3.text = this.$t('Reporting.Event.List.Table.CustomerVip');
                vm.lstPesonType.push(item3);
                let item4 = {};
                item4.id = 4;
                item4.value = 4;
                item4.text = this.$t('Reporting.Event.List.Table.CustomerBlack');
                vm.lstPesonType.push(item4);
            },
            generateListGender() {
                var vm = this;
                vm.lstgender = [];
                let item = {};
                item.id = 0;
                item.value = 0;
                item.text = this.$t('Guess.Detail.Form.Male');
                vm.lstgender.push(item);
                let item2 = {};
                item2.id = 1;
                item2.value = 1;
                item2.text = this.$t('Guess.Detail.Form.Female');
                vm.lstgender.push(item2);
                let item3 = {};
                item3.id = -1;
                item3.value = -1;
                item3.text = this.$t('Guess.Detail.Form.Unknown');
                vm.lstgender.push(item3);
            },
            async exportData() {
                var vm = this;
                var pagination = {
                    page: 1,
                    itemsPerPage: 99999,
                    sortBy: 'accessDate',
                    sortDesc: true,
                };
                let compName = null;
                let deptName = [];
                let areaName = [];
                let deviceName = [];
                let statusName = null;
                let personTypeName = null;
                vm.lstcompany.forEach((company) => {
                    if (company.value == vm.searchForm.compId) {
                        compName = company.text;
                    }
                });
                if (compName == null) {
                    compName = vm.$t('Reporting.Event.List.ExportExcel.All');
                }
                if (vm.deptId != null && vm.deptId.length > 0) {
                    vm.deptId.forEach((deptSearchId) => {
                        vm.lstdepartment.forEach((department) => {
                            if (department.value == deptSearchId) {
                                deptName.push(department.text);
                            }
                        });
                    });
                } else {
                    deptName.push(vm.$t('Reporting.Event.List.ExportExcel.All'));
                }
                if (deptName.length == 0) {
                    deptName.push(vm.$t('Reporting.Event.List.ExportExcel.All'));
                }
                deptName = deptName.join(', ');
                //if (vm.areaId != null && vm.areaId.length > 0) {
                //    vm.areaId.forEach((areaSearchId) => {
                //        vm.lstArea.forEach((area) => {
                //            if (area.value == areaSearchId) {
                //                areaName.push(area.text);
                //            }
                //        });
                //    });
                //} else {
                //    areaName.push(vm.$t('Reporting.Event.List.ExportExcel.All'));
                //}
                //if (areaName.length == 0) {
                //    areaName.push(vm.$t('Reporting.Event.List.ExportExcel.All'));
                //}
                //areaName = areaName.join(', ');

                //vm.lstHD.forEach((errorCode) => {
                //    if (errorCode.value == vm.searchForm.errorCode) {
                //        errorCodeName = errorCode.text;
                //    } else {
                //        errorCodeName = vm.$t('Reporting.Event.List.ExportExcel.All');
                //    }
                //});
                if (vm.machineId != null && vm.machineId.length > 0) {
                    vm.machineId.forEach((machineSearchId) => {
                        vm.lstmachine.forEach((device) => {
                            if (device.value == machineSearchId) {
                                deviceName.push(device.text);
                            }
                        });
                    });
                } else {
                    deviceName.push(vm.$t('Reporting.Event.List.ExportExcel.All'));
                }
                if (deviceName.length == 0) {
                    deviceName.push(vm.$t('Reporting.Event.List.ExportExcel.All'));
                }
                deviceName = deviceName.join(', ');
                vm.lstStatus.forEach((status) => {
                    if (status.id == vm.searchForm.status) {
                        statusName = status.text;
                    }
                });
                if (statusName == null) {
                    statusName = vm.$t('Reporting.Event.List.ExportExcel.All');
                }
                vm.lstPesonType.forEach((personType) => {
                    if (personType.id == vm.searchForm.personType) {
                        personTypeName = personType.text;
                    }
                });
                if (personTypeName == null) {
                    personTypeName = vm.$t('Reporting.Event.List.ExportExcel.All');
                }
                let dateFrom = moment(this.searchForm.filterDateRequestFrom).format('DD/MM/YYYY HH:mm');
                let dateTo = moment(this.searchForm.filterDateRequestTo).format('DD/MM/YYYY HH:mm');
                if (dateFrom == 'Invalid date') {
                    dateFrom = moment('2020-01-01 00:00').format('DD/MM/YYYY HH:mm');
                }
                if (dateTo == 'Invalid date') {
                    dateTo = moment().format('DD/MM/YYYY HH:mm');
                }

                vm.headerExcelDetail = [
                    vm.$t('Reporting.Event.List.ExportExcel.HeaderExcel.Title'),
                    vm.$t('Reporting.Event.List.ExportExcel.HeaderExcel.Company') + compName,
                    vm.$t('Reporting.Event.List.ExportExcel.HeaderExcel.PersonType') +
                    personTypeName,
                    vm.$t('Reporting.Event.List.ExportExcel.HeaderExcel.DateFrom') +
                    dateFrom +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    '&nbsp;' +
                    vm.$t('Reporting.Event.List.ExportExcel.HeaderExcel.DateTo') +
                    dateTo,
                    '&nbsp;',
                ];
                var formData = $.extend({}, pagination, vm.searchForm);
                var response = await this.$services.post(this.$refs.eventTable.dataUrl, formData);
                for (var i = 0; i < response.data.length; i++) {
                    response.data[i].personTypeStr = this.$t(response.data[i].personTypeStr);
                    response.data[i].genderStr = this.$t(response.data[i].genderStr);
                }
                return response.data;
            },
            //Danh sách phòng ban
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/departments`).done((res) => {
                    vm.lstdepartment = res;
                });
            },
            //Danh sách phòng ban - tree view
            async loadDepartmentTree() {
                var vm = this;
                return this.$services.get(`lookup/department-tree`).done((res) => {
                    vm.treeselect.options = res;
                });
            },
            async loadPerson() {
                var vm = this;
                return this.$services.get(`lookup/persons`).done((res) => {
                    vm.lstperson = res;
                });
            },
            //Danh sách khu vực
            async loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done((res) => {
                    vm.lstArea = res;
                });
            },
            //Thay đổi danh sách thiết bị theo khu vực
            changeListMachine(value) {
                var vm = this;
                var valueStr;
                if (value.length != 0) {
                    valueStr = value.join(',');
                    return this.$services.get(`lookup/machinesByAreaId/` + valueStr).done((res) => {
                        vm.lstmachine = res;
                    });
                }
                else {
                    return this.$services.get(`lookup/machines`).done((res) => {
                        vm.lstmachine = res;
                    });
                }
            },
            //Danh sách thiết bị
            async loadMachine() {
                var vm = this;
                return this.$services.get(`lookup/machines`).done((res) => {
                    vm.lstmachine = res;
                });
            },
            //Danh sách công ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done(lstcompany => {
                    vm.treeselect.options = lstcompany;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách nhóm người
            async loadGroup() {
                var vm = this;
                return this.$services.get(`lookup/groups`).done((res) => {
                    vm.lstgroup = res;
                });
            },
            async loadEventDetail(eventId) {
                var vm = this;
                vm.darkModal = true;
                return this.$services.get(`event/${eventId}`).done((event) => {
                    vm.event = event;
                    if (vm.event.isCheckPCCovid == true || vm.event.isCheckPCCovid == 'true') {
                        //format pccovid info
                        if (vm.event.pcCovidInfo != null) {
                            vm.event.pcCovidInfo.numberInjection = vm.event.pcCovidInfo.historyInjectionList.length;
                        }
                        vm.tableInjectionOptions.items = vm.event.pcCovidInfo.historyInjectionList;
                        vm.tableInjectionOptions.items.forEach((element) => {
                            element.injectTimeFormat = vm.convertMilliSecondsToDate(element.lastTime);
                        });
                        if (vm.event.pcCovidInfo.lastTime > 0) {
                            vm.event.pcCovidInfo.lastTimeFormat = vm.convertMilliSecondsToDate(vm.event.pcCovidInfo.lastTime);
                        }
                    }
                });
            },
            refresh() {
                this.searchForm = {
                    compId: null,
                    filterText: null,
                    filterPhone: null,
                    filterDateRequestFrom: null,
                    filterDateRequestTo: null,
                    personType: -1,
                    areaId: [],
                    personTypeArray: [],
                    valueThreshold: 0.7,
                    fileData: null,
                };
                this.isSearch = false;
                this.avatar = null;
                var accessToken = Services.getAccessToken();
                this.searchForm.compId = accessToken.comId;
                var startWeek = moment().startOf('isoWeek').format('YYYY-MM-DD HH:mm:ss');
                this.searchForm.filterDateRequestFrom = startWeek;
                this.searchForm.filterDateRequestTo = moment().format('YYYY-MM-DD 23:59:59');
                this.setStorage();
                this.$refs.eventTable.refresh();
                this.initFormRangeInput();
            },
            initFormRangeInput() {
                const allRanges = document.querySelectorAll('.range-wrap');
                allRanges.forEach((wrap) => {
                    const range = wrap.querySelector('.range');
                    const bubble = wrap.querySelector('.bubble');
                    range.addEventListener('input', () => {
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
            },
            openDetail(val) {
                this.$router.push({ path: '/report/event/detail/' + val + '?fromHistory=true' });
            },
            search() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    this.setStorage();
                    this.$refs.eventTable.refresh();
                }
            },
            changePageLocal(val) {
                localStorage.setItem('pageSt', val);
            },
            changeItemsPerPageLocal(val) {
                localStorage.setItem('itemsPerPageSt', val);
            },
            setStorage() {
                localStorage.setItem('companySt', this.searchForm.compId);
                localStorage.setItem('fullNameSt', this.searchForm.filterText);
                localStorage.setItem('personTypeSt', this.searchForm.personType);
                localStorage.setItem('personTypeArraySt', this.searchForm.personTypeArray);
                localStorage.setItem('phoneNumberSt', this.searchForm.filterPhone);
                localStorage.setItem('dateFromSt', this.searchForm.filterDateRequestFrom);
                localStorage.setItem('dateToSt', this.searchForm.filterDateRequestTo);
                localStorage.setItem('personCodeSt', this.searchForm.personCode);
                
            },
            lastSearch() {
                if (localStorage.getItem('companySt') != '' && localStorage.getItem('companySt') != null) {
                    this.compId = localStorage.getItem('companySt').split`,`.map((x) => +x);
                    this.searchForm.compId = localStorage.getItem('companySt');
                } else {
                    const accessToken = Services.getAccessToken();
                    this.searchForm.compId = accessToken.comId;
                }

                //if (localStorage.getItem('personTypeSt') != 'null' && localStorage.getItem('personTypeSt') != null) {
                //    this.searchForm.personType = localStorage.getItem('personTypeSt');
                //} else {
                this.searchForm.personType = -1;
                //}

                if (localStorage.getItem('dateFromSt') != 'null' && localStorage.getItem('dateFromSt') != null) {
                    this.searchForm.filterDateRequestFrom = localStorage.getItem('dateFromSt');
                } else {
                    var startWeek = moment().startOf('isoWeek').format('YYYY-MM-DD HH:mm:ss');
                    this.searchForm.filterDateRequestFrom = startWeek;
                }

                if (localStorage.getItem('dateToSt') != 'null' && localStorage.getItem('dateToSt') != null) {
                    this.searchForm.filterDateRequestTo = localStorage.getItem('dateToSt');
                }

                if (localStorage.getItem('fullNameSt') != 'null') {
                    this.searchForm.filterText = localStorage.getItem('fullNameSt');
                }

                if (localStorage.getItem('phoneNumberSt') != 'null') {
                    this.searchForm.filterPhone = localStorage.getItem('phoneNumberSt');
                }

                if (localStorage.getItem('personTypeArraySt') != 'null') {
                    this.searchForm.personTypeArra = localStorage.getItem('personTypeArraySt');
                }

                if (localStorage.getItem('personCodeySt') != 'null') {
                    this.searchForm.personCode = localStorage.getItem('personCodeySt');
                }
                
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
                    this.$refs.eventTable.refresh();
                }
                else {
                    this.isSearch = true;
                }
            },
            onChange: function (evt) {
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
                //vm.person.file1 = f.name;
            },
        },
    };
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
            content: '';
            position: absolute;
            width: 2px;
            height: 2px;
            background: red;
            left: 50%;
        }

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
