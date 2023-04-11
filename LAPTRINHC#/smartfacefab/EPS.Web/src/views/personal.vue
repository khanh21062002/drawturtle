<template>
    <div class="animated fadeIn">
        <b-col>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t("ChangePassword.List.Header")}}</strong>
                    </div>
                    <b-form @submit.prevent="save" autocomplete="off">
                        <!-- disable autocomplete -->
                        <input autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="text" name="username" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <input type="password" name="password" autocomplete="blah blah" style="opacity: 0;position: absolute;">
                        <!-- disable autocomplete -->
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.User.List.SearchForm.UserName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <label class="col-form-label">{{ user.username }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('System.User.Detail.Label.FullName')"
                                              label-for="fullName"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing? 'required' : ''">

                                    <label class="col-form-label" v-if="!editing">{{ user.fullName }}</label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('ChangePassword.List.Form.OldPass')"
                                              label-for="ExPass"
                                              :label-cols="4"
                                              label-align-md="left"
                                              :horizontal="true" label-class="required">
                                    <b-form-input type="password" id="exPass"
                                                  v-model="$v.user.exPass.$model"
                                                  :state="$v.user.exPass.$dirty? !$v.user.exPass.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.exPass.required">
                                        {{$t("ChangePassword.List.Form.Message.OldPass")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col md="6" offset="3">

                                <b-form-group :label="this.$t('ChangePassword.List.Form.NewPass')"
                                              label-for="newPassword"
                                              :label-cols="4"
                                              label-align-md="left"
                                              :horizontal="true"  label-class="required">
                                    <b-form-input type="password" id="newPassword"
                                                  v-model="$v.user.newPassword.$model"
                                                  @input="$v.user.newPassword.$touch"
                                                  :state="$v.user.newPassword.$dirty? !$v.user.newPassword.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.newPassword.required">
                                        {{$t("ChangePassword.List.Form.Message.NewPass")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.user.newPassword.minLength">
                                        {{$t("ChangePassword.List.Form.Message.PassRequired")}}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="$v.user.newPassword.minLength && !$v.user.newPassword.passwordPattern">
                                        {{$t("ChangePassword.List.Form.Message.PassWordRequired")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('ChangePassword.List.Form.NewPassConfirmation')"
                                              label-for="newPasswordConfirmation"
                                              :label-cols="4"
                                              label-align-md="left"
                                              :horizontal="true"  label-class="required">
                                    <b-form-input type="password" id="newPasswordConfirmation"
                                                  v-model="$v.user.newPasswordConfirmation.$model"
                                                  @input="$v.user.newPasswordConfirmation.$touch"
                                                  :state="$v.user.newPasswordConfirmation.$dirty? !$v.user.newPasswordConfirmation.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.newPasswordConfirmation.sameAsPassword">
                                        {{$t("ChangePassword.List.Form.Message.NewPassConfirmation")}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>



                        <div class="text-center">
                            <b-button type="submit" variant="primary">{{$t("Button.Submit")}}</b-button>
                        </div>
                    </b-form>
                </b-card>
            </b-col>
        </b-col>

    </div>
</template>
<script>
    import { validationMixin } from 'vuelidate'
    import { authorizationMixin } from '@/shared/mixins'
    import { required, sameAs, minLength, helpers } from 'vuelidate/lib/validators'
    const passwordPattern = helpers.regex('passwordPattern', /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/)
    import i18n from '@/libs/i18n'

    export default {
        name: 'UserDetail',
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                user: {
                    id: null,
                    username: null,
                    newPassword: null,
                    newPasswordConfirmation: null,
                    fullName: null,
                    email: null,
                    PhoneNumber: null,
                    roleIds: [],
                    roleId: null,
                    roleNames: null,
                    exPass: null,
                }, lstcompany: [],
                editing: false,
                listRoles: []
            }
        },
        computed: {
            userId() {
                return this.$route.params.userId;
            },
        },
        validations: {
            user: {
                exPass: { required },
                newPassword: { required, minLength: minLength(8), passwordPattern },
                newPasswordConfirmation: { sameAsPassword: sameAs('newPassword') },
            }
        },
        created() {
            var vm = this;
            this.$services.get('lookup/roles').done((response) => vm.listRoles = response);
            this.loadUserDetail();
            this.loadCompany();
        },
        methods: {
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done(lstcompany => {
                    vm.lstcompany = lstcompany;
                });
            },
            loadUserDetail() {
                var vm = this;
                this.$services.get(`users/get-self-info`).done(user => {
                    user.exPass = "";
                    user.newPassword = "";
                    user.newPasswordConfirmation = "";
                    user.roleId = user.roleIds[0];
                    vm.user = user;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: '/system/users' });
            },
            async cancel() {
                var vm = this;
                await vm.loadUserDetail();
                vm.editing = false;
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.user.roleIds = [];
                    vm.user.roleIds.push(vm.user.roleId);
                    var infoId = this.userId;
                    this.$services.put(`users/change-password/${infoId}`, this.user).done(() => {
                        alert(i18n.t('ChangePassword.List.Form.Message.Header'))
                        vm.$store.dispatch('deauthenticate');
                        vm.$router.push({ path: '/login' });
                    });
                }
            }
        }
    }
</script>
<style scoped>
</style>
