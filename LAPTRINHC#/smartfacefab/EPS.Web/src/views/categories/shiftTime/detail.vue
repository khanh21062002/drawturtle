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
                                                      label-align-md="right">
                                            <label class="col-form-label" >{{ shifttime.compName }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Tên ca làm việc:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right"
                                                      label-class="required">
                                            <b-form-input style="width: 48%;" type="text" id="txt_name" v-if="editing"
                                                          v-model="$v.shifttime.shiftName.$model"
                                                          :state="$v.shifttime.shiftName.$dirty? !$v.shifttime.shiftName.$error : null">
                                            </b-form-input>
                                            <b-form-invalid-feedback v-if="editing">
                                                Vui lòng nhập tên ca làm việc
                                            </b-form-invalid-feedback>
                                            <label class="col-form-label" v-if="!editing">{{ shifttime.shiftName }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group label="Giờ bắt đầu làm việc:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button  v-model="shifttime.beginShiftTime" v-if="editing"></vue-timepicker>
                                            <label class="col-form-label" v-if="!editing">{{ shifttime.beginShiftTime }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Giờ kết thúc làm việc:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker  advanced-keyboard hide-clear-button  v-model="shifttime.endShiftTime" v-if="editing"></vue-timepicker>
                                            <label class="col-form-label" v-if="!editing">{{ shifttime.endShiftTime }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6">
                                        <b-form-group label="Giờ bắt đầu giải lao:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button  v-model="shifttime.beginFreeShiftTime"  v-if="editing"></vue-timepicker>
                                            <label class="col-form-label" v-if="!editing">{{ shifttime.beginFreeShiftTime }}</label>
                                        </b-form-group>
                                    </b-col>
                                    <b-col md="6">
                                        <b-form-group label="Giờ kết thúc giải lao:"
                                                      :label-cols="4"
                                                      :horizontal="true"
                                                      label-align-md="right">
                                            <vue-timepicker advanced-keyboard hide-clear-button  v-model="shifttime.endFreeShiftTime" v-if="editing"></vue-timepicker>
                                            <label class="col-form-label" v-if="!editing">{{ shifttime.endFreeShiftTime }}</label>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col md="6" offset="3">
                                        <b-form-group :label-cols="4"
                                                      :horizontal="true">
                                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageShiftTime'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                            <b-button type="button" variant="secondary" @click="cancel" v-if="editing"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
                    id: null,
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
                editing: false,
            }
        },
        validations: {
            shifttime: {
                shiftName: { required }
            }
        },
        computed: {
            shifttimeId() {
                return this.$route.params.shifttimeId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            await this.loadCompany();
            this.loadShiftTimeDetail();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            async loadShiftTimeDetail() {
                var vm = this;
                return this.$services.get(`categories/shifttime/${this.shifttimeId}`).done(shifttime => {
                    vm.shifttime = shifttime;
                });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`categories/shifttime/${this.shifttimeId}`, this.shifttime).done(() => {
                        vm.$toastr.s('Cập nhật ca làm việc thành công');
                        vm.$router.push({ path: '/categories/shifttime/list' });
                    });
                }
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/shifttime/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadShiftTimeDetail();
                vm.editing = false;
            },
        }
    }
</script>
<style scoped>
    div {
        width: 100%;
    }
</style>
