<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col lg="12">
        <b-card>
          <div slot="header">
            <strong class="card-title">{{
              $t("System.Company.Create.Header")
            }}</strong>
          </div>
          <b-form @submit.prevent="save">
            <b-row class="form-group pb-3">
              <b-col lg="6" offset-lg="3">
                <b-form-group
                  :label-cols="4"
                  :horizontal="true"
                  label-align-md="left"
                  label-for="name"
                  label-class="col-lg-3 control-label text-lg-start pt-2 "
                  class="form-group row pb-4"
                >
                  <template slot="label"
                    >{{ $t("System.Company.Detail.Label.Code") }}
                    <span class="required">(*)</span></template
                  >
                  <b-form-input
                    type="text"
                    id="txt_code"
                    v-model="$v.company.code.$model"
                    :state="
                      $v.company.code.$dirty ? !$v.company.code.$error : null
                    "
                  >
                  </b-form-input>

                  <b-form-invalid-feedback v-if="!$v.company.code.required">
                    {{ $t("System.Company.Detail.Label.CodeRequire") }}
                  </b-form-invalid-feedback>
                  <b-form-invalid-feedback
                    v-if="!$v.company.code.alphaNumAndDash"
                  >
                    {{ $t("System.Company.Detail.Label.CodeAlphaNumAndDash") }}
                  </b-form-invalid-feedback>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row class="form-group pb-3">
              <b-col lg="6" offset-lg="3">
                <b-form-group
                  :label-cols="4"
                  :horizontal="true"
                  label-align-md="left"
                  label-for="name"
                  label-class="col-lg-3 control-label text-lg-start pt-2 "
                  class="form-group row pb-4"
                >
                  <template slot="label"
                    >{{ $t("System.Company.Detail.Label.Name") }}
                    <span class="required">(*)</span></template
                  >
                  <b-form-input
                    type="text"
                    id="txt_name"
                    v-model="$v.company.name.$model"
                    :state="
                      $v.company.name.$dirty ? !$v.company.name.$error : null
                    "
                  >
                  </b-form-input>
                  <b-form-invalid-feedback>
                    {{ $t("System.Company.Detail.Label.NameRequire") }}
                  </b-form-invalid-feedback>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row class="form-group pb-3">
              <b-col lg="6" offset-lg="3">
                <b-form-group
                  :label-cols="4"
                  :horizontal="true"
                  label-align-md="left"
                  label-for="name"
                  label-class="col-lg-3 control-label text-lg-start pt-2 "
                  class="form-group row pb-4"
                >
                  <template slot="label"
                    >{{ $t("System.Company.Detail.Label.ParentName") }}
                    <span class="required">(*)</span></template
                  >
                  <b-form-input
                    type="text"
                    style="display: none"
                    v-model="company.parentId"
                  >
                  </b-form-input>
                  <treeselect
                    :multiple="false"
                    :options="treeselect.options"
                    :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                    :placeholder="this.$t('PlaceHolder.Select')"
                    v-model="company.parentId"
                  />
                  <b-form-invalid-feedback>
                    {{ $t("System.Company.Detail.Label.ParentRequire") }}
                  </b-form-invalid-feedback>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row class="form-group pb-3">
              <b-col class="text-center">
                <b-form-group>
                  <b-button
                    v-if="authorize(['ManageCompany'])"
                    type="submit"
                    variant="primary"
                    ><i class="fa fa-floppy-o"></i>
                    {{ $t("Button.Submit") }}</b-button
                  >
                  <b-button type="button" variant="secondary" @click="cancel"
                    ><i class="fa fa-ban"></i>
                    {{ $t("Button.Cancel") }}</b-button
                  >
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
import {
  required,
  numeric,
  maxLength,
  minLength,
  helpers,
} from "vuelidate/lib/validators";
import Services from "@/utils/services";
import i18n from "@/libs/i18n";
//Chấp nhận aphabet và dấu ngạch ngang
const alphaNumAndDash = helpers.regex("alphaNumAndDash", /^[a-z\d_]*$/i);

export default {
  name: "CompanyDetail",
  mixins: [validationMixin, authorizationMixin],
  data() {
    return {
      company: {
        code: null,
        name: null,
      },
      lstcompany: [],
      treeselect: {
        value: null,
        options: [],
      },
      lstCompanyType: [],
    };
  },
  validations: {
    company: {
      code: {
        required,
        alphaNumAndDash,
      },
      name: { required },
    },
  },
  created() {
    var accessToken = Services.getAccessToken();
    this.loadCompanyTree();
  },
  computed: {
    parentId() {
      return this.$route.params.parentId;
    },
  },
  methods: {
    //Danh sách công ty
    loadCompany() {
      var vm = this;
      return this.$services.get(`lookup/companys`).done((lstcompany) => {
        vm.lstcompany = lstcompany;
      });
    },
    //Danh sách công ty - tree view
    loadCompanyTree() {
      var vm = this;
      return this.$services.get(`lookup/company-tree`).done((lstcompany) => {
        vm.treeselect.options = lstcompany;
      });
    },
    back() {
      this.$router.push({ path: "/system/company/list" });
    },
    save() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        var vm = this;
        this.$services.post("company", this.company).done((id) => {
          vm.$toastr.s(i18n.t("Messages.CreateSuccess"));
          vm.$router.push({ path: "/system/company/list" });
        });
      }
    },
    cancel() {
      this.$router.push({ path: "/system/company/list" });
    },
  },
};
</script>
<style scoped></style>
