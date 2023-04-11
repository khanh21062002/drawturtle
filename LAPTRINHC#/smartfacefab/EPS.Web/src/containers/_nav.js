export default [
    {
        _name: 'CSidebarNav',
        _children: [
            {
                _name: 'CSidebarNavItem',
                code: 'home',
                name: 'Nav.Home',
                to: '/home',
                icon: 'cil-home'
            },
            {
                _name: 'CSidebarNavItem',
                code: 'manage_monitoring',
                name: 'Nav.Monitoring',
                to: '/monitoring',
                icon: 'cil-camera',
                requiresPrivileges: ['ViewMonitoring', 'ManageMonitoring']
            },
            {
                _name: 'CSidebarNavDropdown',
                code: 'report',
                name: 'Nav.Report',
                to: '/report',
                icon: 'cil-bar-chart',
                items: [
                    {
                        code: 'manage_event',
                        name: 'Nav.EventHistory',
                        to: '/report/event',
                        icon: 'cil-history',
                        requiresPrivileges: []
                    },
                    {
                        code: 'manage_employeeInOut',
                        name: 'Nav.EmployeeInOut',
                        to: '/report/employeeInOut',
                        icon: 'cil-bar-chart',
                        requiresPrivileges: []
                    },
                    {
                        code: 'manage_personInOut',
                        name: 'Nav.PersonInOut',
                        to: '/report/personInOut',
                        icon: 'cil-bar-chart',
                        requiresPrivileges: []
                    },
                    //{
                    //    code: 'manage_reportGuess',
                    //    name: 'Nav.InOutEmployee',
                    //    to: '/report/reportGuess',
                    //    icon: 'cil-bar-chart',
                    //    requiresPrivileges: ['ViewEvent', 'ManageEvent']
                    //},
                    //{
                    //    code: 'manage_reportWorkingTime',
                    //    name: 'Nav.ReportWorking',
                    //    to: '/report/reportSummary',
                    //    icon: 'cil-calendar',
                    //    requiresPrivileges: ['ViewEvent', 'ManageEvent']
                    //},
                    //{
                    //    code: 'manage_reportOverTimeHours',
                    //    name: 'Nav.ReportOverTimeHours',
                    //    to: '/report/overtimehours/reportOverTimeHours',
                    //    icon: 'cil-bar-chart',
                    //    requiresPrivileges: ['ViewOverTimeHours', 'ManageOverTimeHours']
                    //},
                    //{
                    //    code: 'manage_reportOverTimeHoursLate',
                    //    name: 'Nav.ReportOverTimeHoursLate',
                    //    to: '/report/overtimehoursLate/reportOverTimeHoursLate',
                    //    icon: 'cil-bar-chart',
                    //    requiresPrivileges: ['ViewReportWorkingLate', 'ManageReportWorkingLate']
                    //},
                    //{
                    //    code: 'manage_reportOverTimeHoursEarly',
                    //    name: 'Nav.ReportOverTimeHoursEarly',
                    //    to: '/report/overtimehoursEarly/reportOverTimeHoursEarly',
                    //    icon: 'cil-bar-chart',
                    //    requiresPrivileges: ['ViewReportWorkingEarly', 'ManageReportWorkingEarly']
                    //},
                    //{
                    //    code: 'manage_reportRealTime',
                    //    name: 'Nav.RealTime',
                    //    to: '/report/realTime/reportRealTime',
                    //    icon: 'cil-bar-chart',
                    //    requiresPrivileges: ['ViewReportRealTime', 'ManageReportRealTime']
                    //},
                    //{
                    //    code: 'manage_reportInOutStudent',
                    //    name: 'Nav.InOutStudent',
                    //    to: '/report/inOutStudent/reportInOutStudent',
                    //    icon: 'cil-bar-chart',
                    //    requiresPrivileges: ['ViewReportInOutStudent', 'ManageReportInOutStudent']
                    //},


                ]
            },
            {
                _name: 'CSidebarNavDropdown',
                code: 'categories',
                name: "Nav.Category",
                to: '/categories',
                icon: 'cil-list',
                items: [
                    {
                        code: 'manage_department',
                        name: 'Nav.Department',
                        to: '/categories/department',
                        icon: 'cil-sitemap',
                        requiresPrivileges: ['ViewDepartment', 'ManageDepartment']
                    },
                    {
                        code: "manage_areas",
                        name: "Nav.Areas",
                        to: "/categories/areas",
                        icon: "cil-location-pin",
                        requiresPrivileges: ["ViewAreas", "ManageAreas"],
                    },
                    {
                        code: 'manage_device',
                        name: 'Nav.Device',
                        to: '/categories/device',
                        icon: 'cil-mobile',
                        requiresPrivileges: ['ViewMachine', 'ManageMachine'],
                    },
                    //{
                    //    code: "manage_menu",
                    //    name: "Nav.Menu",
                    //    to: "/categories/menu",
                    //    icon: "cil-restaurant",
                    //    requiresPrivileges: ["ViewMenu", "ManageMenu"],
                    //},
                    //{
                    //    code: "manage_preOrder",
                    //    name: "Nav.PreOrder",
                    //    to: "/categories/preOrder",
                    //    icon: "cil-grid",
                    //    requiresPrivileges: ["ViewPreOrder", "ManagePreOrder"],
                    //},
                    //{
                    //    code: 'manage_group',
                    //    name: 'Nav.GroupPeople',
                    //    to: '/categories/group',
                    //    icon: 'cil-people',
                    //    requiresPrivileges: ['ViewGroup', 'ManageGroup']
                    //},
                    //{
                    //    code: 'manage_machine',
                    //    name: 'Nav.Machine',
                    //    to: '/categories/machine',
                    //    icon: 'cil-mobile',
                    //    requiresPrivileges: ['ViewMachine', 'ManageMachine']
                    //},
                    //{
                    //    code: 'manage_accesstimeseg',
                    //    name: 'Nav.AccessTimeSeg',
                    //    to: '/categories/accesstimeseg',
                    //    icon: 'cil-clock',
                    //    requiresPrivileges: ['ViewAccessTimeSeg', 'ManageAccessTimeSeg']
                    //},
                    //{
                    //    code: 'manage_groupaccess',
                    //    name: 'Nav.GroupAccess',
                    //    to: '/categories/groupaccess',
                    //    icon: 'cil-calendar-check',
                    //    requiresPrivileges: ['ViewGroupAccess', 'ManageGroupAccess']
                    //},
                    {
                        code: 'manage_person',
                        name: 'Nav.Person',
                        to: '/categories/person',
                        icon: 'cil-address-book',
                        requiresPrivileges: ['ViewPerson', 'ManagePerson']
                    },
                    {
                        code: 'employee',
                        name: 'Nav.Employee',
                        to: '/categories/employee',
                        icon: 'cil-address-book',
                        requiresPrivileges: ['ViewEmployee', 'ManageEmployee']
                    },


                ]
            },
            //{
            //    _name: 'CSidebarNavDropdown',
            //    code: 'schoolName',
            //    name: 'Nav.Schools',
            //    to: '/school',
            //    icon: 'cil-school',
            //    items: [
            //        {
            //            code: 'manage_grades',
            //            name: 'Nav.Grades',
            //            to: '/school/grades',
            //            icon: 'cil-school',
            //            requiresPrivileges: ['ViewGrades', 'ManageGrades']
            //        },
            //        {
            //            code: 'manage_class',
            //            name: 'Nav.Class',
            //            to: '/school/class',
            //            icon: 'cil-book',
            //            requiresPrivileges: ['ViewClass', 'ManageClass']
            //        },
            //        {
            //            code: 'manage_student',
            //            name: 'Nav.Student',
            //            to: '/school/student',
            //            icon: 'cil-people',
            //            requiresPrivileges: ['ViewStudent', 'ManageStudent']
            //        },
            //        {
            //            code: 'manage_parent',
            //            name: 'Nav.Parent',
            //            to: '/school/parent',
            //            icon: 'cil-address-book',
            //            requiresPrivileges: ['ViewParent', 'ManageParent']
            //        },
            //        {
            //            code: 'manage_studentReport',
            //            name: 'Nav.StudentReport',
            //            to: '/report/studentReport',
            //            icon: 'cil-history',
            //            requiresPrivileges: ['ViewStudentReport', 'ManageStudentReport']
            //        },
            //        {
            //            code: 'manage_parentReport',
            //            name: 'Nav.ParentReport',
            //            to: '/report/parentReport',
            //            icon: 'cil-history',
            //            requiresPrivileges: ['ViewParentReport', 'ManageParentReport']
            //        },
            //    ]
            //},

            {
                _name: 'CSidebarNavDropdown',
                code: 'system',
                name: 'Nav.Systems',
                to: '/system',
                icon: 'cil-settings',
                items: [
                    {
                        code: 'manage_company',
                        name: 'Nav.Company',
                        to: '/system/company',
                        icon: 'cil-Building',
                        requiresPrivileges: ['ViewCompany', 'ManageCompany']
                    },
                    {
                        code: 'manage_users',
                        name: 'Nav.Users',
                        to: '/system/users',
                        icon: 'cil-user',
                        requiresPrivileges: ['ViewUser', 'ManageUser']
                    },
                    {
                        code: 'manage_roles',
                        name: 'Nav.Roles',
                        to: '/system/roles',
                        icon: 'cil-people',
                        requiresPrivileges: ['ViewRole', 'ManageRole']
                    },
                    //{
                    //    code: 'manage_faquestions',
                    //    name: 'Nav.Manager_faq',
                    //    to: '/faquestions/manager_faq',
                    //    icon: 'cil-Puzzle',
                    //    requiresPrivileges: ['ViewFAQuestions', 'ManageFAQuestions']
                    //},
                ]
            },
            //{
            //    _name: 'CSidebarNavDropdown',
            //    code: 'timesheet',
            //    name: 'Nav.TimeSheet',
            //    to: '/timesheet',
            //    icon: 'cil-calendar-check',
            //    items: [
            //        {
            //            code: 'manage_workingshifttime',
            //            name: 'Nav.ShiftTime',
            //            to: '/timesheet/working-shift-times',
            //            icon: '',
            //            requiresPrivileges: ['ViewShiftTime', 'ManageShiftTime']
            //        },
            //        {
            //            code: 'manage_registerworkingshift',
            //            name: 'Nav.RegisterWorkingShift',
            //            to: '/timesheet/register-working-shift',
            //            icon: '',
            //            requiresPrivileges: ['ViewRegisterWorkingShift', 'ManageRegisterWorkingShift']
            //        },
            //        {
            //            code: 'manage_holidays',
            //            name: 'Nav.Holidays',
            //            to: '/timesheet/holidays',
            //            icon: 'cil-featured-playlist',
            //            requiresPrivileges: ['ViewHolidays', 'ManageHolidays']
            //        },
            //        {
            //            code: 'manage_dayoff',
            //            name: 'Nav.DayOff',
            //            to: '/timesheet/dayoff',
            //            icon: 'cil-featured-playlist',
            //            requiresPrivileges: ['ViewDayOff', 'ManageDayOff']
            //        },
            //        {
            //            code: 'manage_workcalendar',
            //            name: 'Nav.WorkCalendar',
            //            to: '/timesheet/workcalendar',
            //            icon: 'cil-featured-playlist',
            //            requiresPrivileges: ['ViewWorkCalendar', 'ManageWorkCalendar']
            //        },
            //        {
            //            code: 'manage_timekeeping',
            //            name: 'Nav.TimeKeeping',
            //            to: '/timesheet/timekeeping',
            //            icon: 'cil-featured-playlist',
            //            requiresPrivileges: ['ViewTimeKeeping', 'ManageTimeKeeping']
            //        },
            //        {
            //            code: 'manage_overtime',
            //            name: 'Nav.OverTime',
            //            to: '/timesheet/overtime',
            //            icon: 'cil-featured-playlist',
            //            requiresPrivileges: ['ViewOverTime', 'ManageOverTime']
            //        },
            //        {
            //            code: 'manage_overtimehours',
            //            name: 'Nav.OverTimeHours',
            //            to: '/timesheet/overtimehours',
            //            icon: 'cil-featured-playlist',
            //            requiresPrivileges: ['ViewOverTimeHours', 'ManageOverTimeHours']
            //        },
            //    ]
            //},
            //{
            //    _name: 'CSidebarNavDropdown',
            //    code: 'utility',
            //    name: 'Nav.Utility',
            //    to: '/utility',
            //    icon: 'cil-aperture',
            //    items: [
            //        {
            //            code: 'manage_qrcode_person',
            //            name: 'QRCodePerson.Issue.BarName',
            //            to: '/utility/qrCodePerson',
            //            icon: 'cil-qr-code',
            //            requiresPrivileges: ['ViewIssueCodeQR', 'ManageIssueCodeQR']
            //        },
            //        {
            //            code: 'manage_qrcode_person',
            //            name: 'QRCodePerson.Approve.BarName',
            //            to: '/utility/approveQRCodePerson',
            //            icon: 'cil-check',
            //            requiresPrivileges: ['ViewApproveCodeQR', 'ManageApproveCodeQR']
            //        },

            //    ]
            //},
            //{
            //    _name: 'CSidebarNavItem',
            //    code: 'manage_guess',
            //    name: 'Nav.GuessManage',
            //    to: '/guess',
            //    icon: 'cil-user-follow',
            //    requiresPrivileges: ['ViewGuess', 'ManageGuess']
            //},
            //{
            //    _name: 'CSidebarNavItem',
            //    code: 'notification',
            //    name: 'Nav.Notification',
            //    to: '/notification',
            //    icon: 'cil-bell',
            //    requiresPrivileges: ['ViewNotification', 'ManageNotification']
            //},

            //{
            //    _name: 'CSidebarNavItem',
            //    code: 'faquestions',
            //    name: 'Nav.FAQuestions',
            //    to: '/faquestions',
            //    icon: 'cil-Puzzle',
            //},

        ]
    }
]