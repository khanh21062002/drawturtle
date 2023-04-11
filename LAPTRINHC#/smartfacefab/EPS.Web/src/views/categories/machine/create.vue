<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Machine.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none;"
                                                  v-model="$v.machine.compId.$model"
                                                  :state="$v.machine.compId.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="lstcompany"
                                             :disabled="true"
                                             v-model="$v.machine.compId.$model"
                                             :state="$v.machine.compId.$dirty? !$v.machine.compId.$error : null">
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
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.machine.deviceFunction.$model"
                                                  :state="$v.machine.deviceFunction.$error ? false : null">
                                    </b-form-input>
                                    <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')"
                                                   v-model="machine.deviceFunction">
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
                                    <b-form-input type="text"
                                                  v-model="$v.machine.devicename.$model"
                                                  :state="$v.machine.devicename.$dirty? !$v.machine.devicename.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.MachineNameRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.CodeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-model.trim="machine.code">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Imei')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.machine.imei.$model"
                                                  :state="$v.machine.imei.$dirty? !$v.machine.imei.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.ImeiRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Mac')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.machine.mac.$model"
                                                  :state="$v.machine.mac.$dirty? !$v.machine.mac.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.MacRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row style="display: none">
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UserName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-model="machine.username">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Password')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="password"
                                                  v-model="machine.password">
                                    </b-form-input>
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
                                    <b-form-input type="text"
                                                  v-model="$v.machine.serverip.$model"
                                                  :state="$v.machine.serverip.$dirty? !$v.machine.serverip.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.ServerRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6" style="display: none">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UserNameServer')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.machine.usernameserver.$model"
                                                  :state="$v.machine.usernameserver.$dirty? !$v.machine.usernameserver.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.ServerUserRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.FaceThreshold')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  placeholder="0-1"
                                                  v-model="machine.facethreshold">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" style="display: none">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PasswordServer')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="password"
                                                  v-model="$v.machine.passwordserver.$model"
                                                  :state="$v.machine.passwordserver.$dirty? !$v.machine.passwordserver.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.PasswordUserRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.TemperatureThreshold')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  placeholder="℃"
                                                  v-model="machine.temperaturethreshold">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.DistanceDetect')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="text"
                                                  v-model="machine.distancedetect">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.DailyReboot')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.machine.dailyReboot.$model"
                                                  :state="$v.machine.dailyReboot.$dirty? !$v.machine.dailyReboot.$error : null">
                                    </b-form-input>
                                    <select2 :options="lstdailyReboot"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="$v.machine.dailyReboot.$model"
                                             :state="$v.machine.dailyReboot.$dirty? !$v.machine.dailyReboot.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.DailyRebootRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.Delay')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="number"
                                                  placeholder="mili giây"
                                                  @change=validateNumber1($event)
                                                  v-model="machine.delay">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.RestartTime')"
                                              :label-cols="4"
                                              v-if="showDept"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" style="display: none"
                                                  v-model="$v.machine.restartTime.$model"
                                                  :state="$v.machine.restartTime.$dirty? !$v.machine.restartTime.$error : null">
                                    </b-form-input>
                                    <date-picker style="width: 100%;"
                                                 v-model="$v.machine.restartTime.$model"
                                                 type="time"
                                                 format="HH:mm:ss"
                                                 valueType="HH:mm:ss">
                                    </date-picker>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.RestartTimeRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('ManagerFAQ.Detail.Label.Type')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.machine.language.$model"
                                                  :state="$v.machine.language.$dirty? !$v.machine.language.$error : null">
                                    </b-form-input>
                                    <select2 :options="lstLanguage"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="$v.machine.language.$model "
                                             :state="$v.machine.language.$dirty? !$v.machine.language.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('ManagerFAQ.Detail.Label.TypeRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AutoSleep')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.machine.autoSleep.$model"
                                                  :state="$v.machine.autoSleep.$dirty? !$v.machine.autoSleep.$error : null">
                                    </b-form-input>
                                    <select2 :options="lstautoSleep"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             v-model="$v.machine.autoSleep.$model"
                                             :state="$v.machine.autoSleep.$dirty? !$v.machine.autoSleep.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{$t('Categories.Machine.Detail.Label.AutoSleepRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AreaName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{allowClear: true}"
                                             :options="lstArea"
                                             v-model="machine.areaId">
                                    </select2>
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
                                            <b-form-checkbox id="chk_fraudproof"
                                                             size="lg"
                                                             v-model="machine.fraudproof"
                                                             name="checkbox-FraudProof"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AutoSaveVisitor')"
                                                      :label-cols="8"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_autosavevisitor"
                                                             size="lg"
                                                             v-model="machine.autosavevisitor"
                                                             name="checkbox-AutoSaveVisitor"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
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
                                            <b-form-checkbox id="chk_led"
                                                             size="lg"
                                                             v-model="machine.led"
                                                             name="checkbox-led"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.AutoStart')"
                                                      :label-cols="10"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_autostart"
                                                             size="lg"
                                                             v-model="machine.autostart"
                                                             name="checkbox-AutoStart"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
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
                                            <b-form-checkbox id="chk_usetemperature"
                                                             size="lg"
                                                             v-model="machine.usetemperature"
                                                             name="checkbox-usetemperature"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Machine.Detail.Label.UseMask')"
                                                      :label-cols="8"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-checkbox id="chk_usemask"
                                                             size="lg"
                                                             v-model="machine.usemask"
                                                             name="checkbox-usemask"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
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
                                            <b-form-checkbox id="chk_useVaccine"
                                                             size="lg"
                                                             v-model="machine.useVaccine"
                                                             name="checkbox-useVaccine"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
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
                                            <b-form-checkbox id="chk_usePCCovid"
                                                             size="lg"
                                                             v-model="machine.usePCCovid"
                                                             name="checkbox-usePCCovid"
                                                             value="true"
                                                             unchecked-value="false">
                                            </b-form-checkbox>
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
                                                    <b-form-file @change="onChange($event)"
                                                                 id="file-input"
                                                                 accept=".jpg, .png">
                                                    </b-form-file>
                                                </div>
                                            </div>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row v-if="machine.usePCCovid == 'true'">
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PCCovidPhone')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.machine.pCCovidPhone.$model"
                                                  :state="$v.machine.pCCovidPhone.$dirty? !$v.machine.pCCovidPhone.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.machine.pCCovidPhone.checkRequire">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidPhoneRequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.machine.pCCovidPhone.checkValidPhone">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidPhoneValid')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PCCovidLocation')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.machine.pCCovidLocation.$model"
                                                  :state="$v.machine.pCCovidLocation.$dirty? !$v.machine.pCCovidLocation.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.machine.pCCovidLocation.checkRequire">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidLocationRequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">

                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Machine.Detail.Label.PCCovidToken')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.machine.pCCovidToken.$model"
                                                  :state="$v.machine.pCCovidToken.$dirty? !$v.machine.pCCovidToken.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.machine.pCCovidToken.checkRequire">
                                        {{$t('Categories.Machine.Detail.Label.PCCovidTokenRequire')}}
                                    </b-form-invalid-feedback>

                                </b-form-group>
                            </b-col>
                        </b-row>
                        <br />
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageMachine'])"
                                              @click="save"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel">
                                        <i class="fa fa-ban"></i> {{$t("Button.Cancel")}}
                                    </b-button>
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
    import { validationMixin } from "vuelidate";
    import { authorizationMixin } from "@/shared/mixins";
    import { required } from "vuelidate/lib/validators";
    import Services from "@/utils/services";
    import i18n from "@/libs/i18n";

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
                    serverip: null,
                    serverport: null,
                    fraudproof: null,
                    autostart: null,
                    angledetect: null,
                    autosavevisitor: null,
                    distancedetect: null,
                    username: 'admin',
                    password: 'atin@123',
                    logo: null,
                    volume: null,
                    brightness: null,
                    delay: null,
                    led: null,
                    temperaturethreshold: null,
                    facethreshold: null,
                    status: 1,
                    usernameserver: null,
                    passwordserver: 'password',
                    usePCCovid: null,
                    pCCovidPhone: null,
                    pCCovidLocation: null,
                    pCCovidToken: null,
                    useVaccine: null,
                    language: 'VI',
                    dailyReboot: 'false',
                    restartTime: '00:00:00',
                    autoSleep: 'false',
                },
                lstcompany: [],
                lstArea: [],
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
                editing: false, lstDeviceFunction: [
                    { id: '1', text: 'Check in' },
                    { id: '2', text: 'Check out' },
                    { id: '3', text: 'Chấm công' },
                ],
                isAdmin: false,
            }
        },
        watch: {
            // whenever typeWatch changes, this function will run
            typeWatch: function (newType, oldType) {
                var vm = this;
                if (newType == 'false') {
                    vm.machine.restartTime = '00:00:00';
                }
            },
        },
        computed: {
            showDept: function () {
                var vm = this;
                if (vm.machine.dailyReboot == 'true') {
                    return true;
                }
                return false;
            },
            typeWatch: function () {
                var vm = this;
                if (vm.machine.dailyReboot)
                    return vm.machine.dailyReboot;
                return false;
            },
        },
        validations: {
            machine: {
                devicename: { required },
                compId: { required },
                deviceFunction: { required },
                imei: { required },
                mac: { required },
                passwordserver: { required },
                usernameserver: { required },
                serverip: { required },
                dailyReboot: { required },
                autoSleep: { required },
                language: { required },
                restartTime: {
                    customValidate(value) {
                        if (
                            this.machine.dailyReboot == 'true' &&
                            (this.machine.restartTime == null ||
                                this.machine.restartTime == "")
                        ) {
                            return false;
                        } else return true;
                    },
                },
                pCCovidPhone: {
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
                pCCovidLocation: {
                    checkRequire(value) {
                        if (this.machine.usePCCovid == null || this.machine.usePCCovid == 'false' || this.machine.usePCCovid == false) {
                            return true;
                        }
                        if (value != null && value.trim() != '') return true;
                        return false;
                    },
                },
                pCCovidToken: {
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
            var accessToken = Services.getAccessToken();
            this.machine.departmenT_ID = accessToken.unitId;
            this.machine.compId = accessToken.comId;
            await this.loadCompany();
            this.machine.distancedetect = 80;
            this.machine.delay = 2000;
            this.machine.facethreshold = 0.8;
            this.loadArea();
        },
        methods: {
            validateNumber(event) {
                var vm = this;
                if (event < 0) {
                    vm.machine.distancedetect = 0;
                }
                if (event > 100) {
                    vm.machine.distancedetect = 100;
                }
            },
            validateNumber1(event) {
                var vm = this;
                if (event < 1000) {
                    vm.machine.delay = 1000;
                }
                if (event > 10000) {
                    vm.machine.delay = 10000;
                }
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
            setImage: function (output) {
                this.hasImage = true;
                this.image = output;
            },
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách khu vực
            async loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done(res => {
                    vm.lstArea = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/machine/list' });
            },
            save() {
                this.machine.usernameserver = this.machine.imei;
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('machine', this.machine).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/categories/machine/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/machine/list' });
            }
        }
    }
</script>
<style scoped>
</style>
