<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t('Categories.Device.Create.Header') }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.device.devicename.$model"
                                                  :state="$v.device.devicename.$dirty ? !$v.device.devicename.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t('Categories.Device.Detail.Label.DeviceNameRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.CodeName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model.trim="$v.device.deviceCode.$model"
                                                  :state="$v.device.deviceCode.$dirty ? !$v.device.deviceCode.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.device.deviceCode.required">
                                        {{ $t('Categories.Device.Detail.Label.DeviceCodeRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Area')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.device.areaId.$model"
                                                  :state="$v.device.areaId.$error ? false : null">
                                    </b-form-input>
                                    <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="lstArea"
                                             v-model="$v.device.areaId.$model"
                                             :state="$v.device.areaId.$dirty ? !$v.device.areaId.$error : null">
                                    </select2>
                                    <b-form-invalid-feedback>
                                        {{ $t('Categories.Device.Detail.Label.AreaRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.RstpLink')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  id="txt_rstpLink"
                                                  v-model.trim="$v.device.rstpLink.$model"
                                                  :state="$v.device.rstpLink.$dirty ? !$v.device.rstpLink.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.device.rstpLink.required">
                                        {{ $t('Categories.Device.Detail.Label.DeviceRstpLinkRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6">
                                <b-form-group :label="this.$t('Categories.Device.Detail.Label.Features')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-select type="text"
                                                   style="display: none"
                                                   v-model="$v.device.featuresId.$model"
                                                   :state="$v.device.featuresId.$error ? false : null">
                                    </b-form-select>
                                    <!--<select2 :placeholder="this.$t('PlaceHolder.Select')"
                                             :options="lstfeature"
                                             :selectOnClose="false"
                                             ref="mySelect2"
                                             :settings="{ allowClear: true, multiple: true, closeOnSelect: false }"
                                             v-model="$v.device.featuresId.$model"
                                             :state="$v.device.featuresId.$dirty ? !$v.device.featuresId.$error : null">
                                    </select2>-->
                                    <b-form-checkbox-group size="lg"
                                                           stacked
                                                           v-model="device.featuresId"
                                                           :options="lstfeature">
                                    </b-form-checkbox-group>
                                    <b-form-invalid-feedback>
                                        {{ $t('Categories.Device.Detail.Label.FunctionRequire') }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageMachine'])"
                                              @click="save"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{ $t('Button.Submit') }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel"
                                              style="width: 120px">
                                        <i class="fa fa-ban"></i> {{ $t('Button.Cancel') }}
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
    import { validationMixin } from 'vuelidate';
    import { authorizationMixin } from '@/shared/mixins';
    import { required } from 'vuelidate/lib/validators';
    import Services from '@/utils/services';
    import i18n from '@/libs/i18n';

    export default {
        name: 'DeviceDetail',
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                device: {
                    deviceCode: null,
                    devicename: null,
                    rstpLink: null,
                    status: null,
                    mac: null,
                    compId: null,
                    featuresId: [],
                    areaId: null,
                    direction: null,
                    dependentDeviceId: null,
                },
                lstDependentDevice: [],
                lstfeature: [],
                editing: false,
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
                isAdmin: false,
            };
        },
        validations: {
            device: {
                deviceCode: { required },
                devicename: { required },
                rstpLink: { required },
                featuresId: { required },
                areaId: { required },
            },
        },
        computed: {},
        async created() {
            var accessToken = Services.getAccessToken();
            this.device.compId = accessToken.comId;
            await this.loadFeature();
            await this.loadArea();
            this.loadDependentDevice();
        },
        methods: {
            //Danh sách chức năng
            async loadFeature() {
                var vm = this;
                return this.$services.get(`lookup/featuresstatus`).done((res) => {
                    vm.lstfeature = res;
                });
            },
            async loadArea() {
                var vm = this;
                return this.$services.get(`lookup/area`).done((res) => {
                    vm.lstArea = res;
                });
            },
            loadDependentDevice() {
                var vm = this;
                //return this.$services.get(`lookup/dependentDevice`).done((res) => {
                //    vm.lstDependentDevice = res;
                //});
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/device/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('device', this.device).done((id) => {
                        vm.$toastr.s(i18n.t('Messages.ConfirmCreate'));
                        vm.$router.push({ path: '/categories/device/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/device/list' });
            },
        },
    };
</script>
<style scoped></style>
