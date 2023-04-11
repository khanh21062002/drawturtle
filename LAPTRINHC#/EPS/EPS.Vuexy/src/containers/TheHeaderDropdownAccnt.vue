<template>
  <CDropdown
    inNav
    class="c-header-nav-items"
    placement="bottom-end"
    add-menu-classes="pt-0"
  >
    <template #toggler>
      <CHeaderNavLink>
        <div class="c-avatar">
          <img src="img/avatars/6.jpg" class="c-avatar-img" />
        </div>
      </CHeaderNavLink>
    </template>
    <CDropdownHeader tag="div" class="text-center" color="light">
      <strong>{{ fullName }}</strong>
    </CDropdownHeader>
    <CDropdownItem @click.prevent="logout">
      <CIcon name="cil-lock-locked" /> Logout
    </CDropdownItem>
  </CDropdown>
</template>

<script>
import Services from "@/utils/services";

export default {
  name: "TheHeaderDropdownAccnt",
  data() {
    return {
      fullName: null,
      itemsCount: 42,
    };
  },
  created() {
    var accessToken = Services.getAccessToken();
    this.fullName = accessToken.fullName;
  },
  methods: {
    logout() {
      var vm = this;
      this.$store.dispatch("deauthenticate");
      vm.$router.push({ path: "/login" });
    },
  },
};
</script>

<style scoped>
.c-icon {
  margin-right: 0.3rem;
}
</style>
