<template>
    <treeselect id="department" v-if="rootDepartments.length > 0"
                placeholder="--- Chọn đơn vị ---"
                v-model="Id"
                :load-options="loadDepartments"
                :auto-load-root-options="true"
                :async="true"
                :default-options="rootDepartments"
                :always-open="alwaysOpen"
                :max-height="maxHeight"
                :default-expand-level="1"
                :cacheOptions="false"
                ref="treeDepartment" />
</template>
<script>
    import { mapState } from 'vuex'
    import { LOAD_CHILDREN_OPTIONS, ASYNC_SEARCH } from '@riophae/vue-treeselect'
    var timer;

    export default {
        name: 'DepartmentTree',
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
                Id: null,
                isLoaded: false
            }
        },
        computed: {
            ...mapState({
                departmentTree: (state, getters) => state.common.departmentTree,
                rootDepartments(state, getters) {
                    var root = this.departmentTree.map(x => {
                        return {
                            id: x.id,
                            code: x.code,
                            label: x.label,
                            children: x.children.map(y => {
                                return {
                                    id: y.id,
                                    code: y.code,
                                    label: y.label,
                                    departmentTypeId: y.departmentTypeId,
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
                this.selectDepartment(this.value);
            }
        },
        methods: {
            loadDepartments({ action, parentNode, searchQuery, callback, instanceId }) {
                if (action === LOAD_CHILDREN_OPTIONS) {
                    parentNode.children = this.getDepartmentChildren(parentNode.id);
                    this.$refs.treeDepartment.initialize();
                    callback();
                } else if (action === ASYNC_SEARCH) {
                    if (isNaN(parseInt(searchQuery))) {
                        callback(null, this.getDepartmentsByName(searchQuery));
                    } else {
                        callback(null, this.getDepartmentsById(searchQuery));
                        //this.Id = searchQuery;
                        var vm = this;

                        var node = vm.$refs.treeDepartment.getNode(searchQuery);
                        if (node) {
                            vm.$refs.treeDepartment.select(node);
                            vm.$refs.treeDepartment.resetSearchQuery();
                            //vm.$refs.treeDepartment.trigger.searchQuery = node.label;
                        }
                    }
                }
            },
            getDepartmentChildren(parentId) {
                var children = [];

                var department = this.$store.getters.departmentById(parentId);

                if (department && department.children) {
                    children = JSON.parse(JSON.stringify(department.children));
                    //for (var i = 0; i < children.length; i++) {
                    //    if (children[i].children === null || children[i].children.length == 0) {
                    //        children[i].children = undefined;
                    //    }
                    //}
                }

                return children;
            },
            getDepartmentsByName(searchQuery) {
                if (searchQuery && searchQuery.length > 3) {
                    var tree = JSON.parse(JSON.stringify(this.departmentTree));

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
            getDepartmentsById(id) {
                var tree = JSON.parse(JSON.stringify(this.departmentTree));

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
            buildDepartmentTree(allDepartments, root) {
                var tree = [];

                var parents = [];

                for (var i = 0; i < allDepartments.length; i++) {
                    var currentNode = allDepartments[i];
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
            selectDepartment(id) {
                var vm = this;
                if (timer) {
                    clearTimeout(timer);
                }
                if (this.$refs.treeDepartment) {
                    this.$refs.treeDepartment.trigger.searchQuery = id;
                } else {
                    timer = setTimeout(function () {
                        vm.selectDepartment(id);
                    }, 500);
                }
            }
        },
        watch: {
            Id() {
                this.$emit('input', this.Id);
            },
            rootDepartments(root) {
                if (root.length > 0 && !this.isLoaded) {
                    this.isLoaded = true;
                    if (this.value) {
                        var vm = this;
                        setTimeout(function () {
                            vm.selectDepartment(vm.value);
                        }, 500);
                    }
                }
            },
            value(id) {
                if (id != this.Id) {
                    this.selectDepartment(id);
                }
            }
        }
    }
</script>

