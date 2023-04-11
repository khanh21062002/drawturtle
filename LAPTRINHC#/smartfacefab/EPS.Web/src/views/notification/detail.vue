<template>

    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Warning.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="11">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Company')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left"
                                                      label-class="required">
                                            <label class="col-form-label">{{ notification.compName }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Machine')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.notification.machineId.$model"
                                                          :state="$v.notification.machineId.$error ? false : null">
                                            </b-form-input>
                                            <label class="col-form-label" v-if="!editing">{{ notification.machineName }}</label>
                                            <select2 v-if="editing" :placeholder="this.$t('PlaceHolder.Select')"
                                                     v-model="notification.machineId" :options="lstmachine">
                                            </select2>
                                            <b-form-invalid-feedback v-if="editing">
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
                                            <b-form-input v-if="editing" type="text" style="display: none"
                                                          v-model="$v.notification.type.$model"
                                                          :state="$v.notification.type.$error ? false : null">
                                            </b-form-input>
                                            <label class="col-form-label" v-if="!editing">{{ $t(notification.typeName) }}</label>
                                            <select2 :placeholder="this.$t('PlaceHolder.Select')" v-if="editing"
                                                     v-model="notification.type" :options="lsttype">
                                            </select2>
                                            <b-form-invalid-feedback v-if="editing">
                                                {{$t("Warning.Create.Form.TypeRequire")}}
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6" v-if="checkperson">
                                        <b-form-group :label="this.$t('Warning.Create.Form.Thresholds')"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="left" label-class="required">
                                            <b-form-input v-if="editing" type="text"
                                                          v-model="$v.notification.scoreMatch.$model"
                                                          :state="$v.notification.scoreMatch.$error ? false : null">
                                            </b-form-input>
                                            <label class="col-form-label" v-if="!editing">{{ notification.scoreMatch }}</label>
                                            <b-form-invalid-feedback v-if="editing">
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
                            <b-col md="12">
                                <b-table striped hover :items="lstpersonreceive" :fields="personFields">
                                    <template v-slot:cell(index)="row">
                                        {{ row.index + 1 }}
                                    </template>
                                    <template v-slot:cell(personId)="row">
                                        <select2 :placeholder="$t('PlaceHolder.Select')" v-if="editing"
                                                 v-model="row.item.personId" :options="lstperson">
                                        </select2>
                                        <label class="col-form-label" v-if="!editing">{{ row.item.fullName }}</label>
                                    </template>
                                    <template v-slot:cell(email)="row">
                                        <b-form-input v-if="editing" type="email" v-model="row.item.email" required oninvalid="this.setCustomValidity('Vui lòng nhập Email hợp lệ')" oninput="this.setCustomValidity('')" />
                                        <label class="col-form-label" v-if="!editing">{{ row.item.email }}</label>
                                    </template>
                                    <template v-slot:cell(action)="row">
                                        <b-button-group v-if="editing">
                                            <b-button  size="sm" @click.stop="remove(row.item, row.index, $event.target)" class="mr-1" :title="$t('TitleDetail.Delete')">
                                                <i class="fa fa-trash-o" aria-hidden="true"></i>
                                            </b-button>
                                            <b-button size="sm" @click="addRow(row.index)" class="mr-1" :title="$t('TitleDetail.Add')">+</b-button>
                                        </b-button-group>
                                    </template>
                                </b-table>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="8" offset="1">
                                <b-form-group :label-cols="6"
                                              :horizontal="true">
                                    <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageNotification'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                                    <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
                    compName: null,
                    type: null,
                    typeName: null,
                    machineId: null,
                    machineName: null,
                    scoreMatch: null,
                    timeSchedule: null,
                    notificationDetails: [],
                },
                lstcompany: [],
                lstperson: [],
                lsttype: [],
                lstmachine: [],
                isAdmin: false,
                isExist: false,
                editing: false,
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
                scoreMatch: { required },
            }
        },
        computed: {
            checkperson: function () {
                if (this.notification.type == 1) return true;
                return false;
            },
            notificationId() {
                return this.$route.params.notificationId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.notification.compId = accessToken.comId;
            this.loadType();
            this.loadCompany();
            this.loadMachine();
            this.loadPerson();
            this.loadDetail();
            this.loadPersonDetail();
        },
        methods: {
            async loadDetail() {
                var vm = this;
                return this.$services.get(`notification/${this.notificationId}`).done(notification => {
                    vm.notification = notification;
                });
            },
            loadPersonDetail() {
                var vm = this;
                return this.$services.get(`notification/getListNotiDetails/${this.notificationId}`).done(lstpersonreceive => {
                    vm.lstpersonreceive = [];
                    for (let index = 0; index < lstpersonreceive.length; index++) {
                        let item = lstpersonreceive[index];
                        item.personId = item.personId.toUpperCase();
                        vm.lstpersonreceive.push(item);
                    }
                });
            },
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
            edit() {
                this.editing = true;
            },
            async cancel() {
                var vm = this;
                await this.loadDetail();
                vm.editing = false;
            },
            async save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.notification.notificationDetails = vm.lstpersonreceive;
                    this.$services.put(`notification/${this.notificationId}`, this.notification).done(() => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
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
