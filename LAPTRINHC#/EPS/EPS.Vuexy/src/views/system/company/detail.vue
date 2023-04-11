<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col lg="12">
        <b-card>
          <div slot="header">
            <strong>{{ $t("System.Company.Detail.Header") }}</strong>
          </div>
          <b-form @submit.prevent="save">
            <b-row class="form-group pb-3">
              <b-col lg="6" offset-lg="3">
                <b-form-group
                  :label="this.$t('System.Company.Detail.Label.Code')"
                  :label-cols="4"
                  :horizontal="true"
                  label-align-md="left"
                  label-for="name"
                  class="form-group row pb-4"
                  label-class="'col-lg-3 control-label text-lg-start pt-2'"
                >
                  <template slot="label"
                    >{{ $t("System.Company.Detail.Label.Code") }}
                    <span v-if="editing" class="required">(*)</span></template
                  >
                  <b-form-input
                    type="text"
                    id="txt_code"
                    v-if="editing"
                    v-model="$v.company.code.$model"
                    :state="
                      $v.company.code.$dirty ? !$v.company.code.$error : null
                    "
                  >
                  </b-form-input>

                  <b-form-invalid-feedback v-if="!$v.company.code.required">
                    {{ $t("System.Company.Detail.Label.CodeRequire") }}
                  </b-form-invalid-feedback>

                  <label class="col-form-label" v-if="!editing">{{
                    company.code
                  }}</label>
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
                  class="form-group row pb-4"
                  label-class="'col-lg-3 control-label text-lg-start pt-2'"
                >
                  <template slot="label"
                    >{{ $t("System.Company.Detail.Label.Name") }}
                    <span v-if="editing" class="required">(*)</span></template
                  >
                  <b-form-input
                    type="text"
                    id="txt_name"
                    v-if="editing"
                    v-model="$v.company.name.$model"
                    :state="
                      $v.company.name.$dirty ? !$v.company.name.$error : null
                    "
                  >
                  </b-form-input>

                  <b-form-invalid-feedback>
                    {{ $t("System.Company.Detail.Label.NameRequire") }}
                  </b-form-invalid-feedback>
                  <label class="col-form-label" v-if="!editing">{{
                    company.name
                  }}</label>
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
                  class="form-group row pb-4"
                  label-class="'col-lg-3 control-label text-lg-start pt-2 '"
                >
                  <template slot="label"
                    >{{ $t("System.Company.Detail.Label.ParentName") }}
                    <span v-if="editing" class="required">(*)</span></template
                  >
                  <b-form-input
                    type="text"
                    style="display: none"
                    v-model="company.parentId"
                  >
                  </b-form-input>
                  <treeselect
                    :multiple="false"
                    v-if="editing"
                    :options="treeselect.options"
                    :placeholder="this.$t('PlaceHolder.Select')"
                    v-model="company.parentId"
                  />
                  <label class="col-form-label" v-if="!editing">{{
                    company.parentName
                  }}</label>
                </b-form-group>
              </b-col>
            </b-row>

            <div class="text-center form-group pb-3">
              <b-button
                type="button"
                variant="primary"
                @click="edit"
                v-if="!editing && authorize(['ManageCompany'])"
                ><i class="fa fa-pencil-square-o"></i>
                {{ $t("Button.Edit") }}</b-button
              >
              <b-button
                type="button"
                variant="secondary"
                @click="back"
                v-if="!editing"
                ><i class="fa fa-ban"></i> {{ $t("Button.Back") }}</b-button
              >
              <b-button type="submit" variant="primary" v-if="editing"
                ><i class="fa fa-floppy-o"></i>
                {{ $t("Button.Submit") }}</b-button
              >
              <b-button
                type="button"
                variant="secondary"
                @click="cancel"
                v-if="editing"
                ><i class="fa fa-ban"></i> {{ $t("Button.Cancel") }}</b-button
              >
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
import {
  required,
  numeric,
  maxLength,
  minLength,
} from "vuelidate/lib/validators";
import i18n from "@/libs/i18n";

export default {
  name: "CompanyDetail",
  mixins: [validationMixin, authorizationMixin],
  data() {
    return {
      company: {
        id: null,
        code: null,
        name: null,
      },
      editing: false,
      treeselect: {
        value: null,
        options: [],
      },
      listCompanyType: [],
    };
  },
  computed: {
    companyId() {
      return this.$route.params.companyId;
    },
  },
  validations: {
    company: {
      code: {
        required,
      },
      name: { required },
    },
  },
  created() {
    this.loadCompanyTree();
    this.loadCompanyDetail();
  },
  methods: {
    loadCompanyDetail() {
      var vm = this;
      return this.$services.get(`company/${this.companyId}`).done((company) => {
        vm.company = company;
      });
    },
    //Danh sách cong ty - tree view
    loadCompanyTree() {
      var vm = this;
      return this.$services.get(`lookup/company-tree`).done((lstcompany) => {
        vm.treeselect.options = lstcompany;
      });
    },
    edit() {
      this.editing = true;
    },
    back() {
      this.$router.push({ path: "/system/company/list" });
    },
    cancel() {
      var vm = this;
      this.loadCompanyDetail().done(() => (vm.editing = false));
    },
    save() {
      this.$v.$touch();
      if (!this.$v.$invalid) {
        var vm = this;
        this.$services
          .put(`company/${this.companyId}`, this.company)
          .done(() => {
            vm.$toastr.s(i18n.t("Messages.UpdateSuccess"));
            vm.cancel();
          });
      }
    },
  },
};
</script>
<style scoped></style>
