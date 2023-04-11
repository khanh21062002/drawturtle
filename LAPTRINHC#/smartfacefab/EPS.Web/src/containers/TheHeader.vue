<template>
    <CHeader fixed with-subheader light>
        <CToggler in-header
                  class="ml-3 d-lg-none"
                  @click="$store.commit('toggleSidebarMobile')" />
        <CToggler in-header
                  class="ml-3 d-md-down-none"
                  @click="$store.commit('toggleSidebarDesktop')" />
        <CHeaderBrand class="mx-auto d-lg-none" to="/">
            <!--<CIcon name="logo" height="48" alt="Logo"/>-->
        </CHeaderBrand>
        <CHeaderNav class="d-md-down-none mr-auto">
            <!--<CHeaderNavItem class="px-3">
              <CHeaderNavLink to="/dashboard">
                Dashboard
              </CHeaderNavLink>
            </CHeaderNavItem>
            <CHeaderNavItem class="px-3">
              <CHeaderNavLink to="/users" exact>
                Users
              </CHeaderNavLink>
            </CHeaderNavItem>
            <CHeaderNavItem class="px-3">
              <CHeaderNavLink>
                Settings
              </CHeaderNavLink>
            </CHeaderNavItem>-->
        </CHeaderNav>
        <CHeaderNav class="mr-4">
            <!--<CHeaderNavItem class="d-md-down-none mx-2">
              <CHeaderNavLink>
                <CIcon name="cil-bell"/>
              </CHeaderNavLink>
            </CHeaderNavItem>
            <CHeaderNavItem class="d-md-down-none mx-2">
              <CHeaderNavLink>
                <CIcon name="cil-list"/>
              </CHeaderNavLink>
            </CHeaderNavItem>
            <CHeaderNavItem class="d-md-down-none mx-2">
              <CHeaderNavLink>
                <CIcon name="cil-envelope-open"/>
              </CHeaderNavLink>
            </CHeaderNavItem>-->

            <template>
                <div class="locale-changer">
                    <CIcon style="width: 16px; height: 16px; padding: 0" v-if="$i18n.locale == 'vi'" name="cif-vn" />
                    <CIcon style="width: 16px; height: 16px; padding: 0" v-if="$i18n.locale == 'en'" name="cif-us" />
                    <select v-model="$i18n.locale" @change="onChangeLocale($event)">
                        <option v-for="locale in availableLocales" :key="`locale-${locale.value}`" :value="locale.value">{{ locale.text }}</option>
                    </select>
                </div>
            </template>

            <!--<CHeaderNavItem class="d-md-down-none mx-2">
                <CHeaderNavLink :to="{ path: `/notificationSystem` }">
                    <CIcon name="cil-bell" />
                    <CBadge color="danger" class="ml-auto" v-if="$store.state.common.totalNotifications > 0">{{ $store.state.common.totalNotifications }}</CBadge>
                </CHeaderNavLink>
            </CHeaderNavItem>-->
            <TheHeaderDropdownAccnt />
        </CHeaderNav>
        <CSubheader class="px-3">
            <CBreadcrumbRouter class="border-0 mb-0" />
        </CSubheader>
    </CHeader>
</template>

<script>
    import TheHeaderDropdownAccnt from './TheHeaderDropdownAccnt'
    import { CoolSelect } from 'vue-cool-select'
    import i18n from '@/libs/i18n'
    export default {
        name: 'TheHeader',
        components: {
            TheHeaderDropdownAccnt,
            CoolSelect
        },
        data() {
            return {
                itemsCount: 0,
                availableLocales: [],
            }
        },
        computed: {
            locale: function () {
                return i18n.locale;
            }
        },
        watch: {
            locale: function (newValue, oldValue) {
                var vm = this;
                //get old value
                let localeCache = localStorage.getItem("lang");
                if (newValue != localeCache) {
                    localStorage.setItem("lang", newValue);
                    vm.$router.go();
                }
               
                
                //reload page
                //

            }
        },
        methods: {
            notificationSystemCount() {
                return this.$services.get(`notificationSystem/count`).done(count => {
                    this.$store.commit('setTotalNotifications', count);
                });
            },
            generateLocales() {
                for (var i = 0; i < i18n.availableLocales.length; i++) {

                    var locale = i18n.availableLocales[i];
                    let item = {
                        value: locale,
                        text: locale
                    }
                    if (locale == 'vi') {
                        item.text = "Tiếng Việt";
                    } else if (locale == 'en') {
                        item.text = "English";
                    }
                    this.availableLocales.push(item);
                }
                ////get data from local storage
                //let localeCache = localStorage.getItem("lang");

                //if (localeCache && localeCache != 'undefined' && localeCache != '') {
                //    i18n.locale = localeCache;
                //} else {
                //    i18n.locale = 'vi';
                //}

            },
            onChangeLocale(newLocale) {
               
            }

        },
        created() {
            this.notificationSystemCount();
            this.generateLocales();
        }
    }
</script>
