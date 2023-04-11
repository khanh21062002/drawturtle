<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong class="card-title">
                            {{
              $t("System.Role.Create.Header")
                            }}
                        </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row class="form-group pb-3">
                            <b-col lg="6" offset-lg="3">
                                <b-form-group label-for="name"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="col-lg-3 control-label text-lg-start pt-2"
                                              class="form-group row pb-4">
                                    <template slot="label">
                                        {{ $t("System.Role.List.SearchForm.RoleName") }}
                                        <span class="required">(*)</span>
                                    </template>
                                    <b-form-input type="text"
                                                  id="name"
                                                  v-model="$v.role.name.$model"
                                                  :state="$v.role.name.$dirty ? !$v.role.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t("System.Role.Detail.Label.NameRequire") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row class="form-group pb-3">
                            <b-col lg="6" offset-lg="3">
                                <b-form-group :label="this.$t('System.Role.Detail.Label.Description')"
                                              label-align-md="left"
                                              label-for="name"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4"
                                              :label-cols="4"
                                              :horizontal="true">
                                    <b-form-textarea id="description"
                                                     v-model="role.description"></b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="form-group pb-3">
                            <b-col lg="6" offset-lg="3">
                                <b-form-group :label="this.$t('System.Role.Detail.Label.Status')"
                                              label-align-md="left"
                                              label-for="name"
                                              label-class="col-lg-3 control-label text-lg-start pt-2 "
                                              class="form-group row pb-4"
                                              :label-cols="4"
                                              :horizontal="true">
                                    <b-form-select class="col-sm-12"
                                                   :placeholder="this.$t('PlaceHolder.Select')"
                                                   v-model="role.status"
                                                   style="height: 38px; padding-left: 10px; border-radius: 0">
                                        <b-form-select-option value="1">
                                            {{
                      $t("System.Company.List.Active")
                                            }}
                                        </b-form-select-option>
                                        <b-form-select-option value="0">
                                            {{
                      $t("System.Company.List.Stop")
                                            }}
                                        </b-form-select-option>
                                    </b-form-select>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="form-group pb-3">
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageRole'])"
                                              type="submit"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i>
                                        {{ $t("Button.Submit") }}
                                    </b-button>
                                    <b-button type="button" variant="secondary" @click="cancel">
                                        <i class="fa fa-ban"></i>
                                        {{ $t("Button.Cancel") }}
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
    import { validationMixin } from "vuelidate";
    import { authorizationMixin } from "@/shared/mixins";
    import { required } from "vuelidate/lib/validators";
    import i18n from "@/libs/i18n";

    export default {
        name: "CreateRole",
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                role: {
                    status: null,
                    name: null,
                    description: null,
                },
                lstcompany: [],
            };
        },
        validations: {
            role: {
                name: { required },
            },
        },
        created() {
            this.loadCompany();
            this.role.status = 1;
        },
        methods: {
            //Danh sách cong ty
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done((lstcompany) => {
                    vm.lstcompany = lstcompany;
                });
            },
            cancel() {
                this.$router.push({ path: "/system/roles/list" });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post("roles", this.role).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.CreateSuccess"));
                        vm.$router.push({ path: "/system/roles/list" });
                    });
                }
            },
        },
    };
</script>
<style scoped></style>
