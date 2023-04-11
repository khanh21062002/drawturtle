<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.Menu.Crud.HeaderCreate") }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="8"></b-col>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Mon')"
                                                      :label-cols="5"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-radio id="typeMon" size="lg"
                                                          v-model="menu.type"
                                                          name="checkbox-AutoSaveVisitor"
                                                          value="1"
                                                          unchecked-value="false">
                                            </b-form-radio>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                            <b-col md="6">
                                <b-row>
                                    <b-col md="4">
                                        <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Set')"
                                                      :label-cols="5"
                                                      :horizontal="true"
                                                      label-align-md="left">
                                            <b-form-radio id="typeSet" size="lg"
                                                          v-model="menu.type"
                                                          name="checkbox-FraudProof"
                                                          value="2"
                                                          unchecked-value="false">
                                            </b-form-radio>
                                        </b-form-group>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>
                        <b-row v-if="menu.type == 1">
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.menu.name.$model"
                                                  :state="$v.menu.name.$dirty ? !$v.menu.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.menu.name.required">
                                        {{ $t("Categories.Menu.Crud.Form.NameRequired") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row v-if="menu.type == 2">
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.SetName')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              label-class="required">
                                    <b-form-input type="text"
                                                  v-model="$v.menu.name.$model"
                                                  :state="$v.menu.name.$dirty ? !$v.menu.name.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.menu.name.required">
                                        {{ $t("Categories.Menu.Crud.Form.SetNameRequired") }}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row v-if="menu.type == 2">
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.SetFood')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <select2 :options="listMon"
                                             style="width: 100%"
                                             :placeholder="this.$t('PlaceHolder.Select')"
                                             :settings="{ allowClear: true, multiple: true, closeOnSelect: false }"
                                             v-model="listSetFoodId">
                                    </select2>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Price')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-input type="number"
                                                  v-model="$v.menu.price.$model"
                                                  :state="$v.menu.price.$dirty ? !$v.menu.price.$error : null">
                                    </b-form-input>
                                    <b-form-invalid-feedback v-if="!$v.menu.price.numeric">
                                        {{  $t('Categories.PreOrder.Crud.Form.Require.VehicleLoadPositive' )}}
                                    </b-form-invalid-feedback>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Note')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left">
                                    <b-form-textarea type="text"
                                                     v-model="menu.note">
                                    </b-form-textarea>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="text-center">
                                <b-form-group>
                                    <b-button v-if="authorize(['ManageMenu'])"
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
    import { validationMixin } from "vuelidate"
    import { authorizationMixin } from "@/shared/mixins"
    import { required, numeric } from "vuelidate/lib/validators"
    import Services from "@/utils/services"
    import i18n from "@/libs/i18n"

    export default {
        name: "MenuCreate",
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                menu: {
                    type: null,
                    name: null,
                    setFoodId: null,
                    price: null,
                    note: null,
                    compId: null,
                    isDelete: false,
                },
                listSetFoodId: null,
                listMon: [],
            };
        },
        validations: {
            menu: {
                name: { required },
                price: { numeric },
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.menu.compId = accessToken.comId;
            this.menu.type = 1;
            this.loadMon();
        },
        methods: {
            loadMon() {
                var vm = this;
                return this.$services.get(`lookup/mon`).done((res) => {
                    vm.listMon = res;
                });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    if (vm.listSetFoodId != null) {
                        vm.menu.setFoodId = vm.listSetFoodId.join(",");
                    }
                    this.$services.post("menu", this.menu).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmCreate"));
                        vm.$router.push({ path: "/categories/menu/list" });
                    });
                }
            },
            cancel() {
                this.$router.push({ path: "/categories/menu/list" });
            },
        },
    };
</script>
<style scoped></style>
