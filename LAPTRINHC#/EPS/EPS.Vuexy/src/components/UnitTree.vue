<template>
    <treeselect id="unit" v-if="rootUnits.length > 0"
                placeholder="--- Chọn đơn vị ---"
                v-model="unitId"
                :load-options="loadUnits"
                :auto-load-root-options="true"
                :async="true"
                :default-options="rootUnits"
                :always-open="alwaysOpen"
                :max-height="maxHeight"
                :default-expand-level="1"
                :cacheOptions="false"
                ref="treeUnit" />
</template>
<script>
    import { mapState } from 'vuex'
    import { LOAD_CHILDREN_OPTIONS, ASYNC_SEARCH } from '@riophae/vue-treeselect'
    var timer;

    export default {
        name: 'UnitTree',
        props: {
            value: {},
            alwaysOpen: {
                default: false
            },
            maxHeight: {
                type: Number,
                default: 300
            }
        },
        data() {
            return {
                unitId: null,
                isLoaded: false
            }
        },
        computed: {
            ...mapState({
                unitTree: (state, getters) => state.common.unitTree,
                rootUnits(state, getters) {
                    var root = this.unitTree.map(x => {
                        return {
                            id: x.id,
                            code: x.code,
                            label: x.label,
                            children: x.children.map(y => {
                                return {
                                    id: y.id,
                                    code: y.code,
                                    label: y.label,
                                    unitTypeId: y.unitTypeId,
                                    children: y.hasChildren ? null : undefined
                                };
                            })
                        };
                    });

                    return root;
                }
            })

        },
        mounted: function () {
            if (this.value) {
                this.selectUnit(this.value);
            }
        },
        methods: {
            loadUnits({ action, parentNode, searchQuery, callback, instanceId }) {
                if (action === LOAD_CHILDREN_OPTIONS) {
                    parentNode.children = this.getUnitChildren(parentNode.id);
                    this.$refs.treeUnit.initialize();
                    callback();
                } else if (action === ASYNC_SEARCH) {
                    if (isNaN(parseInt(searchQuery))) {
                        callback(null, this.getUnitsByName(searchQuery));
                    } else {
                        callback(null, this.getUnitsById(searchQuery));
                        //this.unitId = searchQuery;
                        var vm = this;

                        var node = vm.$refs.treeUnit.getNode(searchQuery);
                        if (node) {
                            vm.$refs.treeUnit.select(node);
                            vm.$refs.treeUnit.resetSearchQuery();
                            //vm.$refs.treeUnit.trigger.searchQuery = node.label;
                        }
                    }
                }
            },
            getUnitChildren(parentId) {
                var children = [];

                var unit = this.$store.getters.unitById(parentId);

                if (unit && unit.children) {
                    children = JSON.parse(JSON.stringify(unit.children));
                    //for (var i = 0; i < children.length; i++) {
                    //    if (children[i].children === null || children[i].children.length == 0) {
                    //        children[i].children = undefined;
                    //    }
                    //}
                }

                return children;
            },
            getUnitsByName(searchQuery) {
                if (searchQuery && searchQuery.length > 3) {
                    var tree = JSON.parse(JSON.stringify(this.unitTree));

                    var result = [];

                    for (var i = 0; i < tree.length; i++) {
                        if (trimTreeByText(tree[i], searchQuery)) {
                            result.push(tree[i]);
                        }
                    }

                    return result;

                    function trimTreeByText(node, searchQuery) {
                        // Is filter text founded in current node or descendant nodes
                        var matched = false;

                        if (node.children && node.children.length > 0) {
                            // Check filter text in children nodes as well
                            var listRemove = [];
                            for (var i = node.children.length - 1; i >= 0; i--) {
                                var child = node.children[i];
                                if (trimTreeByText(child, searchQuery)) {
                                    matched = true;
                                }
                                else {
                                    // Remove node that does not sastify the condition
                                    //node.Children.RemoveAt(i);
                                    listRemove.push(i);
                                }
                            }
                            for (var index = 0; index < listRemove.length; index++) {
                                node.children.splice(listRemove[index], 1);
                            }
                        }

                        // Either current node contain filter text or its descendants do
                        matched = matched || node.label.latinize().toLowerCase().indexOf(searchQuery.latinize().toLowerCase()) != -1;

                        if (matched) {
                            if (node.children && node.children.length > 0) {
                                node.isDefaultExpanded = true;
                            } else {
                                node.children = undefined;
                            }
                        }

                        return matched;
                    }
                } else {
                    return [];
                }
            },
            getUnitsById(id) {
                var tree = JSON.parse(JSON.stringify(this.unitTree));

                var result = [];

                for (var i = 0; i < tree.length; i++) {
                    if (trimTreeById(tree[i], id)) {
                        result.push(tree[i]);
                        break;
                    }
                }

                return result;

                function trimTreeById(node, id) {
                    // Is filter text founded in current node or descendant nodes
                    var matched = false;

                    if (node.children && node.children.length > 0) {
                        // Check filter text in children nodes as well
                        var listRemove = [];
                        for (var i = node.children.length - 1; i >= 0; i--) {
                            var child = node.children[i];
                            if (trimTreeById(child, id)) {
                                matched = true;
                                break;
                            }
                            else {
                                // Remove node that does not sastify the condition
                                //node.Children.RemoveAt(i);
                                listRemove.push(i);
                            }
                        }
                        for (var index = 0; index < listRemove.length; index++) {
                            node.children.splice(listRemove[index], 1);
                        }
                    }

                    // Either current node contain filter text or its descendants do
                    matched = matched || node.id == id;

                    return matched;
                }
            },
            buildUnitTree(allUnits, root) {
                var tree = [];

                var parents = [];

                for (var i = 0; i < allUnits.length; i++) {
                    var currentNode = allUnits[i];
                    if (currentNode.id == root) {
                        parents.push(currentNode);
                        tree.push(currentNode);
                        continue;
                    }

                    while (parents.length > 0) {
                        var parentNode = parents[parents.length - 1];
                        if (currentNode.parentId == parentNode.id) {
                            if (!parentNode.children) parentNode.children = [];
                            parentNode.children.push(currentNode);
                            parents.push(currentNode);
                            break;
                        }
                        parents.pop();
                    }
                }

                return tree;
            },
            selectUnit(id) {
                var vm = this;
                if (timer) {
                    clearTimeout(timer);
                }
                if (this.$refs.treeUnit) {
                    this.$refs.treeUnit.trigger.searchQuery = id;
                } else {
                    timer = setTimeout(function () {
                        vm.selectUnit(id);
                    }, 500);
                }
            }
        },
        watch: {
            unitId() {
                this.$emit('input', this.unitId);
            },
            rootUnits(root) {
                if (root.length > 0 && !this.isLoaded) {
                    this.isLoaded = true;
                    if (this.value) {
                        var vm = this;
                        setTimeout(function () {
                            vm.selectUnit(vm.value);
                        }, 500);
                    }
                }
            },
            value(id) {
                if (id != this.unitId) {
                    this.selectUnit(id);
                }
            }
        }
    }
</script>

