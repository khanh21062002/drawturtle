<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong class="card-title">
                            {{$t("System.User.Create.Header")}}
                        </strong>
                    </div>
                    <b-form @submit.prevent="save" autocomplete="off">
                        <!-- disable autocomplete -->
                        <input autocomplete="blah blah"
                               style="opacity: 0; position: absolute" />
                        <input type="text"
                               name="username"
                               autocomplete="blah blah"
                               style="opacity: 0; position: absolute" />
                        <input type="email"
                               name="email"
                               autocomplete="blah blah"
                               style="opacity: 0; position: absolute" />
                        <input type="password"
                               name="password"
                               autocomplete="blah blah"
                               style="opacity: 0; position: absolute" />
                        <!-- disable autocomplete -->
                        <b-row class="form-group pb-3">
                            <b-col md="6">
                                <b-form-group :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-for="name"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.CompanyName") }}
                                        <span class="required">(*)</span>
                                    </template>

                                    <b-form-input type="text"
                                                  style="display: none"
                                                  v-model="$v.user.companyId.$model"
                                                  :state="$v.user.companyId.$error ? false : null">
                                    </b-form-input>
                                    <treeselect :multiple="false"
                                                :options="treeselect.options"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                v-model="user.companyId" />
                                    <b-form-invalid-feedback>
                                        {{ $t("System.Company.Detail.Label.ParentRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="form-group pb-3">
                            <b-col md="6">
                                <b-form-group :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-for="username"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.Name") }}
                                        <span class="required">(*)</span>
                                    </template>
                                    <b-form-input type="text"
                                                  id="username"
                                                  v-model="$v.user.username.$model"
                                                  :state="
                      $v.user.username.$dirty ? !$v.user.username.$error : null
                    ">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.username.required">
                                        {{ $t("System.User.Detail.Label.UserNameRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.user.username.alphaNumAndDash">
                                        {{ $t("System.User.Detail.Label.UserNameAlphaNumAndDash") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label-for="fullName"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.FullName") }}
                                        <span class="required">(*)</span>
                                    </template>
                                    <b-form-input type="text"
                                                  id="fullName"
                                                  v-model="$v.user.fullName.$model"
                                                  @input="$v.user.fullName.$touch"
                                                  :state="
                      $v.user.fullName.$dirty ? !$v.user.fullName.$error : null
                    ">
                                    </b-form-input>
                                    <b-form-invalid-feedback id="fullNameFeedback">
                                        {{ $t("System.User.Detail.Label.NameRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="form-group pb-3">
                            <b-col md="6">
                                <b-form-group label-for="password"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.Password") }}
                                        <span class="required">(*)</span>
                                    </template>
                                    <b-form-input type="password"
                                                  id="password"
                                                  v-model="$v.user.password.$model"
                                                  @input="$v.user.password.$touch"
                                                  :state="
                      $v.user.password.$dirty ? !$v.user.password.$error : null
                    ">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.password.required">
                                        {{ $t("System.User.Detail.Label.PasswordRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="!$v.user.password.minLength">
                                        {{ $t("System.User.Detail.Label.MaxlengthRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="
                      $v.user.password.minLength &&
                      !$v.user.password.passwordPattern
                    ">
                                        {{ $t("System.User.Detail.Label.SpecialRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group label-for="passwordConfirmation"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.ConfirmPassword") }}
                                        <span class="required">(*)</span>
                                    </template>
                                    <b-form-input type="password"
                                                  id="passwordConfirmation"
                                                  v-model="$v.user.passwordConfirmation.$model"
                                                  @input="$v.user.passwordConfirmation.$touch"
                                                  :state="
                      $v.user.passwordConfirmation.$dirty
                        ? !$v.user.passwordConfirmation.$error
                        : null
                    ">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.user.passwordConfirmation.required">
                                        {{ $t("System.User.Detail.Label.ConfirmPasswordRequire") }}
                                    </b-form-invalid-feedback>
                                    <b-form-invalid-feedback v-if="
                      $v.user.passwordConfirmation.required &&
                      !$v.user.passwordConfirmation.sameAsPassword
                    ">
                                        {{ $t("System.User.Detail.Label.ConfirmationRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="form-group pb-3">
                            <b-col md="6">
                                <b-form-group label-for="PhoneNumber"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.Phone") }}
                                    </template>
                                    <b-form-input type="number"
                                                  id="PhoneNumber"
                                                  :formatter="formatYear"
                                                  v-model="user.PhoneNumber">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                            <b-col md="6">
                                <b-form-group :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-for="email"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        Email:
                                    </template>
                                    <b-form-input type="email" id="email" v-model="user.email">
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <!--<b-row>
                                        <b-col md="6">
                                            <b-form-group label="Trạng thái:"
                                                          :label-cols="4"
                                                          :horizontal="true"
                                                          label-align-md="right">
                                                <b-form-select class="col-sm-12" placeholder="Chọn giá trị"
                                                               v-model="user.status">
                                                    <b-form-select-option value="1">Hoạt động</b-form-select-option>
                                                    <b-form-select-option value="0">Ngừng hoạt động</b-form-select-option>
                                                </b-form-select>
                                            </b-form-group>
                                        </b-col>
                                    </b-row>-->
                        <b-row class="form-group pb-3">
                            <b-col md="6">
                                <b-form-group label-for="roles"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.User.Detail.Label.RoleName") }}
                                        <span class="required">(*)</span>
                                    </template>
                                    <b-form-input type="text"
                                                  id="passwordConfirmation"
                                                  style="display: none"
                                                  v-model="$v.user.roleId.$model"
                                                  :state="
                      $v.user.roleId.$dirty ? !$v.user.roleId.$error : null
                    ">
                                    </b-form-input>
                                    <b-form-radio-group v-model="$v.user.roleId.$model"
                                                        class="radio-group"
                                                        :state="
                      $v.user.roleId.$dirty ? !$v.user.roleId.$error : null
                    ">
                                        <b-form-radio v-for="role in listRoles"
                                                      :key="role.id"
                                                      :value="role.id"
                                                      class="col-md-12">{{ $t(role.text) }}</b-form-radio>
                                    </b-form-radio-group>

                                    <b-form-invalid-feedback v-if="!$v.user.roleId.required">
                                        {{ $t("System.User.Detail.Label.RoleNameRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <div class="text-center form-group pb-3">
                            <b-button @click="save"
                                      v-if="authorize(['ManageUser'])"
                                      variant="primary">{{ $t("Button.Submit") }}</b-button>
                            <b-button type="button" variant="secondary" @click="cancel">
                                {{
                $t("Button.Cancel")
                                }}
                            </b-button>
                        </div>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>
<script>
    import { validationMixin } from "vuelidate";
    import { authorizationMixin } from "@/shared/mixins";
    import { required, sameAs, minLength, helpers } from "vuelidate/lib/validators";
    const passwordPattern = helpers.regex(
        "passwordPattern",
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$/
    );
    //Chấp nhận aphabet và dấu ngạch ngang
    const alphaNumAndDash = helpers.regex("alphaNumAndDash", /^[a-z\d_]*$/i);
    import i18n from "@/libs/i18n";
    export default {
        name: "CreateUser",
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                listRoles: [],
                user: {
                    status: null,
                    username: null,
                    password: null,
                    passwordConfirmation: null,
                    companyId: null,
                    fullName: null,
                    email: null,
                    PhoneNumber: null,
                    roleIds: [],
                    roleId: null,
                },
                lstcompany: {},
                editing: false,
                treeselect: {
                    value: null,
                    options: [],
                },
            };
        },
        validations: {
            user: {
                username: { required, alphaNumAndDash },
                password: { required, minLength: minLength(8), passwordPattern },
                passwordConfirmation: { required, sameAsPassword: sameAs("password") },
                companyId: { required },
                fullName: { required },
                roleId: { required },
            },
        },

        created() {
            var vm = this;
            this.$services.get("lookup/roles").done((response) => {
                //for (var i = 0; i < response.length; i++) {
                //    let item = response[i];

                //    let name = i18n.t(item.text);

                //    item.text = name;

                //    vm.listRoles.push(item);

                //}

                vm.listRoles = response;
            });
            //this.loadCompany();
            this.loadCompanyTree();
            this.user.status = 1;
        },
        methods: {
            formatYear(e) {
                return String(e).substring(0, 10);
            },
            edit() {
                debugger;
                this.editing = false;
            },
            //Danh sách cong ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done((lstcompany) => {
                    vm.lstcompany = lstcompany;
                });
            },
            //Danh sách cong ty - tree view
            loadCompanyTree() {
                var vm = this;
                return this.$services.get(`lookup/company-tree`).done((lstcompany) => {
                    vm.treeselect.options = lstcompany;
                });
            },
            cancel() {
                this.$router.push({ path: "/system/users/list" });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    vm.user.roleIds = [];
                    vm.user.roleIds.push(vm.user.roleId);
                    this.$services.post("users", this.user).done((id) => {
                        vm.$router.push({ path: "/system/users/list" });
                        let message = i18n.t("Messages.CreateSuccess");
                        vm.$toastr.s(message);
                    });
                }
            },
        },
    };
</script>
<style scoped></style>
