<template>
    <CContainer class="d-flex content-center min-vh-100">
        <CRow>
            <CCol>
                <CCardGroup>
                    <CCard class="p-4">
                        <CCardBody>
                            <CForm @submit.prevent="login">
                                <h1>{{displayName}}</h1>
                                <p class="text-muted">Đăng nhập</p>
                                <b-form-input type="text" id="txt_username" style="display: none;"
                                              v-model="$v.username.$model"
                                              :state="$v.username.$dirty? !$v.username.$error : null">
                                </b-form-input>
                                <b-form-invalid-feedback v-if="!$v.username.required" style="text-align: left;">
                                    Vui lòng nhập tên tài khoản
                                </b-form-invalid-feedback>
                                <CInput placeholder="Người dùng" v-model="$v.username.$model" :state="$v.username.$dirty? !$v.username.$error : null"
                                        autocomplete="username email">
                                    <template #prepend-content>
                                        <CIcon name="cil-user" />
                                    </template>
                                </CInput>

                                <b-form-input type="text" id="txt_password" style="display: none;"
                                              v-model="$v.password.$model"
                                              :state="$v.password.$dirty? !$v.password.$error : null">
                                </b-form-input>
                                <b-form-invalid-feedback v-if="!$v.password.required" style="text-align: left;">
                                    Vui lòng nhập mật khẩu
                                </b-form-invalid-feedback>
                                <CInput placeholder="Mật khẩu" v-model="$v.password.$model" :state="$v.password.$dirty? !$v.password.$error : null"
                                        type="password"
                                        autocomplete="curent-password">
                                    <template #prepend-content>
                                        <CIcon name="cil-lock-locked" />
                                    </template>
                                </CInput>
                                <CRow>
                                    <CCol col="8" style="margin:auto;">
                                        <CButton color="primary" class="px-4" type="submit">Đăng nhập</CButton>
                                    </CCol>
                                </CRow>
                            </CForm>
                        </CCardBody>
                    </CCard>
                </CCardGroup>
            </CCol>
        </CRow>
    </CContainer>
</template>

<script>
    import { validationMixin } from 'vuelidate'
    import { required } from 'vuelidate/lib/validators'
    import i18n from '@/libs/i18n'
    export default {
        name: 'Login',
        mixins: [validationMixin],
        data() {
            return {
                username: null,
                password: null,
                errorMsg: null,
                displayName: "",
            };
        },
        validations: {
            username: {
                required
            },
            password: {
                required,
            }
        },
        created() {
            var vm = this;
            $.getJSON('brandconfig.json', function (data) {
                if (data && data.Company && data.Company.DisplayName) {
                    vm.displayName = data.Company.DisplayName;
                }
            });
            localStorage.removeItem("companySt");
            localStorage.removeItem("itemsPerPageSt");
            localStorage.removeItem("personTypeSt");
            localStorage.removeItem("dateFromSt");
            localStorage.removeItem("dateToSt");
            localStorage.removeItem("fullNameSt");
            localStorage.removeItem("phoneNumberSt");

            //localStorage.removeItem("searchFormVehicle");
            //localStorage.removeItem("paginationVehicle");
        },
        methods: {
            login() {
                var vm = this;
                this.errorMsg = null;
                this.$v.$touch();
                if (!this.$v.$invalid) {

                    this.$store.dispatch("authenticate", { username: this.username, password: this.password })
                        .done(() => vm.$router.push({ path: '/' }))
                        .fail(
                            function (error) {
                                if (error.responseJSON) {
                                       var messageMultiLang = "";

                                    if (error.responseJSON.ExceptionMessage) {
                                        messageMultiLang = error.responseJSON.ExceptionMessage;
                                    } else if (error.responseJSON.Message) {
                                        messageMultiLang = error.responseJSON.Message;
                                    } else if (error.responseJSON.error) {
                                        messageMultiLang = error.responseJSON.error;
                                    } else  {
                                        messageMultiLang = JSON.stringify(error.responseJSON.error);
                                    }
                                    var message = i18n.t(messageMultiLang);
                                    debugger;
                                    alert(message);

                                } else if (error.responseText) {
                                    let message = i18n.t(error.responseText);
                                    alert(message);
                                }
                            }
                        );
                }
            }
        }
    }
</script>
