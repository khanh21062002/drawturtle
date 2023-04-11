<template>
    <div class="animated fadeIn">
        <b-row>
            <b-col lg="12">
                <b-card>
                    <div slot="header">
                        <strong>{{$t('System.User.Privileges.Header')}}</strong>
                    </div>
                    <b-form @submit.prevent="save" style="min-height: 500px">
                        <b-row>
                            <b-col md="4">
                                <b-form-group :label="this.$t('System.User.Privileges.Label.CategoriesList')"
                                              label-for="module">
                                    <treeselect id="module"
                                                ref="moduleTree"
                                                v-model="moduleId"
                                                :normalizer="normalizer"
                                                :branchNodesFirst="true"
                                                :defaultExpandLevel="0"
                                                :always-open="true"
                                                :noChildrenText="this.$t('TreeSelect.noChildrenText')"
                                                :append-to-body="true"
                                                :max-height="430"
                                                open-direction="below"
                                                :placeholder="this.$t('PlaceHolder.Select')"
                                                :options="allModules" />
                                </b-form-group>
                            </b-col>
                            <b-col md="8">
                                <b-form-group :label="this.$t('System.User.Privileges.Label.Categories') +' '+ selectedModule.name.toLowerCase()"
                                              v-if="selectedModule"
                                              label-for="privileges">
                                    <b-form-checkbox-group id="privileges" v-model="userPrivileges" class="checkbox-group">
                                        <template v-if="selectedModule.items && selectedModule.items.length > 0">
                                            <template v-for="module in selectedModule.items">
                                                <span style="font-weight: bold;"> {{ module.name }} </span>
                                                <hr />
                                                <b-row>
                                                    <b-col>
                                                        <b-form-checkbox v-for="privilege in getModulePrivileges(module)"
                                                                         :key="privilege.id"
                                                                         :value="privilege.id"
                                                                         class="col-md-6">
                                                            {{ $t("PrivilegesText." + privilege.id) }}
                                                        </b-form-checkbox>
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
                                                    <b-form-checkbox v-for="privilege in getModulePrivileges(selectedModule)"
                                                                     :key="privilege.id"
                                                                     :value="privilege.id"
                                                                     class="col-md-6">
                                                        {{  $t("PrivilegesText." + privilege.id) }}
                                                    </b-form-checkbox>
                                                </b-col>
                                            </b-row>
                                        </template>
                                    </b-form-checkbox-group>
                                </b-form-group>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col md="8" offset="4">
                                <b-button type="submit"
                                          variant="primary">
                                    {{ $t("Button.Submit") }}
                                </b-button>
                                <b-button type="button"
                                          variant="secondary"
                                          @click="cancel">
                                    {{ $t("Button.Cancel") }}
                                </b-button>
                            </b-col>
                        </b-row>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import nav from '@/containers/_nav'
    import { mapState } from 'vuex'
    import { authorizationMixin } from '@/shared/mixins'
    import i18n from '@/libs/i18n'

    export default {
        name: 'UserPrivileges',
        mixins: [authorizationMixin],
        data() {
            return {
                userPrivileges: [],
                moduleId: null,
                normalizer(node) {
                    return {
                        id: node.code,
                        label: node.name,
                        children: node.items,
                    }
                },
            }
        },
        computed: {
            selectedModule() {
                if (!this.moduleId)
                    return null;

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
                var vm = this;
                var rs = nav[0]._children.slice(1).filter(x => x.code);
                var result = [];
                for (var i = 0; i < rs.length; i++) {
                    var item = rs[i];
                    item.name = vm.$t(item.name);
                    $.each(item.items, function (key, value) {
                        value.name = vm.$t(value.name);
                    });
                    if (vm.authorize(['ManageRole']) || vm.authorize(['ViewRole'])) {
                        result.push(item);
                        continue;
                    }
                    if (item.code == "system") {
                        if (item.items && item.items.length > 0) {
                            var _items = [];
                            for (var j = 0; j < item.items.length; j++) {
                                let itemChild = item.items[j];
                                if (itemChild && itemChild.code == 'manage_roles') { }
                                else {
                                    _items.push(itemChild);
                                }
                            }
                            item.items = _items;
                        }
                    }
                    result.push(item);
                }
                return rs;
            },
            ...mapState({
                allPrivileges: (state, getters) => state.common.privileges
            }),
        },
        created() {
            var vm = this;
            this.$services.get(`users/${this.$route.params.userId}/privileges`).done((res) => {
                vm.userPrivileges = res;
            });
        },
        methods: {
            getModulePrivileges(module) {
                var requiresPrivileges = module.requiresPrivileges || [];
                return this.allPrivileges.filter(x => requiresPrivileges.indexOf(x.id) != -1);
            },
            save() {
                var vm = this;
                let data = this.userPrivileges.join(',');
                this.$services.put(`users/${this.$route.params.userId}/privileges?privileges=${data}`).done(() => {
                    vm.$toastr.s(i18n.t('System.User.Privileges.Label.Decentralization'));
                    vm.cancel();
                });
            },
            cancel() {
                this.$router.push({ path: '/system/users/list' });
            },
        }
    }
</script>
<style scoped>
</style>
