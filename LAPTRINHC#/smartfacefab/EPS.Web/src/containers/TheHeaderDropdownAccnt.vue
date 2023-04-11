<template>
    <CDropdown inNav
               class="c-header-nav-items"
               placement="bottom-end"
               add-menu-classes="pt-0">
        <template #toggler>
            <CHeaderNavLink>
                <div class="c-avatar">
                    <img src="img/avatars/images.jpg"
                         class="c-avatar-img " />
                </div>
            </CHeaderNavLink>
        </template>
        <CDropdownHeader tag="div" class="text-center" color="light">
            <strong>{{fullName}}</strong>
        </CDropdownHeader>
        <CDropdownHeader tag="div" class="text-center" color="light">
            <strong>{{user.company}}</strong>
        </CDropdownHeader>
        <CDropdownItem :to="{ path: `/personal/${userId}` }">
            <CIcon name="cil-user" /> {{$t("ChangePassword.List.Header")}}
        </CDropdownItem>
        <!--<CDropdownItem>
            <CIcon name="cil-envelope-open" /> Messages
            <CBadge color="success" class="ml-auto">{{ itemsCount }}</CBadge>
        </CDropdownItem>
        <CDropdownItem>
            <CIcon name="cil-task" /> Tasks
            <CBadge color="danger" class="ml-auto">{{ itemsCount }}</CBadge>
        </CDropdownItem>
        <CDropdownItem>
            <CIcon name="cil-comment-square" /> Comments
            <CBadge color="warning" class="ml-auto">{{ itemsCount }}</CBadge>
        </CDropdownItem>
        <CDropdownHeader tag="div"
                         class="text-center"
                         color="light">
            <strong>Settings</strong>
        </CDropdownHeader>
        <CDropdownItem>
            <CIcon name="cil-user" /> Profile
        </CDropdownItem>
        <CDropdownItem>
            <CIcon name="cil-settings" /> Settings
        </CDropdownItem>
        <CDropdownItem>
            <CIcon name="cil-dollar" /> Payments
            <CBadge color="secondary" class="ml-auto">{{ itemsCount }}</CBadge>
        </CDropdownItem>
        <CDropdownItem>
            <CIcon name="cil-file" /> Projects
            <CBadge color="primary" class="ml-auto">{{ itemsCount }}</CBadge>
        </CDropdownItem>
        <CDropdownDivider />-->
        <!--<CDropdownItem>
            <CIcon name="cil-shield-alt" /> Lock Account
        </CDropdownItem>-->
        <CDropdownItem @click.prevent="logout">
            <CIcon name="cil-lock-locked" /> {{$t("ChangePassword.List.Form.Logout")}}
        </CDropdownItem>
    </CDropdown>
</template>

<script>
    import Services from '@/utils/services'
    import i18n from '@/libs/i18n'
    export default {
        name: 'TheHeaderDropdownAccnt',
        data() {
            return {
                user: {
                    fullname: null,
                    compId: null,
                    company: null,
                    userId: null,
                },
                itemsCount: 42,
                fullName: null
            }
        },
        created() {
            var accessToken = Services.getAccessToken();
            this.fullName = accessToken.fullName;
            this.userId = accessToken.userId;
            var userid = accessToken.userId;
            if (userid) {
                this.loadUserDetail(userid);
            }
        },
        methods: {
            logout() {
                var vm = this;
                this.$store.dispatch('deauthenticate');
                vm.$router.push({ path: '/login' });
            },
            loadUserDetail(userId) {
                var vm = this;
                return this.$services.get(`users/get-self-info`).done(user => {
                    vm.user = user;
                });
            }
        }
    }
</script>

<style scoped>
    .c-icon {
        margin-right: 0.3rem;
    }
</style>