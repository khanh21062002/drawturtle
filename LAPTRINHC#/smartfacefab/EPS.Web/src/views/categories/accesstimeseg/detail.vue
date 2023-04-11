<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Accesstimeseg.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="5">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="2"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                   v-model="accesstimeseg.compId" :options="lstcompany">
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                            <b-col md="5" offset="1">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.List.SearchForm.DateName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" v-if="editing" id="txt_TimeSegName" :placeholder="this.$t('PlaceHolder.SearchInput')"
                                                  v-model="$v.accesstimeseg.timeSegName.$model"
                                                  :state="$v.accesstimeseg.timeSegName.$dirty? !$v.accesstimeseg.timeSegName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Accesstimeseg.Create.Require.TimeSegNameRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.timeSegName }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;"> {{$t('Common.DayOfWeek.Mon')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayStart3" id="ID" type="text" v-if="editing" placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.mondayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Tue')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayStart3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.tuesdayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Wed')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayStart3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.wednesdayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Thu')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayStart3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.thursdayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Fri')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayStart3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.fridayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Sat')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayStart3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.saturdayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Sun')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayStart1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayStart1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayStart2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayStart2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayStart3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayStart3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayStart4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayStart4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayEnd1" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayEnd1 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayEnd2" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayEnd2 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayEnd3" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayEnd3 }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayEnd4" id="ID" type="text" v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                    <label class="col-form-label" v-if="!editing">{{ accesstimeseg.sundayEnd4 }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageAccessTimeSeg'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                        </div>
                    </b-form>
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
        name: 'AccessTimeSegDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                accesstimeseg: {
                    compid: null,
                    deptId: 0,
                    groupId: 0,
                    timeSegName: null,
                    sundaystart1: null,
                    sundayend1: null,
                    sundaystart2: null,
                    sundayend2: null,
                    sundaystart3: null,
                    sundayend3: null,
                    sundaystart4: null,
                    sundayend4: null,
                    mondaystart1: null,
                    mondayend1: null,
                    mondaystart2: null,
                    mondayend2: null,
                    mondaystart3: null,
                    mondayend3: null,
                    mondaystart4: null,
                    mondayend4: null,
                    tuesdaystart1: null,
                    tuesdayend1: null,
                    tuesdaystart2: null,
                    tuesdayend2: null,
                    tuesdaystart3: null,
                    tuesdayend3: null,
                    tuesdaystart4: null,
                    tuesdayend4: null,
                    wednesdaystart1: null,
                    wednesdayend1: null,
                    wednesdaystart2: null,
                    wednesdayend2: null,
                    wednesdaystart3: null,
                    wednesdayend3: null,
                    wednesdaystart4: null,
                    wednesdayend4: null,
                    thursdaystart: null,
                    thursdayend1: null,
                    thursdaystart2: null,
                    thursdayend2: null,
                    thursdaystart3: null,
                    thursdayend3: null,
                    thursdaystart4: null,
                    thursdayend4: null,
                    fridaystart1: null,
                    fridayend1: null,
                    fridaystart2: null,
                    fridayend2: null,
                    fridaystart3: null,
                    fridayend3: null,
                    fridaystart4: null,
                    fridayend4: null,
                    saturdaystart1: null,
                    saturdayend1: null,
                    saturdaystart2: null,
                    saturdayend2: null,
                    saturdaystart3: null,
                    saturdayend3: null,
                    saturdaystart4: null,
                    saturdayend4: null
                }, lstdepartment: [], lstgroup: [], lstcompany: [],
                editing: false,
                isAdmin: false,
            }
        },
        computed: {
            accesstimesegId() {
                return this.$route.params.accesstimesegId;
            },
        },
        validations: {
            accesstimeseg: {
                timeSegName: { required }
               
            }
        },
        async created() {
            await this.loadCompany();
            await this.loadAccessTimeSegDetail();
            await this.loadDepartment();
            await this.loadGroup();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách vung truy nhap
            async loadDepartment() {
                var vm = this;
                return this.$services.get(`lookup/all-departments`).done(lstdepartment => {
                    vm.lstdepartment = lstdepartment;

                })
            }, //Danh sách vung thiet bị
            async loadGroup() {
                var vm = this;
                if (vm.isAdmin) {
                    return this.$services.get(`lookup/groups`).done(lstgroup => {
                        vm.lstgroup = lstgroup;
                    })
                } else {
                    var company = vm.lstcompany[0];
                    return this.$services.get(`lookup/groups/` + company.id).done(lstgroup => {
                        vm.lstgroup = lstgroup;
                    })
                }
            },
            async loadAccessTimeSegDetail() {
                var vm = this;
                return this.$services.get(`accesstimeseg/${this.accesstimesegId}`).done(accesstimeseg => {
                    vm.accesstimeseg = accesstimeseg;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/accesstimeseg/list' });
            },
            async cancel() {
                var vm = this;
                this.loadAccessTimeSegDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`accesstimeseg/${this.accesstimesegId}`, this.accesstimeseg).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
            changeGroup(deptId) {
                var vm = this;
                vm.accesstimeseg.groupId = null;
                return this.$services.get(`lookup/group-by-dept/${deptId}`).done(lstgroup => {
                    vm.lstgroup = lstgroup;
                })
            }
        }
    }
</script>
<style scoped>
</style>
