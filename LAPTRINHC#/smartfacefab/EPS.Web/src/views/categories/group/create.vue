<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("Categories.Group.Create.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
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
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Group.Detail.Label.CodeGroup')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="txt_groupcode"
                                                  v-model.trim="$v.group.groupcode.$model"
                                                  :state="$v.group.groupcode.$dirty? !$v.group.groupcode.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.group.groupcode.required">
                                        {{$t('Categories.Group.Detail.Label.Coderequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.group.groupcode.maxLengthCode">
                                        {{$t('Categories.Group.Detail.Label.MalenghtCode')}}
                                    </b-form-invalid-feedback>
                                    <!--<b-form-invalid-feedback v-if="!$v.group.groupcode.passwordPattern">
                                        {{$t('Categories.Group.Detail.Label.EspeciallyCode')}}
                                    </b-form-invalid-feedback>-->
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Group.List.SearchForm.GroupName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text" id="txt_groupname"
                                                  v-model="$v.group.groupname.$model"
                                                  :state="$v.group.groupname.$dirty? !$v.group.groupname.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.group.groupname.required">
                                        {{$t('Categories.Group.Detail.Label.GroupNameRequire')}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.group.groupname.maxLength">
                                        {{$t('Categories.Group.Detail.Label.GroupNamerequire')}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label-cols="3">
                                    <b-button v-if="authorize(['ManageGroup'])" @click="save" variant="primary"><i class="fa fa-floppy-o"></i> {{$t("Button.Submit")}}</b-button>
                                    <b-button type="button" variant="secondary" style="width: 120px" @click="cancel"><i class="fa fa-ban"></i> {{$t("Button.Cancel")}}</b-button>
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
    import Services from '@/utils/services'
    import { required, maxLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^[a-zA-Z0-9_]*$/)
    import i18n from '@/libs/i18n'

    export default {
        name: 'GroupDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                group: {
                    groupcode: null,
                    groupname: null,
                    note: null,
                    status: 1,
                    deleted: null,
                    compId: null,
                },
                lstdepartment: [],
                lstcompany: [],
                isAdmin: false,
            }
        },
        validations: {
            group: {
                groupname: {
                    required,
                    maxLength: maxLength(250),
                },
                groupcode: {
                    //passwordPattern,
                    required,
                    maxLengthCode: maxLength(50),
                }
            }
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.group.compId = accessToken.comId;
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
            back() {
                this.$router.push({ path: '/categories/group/list' });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post('group', this.group).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: '/categories/group/list' });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: '/categories/group/list' });
            }
        }
    }
</script>
<style scoped>
</style>
