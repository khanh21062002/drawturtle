<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.Areas.Create.Header") }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Areas.Detail.Form.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model.trim="$v.areas.Code.$model"
                                                  :state="$v.areas.Code.$dirty ? !$v.areas.Code.$error : null"
                                                  @keypress.space.prevent>
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.areas.Code.required">
                                        {{ $t("Categories.Areas.Detail.Form.CodeRequired") }}
                                    </b-form-invalid-feedback><b-form-invalid-feedback v-if="!$v.areas.Code.codePattern">
                                        {{ $t("Categories.Areas.Detail.Form.CodePattern") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Areas.Detail.Form.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.areas.Name.$model"
                                                  :state="$v.areas.Name.$dirty ? !$v.areas.Name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.areas.Name.required">
                                        {{ $t("Categories.Areas.Detail.Form.NameRequired") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Areas.Detail.Form.Note')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-textarea type="text"
                                                     v-model="areas.Note">
                                    </b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageAreas'])"
                                              @click="save"
                                              variant="primary">
                                        <i class="fa fa-floppy-o"></i> {{ $t("Button.Submit") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel"
                                              style="width: 120px">
                                        <i class="fa fa-ban"></i> {{ $t("Button.Cancel") }}
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
    import { required, helpers } from "vuelidate/lib/validators";
    import Services from "@/utils/services";
    import i18n from "@/libs/i18n";
    const codePattern = helpers.regex("codePattern", /^[a-zA-Z0-9_]*$/);
    export default {
        name: "AreasCreate",
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                areas: {
                    Code: null,
                    Name: null,
                    Status: 1,
                    Note: null,
                    compId: null,
                },
                lstStatus: [
                    { id: "1", text: this.$t("Guess.Detail.Form.Active") },
                    { id: "0", text: this.$t("Guess.Detail.Form.Stop") },
                ],
                isAdmin: false,
            };
        },
        validations: {
            areas: {
                Code: { required, codePattern },
                Name: { required },
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.areas.compId = accessToken.comId;
        },
        methods: {
            back() {
                this.$router.push({ path: "/categories/areas/list" });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.post("areas", this.areas).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: "/categories/areas/list" });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: "/categories/areas/list" });
            },
        },
    };
</script>
<style scoped></style>
