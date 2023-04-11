import Vue from "vue";
import Router from "vue-router";
import store from "@/store/index";
import i18n from "@/libs/i18n";

// Containers
const TheContainer = () => import("@/containers/TheContainer");

// Views - Pages
import Page404 from "@/views/pages/Page404";
//import Page500 from '@/views/pages/Page500'
import Login from "@/views/pages/Login";
//import Register from '@/views/pages/Register'

Vue.use(Router);

var router = new Router({
  mode: "hash", // Demo is living in GitHub.io, so required!
  linkActiveClass: "open active",
  scrollBehavior: () => ({ y: 0 }),
  routes: [
    {
      path: "/",
      redirect: "/home",
      name: "default",
      component: TheContainer,
      meta: { requiresAuth: true, label: i18n.t("Nav.Home") },
      children: [
        {
          path: "home",
          name: "home",
          meta: { label: i18n.t("Nav.Home"), requiresAuth: true },
          component: () => import("@/views/home"),
        },
        {
          path: "personal/:userId",
          name: "personal",
          meta: {
            label: i18n.t("Nav.PersonalInformation"),
            requiresAuth: true,
          },
          component: () => import("@/views/personal"),
        },
        {
          path: "system",
          name: "system",
          redirect: "/system/users",
          meta: { label: i18n.t("Nav.System") },
          component: {
            render(c) {
              return c("router-view");
            },
          },
          children: [
            {
              path: "users",
              name: "users",
              redirect: "/system/users/list",
              meta: { label: i18n.t("Nav.Users") },
              component: {
                render(c) {
                  return c("router-view");
                },
              },
              children: [
                {
                  path: "list",
                  name: "user_list",
                  meta: {
                    label: i18n.t("Nav.ChildUser.ListAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ViewUser", "ManageUser"],
                  },
                  component: () => import("@/views/system/users/list"),
                },
                {
                  path: "create",
                  name: "create_user",
                  meta: {
                    label: i18n.t("Nav.ChildUser.CreateAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ManageUser"],
                  },
                  component: () => import("@/views/system/users/create"),
                },
                {
                  path: "detail/:userId",
                  name: "user_detail",
                  meta: {
                    label: i18n.t("Nav.ChildUser.DetailAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ViewUser", "ManageUser"],
                  },
                  component: () => import("@/views/system/users/detail"),
                },
                {
                  path: "privileges/:userId",
                  name: "user_privileges",
                  meta: {
                    label: i18n.t("Nav.ChildUser.PrivilegesAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ManageUser"],
                  },
                  component: () => import("@/views/system/users/privileges"),
                },
              ],
            },
            {
              path: "roles",
              name: "roles",
              redirect: "/system/roles/list",
              meta: { label: i18n.t("Nav.Roles") },
              component: {
                render(c) {
                  return c("router-view");
                },
              },
              children: [
                {
                  path: "list",
                  name: "role_list",
                  meta: {
                    label: i18n.t("Nav.ChildRoles.ListAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ViewRole", "ManageRole"],
                  },
                  component: () => import("@/views/system/roles/list"),
                },
                {
                  path: "create",
                  name: "create_role",
                  meta: {
                    label: i18n.t("Nav.ChildRoles.CreateAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ViewRole", "ManageRole"],
                  },
                  component: () => import("@/views/system/roles/create"),
                },
                {
                  path: "detail/:roleId",
                  name: "role_detail",
                  meta: {
                    label: i18n.t("Nav.ChildRoles.DetailAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ViewRole", "ManageRole"],
                  },
                  component: () => import("@/views/system/roles/detail"),
                },
                {
                  path: "privileges/:roleId",
                  name: "role_privileges",
                  meta: {
                    label: i18n.t("Nav.ChildRoles.PrivilegesAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ManageRole"],
                  },
                  component: () => import("@/views/system/roles/privileges"),
                },
              ],
            },
            {
              path: "company",
              name: "company",
              redirect: "/system/company/list",
              meta: { label: i18n.t("Nav.Company") },
              component: {
                render(c) {
                  return c("router-view");
                },
              },
              children: [
                {
                  path: "list",
                  name: "company_list",
                  meta: {
                    label: i18n.t("Nav.ChildCompany.ListAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ViewCompany", "ManageCompany"],
                  },
                  component: () => import("@/views/system/company/list_tree"),
                },
                {
                  path: "create",
                  name: "create_company",
                  meta: {
                    label: i18n.t("Nav.ChildCompany.CreateAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ManageCompany"],
                  },
                  component: () => import("@/views/system/company/create"),
                },
                //{
                //    path: 'create/:parentId',
                //    name: 'create_company_with_parent',
                //    meta: { label: i18n.t('Nav.ChildCompany.CreateAccess'), requiresAuth: true, requiresPrivileges: [] },
                //    component: () => import('@/views/system/company/create')
                //},
                {
                  path: "detail/:companyId",
                  name: "company_detail",
                  meta: {
                    label: i18n.t("Nav.ChildCompany.DetailAccess"),
                    requiresAuth: true,
                    requiresPrivileges: ["ManageCompany"],
                  },
                  component: () => import("@/views/system/company/detail"),
                },
              ],
            },
          ],
        },
      ],
    },
    {
      path: "/login",
      name: "login",
      component: Login,
    },
    {
      path: "*",
      component: Page404,
    },
  ],
});

router.beforeEach((to, from, next) => {
  var isAuthorized = true;
  for (var i = 0; i < to.matched.length; i++) {
    var route = to.matched[i];

    if (route.meta.requiresAuth) {
      // Check if this route require authentication
      if (!store.state.identity.isAuthenticated) {
        isAuthorized = false;
        break;
      }
      // Check if this route require admin permission
      else if (route.meta.adminOnly && !store.state.identity.isAdministrator) {
        isAuthorized = false;
        break;
      }
      // Check if this route require certain privileges
      else if (
        route.meta.requiresPrivileges &&
        route.meta.requiresPrivileges.length > 0 &&
        intersect(
          store.state.identity.privileges,
          route.meta.requiresPrivileges
        ).length == 0
      ) {
        isAuthorized = false;
        break;
      }
    }
  }

  if (isAuthorized) {
    next();
  } else {
    next("/login");
  }
});

export default router;
