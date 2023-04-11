﻿
<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t('Categories.Device.Detail.Header') }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model="$v.device.deviceName.$model"
                                                  :state="$v.device.deviceName.$dirty ? !$v.device.deviceName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t('Categories.Device.Detail.Label.DeviceNameRequire') }}
                                    </b-form-invalid-feedback>
                                    <b-form-input v-model="device.deviceName"
                                                  :disabled="!editing"
                                                  v-if="!editing">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.CodeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model.trim="$v.device.deviceCode.$model"
                                                  :state="$v.device.deviceCode.$dirty ? !$v.device.deviceCode.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.device.deviceCode.required">
                                        {{ $t('Categories.Device.Detail.Label.DeviceCodeRequire') }}
                                    </b-form-invalid-feedback>
                                    <b-form-input v-model="device.deviceCode"
                                                  :disabled="!editing"
                                                  v-if="!editing">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Area')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.device.areaId.$model"
                                                  :state="$v.device.areaId.$error ? false : null">
                                    </b-form-input>
                                    <select2 placeholder="Chọn giá trị"
                                             :options="lstArea"
                                             v-if="editing"
                                             :disabled="!editing"
                                             v-model="$v.device.areaId.$model"
                                             :state="$v.device.areaId.$dirty ? !$v.device.areaId.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{ $t('Categories.Device.Detail.Label.AreaRequire') }}
                                    </b-form-invalid-feedback>
                                    <b-form-input v-model="device.areaName"
                                                  :disabled="!editing"
                                                  v-if="!editing">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6" class="text-wrap">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.RstpLink')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  v-if="editing"
                                                  v-model.trim="$v.device.rstpLink.$model"
                                                  :state="$v.device.rstpLink.$dirty? !$v.device.rstpLink.$error: null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.device.rstpLink.required">
                                        {{$t('Categories.Device.Detail.Label.DeviceRstpLinkRequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-input v-model="device.rstpLink"
                                                  :disabled="!editing"
                                                  v-if="!editing">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Features')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-select type="text"
                                                   style="display: none"
                                                   v-model="$v.device.featuresId.$model"
                                                   :state="$v.device.featuresId.$error ? false : null">
                                    </b-form-select>
                                    <!--<select2 placeholder="Chọn giá trị"
                                             v-if="editing"
                                             :options="lstfeature"
                                             :disabled="!editing"
                                             :settings="{ allowClear: true, multiple: true, closeOnSelect: false }"
                                             v-model="$v.device.featuresId.$model"
                                             :state="$v.device.featuresId.$dirty ? !$v.device.featuresId.$error : null">
                                    </select2>-->
                                    <b-form-checkbox-group size="lg"
                                                           v-model="device.featuresId"
                                                           v-if="editing"
                                                           stacked
                                                           :options="lstfeature">
                                    </b-form-checkbox-group>
                                    <b-form-checkbox-group size="lg"
                                                           disabled
                                                           stacked
                                                           v-if="!editing"
                                                           v-model="device.featuresId"
                                                           :options="lstfeature">
                                    </b-form-checkbox-group>
                                    <b-form-invalid-feedback>
                                        {{ $t('Categories.Device.Detail.Label.FunctionRequire') }}
                                    </b-form-invalid-feedback>
                                    <!--<b-form-input v-model="device.featuresName"
                                                  :disabled="!editing"
                                                  v-if="!editing">
                                    </b-form-input>-->
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-button type="button"
                                          variant="primary"
                                          @click="edit"
                                          v-if="!editing && authorize(['ManageMachine'])">
                                    <i class="fa fa-pencil-square-o"></i> {{ $t('Button.Edit') }}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="back"
                                          style="width: 120px"
                                          v-if="!editing">
                                    <i class="fa fa-ban"></i> {{ $t('Button.Back') }}
                                </b-button>
                                <b-button type="submit" variant="primary" v-if="editing">
                                    <i class="fa fa-floppy-o"></i> {{ $t('Button.Submit') }}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="cancel"
                                          style="width: 120px"
                                          v-if="editing">
                                    <i class="fa fa-ban"></i> {{ $t('Button.Cancel') }}
                                </b-button>
                            </b-col>
                        </b-row>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { validationMixin } from 'vuelidate';
    import { authorizationMixin } from '@/shared/mixins';
    import { required } from 'vuelidate/lib/validators';
    import i18n from '@/libs/i18n';

    export default {
        name: 'DeviceDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                device: {
                    deviceCode: null,
                    deviceName: null,
                    areaName: null,
                    featuresName: null,
                    rstpLink: null,
                    featuresId: [],
                    direction: null,
                    directionStr: null,
                    areaId: null,
                },
                lstfeature: [],
                lstArea: [],
                listDirection: [
                    {
                        id: 1,
                        text: this.$t('Categories.Device.Common.ListDirection.Entry'),
                    },
                    {
                        id: 2,
                        text: this.$t('Categories.Device.Common.ListDirection.Exit'),
                    },
                ],
                editing: false,
                isAdmin: false,
                lstDependentDevice: [],
            };
        },
        computed: {
            deviceId() {
                return this.$route.params.deviceId;
            },
        },
        validations: {
            device: {
                deviceCode: { required },
                deviceName: { required },
                rstpLink: { required },
                featuresId: { required },
                areaId: { required },
            },
        },
        async created() {
            await this.loadDeviceDetail();
            await this.loadFeature();
            await this.loadArea();
            this.loadDependentDevice();
        },

        methods: {
            async loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done((res) => {
                    vm.lstArea = res;
                });
            },
            async loadFeature() {
                var vm = this;
                return this.$services.get(`lookup/features`).done((res) => {
                    vm.lstfeature = res;
                });
            },
            async loadDeviceDetail() {
                var vm = this;
                return this.$services.get(`device/${this.deviceId}`).done((res) => {
                    vm.device = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/device/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadDeviceDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`device/${this.deviceId}`, this.device).done(() => {
                        vm.$toastr.s(i18n.t('Messages.ConfirmUpdate'));
                        vm.cancel();
                    });
                }
            },
            loadDependentDevice() {
                //var vm = this;
                //return this.$services.get(`lookup/dependentDevice`).done((res) => {
                //    vm.lstDependentDevice = res;
                //});
            },
        },
    };
</script>

<style scoped>
    .keepAll {
        word-break: keep-all;
    }
</style>
