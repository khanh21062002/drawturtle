<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Group.Detail.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="2">
                                <b-form-group :label="this.$t('Timesheet.Dayoff.List.SearchForm.CompanyName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-select class="col-sm-12" :placeholder="this.$t('PlaceHolder.Select')" :disabled="true"
                                                   v-model="group.compId" :options="lstcompany">
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="2">
                                <b-form-group :label="this.$t('Categories.Group.Detail.Label.CodeGroup')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="txt_groupCode" v-if="editing"
                                                  v-model.trim="$v.group.groupCode.$model"
                                                  :state="$v.group.groupCode.$dirty? !$v.group.groupCode.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.group.groupCode.required">
                                        {{$t('Categories.Group.Detail.Label.Coderequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.group.groupCode.maxLengthCode">
                                        {{$t('Categories.Group.Detail.Label.MalenghtCode')}}
                                    </b-form-invalid-feedback>
                                    <!--<b-form-invalid-feedback v-if="!$v.group.groupCode.passwordPattern">
                                        {{$t('Categories.Group.Detail.Label.EspeciallyCode')}}
                                    </b-form-invalid-feedback>-->
                                    <label class="col-form-label" v-if="!editing">{{ group.groupCode }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="2">
                                <b-form-group :label="this.$t('Categories.Group.List.SearchForm.GroupName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">
                                    <b-form-input type="text" id="txt_groupName" v-if="editing"
                                                  v-model="$v.group.groupName.$model"
                                                  :state="$v.group.groupName.$dirty? !$v.group.groupName.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.group.groupName.required">
                                        {{$t('Categories.Group.Detail.Label.GroupNameRequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.group.groupName.maxLength">
                                        {{$t('Categories.Group.Detail.Label.GroupNamerequire')}}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">{{ group.groupName }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center">
                            <b-button type="button" variant="primary" @click="edit" v-if="!editing && authorize(['ManageGroup'])"><i class="fa fa-pencil-square-o"></i> {{$t("Button.Edit")}}</b-button>
                            <b-button type="button" variant="secondary" @click="back" v-if="!editing"><i class="fa fa-ban"></i> {{$t("Button.Back")}}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing"><i class="fa fa-floppy-o"></i>{{$t("Button.Submit")}}</b-button>
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
    import { required, maxLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^[a-zA-Z0-9_]*$/)
    import i18n from '@/libs/i18n'

    export default {
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                group: {
                    groupid: null,
                    groupCode: null,
                    groupName: null,
                    note: null,
                    status: null,
                    deleted: null,
                    statuS_NAME: null,
                },
                lstdepartment: {},
                lstcompany: [],
                editing: false,
                isAdmin: false,
            }
        },
        computed: {
            groupId() {
                return this.$route.params.groupId;
            },
        },
        validations: {
            group: {
                groupName: {
                    required,
                    maxLength: maxLength(250),
                },
                groupCode: {
                    //passwordPattern,
                    required,
                    maxLengthCode: maxLength(50),
                }
            }
        },
        async created() {
            await this.loadGroupDetail();
            await this.loadCompany();
        },
        methods: {
            //Danh sách công ty
            async loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(res => {
                    vm.lstcompany = res;
                });
            },
            async loadGroupDetail() {
                var vm = this;
                return this.$services.get(`group/${this.groupId}`).done(res => {
                    vm.group = res;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/categories/group/list' });
            },
            async cancel() {
                var vm = this;
                await this.loadGroupDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`group/${this.groupId}`, this.group).done(() => {
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
