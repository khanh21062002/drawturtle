<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Warning.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save" @submit="$v.$touch()">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Company')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                     v-model="notification.compId" :options="lstcompany">
                                            </select2>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Machine')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.notification.machineId.$model"
                                                          :state="$v.notification.machineId.$error ? false : null">
                                            </b-form-input>
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="notification.machineId" :options="lstmachine">
                                            </select2>
                                            <b-form-invalid-feedback>
                                                {{$t("Warning.Create.Form.MachineRequire")}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Type')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="text" style="display: none"
                                                          v-model="$v.notification.type.$model"
                                                          :state="$v.notification.type.$error ? false : null">
                                            </b-form-input>
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="notification.type" :options="lsttype">
                                            </select2>
                                            <b-form-invalid-feedback>
                                                {{$t("Warning.Create.Form.TypeRequire")}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6" v-if="checkperson">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Thresholds')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input type="number"
                                                          v-model="$v.notification.scoreMatch.$model" min="0" max="1" step="0.01"
                                                          :state="$v.notification.scoreMatch.$error ? false : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                {{$t("Warning.Create.Form.ThresholdsRequire")}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <br/>
                        <b-row>
                            <b-col md="7">
                                <h2>{{$t("Warning.Create.HeaderTable")}}</h2>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="12" >
                                <b-table striped hover :items="lstpersonreceive" :fields="personFields">
                                    <template v-slot:cell(index)="row">
                                        {{ row.index + 1 }}
                                    </template>
                                    <template v-slot:cell(personId)="row">
                                        <select2 :placeholder="$t('PlaceHolder.Select')"
                                                 v-model="row.item.personId" :options="lstperson">
                                        </select2>
                                    </template>
                                    <template v-slot:cell(email)="row">
                                        <b-form-input type="email" v-model="row.item.email" required oninvalid="this.setCustomValidity('Vui lòng nhập Email hợp lệ')" oninput="this.setCustomValidity('')" />
                                    </template>
                                    <template v-slot:cell(action)="row">
                                        <b-button-group>
                                            <b-button size="sm"  v-if="lstpersonreceive.length > 1" @click.stop="remove(row.item, row.index, $event.target)" class="mr-1" :title="$t('TitleDetail.Delete')">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </b-button>
                                            <b-button size="sm"  @click="addRow(row.index)" class="mr-1" :title="$t('TitleDetail.Add')">+</b-button>
                                        </b-button-group>
                                    </template>
                                </b-table>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="8" offset="1">
                                <b-form-group :label-cols="6"
                                              :horizontal="true">
                                    <b-button v-if="authorize(['ManageNotification'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
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
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                notification: {
                    id: 0,
                    compId: null,
                    type: null,
                    machineId: null,
                    scoreMatch: 0,
                    timeSchedule: null,
                    notificationDetails: [],
                },
                lstcompany: [],
                lstperson: [],
                lsttype: [],
                lstmachine: [],
                isAdmin: false,
                isExist: false,
                personFields: [
                    { key: 'index', label: this.$t('Warning.Create.Table.Index'), sortable: false },
                    { key: 'personId', label: this.$t('Warning.Create.Table.Employee'), sortable: false },
                    { key: 'email', label: this.$t('Warning.Create.Table.Email'), sortable: false, },
                    { key: 'action', label: this.$t('Warning.Create.Table.Action') }
                ],
                lstpersonreceive: [],
            }
        },
        validations: {
            notification: {
                compId: { required },
                type: { required },
                machineId: { required },
                scoreMatch: {
                   required },
            }
        },
        computed: {
            checkperson: function () {
                if (this.notification.type == 1) return true;
                return false;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.notification.compId = accessToken.comId;
            this.loadType();
            this.loadCompany();
            this.loadMachine();
            this.addRow(-1);
            this.loadPerson();
        },
        methods: {
            loadPerson() {
                var vm = this;
                return this.$services.get(`lookup/persons`).done(lstperson => {
                    vm.lstperson = lstperson;
                })
            },
            addRow(index) {
                var vm = this;
                let item = {
                    'personId': '',
                    'email': '',
                }
                var cloneItem = JSON.parse(JSON.stringify(item));
                if (index == vm.lstpersonreceive.length - 1) {
                    vm.lstpersonreceive.push(item);
                } else if (index < vm.lstpersonreceive.length - 1) {
                    vm.lstpersonreceive.splice(index + 1, 0, item);
                } else {
                    // do nothing
                }
            },
            //Danh sách công ty
            async loadMachine() {
                var vm = this;
                return this.$services.get(`lookup/machines`).done(lstmachine => {
                    vm.lstmachine = lstmachine;
                })
            },
            loadType() {
                var vm = this;
                var type1 = {
                    id: 1,
                    value: 1,
                    text: i18n.t("Warning.Create.Type.Stranger")
                };
                var type2 = {
                    id: 2,
                    value: 2,
                    text: i18n.t("Warning.Create.Type.Temperature")
                };
                vm.lsttype = [];
                vm.lsttype.push(type1);
                vm.lsttype.push(type2);
            },
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            back() {
                this.$router.push({ path: '/notification/list' });
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.notification.notificationDetails = vm.lstpersonreceive;
                    this.$services.post('notification', this.notification).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/notification/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/notification/list' });
            },
            remove(item, index, button) {
                this.lstpersonreceive.splice(index, 1);
            },
        }
    }
</script>
<style scoped>
    div {
        width: 100%;
    }
</style>
