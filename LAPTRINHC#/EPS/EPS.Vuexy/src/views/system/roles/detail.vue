<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong class="card-title">
                            {{
              $t("System.Role.Detail.Header")
                            }}
                        </strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row class="form-group pb-3">
                            <b-col lg="6" offset-lg="3">
                                <b-form-group :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-for="name"
                                              class="form-group row pb-4"
                                              label-class=" 'col-lg-3 control-label text-lg-start pt-2'
                  ">
                                    <template slot="label">
                                        {{ $t("System.Role.List.SearchForm.RoleName") }}
                                        <span v-if="editing" class="required">(*)</span>
                                    </template>
                                    <b-form-input type="text"
                                                  id="name"
                                                  v-if="editing"
                                                  v-model="$v.role.name.$model"
                                                  :state="$v.role.name.$dirty ? !$v.role.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback>
                                        {{ $t("System.Role.Detail.Label.NameRequire") }}
                                    </b-form-invalid-feedback>
                                    <label class="col-form-label" v-if="!editing">
                                        {{
                    $t(role.name)
                                        }}
                                    </label>
                                </b-form-group>
                            </b-col>
                        </b-row>

                        <b-row class="form-group pb-3">
                            <b-col lg="6" offset-lg="3">
                                <b-form-group :label="this.$t('System.Role.Detail.Label.Description')"
                                              label-align-md="left"
                                              label-for="name"
                                              class="form-group row pb-4"
                                              label-class="'col-lg-3 control-label text-lg-start pt-2'"
                                              :label-cols="4"
                                              :horizontal="true">
                                    <b-form-textarea id="description"
                                                     v-model="role.description"
                                                     v-if="editing"></b-form-textarea>
                                    <label class="col-form-label" v-if="!editing">
                                        {{
                    role.description
                                        }}
                                    </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row class="form-group pb-3">
                            <b-col lg="6" offset-lg="3">
                                <b-form-group :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-for="name"
                                              class="form-group row pb-4"
                                              label-class=" col-lg-4 control-label text-lg-start pt-2
                  ">
                                    <template slot="label">
                                        {{ $t("System.Role.Detail.Label.Status") }}
                                    </template>
                                    <select2 v-model="role.status"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{ multiple: true, closeOnSelect: false }"
                                             :options="listStatus"
                                             v-if="editing"
                                             style="width: 100%"></select2>
                                    <label class="col-form-label" v-if="!editing">
                                        {{
                    $t(role.statusName)
                                        }}
                                    </label>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <div class="text-center form-group pb-3">
                            <b-button type="button"
                                      variant="primary"
                                      @click="edit"
                                      v-if="!editing && authorize(['ManageRole'])">
                                <i class="fa fa-pencil-square-o"></i>
                                {{ $t("Button.Edit") }}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="back"
                                      v-if="!editing"><i class="fa fa-ban"></i> {{ $t("Button.Back") }}</b-button>
                            <b-button type="submit" variant="primary" v-if="editing">
                                <i class="fa fa-floppy-o"></i>
                                {{ $t("Button.Submit") }}
                            </b-button>
                            <b-button type="button"
                                      variant="secondary"
                                      @click="cancel"
                                      v-if="editing"><i class="fa fa-ban"></i> {{ $t("Button.Cancel") }}</b-button>
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
    import { required } from "vuelidate/lib/validators";
    import i18n from "@/libs/i18n";

    export default {
        name: "RoleDetail",
        mixins: [validationMixin, authorizationMixin],
        data() {
            return {
                role: {
                    id: null,
                    name: null,
                    description: null,
                },
                lstcompany: [],
                editing: false,
                listStatus: [
                    {
                        id: 0,
                        text: this.$t("StatusList.Inactive"),
                    },
                    {
                        id: 1,
                        text: this.$t("StatusList.Active"),
                    },
                ],
            };
        },
        computed: {
            roleId() {
                return this.$route.params.roleId;
            },
        },
        validations: {
            role: {
                name: { required },
            },
        },
        created() {
            this.loadRoleDetail();
            this.loadCompany();
        },
        methods: {
            loadCompany() {
                var vm = this;
                return this.$services.get(`lookup/companys`).done((lstcompany) => {
                    vm.lstcompany = lstcompany;
                });
            },
            loadRoleDetail() {
                var vm = this;
                return this.$services.get(`roles/${this.roleId}`).done((role) => {
                    vm.role = role;
                });
            },
            edit() {
                this.editing = true;
            },
            back() {
                this.$router.push({ path: "/system/roles" });
            },
            cancel() {
                var vm = this;
                this.loadRoleDetail().done(() => (vm.editing = false));
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`roles/${this.roleId}`, this.role).done(() => {
                        vm.$toastr.s(i18n.t("Messages.UpdateSuccess"));
                        vm.cancel();
                    });
                }
            },
        },
    };
</script>
<style scoped></style>
