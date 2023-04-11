using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPS.Data.Entities
{
    public partial class Privilege
    {
        public Privilege()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
            UserPrivileges = new HashSet<UserPrivilege>();
        }

        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public Nullable<int> IsDefaultAdminPrivileges { get; set; }

        [InverseProperty("Privilege")]
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        [InverseProperty("Privilege")]
        public virtual ICollection<UserPrivilege> UserPrivileges { get; set; }
    }

    public enum PrivilegeList
    {
        [Description("Sửa lịch sử ra vào")]
        ManageEvent,
        [Description("Xem lịch sử ra vào")]
        ViewEvent,

        [Description("Xem người dùng")]
        ViewUser,
        [Description("Sửa người dùng")]
        ManageUser,

        [Description("Xem nhóm người dùng")]
        ViewRole,
        [Description("Sửa nhóm người dùng")]
        ManageRole,

        [Description("Xem thiết bị")]
        ViewMachine,
        [Description("Sửa thiết bị")]
        ManageMachine,

        [Description("Xem nhóm người")]
        ViewGroup,
        [Description("Sửa nhóm người")]
        ManageGroup,

        [Description("Xem phòng ban")]
        ViewDepartment,
        [Description("Sửa phòng ban")]
        ManageDepartment,

        [Description("Xem đơn vị")]
        ViewCompany,
        [Description("Sửa đơn vị")]
        ManageCompany,

        [Description("Xem phân quyền truy cập")]
        ViewGroupAccess,
        [Description("Sửa phân quyền truy cập")]
        ManageGroupAccess,

        [Description("Xem thời gian")]
        ViewAccessTimeSeg,
        [Description("Sửa thời gian")]
        ManageAccessTimeSeg,

        [Description("Xem blacklist")]
        ViewPerson,
        [Description("Sửa blacklist")]
        ManagePerson,

        [Description("Xem nhân viên")]
        ViewEmployee,
        [Description("Sửa nhân viên")]
        ManageEmployee,

        [Description("Sửa ảnh")]
        ManageFace,
        [Description("Xem ảnh")]
        ViewFace,

        [Description("Sửa chung")]
        ManageAppParam,
        [Description("Xem chung")]
        ViewAppParam,

        [Description("Xem ca làm việc")]
        ViewShiftTime,
        [Description("Sửa ca làm việc")]
        ManageShiftTime,

        [Description("Xem báo cáo chấm công")]
        ViewReportWorking,
        [Description("Sửa báo cáo chấm công")]
        ManageReportWorking,

        [Description("Xem khách ra vào")]
        ViewGuess,
        [Description("Sửa khách ra vào")]
        ManageGuess,

        [Description("Xem ngày lễ")]
        ViewHolidays,
        [Description("Sửa ngày lễ")]
        ManageHolidays,

        [Description("Xem ngày nghỉ")]
        ViewDayOff,
        [Description("Sửa ngày nghỉ")]
        ManageDayOff,

        [Description("Xem lịch làm việc")]
        ViewWorkCalendar,
        [Description("Sửa lịch làm việc")]
        ManageWorkCalendar,

        [Description("Xem cấu hình cảnh báo")]
        ViewNotification,
        [Description("Sửa cấu hình cảnh báo")]
        ManageNotification,

        [Description("Xem ngoài giờ")]
        ViewOverTime,
        [Description("Sửa ngoài giờ")]
        ManageOverTime,

        [Description("Xem đăng ký ca làm việc")]
        ViewRegisterWorkingShift,
        [Description("Sửa đăng ký ca làm việc")]
        ManageRegisterWorkingShift,

        [Description("Xem chấm công")]
        ViewTimeKeeping,
        [Description("Sửa chấm công")]
        ManageTimeKeeping,

        [Description("Xem OT")]
        ViewOverTimeHours,
        [Description("Sửa OT")]
        ManageOverTimeHours,

        [Description("Xem câu hỏi thường gặp")]
        ViewFAQuestions,
        [Description("Sửa câu hỏi thường gặp")]
        ManageFAQuestions,

        [Description("Xem khối")]
        ViewGrades,
        [Description("Sửa khối")]
        ManageGrades,

        [Description("Xem lớp")]
        ViewClass,
        [Description("Sửa lớp")]
        ManageClass,

        [Description("Xem học sinh")]
        ViewStudent,
        [Description("Sửa học sinh")]
        ManageStudent,

        [Description("Xem phụ huynh")]
        ViewParent,
        [Description("Sửa phụ huynh")]
        ManageParent,

        [Description("Xem lịch sử ra vào học sinh")]
        ViewStudentReport,
        [Description("Sửa lịch sử ra vào học sinh")]
        ManageStudentReport,

        [Description("Xem danh sách đưa đón")]
        ViewParentReport,
        [Description("Sửa danh sách đưa đón")]
        ManageParentReport,

        [Description("Xem báo cáo thông kế đi muộn ")]
        ViewReportWorkingLate,
        [Description("Sửa báo cáo thông kế đi muộn")]
        ManageReportWorkingLate,

        [Description("Xem báo cáo thông kế về sớm ")]
        ViewReportWorkingEarly,
        [Description("Sửa báo cáo thông kế về sớm")]
        ManageReportWorkingEarly,

        [Description("Xem báo cáo thời gian thực làm việc ")]
        ViewReportRealTime,
        [Description("Sửa báo cáo thười gian thực làm việc")]
        ManageReportRealTime,

        [Description("Xem báo cáo điểm danh học sinh ")]
        ViewReportInOutStudent,
        [Description("Sửa báo cáo điểm danh học sinh")]
        ManageReportInOutStudent,

        [Description("Xem cấp mã cập nhật nhân viên")]
        ViewIssueCodeQR,
        [Description("Sửa cấp mã cập nhật nhân viên")]
        ManageIssueCodeQR,

        [Description("Xem phê duyệt cập nhật nhân viên")]
        ViewApproveCodeQR,
        [Description("Sửa phê duyệt cập nhật nhân viên")]
        ManageApproveCodeQR,

        [Description("Xem khu vực")]
        ViewAreas,
        [Description("Sửa khu vực")]
        ManageAreas,

        [Description("Xem theo dõi giám sát")]
        ViewMonitoring,
        [Description("Sửa theo dõi giám sát")]
        ManageMonitoring,

        [Description("Xem thực đơn")]
        ViewMenu,
        [Description("Sửa thực đơn")]
        ManageMenu,

        [Description("Xem đặt bàn")]
        ViewPreOrder,
        [Description("Sửa đặt bàn")]
        ManagePreOrder,
    }
}
