<template>
  <div class="animated fadeIn">
    <b-row>
      <b-col lg="12">
        <b-card>
          <div slot="header">
            <strong class="card-title">{{
              $t("System.Role.Privileges.Header")
            }}</strong>
          </div>
          <b-form @submit.prevent="save" style="min-height: 500px">
            <b-row>
              <b-col md="4">
                <b-form-group
                  :label="
                    this.$t('System.Role.Privileges.Label.CategoriesList')
                  "
                  label-for="module"
                >
                  <treeselect
                    id="module"
                    ref="moduleTree"
                    v-model="moduleId"
                    :normalizer="normalizer"
                    :branchNodesFirst="true"
                    :defaultExpandLevel="0"
                    :always-open="true"
                    :append-to-body="true"
                    :max-height="430"
                    open-direction="below"
                    :placeholder="this.$t('PlaceHolder.Select')"
                    :options="allModules"
                  />
                </b-form-group>
              </b-col>
              <b-col md="8">
                <b-form-group
                  :label="
                    this.$t('System.User.Privileges.Label.Categories') +
                    ' ' +
                    selectedModule.name.toLowerCase()
                  "
                  v-if="selectedModule"
                  label-for="privileges"
                >
                  <b-form-checkbox-group
                    id="privileges"
                    v-model="rolePrivileges"
                    class="checkbox-group"
                  >
                    <template
                      v-if="
                        selectedModule.items && selectedModule.items.length > 0
                      "
                    >
                      <template v-for="module in selectedModule.items">
                        <span style="font-weight: bold">{{ module.name }}</span>
                        <hr />
                        <b-row>
                          <b-col>
                            <b-form-checkbox
                              v-for="privilege in getModulePrivileges(module)"
                              :key="privilege.id"
                              :value="privilege.id"
                              class="col-md-6 checkbox-privileges"
                              >{{
                                $t("PrivilegesText." + privilege.id)
                              }}</b-form-checkbox
                            >
                          </b-col>
                        </b-row>
                        <br />
                        <br />
                      </template>
                    </template>
                    <template v-else>
                      <hr />
                      <b-row>
                        <b-col>
                          <b-form-checkbox
                            v-for="privilege in getModulePrivileges(
                              selectedModule
                            )"
                            :key="privilege.id"
                            :value="privilege.id"
                            class="col-md-6"
                            >{{
                              $t("PrivilegesText." + privilege.id)
                            }}</b-form-checkbox
                          >
                        </b-col>
                      </b-row>
                    </template>
                  </b-form-checkbox-group>
                </b-form-group>
              </b-col>
            </b-row>
            <b-row>
              <b-col md="8" offset="4">
                <b-button type="submit" variant="primary">{{
                  $t("Button.Submit")
                }}</b-button>
                <b-button type="button" variant="secondary" @click="cancel">{{
                  $t("Button.Cancel")
                }}</b-button>
              </b-col>
            </b-row>
          </b-form>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>
<script>
import nav from "@/containers/_nav";
import { mapState } from "vuex";
import i18n from "@/libs/i18n";

export default {
  name: "RolePrivileges",
  data() {
    return {
      rolePrivileges: [],
      moduleId: null,
      normalizer(node) {
        return {
          id: node.code,
          label: node.name,
          children: node.items,
        };
      },
    };
  },
  computed: {
    selectedModule() {
      if (!this.moduleId) return null;

      function findModule(modules, moduleId) {
        for (var i = 0; i < modules.length; i++) {
          if (modules[i].code == moduleId) {
            return modules[i];
          } else if (modules[i].items && modules[i].items.length > 0) {
            var nested = findModule(modules[i].items, moduleId);
            if (nested) {
              return nested;
            }
          }
        }

        return null;
      }
      var module = findModule(this.allModules, this.moduleId);

      return module;
    },
    allModules() {
      var lstModules = nav[0]._children.slice(1).filter((x) => x.code);

      $.each(lstModules, function (key, value) {
        value.name = i18n.t(value.name);

        $.each(value.items, function (keyLevel2, valueLevel2) {
          valueLevel2.name = i18n.t(valueLevel2.name);
        });
      });
      return lstModules;
    },
    ...mapState({
      allPrivileges: (state, getters) => state.common.privileges,
    }),
  },
  created() {
    var vm = this;

    this.$services
      .get(`roles/${this.$route.params.roleId}/privileges`)
      .done((privileges) => {
        vm.rolePrivileges = privileges;
      });
  },
  methods: {
    getModulePrivileges(module) {
      var requiresPrivileges = module.requiresPrivileges || [];

      return this.allPrivileges.filter(
        (x) => requiresPrivileges.indexOf(x.id) != -1
      );
    },
    save() {
      var vm = this;

      let data = this.rolePrivileges.join(",");
      this.$services
        .put(`roles/${this.$route.params.roleId}/privileges?privileges=${data}`)
        .done(() => {
          vm.$toastr.s(i18n.t("System.User.Privileges.Label.Decentralization"));
          vm.cancel();
        });
    },
    cancel() {
      this.$router.push({ path: "/system/roles/list" });
    },
  },
};
</script>
<style scoped></style>
