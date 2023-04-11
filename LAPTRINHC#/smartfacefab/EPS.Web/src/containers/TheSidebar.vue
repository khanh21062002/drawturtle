<template>
    <CSidebar fixed
              :minimize="minimize"
              :show="show"
              @update:show="(value) => $store.commit('set', ['sidebarShow', value])">
        <CSidebarBrand class="d-md-down-none" to="/">
            <img class="navbar-brand-full" src="img/Logo.png" height="35" alt="Logo" style="padding-right: 5px">
            <span v-if="!minimize" >
                {{logoText}}
            </span>
        </CSidebarBrand>

        <CRenderFunction flat :content-to-render="nav" />
        <CSidebarMinimizer class="d-md-down-none"
                           @click.native="$store.commit('set', ['sidebarMinimize', !minimize])" />
    </CSidebar>
</template>

<script>
    import nav from './_nav'
    
    export default {
        name: 'TheSidebar',
        data() {
            return {
                logoText: "",
            };
        },
        created() {
            var vm = this;
            //read config
            $.getJSON('brandconfig.json', function (data) {
                if (data && data.Company && data.Company.LogoText) {
                    vm.logoText = data.Company.LogoText;
                }
                

            });


        },
        computed: {
            nav() {
                var result = JSON.parse(JSON.stringify(nav));
                var navItems = result[0]._children;
                var privileges = this.$store.state.identity.privileges;
                var vm = this;

                RemoveUnauthorizedItems(navItems);

                function RemoveUnauthorizedItems(items) {
                    if (items && items.length > 0) {
                        for (var i = items.length - 1; i >= 0; i--) {
                            var item = items[i];

                            item.name = vm.$t(item.name);

                            var requiresPrivileges = item.requiresPrivileges || [];
                            if (item.items && item.items.length > 0) {
                                requiresPrivileges = requiresPrivileges.concat(item.items.map(x => x.requiresPrivileges).reduce((x, y) => (x || []).concat(y || [])));
                            }

                            if (requiresPrivileges && requiresPrivileges.length > 0
                                && intersect(privileges, requiresPrivileges).length == 0) {
                                items.remove(item);
                            } else {
                                RemoveUnauthorizedItems(item.items);
                            }
                        }
                    }
                }
                return result;
            },
            show() {
                return this.$store.state.common.sidebarShow
            },
            minimize() {
                return this.$store.state.common.sidebarMinimize
            },
          
        }
    }
</script>
