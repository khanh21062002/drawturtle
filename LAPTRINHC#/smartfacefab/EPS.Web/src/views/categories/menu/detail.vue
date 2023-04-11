<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{ $t("Categories.Menu.Crud.HeaderDetail") }}</strong>
                    </div>
                    <b-form @submit.prevent="save">
                        <b-row>
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Type')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  disabled
                                                  :value="listType.filter(res => menu.type == res.id).map(x => x.text).join(',')">
                                        <!--v-model="menu.typeStr">-->
                                    </b-form-input>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row v-if="menu.type == 1">
                            <b-col md="6" offset="3">
                                <b-form-group :label="this.$t('Categories.Menu.Crud.Form.Name')"
                                              :label-cols="4"
                                              :horizontal="true"
                                              label-align-md="left"
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  :disabled="!editing"
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
                                              :label-class="editing ? 'required' : ''">
                                    <b-form-input type="text"
                                                  :disabled="!editing"
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
                                    <b-form-input type="text"
                                                  v-if="!editing"
                                                  disabled
                                                  :value="getMon(menu.setFoodId)">
                                        <!--v-model="menu.setFoodId">-->
                                    </b-form-input>
                                    <select2 :options="listMon"
                                             style="width: 100%"
                                             v-if="editing"
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
                                    <b-form-input type="text"
                                                  disabled
                                                  v-model="menu.priceStr"
                                                  v-if="!editing">
                                    </b-form-input>
                                    <b-form-input type="number"
                                                  v-if="editing"
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
                                                     v-model="menu.note"
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
                                              v-if="!editing && authorize(['ManageMenu'])">
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
    import { required, numeric } from "vuelidate/lib/validators";
    import Services from "@/utils/services";
    import i18n from "@/libs/i18n";

    export default {
        name: "MenuDetail",
        mixins: [validationMixin, authorizationMixin],

        data() {
            return {
                menu: {
                    type: null,
                    typeStr: null,
                    name: null,
                    setFoodId: null,
                    price: null,
                    note: null,
                    compId: null,
                    isDelete: false,
                },
                editing: false,
                listType: [
                    { id: '1', text: this.$t("Categories.Menu.Crud.Type1") },
                    { id: '2', text: this.$t("Categories.Menu.Crud.Type2") }
                ],
                listMon: [],
                listSetFoodId: null,
            };
        },
        validations: {
            menu: {
                name: { required },
                price: { numeric },
            },
        },
        computed: {
            menuId() {
                return this.$route.params.menuId;
            },
        },
        async created() {
            var accessToken = Services.getAccessToken();
            this.menu.compId = accessToken.comId;
            await this.loadMenuDetail();
            this.loadMon();
        },
        methods: {
            async loadMenuDetail() {
                return this.$services.get(`menu/${this.menuId}`).done((res) => {
                    this.menu = res;
                    if (res.setFoodId != null) {
                        this.listSetFoodId = res.setFoodId.split(",");
                    }
                });
            },
            loadMon() {
                var vm = this;
                return this.$services.get(`lookup/mon`).done((res) => {
                    vm.listMon = res;
                });
            },
            getMon(idMon) {
                if (idMon != null) {
                    const vm = this;
                    const listId = idMon.split(",");
                    const listName = [];
                    listId.forEach(x => {
                        const name = vm.listMon.filter(item => item.id == x) ? vm.listMon.filter(item => item.id == x).map(x => x.text)[0] : '';
                        listName.push(name);
                    })
                    return listName.join(", ");
                }
                return '';
            },
            edit() {
                this.editing = true;
            },
            cancel() {
                var vm = this;
                vm.loadMenuDetail();
                vm.editing = false;
            },
            back() {
                this.$router.push({ path: "/categories/menu/list" });
            },
            save() {
                this.$v.$touch();
                if (!this.$v.$invalid) {
                    var vm = this;
                    if (vm.listSetFoodId != null) {
                        vm.menu.setFoodId = vm.listSetFoodId.join(",");
                    }
                    this.$services.put(`menu/${this.menuId}`, this.menu).done((id) => {
                        vm.$toastr.s(i18n.t("Messages.ConfirmUpdate"));
                        vm.cancel();
                    });
                }
            },
        },
    };
</script>
<style scoped></style>
