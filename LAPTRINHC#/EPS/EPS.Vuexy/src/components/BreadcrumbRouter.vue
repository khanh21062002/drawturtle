<template>
  <div class="right-wrapper text-end">
    <ol class="breadcrumbs">
      <li v-for="(item, index) in nav" :key="index">
        <a class="nav-link" :href="'#' + item.path">
          <span>{{ item.meta.label }}</span>
        </a>
      </li>
      <!-- <ul>
              <li v-if="itemlv1._name === 'CSidebarNavItem'">
                <a class="nav-link" :href="'#' + itemlv1.to">
                  <i :class="itemlv1.icon" aria-hidden="true"></i>
                  <span>{{ itemlv1.name }}</span>
                </a>
              </li>
              <li
                class="nav-parent"
                v-if="itemlv1._name === 'CSidebarNavDropdown'"
              >
                <a class="nav-link" href="#">
                  <i :class="itemlv1.icon" aria-hidden="true"></i>
                  <span>{{ itemlv1.name }}</span>
                </a>
                <ul class="nav nav-children">
                  <li v-for="itemlv2 in itemlv1.items">
                    <a class="nav-link" :href="'#' + itemlv2.to">
                      {{ itemlv2.name }}
                    </a>
                  </li>
                </ul>
              </li>
            </ul> -->
      <!-- <li><span>Layouts</span></li>

      <li><span>Default</span></li> -->
    </ol>
  </div>
</template>

<script>
export default {
  props: {},
  computed: {
    nav() {
      const nav = this.$route.matched.filter((route) => {
        return (
          (route.name || (route.meta && route.meta.label)) && route.path != ""
        );
      });
      return nav;
    },
  },
};
</script>

<style>
.breadcrumbs li:after {
  content: "/";
  display: inline-block;
  font-size: 13.6px;
  font-size: 0.85rem;
  margin: 0 10px;
  vertical-align: middle;
  border-width: 0;
  border-style: solid;
  border-color: #e2e8f0;
}
</style>
