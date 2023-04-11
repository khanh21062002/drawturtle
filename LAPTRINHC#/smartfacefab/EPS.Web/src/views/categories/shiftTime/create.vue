<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>Thêm mới ca làm việc</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="8" offset="2">
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group label="Đơn vị:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right" label-class="required">
                                            <b-form-input type="text" id="txt_name"
                                                          v-model="$v.shifttime.compId.$model" style="display: none;"
                                                          :state="$v.shifttime.compId.$dirty? !$v.shifttime.compId.$error : null">
                                            </b-form-input>
                                            <select2 placeholder="Chọn giá trị" :disabled="true" :settings="{allowClear: true}"
                                                     v-model="shifttime.compId" :options="lstcompany">
                                            </select2>
                                            <b-form-invalid-feedback>
                                                Vui lòng chọn đơn vị
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Tên ca làm việc:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right"
                                                      label-class="required">
                                            <b-form-input type="text" id="txt_name"
                                                          v-model="$v.shifttime.shiftName.$model"
                                                          :state="$v.shifttime.shiftName.$dirty? !$v.shifttime.shiftName.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback>
                                                Vui lòng nhập tên ca làm việc
                                            </b-form-invalid-feedback>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group label="Giờ bắt đầu làm việc:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button v-model="shifttime.beginShiftTime"></vue-timepicker>

                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Giờ kết thúc làm việc:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button v-model="shifttime.endShiftTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group label="Giờ bắt đầu giải lao:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button v-model="shifttime.beginFreeShiftTime"></vue-timepicker>

                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Giờ kết thúc giải lao:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button v-model="shifttime.endFreeShiftTime"></vue-timepicker>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label-cols="4"
                                                      :horizontal="true">
                                            <b-button v-if="authorize(['ManageShiftTime'])" type="submit" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
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

    export default {
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                shifttime: {
                    id: 0,
                    shiftName: null,
                    compId: null,
                    beginShiftTime: '01:00',
                    endShiftTime: '01:00',
                    beginFreeShiftTime: '01:00',
                    endFreeShiftTime: '01:00',
                    deptId: null,
                    compId: null,
                },
                lstcompany: [],
                isAdmin: false,
                isExist: false,
            }
        },
        validations: {
            shifttime: {
                shiftName: { required },
                compId: { required },
            }
        },
        computed: {
            companyId: function () {
                return this.shifttime.compId;
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.shifttime.compId = accessToken.comId;
            await this.loadCompany();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;

                });
            },
            async loadShiftTimeByCompId() {
                var vm = this;
                return this.$services.get(`categories/shifttime/loadShiftTimeByCompId/${vm.companyId}`).done(shifttime => {
                    if (shifttime) {
                        vm.isExist = true;
                    } else {
                        vm.isExist = false;
                    }
                });
            },
            back() {
                this.$router.push({ path: '/categories/shifttime/list' });
            },
            async save() {
                this.$v.$touch();

                if (!this.$v.$invalid) {
                    var vm = this;
                    await vm.loadShiftTimeByCompId();
                    if (!vm.isExist) {
                        this.$services.post('categories/shifttime', this.shifttime).done((id) => {
                            vm.$toastr.s('Tạo mới ca làm việc thành công');
                            vm.$router.push({ path: '/categories/shifttime/list' });
                        });
                    } else {
                        alert("Công ty này đã tồn tại ca làm việc!")
                    }
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/shifttime/list' });
            }
        }
    }
</script>
<style scoped>
    div {
        width: 100%;
    }
</style>
