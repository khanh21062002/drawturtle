<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Machine.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" style="display: none;"
                                                  v-model="$v.machine.compId.$model"
                                                  :state="$v.machine.compId.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')" :options="lstcompany" :disabled="true"
                                             v-model="$v.machine.compId.$model" :state="$v.machine.compId.$dirty? !$v.machine.compId.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Group.Detail.Label.CompanyRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Function')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="!editing"
                                                   v-model="$v.machine.deviceFunction.$model" :state="$v.machine.deviceFunction.$dirty? !$v.machine.deviceFunction.$error : null">
                                        <b-form-select-option value="1">{{$t('Categories.Machine.Detail.Label.Check1')}}</b-form-select-option>
                                        <b-form-select-option value="2">{{$t('Categories.Machine.Detail.Label.Check2')}}</b-form-select-option>
                                        <b-form-select-option value="3">{{$t('Categories.Machine.Detail.Label.Check3')}}</b-form-select-option>
                                    </b-form-select>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.MachineRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="txt_name" v-if="editing"
                                                  v-model="$v.machine.deviceName.$model"
                                                  :state="$v.machine.deviceName.$dirty? !$v.machine.deviceName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.machine.deviceName.required">
                                        {{$t('Categories.Machine.Detail.Label.MachineNameRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ machine.deviceName }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.CodeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_code" v-if="editing"
                                                  v-model="machine.code">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.code }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Imei')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left" label-class="required">
                                    <b-form-input type="text" id="txt_imei" v-if="editing"
                                                  v-model="$v.machine.imei.$model"
                                                  :state="$v.machine.imei.$dirty? !$v.machine.imei.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.ImeiRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ machine.imei }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Mac')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="txt_mac" v-if="editing"
                                                  v-model="$v.machine.mac.$model"
                                                  :state="$v.machine.mac.$dirty? !$v.machine.mac.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.MacRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ machine.mac }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row style="display: none">
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UserName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_Username" v-if="editing"
                                                  v-model="machine.username">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.username }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Password')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="password" id="txt_Password" v-if="editing"
                                                  v-model="machine.password">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">******</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.ServerIP')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="txt_ServerIp" v-if="editing"
                                                  v-model="$v.machine.serverIp.$model"
                                                  :state="$v.machine.serverIp.$dirty? !$v.machine.serverIp.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.ServerRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ machine.serverIp }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6" style="display: none">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UserNameServer')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="usernameServer" v-if="editing"
                                                  v-model="$v.machine.usernameServer.$model"
                                                  :state="$v.machine.usernameServer.$dirty? !$v.machine.usernameServer.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.ServerUserRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ machine.usernameServer }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.FaceThreshold')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_FaceThreshold" placeholder="0-1" v-if="editing"
                                                  v-model="machine.faceThreshold">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.faceThreshold }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row style="display: none">
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PasswordServer')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left" label-class="required">
                                    <b-form-input type="password" id="txt_passwordserver" v-if="editing"
                                                  v-model="$v.machine.passwordServer.$model"
                                                  :state="$v.machine.passwordServer.$dirty? !$v.machine.passwordServer.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.PasswordUserRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">******</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.TemperatureThreshold')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_TemperatureThreshold" placeholder="℃" v-if="editing"
                                                  v-model="machine.temperatureThreshold">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.temperatureThreshold }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.DistanceDetect')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" id="txt_DistanceDetect" v-if="editing"
                                                  v-model="machine.distanceDetect">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.distanceDetect }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.DailyReboot')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.machine.dailyReboot.$model"
                                                  :state="$v.machine.dailyReboot.$dirty? !$v.machine.dailyReboot.$error : null">
                                    </b-form-input>
                                    <select2 :options="lstdailyReboot" :placeholder="this.$t('PlaceHolder.Select')" :settings="{allowClear: true}"
                                             v-model="$v.machine.dailyReboot.$model" v-if="editing" id="ID_D"
                                             :state="$v.machine.dailyReboot.$dirty? !$v.machine.dailyReboot.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.DailyRebootRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ $t(machine.dailyRebootStr) }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Delay')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="number" id="txt_Delay" placeholder="mili giây" @change=validateNumber1($event) v-if="editing"
                                                  v-model="machine.delay">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.delay }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" v-if="machine.dailyReboot != 'true' && machine.dailyReboot != true"></b-col>
                            <b-col md="6" v-if="machine.dailyReboot == 'true' || machine.dailyReboot == true">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.RestartTime')"
                                              :label-cols="4"
                                              v-if="showDept"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.machine.restartTime.$model"
                                                  :state="$v.machine.restartTime.$dirty? !$v.machine.restartTime.$error : null">
                                    </b-form-input>
                                    <date-picker style="width: 100%;" v-model="$v.machine.restartTime.$model" v-if="editing"
                                                 type="time" format="HH:mm:ss" valueType="HH:mm:ss"></date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.RestartTimeRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ $t(machine.restartTimeStr) }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Type')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.machine.language.$model"
                                                  :state="$v.machine.language.$dirty? !$v.machine.language.$error : null">
                                    </b-form-input>
                                    <select2 :options="lstLanguage" :placeholder="this.$t('PlaceHolder.Select')" id="language" :settings="{allowClear: true}"
                                             v-model="$v.machine.language.$model " v-if="editing"
                                             :state="$v.machine.language.$dirty? !$v.machine.language.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('ManagerFAQ.Detail.Label.TypeRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ $t(machine.languageStr) }}</label>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AutoSleep')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.machine.autoSleep.$model"
                                                  :state="$v.machine.autoSleep.$dirty? !$v.machine.autoSleep.$error : null">
                                    </b-form-input>
                                    <select2 :options="lstautoSleep" :placeholder="this.$t('PlaceHolder.Select')" id="dpl_compId" :settings="{allowClear: true}"
                                             v-model="$v.machine.autoSleep.$model" v-if="editing"
                                             :state="$v.machine.autoSleep.$dirty? !$v.machine.autoSleep.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.AutoSleepRequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ $t(machine.autoSleepStr) }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AreaName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="machine.areaId">
                                    </b-form-input>
                                    <select2 :options="lstArea" :placeholder="this.$t('PlaceHolder.Select')" :settings="{allowClear: true}"
                                             v-model="machine.areaId" v-if="editing" id="ID_D">
                                    </select2>
                                    <label class="col-form-label" v-if="!editing">{{ machine.areaName }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="8">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.FraudProof')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_fraudproof" size="lg" v-if="editing"
                                                             v-model="machine.fraudProof"
                                                             name="checkbox-FraudProof"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.fraudProofName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AutoSaveVisitor')"
                                                      :label-cols="8"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_autosavevisitor" size="lg" v-if="editing"
                                                             v-model="machine.autoSaveVisitor"
                                                             name="checkbox-AutoSaveVisitor"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.autoSaveVisitorName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="8">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Led')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_led" size="lg" v-if="editing"
                                                             v-model="machine.led"
                                                             name="checkbox-led"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.ledName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AutoStart')"
                                                      :label-cols="10"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_autostart" size="lg" v-if="editing"
                                                             v-model="machine.autoStart"
                                                             name="checkbox-AutoStart"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.autoStartName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="8">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UseTemperature')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_usetemperature" size="lg" v-if="editing"
                                                             v-model="machine.useTemperature"
                                                             name="checkbox-usetemperature"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.useTemperatureName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UseMask')"
                                                      :label-cols="8"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_usemask" size="lg" v-if="editing"
                                                             v-model="machine.useMask"
                                                             name="checkbox-usemask"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.useMaskName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="8">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UseVaccine')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox v-if="editing" id="chk_useVaccine" size="lg"
                                                             v-model="machine.useVaccine"
                                                             name="checkbox-useVaccine"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.useVaccineName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <br />
                        <b-row>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="8">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UsePCCovid')"
                                                      :label-cols="6"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox v-if="editing" id="chk_usePCCovid" size="lg"
                                                             v-model="machine.usePCCovid"
                                                             name="checkbox-usePCCovid"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                            <label class="col-form-label" v-if="!editing">{{ $t(machine.usePCCovidName) }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="12">
                                        <b-form-group label="Logo:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <div class="form-group">
                                                <div>
                                                    <b-form-file @change="onChange($event)" id="file-input" v-if="editing" accept=".jpg, .png">
                                                    </b-form-file>
                                                </div>
                                            </div>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" v-if="machine.usePCCovid != 'true' && machine.usePCCovid != true"></b-col>
                            <b-col md="6" v-if="machine.usePCCovid == 'true' || machine.usePCCovid == true">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PCCovidPhone')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left" label-class="required">
                                    <b-form-input v-if="editing" type="text" id="txt_pcCovidPhone"
                                                  v-model="$v.machine.pcCovidPhone.$model"
                                                  :state="$v.machine.pcCovidPhone.$dirty? !$v.machine.pcCovidPhone.$error : null">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ $t(machine.pcCovidPhone) }}</label>
                                    <b-form-invalid-feedback v-if="!$v.machine.pcCovidPhone.checkRequire">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidPhoneRequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.machine.pcCovidPhone.checkValidPhone">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidPhoneValid')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PCCovidLocation')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input v-if="editing" type="text" id="txt_pcCovidLocation"
                                                  v-model="$v.machine.pcCovidLocation.$model"
                                                  :state="$v.machine.pcCovidLocation.$dirty? !$v.machine.pcCovidLocation.$error : null">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ $t(machine.pcCovidLocation) }}</label>
                                    <b-form-invalid-feedback v-if="!$v.machine.pcCovidLocation.checkRequire">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidLocationRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PCCovidToken')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input v-if="editing" type="text" id="txt_pcCovidToken"
                                                  v-model="$v.machine.pcCovidToken.$model"
                                                  :state="$v.machine.pcCovidToken.$dirty? !$v.machine.pcCovidToken.$error : null">
                                    </b-form-input>
                                    <label class="col-form-label" v-if="!editing">{{ machine.pcCovidToken }}</label>
                                    <b-form-invalid-feedback v-if="!$v.machine.pcCovidToken.checkRequire">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidTokenRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label=""
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <div class="form-group">
                                        <img v-bind:src="machine.logo" alt="No Image" width="250" height="250">
                                    </div>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <br />
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageMachine'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
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
        name: 'MachineDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                machine: {
                    code: null,
                    devicename: null,
                    usemark: null,
                    usetemperature: null,
                    mac: null,
                    status: null,
                    compId: null,
                    areaId: null,
                    ipaddress: null,
                    imei: null,
                    serverIp: null,
                    serverport: null,
                    fraudproof: null,
                    autostart: null,
                    angledetect: null,
                    autoSaveVisitor: null,
                    distancedetect: null,
                    username: null,
                    password: null,
                    logo: null,
                    volume: null,
                    brightness: null,
                    delay: null,
                    led: null,
                    deviceFunction: null,
                    temperaturethreshold: null,
                    facethreshold: null,
                    status: 1,
                    autoStartName: null,
                    usernameServer: null,
                    passwordServer: null,
                    deviceFunction: null,
                    usePCCovid: null,
                    pcCovidPhone: null,
                    pcCovidLocation: null,
                    pcCovidToken: null,
                    useVaccine: null,
                    language: null,
                    dailyReboot: null,
                    restartTime: null,
                    autoSleep: null,
                },
                lstcompany: [],
                lstArea: [],
                editing: false,
                isAdmin: false,
                lstLanguage: [
                    { id: 'EN', text: this.$t('ManagerFAQ.Detail.Label.TypeEn') },
                    { id: 'VI', text: this.$t('ManagerFAQ.Detail.Label.TypeVi') }
                ],
                lstautoSleep: [
                    { id: 'true', text: this.$t('Categories.Machine.List.SearchForm.StatusOn') },
                    { id: 'false', text: this.$t('Categories.Machine.List.SearchForm.StatusOff') }
                ],
                lstdailyReboot: [
                    { id: 'true', text: this.$t('Categories.Machine.List.SearchForm.StatusOn') },
                    { id: 'false', text: this.$t('Categories.Machine.List.SearchForm.StatusOff') }
                ],
            }
        },
        watch: {
            //whenever typeWatch changes, this function will run
            typeWatch: function (newType, oldType) {
                var vm = this;
                if (newType == 'false') {
                    vm.machine.restartTime = '00:00:00';
                }
            },
        },
        computed: {
            machineId() {
                return this.$route.params.machineId;
            },
            showDept: function () {
                var vm = this;
                if (vm.machine.dailyReboot == 'true' || vm.machine.dailyReboot == true) {
                    return true;
                }
                return false;
            },
            typeWatch: function () {
                var vm = this;
                if (vm.machine.dailyReboot == 'true')
                    return true;
                return false;
            },
        },
        validations: {
            machine: {
                deviceName: { required },
                compId: { required },
                deviceFunction: { required },
                imei: { required },
                mac: { required },
                passwordServer: { required },
                usernameServer: { required },
                serverIp: { required },
                dailyReboot: { required },
                autoSleep: { required },
                language: { required },
                restartTime: {
                    customValidate(value) {
                        if (this.machine.dailyReboot == 'true' && (this.machine.restartTime == true || this.machine.restartTime == null)) {
                            return false;
                        } else return true;
                    }
                },
                pcCovidPhone: {
                    checkRequire(value) {
                        if (this.machine.usePCCovid == null || this.machine.usePCCovid == 'false' || this.machine.usePCCovid == false) {
                            return true;
                        }
                        if (value != null && value.trim() != '') return true;
                        return false;
                    },
                    checkValidPhone(value) {
                        //add regex compare here
                        if (value == null || value == '') return true;
                        return /([\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})\b/.test(value);
                    }
                },
                pcCovidLocation: {
                    checkRequire(value) {
                        if (this.machine.usePCCovid == null || this.machine.usePCCovid == 'false' || this.machine.usePCCovid == false) {
                            return true;
                        }
                        if (value != null && value.trim() != '') return true;
                        return false;
                    },
                },
                pcCovidToken: {
                    checkRequire(value) {
                        if (this.machine.usePCCovid == null || this.machine.usePCCovid == 'false' || this.machine.usePCCovid == false) {
                            return true;
                        }
                        if (value != null && value.trim() != '') return true;
                        return false;
                    },
                }
            }
        },
        async created() {
            await this.loadMachineDetail();
            await this.loadCompany();
            await this.loadArea();
        },
        methods: {
            //Danh sách khu vực
            async loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done(res => {
                    vm.lstArea = res;
                });
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            onChange: function (evt) {
                var vm = this;
                var f = evt.target.files[0]; // FileList object
                var acceptExtensions = ["jpg", "png", "PNG", "JPG"];
                let fileExtensions = (/[.]/.exec(f.name)) ? /[^.]+$/.exec(f.name) : undefined;

                if (fileExtensions && !acceptExtensions.includes(fileExtensions[0])) {
                    alert("Vui lòng chọn file có định dạng .jpg, .png");
                    return;
                }
                if (f.size > 5000000) {
                    alert("Vui lòng chọn file có dung lượng dưới 5MB");
                    f.value = "";
                    return;
                };
                var reader = new FileReader();
                // Closure to capture the file information.
                reader.onload = (function (theFile) {
                    return function (e) {
                        var binaryData = e.target.result;
                        //Converting Binary Data to base 64
                        vm.machine.fileData = window.btoa(binaryData);
                    };
                })(f);
                reader.readAsBinaryString(f, vm);
                vm.machine.file1 = f.name;
            },
            async loadMachineDetail() {
                var vm = this;
                return this.$services.get(`machine/${this.machineId}`).done(machine => {
                    debugger;
                    vm.machine = machine;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/machine/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadMachineDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`machine/${this.machineId}`, this.machine).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
