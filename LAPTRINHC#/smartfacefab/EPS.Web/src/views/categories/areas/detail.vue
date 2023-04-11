<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.Areas.Detail.Header") }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Areas.Detail.Form.Code')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  :disabled="!editing"
                                                  v-model.trim="$v.areas.code.$model"
                                                  :state="$v.areas.code.$dirty ? !$v.areas.code.$error : null"
                                                  @keypress.space.prevent>
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.areas.code.required">
                                        {{ $t("Categories.Areas.Detail.Form.CodeRequired") }}
                                    </b-form-invalid-feedback><b-form-invalid-feedback v-if="!$v.areas.code.codePattern">
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
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  :disabled="!editing"
                                                  v-model="$v.areas.name.$model"
                                                  :state="$v.areas.name.$dirty ? !$v.areas.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.areas.name.required">
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
                                                     v-model="areas.note"
                                                     :disabled="!editing">
                                    </b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button type="button"
                                              variant="primary"
                                              @click="edit"
                                              v-if="!editing && authorize(['ManageAreas'])">
                                        <i class="fa fa-pencil-square-o"></i> {{ $t("Button.Edit") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="back"
                                              v-if="!editing"
                                              style="width: 120px">
                                        <i class="fa fa-ban"></i> {{ $t("Button.Back") }}
                                    </b-button>
                                    <b-button type="submit"
                                              variant="primary"
                                              v-if="editing">
                                        <i class="fa fa-floppy-o"></i> {{ $t("Button.Submit") }}
                                    </b-button>
                                    <b-button type="button"
                                              variant="secondary"
                                              @click="cancel"
                                              style="width: 120px"
                                              v-if="editing">
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
        name: "AreasDetail",
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                areas: {
                    code: null,
                    name: null,
                    status: 1,
                    note: null,
                    compId: null,
                },
                lstStatus: [
                    { id: "1", text: this.$t("Guess.Detail.Form.Active") },
                    { id: "0", text: this.$t("Guess.Detail.Form.Stop") },
                ],
                isAdmin: false,
                editing: false,
            };
        },
        validations: {
            areas: {
                code: { required, codePattern },
                name: { required },
            },
        },
        computed: {
            areasId() {
                return this.$route.params.areasId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.areas.compId = accessToken.comId;
            await this.loadAreasDetail();
        },
        methods: {
            async loadAreasDetail() {
                return this.$services.get(`areas/${this.areasId}`).done((areas) => {
                    this.areas = areas;
                });
            },
            convertDataStatus(value) {
                return this.lstStatus
                    .filter((status) => value == status.id)
                    .map((element) => element.text)
                    .join(",");
            },
            edit() {
                this.editing = true;
            },
            cancel() {
                var vm = this;
                vm.loadAreasDetail();
                vm.editing = false;
            },
            back() {
                this.$router.push({ path: "/categories/areas/list" });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    this.$services.put(`areas/${this.areasId}`, this.areas).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.$router.push({ path: "/categories/areas/list" });
                    });
                }
            },
        },
    };
</script>
<style scoped></style>
