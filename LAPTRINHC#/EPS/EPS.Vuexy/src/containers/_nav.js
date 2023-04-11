export default [
    {
        _name: 'CSidebarNav',
        _children: [
            {
                _name: 'CSidebarNavItem',
                code: 'home',
                name: 'Nav.Home',
                to: '/home',
                icon: 'bx bx-home-alt'
            },
            {
                _name: 'CSidebarNavDropdown',
                code: 'system',
                name: 'Nav.System',
                to: '/system',
                icon: 'bx bx-cart-alt',
                items: [
                    {
                        code: 'manage_company',
                        name: 'Nav.Company',
                        to: '/system/company',
                        icon: 'cil-Building',
                        requiresPrivileges:[],
                        requiresPrivileges: ['ViewCompany', 'ManageCompany']
                    },
                    {
                        code: "manage_users",
                        name: "Nav.Users",
                        to: "/system/users",
                        icon: "cil-user",
                        requiresPrivileges: ["ViewUser", "ManageUser"],
                    },
                    {
                        code: "manage_roles",
                        name: "Nav.Roles",
                        to: "/system/roles",
                        icon: "cil-people",
                        requiresPrivileges: ["ViewRole", "ManageRole"],
                    },
                ]
            }
        ]
    }
]