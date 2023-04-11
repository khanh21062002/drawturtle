import Vue from "vue";
import Router from "vue-router";
import i18n from "@/libs/i18n";
import store from "@/store/index";

// Containers
const TheContainer = () => import("@/containers/TheContainer");

// Views - Pages
import Page404 from "@/views/pages/Page404";
//import Page500 from '@/views/pages/Page500'
import Login from "@/views/pages/Login";
import SearchPersonByCode from "@/views/utility/qrCodePerson/updatePerson/findByCode";
import UpdateImagePerson from "@/views/utility/qrCodePerson/updatePerson/updateImage";

/*import i18n from '../libs/i18n';*/
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
            component: TheContainer,
            meta: { requiresAuth: true },
            children: [
                {
                    path: "home",
                    name: "home",
                    meta: { label: "Nav.Home", requiresAuth: true },
                    component: () => import("@/views/home"),
                },
                {
                    path: "/notificationSystem",
                    name: "listNotificationSystem",
                    meta: { label: "Nav.SystemNotification", requiresAuth: true },
                    component: () => import("@/views/notificationSystem/list"),
                },
                {
                    path: "/notificationSystem/detail/:notificationSystemId",
                    name: "notificationSystemDetail",
                    meta: { label: "Nav.SystemNotification", requiresAuth: true },
                    component: () => import("@/views/notificationSystem/detail"),
                },
                {
                    path: "personal/:userId",
                    name: "personal",
                    meta: { label: "Nav.PersonalInformation", requiresAuth: true },
                    component: () => import("@/views/personal"),
                },
                {
                    path: "system",
                    name: "system",
                    redirect: "/system/company",
                    meta: { label: "Nav.Systems" },
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
                            meta: { label: "Nav.Users" },
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
                                        label: "Nav.ChildUser.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewUser", "ManageUser"],
                                    },
                                    component: () => import("@/views/system/users/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_user",
                                    meta: {
                                        label: "Nav.ChildUser.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageUser"],
                                    },
                                    component: () => import("@/views/system/users/create"),
                                },
                                {
                                    path: "detail/:userId",
                                    name: "user_detail",
                                    meta: {
                                        label: "Nav.ChildUser.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewUser", "ManageUser"],
                                    },
                                    component: () => import("@/views/system/users/detail"),
                                },
                                {
                                    path: "privileges/:userId",
                                    name: "user_privileges",
                                    meta: {
                                        label: "Nav.ChildUser.PrivilegesAccess",
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
                            meta: { label: "Nav.Roles" },
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
                                        label: "Nav.ChildRoles.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewRole", "ManageRole"],
                                    },
                                    component: () => import("@/views/system/roles/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_role",
                                    meta: {
                                        label: "Nav.ChildRoles.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageRole"],
                                    },
                                    component: () => import("@/views/system/roles/create"),
                                },
                                {
                                    path: "detail/:roleId",
                                    name: "role_detail",
                                    meta: {
                                        label: "Nav.ChildRoles.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewRole", "ManageRole"],
                                    },
                                    component: () => import("@/views/system/roles/detail"),
                                },
                                {
                                    path: "privileges/:roleId",
                                    name: "role_privileges",
                                    meta: {
                                        label: "Nav.ChildRoles.PrivilegesAccess",
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
                            meta: { label: "Nav.Company" },
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
                                        label: "Nav.ChildCompany.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewCompany", "ManageCompany"],
                                    },
                                    component: () => import("@/views/system/company/list_tree"),
                                },
                                {
                                    path: "create",
                                    name: "create_company",
                                    meta: {
                                        label: "Nav.ChildCompany.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageCompany"],
                                    },
                                    component: () => import("@/views/system/company/create"),
                                },
                                {
                                    path: "create/:parentId",
                                    name: "create_company_with_parent",
                                    meta: {
                                        label: "Nav.ChildCompany.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageCompany"],
                                    },
                                    component: () => import("@/views/system/company/create"),
                                },
                                {
                                    path: "detail/:companyId",
                                    name: "company_detail",
                                    meta: {
                                        label: "Nav.ChildCompany.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewCompany", "ManageCompany"],
                                    },
                                    component: () => import("@/views/system/company/detail"),
                                },
                            ],
                        },
                    ],
                },
                {
                    path: "categories",
                    name: "categories",
                    redirect: "/categories/department/list",
                    meta: { label: "Nav.Category" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "department",
                            name: "department",
                            redirect: "/categories/department/list",
                            meta: { label: "Nav.Department" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "department_list",
                                    meta: {
                                        label: "Nav.ChildDepartment.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewDepartment", "ManageDepartment"],
                                    },
                                    component: () =>
                                        import("@/views/categories/department/list_tree"),
                                },
                                {
                                    path: "create",
                                    name: "create_department",
                                    meta: {
                                        label: "Nav.ChildDepartment.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageDepartment"],
                                    },
                                    component: () =>
                                        import("@/views/categories/department/create"),
                                },
                                {
                                    path: "detail/:Id",
                                    name: "department_detail",
                                    meta: {
                                        label: "Nav.ChildDepartment.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewDepartment", "ManageDepartment"],
                                    },
                                    component: () =>
                                        import("@/views/categories/department/detail"),
                                },
                                {
                                    path: "create/:parentId",
                                    name: "create_department_with_parent",
                                    meta: {
                                        label: "Nav.ChildDepartment.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageDepartment"],
                                    },
                                    component: () =>
                                        import("@/views/categories/department/create"),
                                },
                            ],
                        },
                        {
                            path: 'device',
                            name: 'device',
                            redirect: '/categories/device/list',
                            meta: { label: 'Nav.Device' },
                            component: {
                                render(c) {
                                    return c('router-view');
                                },
                            },
                            children: [
                                {
                                    path: 'list',
                                    name: 'device_list',
                                    meta: {
                                        label: 'Nav.ChildDevice.ListAccess',
                                        requiresAuth: true,
                                        requiresPrivileges: ['ViewMachine', 'ManageMachine'],
                                    },
                                    component: () => import('@/views/categories/device/list'),
                                },
                                {
                                    path: 'create',
                                    name: 'create_access',
                                    meta: {
                                        label: 'Nav.ChildDevice.CreateAccess',
                                        requiresAuth: true,
                                        requiresPrivileges: ['ManageMachine'],
                                    },
                                    component: () => import('@/views/categories/device/create'),
                                },
                                {
                                    path: 'detail/:deviceId',
                                    name: 'device_detail',
                                    meta: {
                                        label: 'Nav.ChildDevice.DetailAccess',
                                        requiresAuth: true,
                                        requiresPrivileges: ['ViewMachine', 'ManageMachine'],
                                    },
                                    component: () => import('@/views/categories/device/detail'),
                                },
                            ],
                        },
                        {
                            path: "areas",
                            name: "areas",
                            redirect: "/categories/areas/list",
                            meta: { label: "Nav.Areas" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "areas_list",
                                    meta: {
                                        label: "Nav.ChildAreas.List",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewAreas", "ManageAreas"],
                                    },
                                    component: () => import("@/views/categories/areas/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_areas",
                                    meta: {
                                        label: "Nav.ChildAreas.Create",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageAreas"],
                                    },
                                    component: () => import("@/views/categories/areas/create"),
                                },
                                {
                                    path: "detail/:areasId",
                                    name: "areas_detail",
                                    meta: {
                                        label: "Nav.ChildAreas.Detail",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewAreas", "ManageAreas"],
                                    },
                                    component: () => import("@/views/categories/areas/detail"),
                                },
                            ],
                        },
                        {
                            path: "machine",
                            name: "machine",
                            redirect: "/categories/machine/list",
                            meta: { label: "Nav.Machine" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "machine_list",
                                    meta: {
                                        label: "Nav.ChildMachine.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewMachine", "ManageMachine"],
                                    },
                                    component: () => import("@/views/categories/machine/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_machine",
                                    meta: {
                                        label: "Nav.ChildMachine.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageMachine"],
                                    },
                                    component: () => import("@/views/categories/machine/create"),
                                },
                                {
                                    path: "detail/:machineId",
                                    name: "machine_detail",
                                    meta: {
                                        label: "Nav.ChildMachine.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewMachine", "ManageMachine"],
                                    },
                                    component: () => import("@/views/categories/machine/detail"),
                                },
                            ],
                        },
                        {
                            path: "menu",
                            name: "menu",
                            redirect: "/categories/menu/list",
                            meta: { label: "Nav.Menu" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "menu_list",
                                    meta: {
                                        label: "Nav.ChildMenu.List",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewMenu", "ManageMenu"],
                                    },
                                    component: () => import("@/views/categories/menu/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_menu",
                                    meta: {
                                        label: "Nav.ChildMenu.Create",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageMenu"],
                                    },
                                    component: () => import("@/views/categories/menu/create"),
                                },
                                {
                                    path: "detail/:menuId",
                                    name: "menu_detail",
                                    meta: {
                                        label: "Nav.ChildMenu.Detail",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewMenu", "ManageMenu"],
                                    },
                                    component: () => import("@/views/categories/menu/detail"),
                                },
                            ],
                        },
                        {
                            path: "preOrder",
                            name: "preOrder",
                            redirect: "/categories/preOrder/list",
                            meta: { label: "Nav.PreOrder" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "preOrder_list",
                                    meta: {
                                        label: "Nav.ChildPreOrder.List",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewPreOrder", "ManagePreOrder"],
                                    },
                                    component: () => import("@/views/categories/preOrder/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_preOrder",
                                    meta: {
                                        label: "Nav.ChildPreOrder.Create",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManagePreOrder"],
                                    },
                                    component: () => import("@/views/categories/preOrder/create"),
                                },
                                {
                                    path: "detail/:preOrderId",
                                    name: "preOrder_detail",
                                    meta: {
                                        label: "Nav.ChildPreOrder.Detail",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewPreOrder", "ManagePreOrder"],
                                    },
                                    component: () => import("@/views/categories/preOrder/detail"),
                                },
                            ],
                        },
                        {
                            path: "group",
                            name: "group",
                            redirect: "/categories/group/list",
                            meta: { label: "Nav.GroupPeople" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "group_list",
                                    meta: {
                                        label: "Nav.ChildGroupPeople.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewGroup", "ManageGroup"],
                                    },
                                    component: () => import("@/views/categories/group/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_group",
                                    meta: {
                                        label: "Nav.ChildGroupPeople.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageGroup"],
                                    },
                                    component: () => import("@/views/categories/group/create"),
                                },
                                {
                                    path: "detail/:groupId",
                                    name: "group_detail",
                                    meta: {
                                        label: "Nav.ChildGroupPeople.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewGroup", "ManageGroup"],
                                    },
                                    component: () => import("@/views/categories/group/detail"),
                                },
                            ],
                        },
                        {
                            path: "groupaccess",
                            name: "groupaccess",
                            redirect: "/categories/groupaccess/list",
                            meta: { label: "Nav.GroupAccess" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "groupaccess_list",
                                    meta: {
                                        label: "Nav.ChildAccess.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewGroupAccess",
                                            "ManageGroupAccess",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/categories/groupaccess/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_groupaccess",
                                    meta: {
                                        label: "Nav.ChildAccess.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewGroupAccess",
                                            "ManageGroupAccess",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/categories/groupaccess/create"),
                                },
                                {
                                    path: "detail/:groupaccessId",
                                    name: "groupaccess_detail",
                                    meta: {
                                        label: "Nav.ChildAccess.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewGroupAccess",
                                            "ManageGroupAccess",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/categories/groupaccess/detail"),
                                },
                            ],
                        },
                        {
                            path: "accesstimeseg",
                            name: "accesstimeseg",
                            redirect: "/categories/accesstimeseg/list",
                            meta: { label: "Nav.AccessTimeSeg" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "accesstimeseg_list",
                                    meta: {
                                        label: "Nav.ChildTimeSeg.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewAccessTimeSeg",
                                            "ManageAccessTimeSeg",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/categories/accesstimeseg/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_accesstimeseg",
                                    meta: {
                                        label: "Nav.ChildTimeSeg.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageAccessTimeSeg"],
                                    },
                                    component: () =>
                                        import("@/views/categories/accesstimeseg/create"),
                                },
                                {
                                    path: "detail/:accesstimesegId",
                                    name: "accesstimeseg_detail",
                                    meta: {
                                        label: "Nav.ChildTimeSeg.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewAccessTimeSeg",
                                            "ManageAccessTimeSeg",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/categories/accesstimeseg/detail"),
                                },
                            ],
                        },
                        {
                            path: "person",
                            name: "person",
                            redirect: "/categories/person/list",
                            meta: { label: "Nav.Person" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "person_list",
                                    meta: {
                                        label: "Nav.ChildPerson.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewPerson", "ManagePerson"],
                                    },
                                    component: () => import("@/views/categories/person/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_person",
                                    meta: {
                                        label: "Nav.ChildPerson.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManagePerson"],
                                    },
                                    component: () => import("@/views/categories/person/create"),
                                },
                                {
                                    path: "detail/:personId",
                                    name: "person_detail",
                                    meta: {
                                        label: "Nav.ChildPerson.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewPerson", "ManagePerson"],
                                    },
                                    component: () => import("@/views/categories/person/detail"),
                                },
                                {
                                    path: "import",
                                    name: "import",
                                    meta: {
                                        label: "Nav.ChildPerson.ImportAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManagePerson"],
                                    },
                                    component: () => import("@/views/categories/person/import"),
                                },
                            ],
                        },
                        {
                            path: "employee",
                            name: "employee",
                            redirect: "/categories/employee/listEmployee",
                            meta: { label: "Nav.Employee" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "listEmployee",
                                    name: "person_list",
                                    meta: {
                                        label: "Nav.ChildEmployee.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewEmployee", "ManageEmployee"],
                                    },
                                    component: () => import("@/views/categories/employee/listEmployee"),
                                },
                                {
                                    path: "create",
                                    name: "create_person",
                                    meta: {
                                        label: "Nav.ChildEmployee.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageEmployee"],
                                    },
                                    component: () => import("@/views/categories/employee/create"),
                                },
                                {
                                    path: "detail/:personId",
                                    name: "person_detail",
                                    meta: {
                                        label: "Nav.ChildEmployee.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewEmployee", "ManageEmployee"],
                                    },
                                    component: () => import("@/views/categories/employee/detail"),
                                },
                                {
                                    path: "import",
                                    name: "importEmp",
                                    meta: {
                                        label: "Nav.ChildPerson.ImportAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageEmployee"],
                                    },
                                    component: () => import("@/views/categories/employee/import"),
                                },
                            ],
                        },

                    ],
                },
                {
                    path: "guess",
                    name: "guess",
                    redirect: "/guess/list",
                    meta: { label: "Nav.GuessManage" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "list",
                            name: "guess_list",
                            meta: {
                                label: "Nav.ChildGuess.ListAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ViewGuess", "ManageGuess"],
                            },
                            component: () => import("@/views/categories/guess/list"),
                        },
                        {
                            path: "create",
                            name: "guess_create",
                            meta: {
                                label: "Nav.ChildGuess.DetailAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ManageGuess"],
                            },
                            component: () => import("@/views/categories/guess/create"),
                        },
                        {
                            path: "detail/:guessId",
                            name: "guess_detail",
                            meta: {
                                label: "Nav.ChildGuess.DetailAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ManageGuess"],
                            },
                            component: () => import("@/views/categories/guess/detail"),
                        },
                    ],
                },
                {
                    path: "school",
                    name: "school",
                    redirect: "/school/student",
                    meta: { label: "Nav.Schools" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "grades",
                            name: "grades",
                            redirect: "/school/grades/list",
                            meta: { label: "Nav.Grades" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "grades_list",
                                    meta: {
                                        label: "Nav.ChildGrades.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewGrades", "ManageGrades"],
                                    },
                                    component: () => import("@/views/school/grades/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_grades",
                                    meta: {
                                        label: "Nav.ChildGrades.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageGrades"],
                                    },
                                    component: () => import("@/views/school/grades/create"),
                                },
                                {
                                    path: "detail/:Id",
                                    name: "grades_detail",
                                    meta: {
                                        label: "Nav.ChildGrades.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewGrades", "ManageGrades"],
                                    },
                                    component: () => import("@/views/school/grades/detail"),
                                },
                            ],
                        },
                        {
                            path: "class",
                            name: "class",
                            redirect: "/school/class/list",
                            meta: { label: "Nav.Class" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "class_list",
                                    meta: {
                                        label: "Nav.ChildClass.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewClass", "ManageClass"],
                                    },
                                    component: () => import("@/views/school/class/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_class",
                                    meta: {
                                        label: "Nav.ChildClass.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageClass"],
                                    },
                                    component: () => import("@/views/school/class/create"),
                                },
                                {
                                    path: "detail/:Id",
                                    name: "class_detail",
                                    meta: {
                                        label: "Nav.ChildClass.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewClass", "ManageClass"],
                                    },
                                    component: () => import("@/views/school/class/detail"),
                                },
                            ],
                        },
                        {
                            path: "student",
                            name: "student",
                            redirect: "/school/student/list",
                            meta: { label: "Nav.Student" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "student_list",
                                    meta: {
                                        label: "Nav.ChildStudent.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewStudent", "ManageStudent"],
                                    },
                                    component: () => import("@/views/school/student/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_person",
                                    meta: {
                                        label: "Nav.ChildStudent.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageStudent"],
                                    },
                                    component: () => import("@/views/school/student/create"),
                                },
                                {
                                    path: "detail/:personId",
                                    name: "person_detail",
                                    meta: {
                                        label: "Nav.ChildStudent.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewStudent", "ManageStudent"],
                                    },
                                    component: () => import("@/views/school/student/detail"),
                                },
                                {
                                    path: "import",
                                    name: "import",
                                    meta: {
                                        label: "Nav.ChildStudent.ImportAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageStudent"],
                                    },
                                    component: () => import("@/views/school/student/import"),
                                },
                            ],
                        },
                        {
                            path: "parent",
                            name: "parent",
                            redirect: "/school/parent/list",
                            meta: { label: "Nav.Parent" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "student_list",
                                    meta: {
                                        label: "Nav.ChildParent.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewParent", "ManageParent"],
                                    },
                                    component: () => import("@/views/school/parent/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_person",
                                    meta: {
                                        label: "Nav.ChildParent.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageParent"],
                                    },
                                    component: () => import("@/views/school/parent/create"),
                                },
                                {
                                    path: "detail/:personId",
                                    name: "person_detail",
                                    meta: {
                                        label: "Nav.ChildParent.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewParent", "ManageParent"],
                                    },
                                    component: () => import("@/views/school/parent/detail"),
                                },
                                {
                                    path: "import",
                                    name: "import",
                                    meta: {
                                        label: "Nav.ChildParent.ImportAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageParent"],
                                    },
                                    component: () => import("@/views/school/parent/import"),
                                },
                            ],
                        },
                    ],
                },
                {
                    path: "monitoring",
                    name: "monitoring",
                    meta: {
                        label: "Nav.Monitoring",
                        requiresAuth: true,
                        requiresPrivileges: ["ViewMonitoring", "ManageMonitoring"],
                    },
                    component: () => import("@/views/report/monitoring/follow"),
                },
                {
                    path: "report",
                    name: "report",
                    redirect: "/report/event",
                    meta: { label: "Nav.Report" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "event",
                            name: "event",
                            redirect: "/report/event/list",
                            meta: { label: "Nav.EventHistory" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "event_list",
                                    meta: {
                                        label: "Nav.ChildHistory.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewEvent", "ManageEvent"],
                                    },
                                    component: () => import("@/views/report/event/list"),
                                },
                                {
                                    path: "detail/:eventId",
                                    name: "event_detail",
                                    meta: {
                                        label: "Nav.ChildHistory.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewEvent", "ManageEvent"],
                                    },
                                    component: () => import("@/views/report/event/detail"),
                                },

                            ],
                        },
                        {
                            path: "employeeInOut",
                            name: "employeeInOut",
                            meta: {
                                label: "Nav.EmployeeInOut",
                                requiresAuth: true,
                                requiresPrivileges: [],
                            },
                            component: () =>
                                import(
                                    "@/views/report/employeeInOut/list"
                                ),
                        },
                        {
                            path: "personInOut",
                            name: "personInOut",
                            meta: {
                                label: "Nav.PersonInOut",
                                requiresAuth: true,
                                requiresPrivileges: [],
                            },
                            component: () =>
                                import(
                                    "@/views/report/personInOut/list"
                                ),
                        },
                        {
                            path: "reportSummary",
                            name: "reportSummary",
                            meta: {
                                label: "Nav.ChildWorking.ListAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ViewEvent"],
                            },
                            component: () =>
                                import(
                                    "@/views/report/event/reportSummary"
                                ),
                        },
                        {
                            path: "reportGuess",
                            name: "reportGuess",
                            meta: {
                                label: "Nav.InOutEmployeeList.ListAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ViewEvent"],
                            },
                            component: () =>
                                import(
                                    "@/views/report/event/reportGuess"
                                ),
                        },
                        {
                            path: "inOutEmployee",
                            name: "reportEmployee",
                            redirect: "/report/inOutEmployee/reportInOutEmployee",
                            meta: { label: "Nav.InOutEmployee" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportInOutEmployee",
                                    name: "reportInOutEmployee",
                                    meta: { label: "Nav.InOutEmployeeList.ListAccess" },
                                    component: () =>
                                        import("@/views/report/inOutEmployee/reportInOutEmployee"),
                                },
                            ],
                        },
                        {
                            path: "reportWorkingTime",
                            name: "reportWorkingTime",
                            redirect: "/report/reportWorkingTime/reportWorkingHour",
                            meta: { label: "Nav.ReportWorking" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportWorkingHour",
                                    name: "reportWorkingHour",
                                    meta: {
                                        label: "Nav.ChildWorking.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewReportWorking"],
                                    },
                                    component: () =>
                                        import(
                                            "@/views/report/reportWorkingTime/reportWorkingHour"
                                        ),
                                },
                            ],
                        },
                        {
                            path: "overTimeHours",
                            name: "reportOverTimeHours",
                            redirect: "/report/overtimehours/reportOverTimeHours",
                            meta: { label: "Nav.ReportOverTimeHours" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportOverTimeHours",
                                    name: "reportOverTimeHours_report",
                                    meta: {
                                        label: "Nav.ChildReport.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewOverTimeHours"],
                                    },
                                    component: () =>
                                        import("@/views/report/overtimehours/reportOverTimeHours"),
                                },
                            ],
                        },
                        {
                            path: "overTimeHoursLate",
                            name: "reportOverTimeHoursLate",
                            redirect: "/report/overtimehoursLate/reportOverTimeHoursLate",
                            meta: { label: "Nav.ReportOverTimeHoursLate" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportOverTimeHoursLate",
                                    name: "Nav.ReportOverTimeHours",
                                    meta: { label: "Nav.OverTimeHoursReportLate.ListAccess" },
                                    component: () =>
                                        import(
                                            "@/views/report/overtimehoursLate/reportOverTimeHoursLate"
                                        ),
                                },
                            ],
                        },
                        {
                            path: "overTimeHoursEarly",
                            name: "reportOverTimeHoursEarly",
                            redirect: "/report/overtimehours/reportOverTimeHoursEarly",
                            meta: { label: "Nav.ReportOverTimeHoursEarly" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportOverTimeHoursEarly",
                                    name: "reportOverTimeHours_reportEarly",
                                    meta: { label: "Nav.OverTimeHoursReportEarly.ListAccess" },
                                    component: () =>
                                        import(
                                            "@/views/report/overtimehoursEarly/reportOverTimeHoursEarly"
                                        ),
                                },
                            ],
                        },
                        {
                            path: "realTime",
                            name: "reportRealTime",
                            redirect: "/report/realTime/reportRealTime",
                            meta: { label: "Nav.RealTime" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportRealTime",
                                    name: "reportRealTime",
                                    meta: { label: "Nav.RealTimeList.ListAccess" },
                                    component: () =>
                                        import("@/views/report/realTime/reportRealTime"),
                                },
                            ],
                        },
                        {
                            path: "inOutStudent",
                            name: "reportInOutStudent",
                            redirect: "/report/inOutStuden/reportInOutStudent",
                            meta: { label: "Nav.InOutStudent" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "reportInOutStudent",
                                    name: "reportInOutStudent",
                                    meta: { label: "Nav.InOutStudentList.ListAccess" },
                                    component: () =>
                                        import("@/views/report/inOutStudent/reportInOutStudent"),
                                },
                            ],
                        },
                        {
                            path: "studentReport",
                            name: "studentReport",
                            redirect: "/report/studentReport/list",
                            meta: { label: "Nav.StudentReport" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "event_list",
                                    meta: {
                                        label: "Nav.ChildStudentReport.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewStudentReport",
                                            "MangageStudentReport",
                                        ],
                                    },
                                    component: () => import("@/views/report/studentReport/list"),
                                },
                            ],
                        },
                        {
                            path: "parentReport",
                            name: "parentReport",
                            redirect: "/report/parentReport/list",
                            meta: { label: "Nav.ParentReport" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "parentReport_list",
                                    meta: {
                                        label: "Nav.ChildParentReport.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewParentReport",
                                            "ManageParentReport",
                                        ],
                                    },
                                    component: () => import("@/views/report/parentReport/list"),
                                },
                            ],
                        },
                    ],
                },
                {
                    path: "timesheet",
                    name: "timesheet",
                    redirect: "/timesheet/working-shift-times",
                    meta: { label: "Nav.TimeSheet" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "working-shift-times",
                            name: "workingshifttimes",
                            redirect: "/timesheet/working-shift-times/list",
                            meta: { label: "Nav.ShiftTime" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "shifttime_list",
                                    meta: {
                                        label: "Nav.ChildShiftTime.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewShiftTime", "ManageShiftTime"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/workingshifttime/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_shifttime",
                                    meta: {
                                        label: "Nav.ChildShiftTime.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewShiftTime"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/workingshifttime/create"),
                                },
                                {
                                    path: "detail/:shifttimeId",
                                    name: "shifttime_detail",
                                    meta: {
                                        label: "Nav.ChildShiftTime.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewShiftTime", "ManageShiftTime"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/workingshifttime/detail"),
                                },
                            ],
                        },
                        {
                            path: "register-working-shift",
                            name: "register-working-shift",
                            redirect: "/timesheet/register-working-shift/list",
                            meta: { label: "Nav.RegisterWorkingShift" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "registerworkingshift_list",
                                    meta: {
                                        label: "Nav.ChildTimeSheet.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewRegisterWorkingShift",
                                            "ManageRegisterWorkingShift",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/registerworkingshift/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_registerworkingshift",
                                    meta: {
                                        label: "Nav.ChildTimeSheet.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageRegisterWorkingShift"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/registerworkingshift/create"),
                                },
                                {
                                    path: "detail/:registerworkingshiftId",
                                    name: "registerworkingshift_detail",
                                    meta: {
                                        label: "Nav.ChildTimeSheet.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewRegisterWorkingShift",
                                            "ManageRegisterWorkingShift",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/registerworkingshift/detail"),
                                },
                            ],
                        },
                        {
                            path: "holidays",
                            name: "holidays",
                            redirect: "/timesheet/holidays/list",
                            meta: { label: "Nav.Holidays" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "holidays_list",
                                    meta: {
                                        label: "Nav.ChildHolidays.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewHolidays", "ManageHolidays"],
                                    },
                                    component: () => import("@/views/timesheet/holidays/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_holidays",
                                    meta: {
                                        label: "Nav.ChildHolidays.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageHolidays"],
                                    },
                                    component: () => import("@/views/timesheet/holidays/create"),
                                },
                                {
                                    path: "detail/:holidaysId",
                                    name: "holidays_detail",
                                    meta: {
                                        label: "Nav.ChildHolidays.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewHolidays", "ManageHolidays"],
                                    },
                                    component: () => import("@/views/timesheet/holidays/detail"),
                                },
                            ],
                        },
                        {
                            path: "dayoff",
                            name: "dayoff",
                            redirect: "/timesheet/dayoff/list",
                            meta: { label: "Nav.DayOff" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "dayoff_list",
                                    meta: {
                                        label: "Nav.ChildDayOff.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewDayOff", "ManageDayOff"],
                                    },
                                    component: () => import("@/views/timesheet/dayoff/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_dayoff",
                                    meta: {
                                        label: "Nav.ChildDayOff.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageDayOff"],
                                    },
                                    component: () => import("@/views/timesheet/dayoff/create"),
                                },
                                {
                                    path: "detail/:dayoffId",
                                    name: "dayoff_detail",
                                    meta: {
                                        label: "Nav.ChildDayOff.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewDayOff", "ManageDayOff"],
                                    },
                                    component: () => import("@/views/timesheet/dayoff/detail"),
                                },
                            ],
                        },
                        {
                            path: "workcalendar",
                            name: "workcalendar",
                            redirect: "/timesheet/workcalendar/list",
                            meta: { label: "Nav.WorkCalendar" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "workcalendar_list",
                                    meta: {
                                        label: "Nav.ChildWorkCalendar.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewWorkCalendar",
                                            "ManageWorkCalendar",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/workcalendar/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_workcalendar",
                                    meta: {
                                        label: "Nav.ChildWorkCalendar.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageWorkCalendar"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/workcalendar/create"),
                                },
                                {
                                    path: "detail/:workcalendarId",
                                    name: "workcalendar_detail",
                                    meta: {
                                        label: "Nav.ChildWorkCalendar.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewWorkCalendar",
                                            "ManageWorkCalendar",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/workcalendar/detail"),
                                },
                            ],
                        },
                        {
                            path: "timekeeping",
                            name: "timekeeping",
                            redirect: "/timesheet/timekeeping/list",
                            meta: { label: "Nav.TimeKeeping" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "timekeeping_list",
                                    meta: {
                                        label: "Nav.ChildTimeKeeping.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewTimeKeeping",
                                            "ManageTimeKeeping",
                                        ],
                                    },
                                    component: () => import("@/views/timesheet/timekeeping/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_timekeeping",
                                    meta: {
                                        label: "Nav.ChildTimeKeeping.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageTimeKeeping"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/timekeeping/create"),
                                },
                                {
                                    path: "detail/:timekeepingId",
                                    name: "timekeeping_detail",
                                    meta: {
                                        label: "Nav.ChildTimeKeeping.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewTimeKeeping",
                                            "ManageTimeKeeping",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/timekeeping/detail"),
                                },
                                {
                                    path: "import",
                                    name: "import",
                                    meta: {
                                        label: "Nav.ChildTimeKeeping.ImportAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageTimeKeeping"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/timekeeping/import"),
                                },
                            ],
                        },
                        {
                            path: "overtime",
                            name: "overtime",
                            redirect: "/timesheet/overtime/list",
                            meta: { label: "Nav.OverTime" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "overtime_list",
                                    meta: {
                                        label: "Nav.ChildOverTime.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewOverTime", "ManageOverTime"],
                                    },
                                    component: () => import("@/views/timesheet/overtime/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_overtime",
                                    meta: {
                                        label: "Nav.ChildOverTime.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageOverTime"],
                                    },
                                    component: () => import("@/views/timesheet/overtime/create"),
                                },
                                {
                                    path: "detail/:overtimeId",
                                    name: "overtime_detail",
                                    meta: {
                                        label: "Nav.ChildOverTime.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ViewOverTime", "ManageOverTime"],
                                    },
                                    component: () => import("@/views/timesheet/overtime/detail"),
                                },
                            ],
                        },
                        {
                            path: "overtimehours",
                            name: "overtimehours",
                            redirect: "/timesheet/overtimehours/list",
                            meta: { label: "Nav.OverTimeHours" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "overtimehours_list",
                                    meta: {
                                        label: "Nav.ChildOverTimeHours.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewOverTimeHours",
                                            "ManageOverTimeHours",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/overtimehours/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_overtimehours",
                                    meta: {
                                        label: "Nav.ChildOverTimeHours.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageOverTimeHours"],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/overtimehours/create"),
                                },
                                {
                                    path: "detail/:overtimehoursId",
                                    name: "overtimehours_detail",
                                    meta: {
                                        label: "Nav.ChildOverTimeHours.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewOverTimeHours",
                                            "ManageOverTimeHours",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/timesheet/overtimehours/detail"),
                                },
                            ],
                        },
                    ],
                },
                {
                    path: "notification",
                    name: "notification",
                    redirect: "/notification/list",
                    meta: { label: "Nav.Notification" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "list",
                            name: "notification_list",
                            meta: {
                                label: "Nav.ChildNotification.ListAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ViewNotification", "ManageNotification"],
                            },
                            component: () => import("@/views/notification/list"),
                        },
                        {
                            path: "create",
                            name: "notification_workcalendar",
                            meta: {
                                label: "Nav.ChildNotification.CreateAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ManageNotification"],
                            },
                            component: () => import("@/views/notification/create"),
                        },
                        {
                            path: "detail/:notificationId",
                            name: "notification_detail",
                            meta: {
                                label: "Nav.ChildNotification.DetailAccess",
                                requiresAuth: true,
                                requiresPrivileges: ["ViewNotification", "ManageNotification"],
                            },
                            component: () => import("@/views/notification/detail"),
                        },
                    ],
                },
                {
                    path: "utility",
                    name: "utility",
                    redirect: "/utility/list",
                    meta: { label: "Nav.Utility" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "list",
                            name: "utility_list",
                            meta: {
                                label: "Nav.ChildUtility.ListAccess",
                                requiresAuth: true,
                            },
                            component: () => import("@/views/utility/qrCodePerson/issueQR"),

                        },
                        {
                            path: "qrCodePerson",
                            name: "qrCodePerson",
                            redirect: "/utility/qrCodePerson/issue",
                            meta: { label: "QRCodePerson.Issue.BarName" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "issue",
                                    name: "qrCode_person_list",
                                    meta: {
                                        label: "QRCodePerson.Issue.Header",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewIssueCodeQR",
                                            "ManageIssueCodeQR",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/utility/qrCodePerson/issueQR"),
                                },
                            ],
                        },
                        {
                            path: "approveQRCodePerson",
                            name: "approveQRCodePerson",
                            redirect: "/utility/approveQRCodePerson/approve",
                            meta: { label: "QRCodePerson.Approve.BarName" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "approve",
                                    name: "qrCode_person_approve",
                                    meta: {
                                        label: "QRCodePerson.Approve.Header",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewApproveCodeQR",
                                            "ManageApproveCodeQR",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/utility/qrCodePerson/approve"),
                                },
                            ],
                        },
                    ],
                },
                {
                    path: "faquestions",
                    name: "faquestions",
                    redirect: "/faquestions/list",
                    meta: { label: "Nav.FAQuestions" },
                    component: {
                        render(c) {
                            return c("router-view");
                        },
                    },
                    children: [
                        {
                            path: "list",
                            name: "faquestion_list",
                            meta: {
                                label: "Nav.ChildFAQuestions.ListAccess",
                                requiresAuth: true,
                            },
                            component: () => import("@/views/faquestions/list"),
                        },
                        {
                            path: "manager_faq",
                            name: "manager_faq",
                            redirect: "/faquestions/manager_faq/list",
                            meta: { label: "Nav.Manager_faq" },
                            component: {
                                render(c) {
                                    return c("router-view");
                                },
                            },
                            children: [
                                {
                                    path: "list",
                                    name: "faquestions_list",
                                    meta: {
                                        label: "Nav.ChildManager_faq.ListAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewFAQuestions",
                                            "ManageFAQuestions",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/faquestions/manager_faq/list"),
                                },
                                {
                                    path: "create",
                                    name: "create_faquestions",
                                    meta: {
                                        label: "Nav.ChildManager_faq.CreateAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: ["ManageFAQuestions"],
                                    },
                                    component: () =>
                                        import("@/views/faquestions/manager_faq/create"),
                                },
                                {
                                    path: "detail/:faquestionsId",
                                    name: "faquestions_detail",
                                    meta: {
                                        label: "Nav.ChildManager_faq.DetailAccess",
                                        requiresAuth: true,
                                        requiresPrivileges: [
                                            "ViewFAQuestions",
                                            "ManageFAQuestions",
                                        ],
                                    },
                                    component: () =>
                                        import("@/views/faquestions/manager_faq/detail"),
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
        {
            path: "/searchPersonByCode/:compId/:qrId",
            name: "searchPersonByCode",
            component: SearchPersonByCode,
        },
        {
            path: "/updateImagePerson/:compId/:qrId/:pid",
            name: "updateImagePerson",
            component: UpdateImagePerson,
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

        let label = route.meta.label;
        //set i18n
        route.meta.label = i18n.t(label);
    }
    if (isAuthorized) {
        next();
    } else {
        next("/login");
    }
});

export default router;
