<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Accesstimeseg.Create.Header")}} </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.accesstimeseg.compid.$model"
                                                  :state="$v.accesstimeseg.compid.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstcompany" :disabled="!showCompany"
                                             v-model="$v.accesstimeseg.compid.$model" :state="$v.accesstimeseg.compid.$dirty? !$v.accesstimeseg.compid.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        Vui lòng chọn đơn vị
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.List.SearchForm.DateName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="txt_TimeSegName"
                                                  v-model="$v.accesstimeseg.timesegname.$model"
                                                  :state="$v.accesstimeseg.timesegname.$dirty? !$v.accesstimeseg.timesegname.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Accesstimeseg.Create.Require.TimeSegNameRequire')}}
                                    </b-form-invalid-feedback>
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
                                    <vue-timepicker v-model="accesstimeseg.mondaystart1" id="ID" type="time" placeholder="00:00" formart="00:00 A"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')" formart="hh:mm A"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.MondayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.mondayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
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
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdaystart1" id="ID" type="text" placeholder="00:00"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.TuesdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.tuesdayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
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
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdaystart1" id="ID" type="text" placeholder="00:00"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.WednesdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.wednesdayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
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
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdaystart1" id="ID" type="text" placeholder="00:00"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.ThursdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.thursdayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
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
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridaystart1" id="ID" type="text" placeholder="00:00"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.FridayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.fridayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
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
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdaystart1" id="ID" type="text" placeholder="00:00"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SaturdayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.saturdayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="grid-border-top">
                            <b-col>
                                <h2 style="font-weight: bolder;">{{$t('Common.DayOfWeek.Sun')}}</h2>
                            </b-col>
                        </b-row>
                        <b-row >
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundaystart1" id="ID" type="text" placeholder="00:00"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundaystart2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundaystart3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayStart4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundaystart4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd1')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayend1" id="ID" type="text" placeholder="23:59"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd2')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayend2" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd3')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayend3" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                            <b-col md="3">
                                <b-form-group :label="this.$t('Categories.Accesstimeseg.Detail.Label.SundayEnd4')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <vue-timepicker style="width: 100%;" v-model="accesstimeseg.sundayend4" id="ID" type="text" :placeholder="this.$t('PlaceHolder.Select')"></vue-timepicker>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <!--<b-row>
        <b-col md="3">
            <b-form-group label="Trạng thái:"
                          :label-cols="4"
                          :horizontal="true"
                          label-align-md="left">
                <b-form-select class="col-sm-12" placeholder="0-1"
                               v-model="accesstimeseg.status">
                    <b-form-select-option value="1">Hoạt động</b-form-select-option>
                    <b-form-select-option value="0">Ngừng hoạt động</b-form-select-option>
                </b-form-select>
            </b-form-group>
        </b-col>
    </b-row>-->
                        <b-row >
                            <b-col class="text-center">
                                <b-form-group >
                                    <b-button v-if="authorize(['ManageAccessTimeSeg'])" @click="save" id="submit_save" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                </b-form-group>
                            </b-col>
                        </b-row>
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
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'

    export default {
        name: 'AccessTimeSegDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                accesstimeseg: {
                    status: null,
                    compid: null,
                    deptId: 0,
                    groupId: 0,
                    timesegname: null,
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
                    thursdaystart1: null,
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
                showCompany: false,
            }
        },
        validations: {
            accesstimeseg: {
                groupId: { required },
                compid: { required },
                deptId: { required },
                timesegname: { required }
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.accesstimeseg.compid = accessToken.comId;
            await this.loadCompany();
            await this.loadDepartment();
            await this.loadGroup();
            this.accesstimeseg.status = 1;
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
            },
            //Danh sách vung thiet bị
            async loadGroup() {
                var vm = this;
                vm.lstgroup = [];
            },
            back() {
                this.$router.push({ path: '/categories/accesstimeseg/list' });
            },
            save() {
                $("#submit_save").attr("disabled", false);
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    this.accesstimeseg.mondaystart1 = formatDateTime(this.accesstimeseg.mondaystart1);
                    this.accesstimeseg.mondaystart2 = formatDateTime(this.accesstimeseg.mondaystart2);
                    this.accesstimeseg.mondaystart3 = formatDateTime(this.accesstimeseg.mondaystart3);
                    this.accesstimeseg.mondaystart4 = formatDateTime(this.accesstimeseg.mondaystart4);

                    this.accesstimeseg.mondayend1 = formatDateTime1(this.accesstimeseg.mondayend1);
                    this.accesstimeseg.mondayend2 = formatDateTime(this.accesstimeseg.mondayend2);
                    this.accesstimeseg.mondayend3 = formatDateTime(this.accesstimeseg.mondayend3);
                    this.accesstimeseg.mondayend4 = formatDateTime(this.accesstimeseg.mondayend4);

                    this.accesstimeseg.tuesdaystart1 = formatDateTime(this.accesstimeseg.tuesdaystart1);
                    this.accesstimeseg.tuesdaystart2 = formatDateTime(this.accesstimeseg.tuesdaystart2);
                    this.accesstimeseg.tuesdaystart3 = formatDateTime(this.accesstimeseg.tuesdaystart3);
                    this.accesstimeseg.tuesdaystart4 = formatDateTime(this.accesstimeseg.tuesdaystart4);

                    this.accesstimeseg.tuesdayend1 = formatDateTime1(this.accesstimeseg.tuesdayend1);
                    this.accesstimeseg.tuesdayend2 = formatDateTime(this.accesstimeseg.tuesdayend2);
                    this.accesstimeseg.tuesdayend3 = formatDateTime(this.accesstimeseg.tuesdayend3);
                    this.accesstimeseg.tuesdayend4 = formatDateTime(this.accesstimeseg.tuesdayend4);

                    this.accesstimeseg.wednesdaystart1 = formatDateTime(this.accesstimeseg.wednesdaystart1);
                    this.accesstimeseg.wednesdaystart2 = formatDateTime(this.accesstimeseg.wednesdaystart2);
                    this.accesstimeseg.wednesdaystart3 = formatDateTime(this.accesstimeseg.wednesdaystart3);
                    this.accesstimeseg.wednesdaystart4 = formatDateTime(this.accesstimeseg.wednesdaystart4);

                    this.accesstimeseg.wednesdayend1 = formatDateTime1(this.accesstimeseg.wednesdayend1);
                    this.accesstimeseg.wednesdayend2 = formatDateTime(this.accesstimeseg.wednesdayend2);
                    this.accesstimeseg.wednesdayend3 = formatDateTime(this.accesstimeseg.wednesdayend3);
                    this.accesstimeseg.wednesdayend4 = formatDateTime(this.accesstimeseg.wednesdayend4);

                    this.accesstimeseg.thursdaystart1 = formatDateTime(this.accesstimeseg.thursdaystart1);
                    this.accesstimeseg.thursdaystart2 = formatDateTime(this.accesstimeseg.thursdaystart2);
                    this.accesstimeseg.thursdaystart3 = formatDateTime(this.accesstimeseg.thursdaystart3);
                    this.accesstimeseg.thursdaystart4 = formatDateTime(this.accesstimeseg.thursdaystart4);

                    this.accesstimeseg.thursdayend1 = formatDateTime1(this.accesstimeseg.thursdayend1);
                    this.accesstimeseg.thursdayend2 = formatDateTime(this.accesstimeseg.thursdayend2);
                    this.accesstimeseg.thursdayend3 = formatDateTime(this.accesstimeseg.thursdayend3);
                    this.accesstimeseg.thursdayend4 = formatDateTime(this.accesstimeseg.thursdayend4);

                    this.accesstimeseg.fridaystart1 = formatDateTime(this.accesstimeseg.fridaystart1);
                    this.accesstimeseg.fridaystart2 = formatDateTime(this.accesstimeseg.fridaystart2);
                    this.accesstimeseg.fridaystart3 = formatDateTime(this.accesstimeseg.fridaystart3);
                    this.accesstimeseg.fridaystart4 = formatDateTime(this.accesstimeseg.fridaystart4);

                    this.accesstimeseg.fridayend1 = formatDateTime1(this.accesstimeseg.fridayend1);
                    this.accesstimeseg.fridayend2 = formatDateTime(this.accesstimeseg.fridayend2);
                    this.accesstimeseg.fridayend3 = formatDateTime(this.accesstimeseg.fridayend3);
                    this.accesstimeseg.fridayend4 = formatDateTime(this.accesstimeseg.fridayend4);

                    this.accesstimeseg.saturdaystart1 = formatDateTime(this.accesstimeseg.saturdaystart1);
                    this.accesstimeseg.saturdaystart2 = formatDateTime(this.accesstimeseg.saturdaystart2);
                    this.accesstimeseg.saturdaystart3 = formatDateTime(this.accesstimeseg.saturdaystart3);
                    this.accesstimeseg.saturdaystart4 = formatDateTime(this.accesstimeseg.saturdaystart4);

                    this.accesstimeseg.saturdayend1 = formatDateTime1(this.accesstimeseg.saturdayend1);
                    this.accesstimeseg.saturdayend2 = formatDateTime(this.accesstimeseg.saturdayend2);
                    this.accesstimeseg.saturdayend3 = formatDateTime(this.accesstimeseg.saturdayend3);
                    this.accesstimeseg.saturdayend4 = formatDateTime(this.accesstimeseg.saturdayend4);

                    this.accesstimeseg.sundaystart1 = formatDateTime(this.accesstimeseg.sundaystart1);
                    this.accesstimeseg.sundaystart2 = formatDateTime(this.accesstimeseg.sundaystart2);
                    this.accesstimeseg.sundaystart3 = formatDateTime(this.accesstimeseg.sundaystart3);
                    this.accesstimeseg.sundaystart4 = formatDateTime(this.accesstimeseg.sundaystart4);

                    this.accesstimeseg.sundayend1 = formatDateTime1(this.accesstimeseg.sundayend1);
                    this.accesstimeseg.sundayend2 = formatDateTime(this.accesstimeseg.sundayend2);
                    this.accesstimeseg.sundayend3 = formatDateTime(this.accesstimeseg.sundayend3);
                    this.accesstimeseg.sundayend4 = formatDateTime(this.accesstimeseg.sundayend4);

                    var vm = this;
                    this.$services.post('accesstimeseg', this.accesstimeseg).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/categories/accesstimeseg/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/accesstimeseg/list' });
            },

        }
    }

    function formatDateTime(time) {
        if (time === null) {
            return '00:00';
        }

        var hour = time.HH;
        var minute = time.mm;

        if (hour == null || hour === '' || minute == null || minute === '') {
            return '00:00'
        }
        return '' + hour + ':' + minute + ':00';
    }
    function formatDateTime1(time) {
        if (time === null) {
            return '23:59';
        }

        var hour = time.HH;
        var minute = time.mm;

        if (hour == null || hour === '' || minute == null || minute === '') {
            return '23:59'
        }
        return '' + hour + ':' + minute + ':00';
    }
</script>
<style scoped>
</style>
