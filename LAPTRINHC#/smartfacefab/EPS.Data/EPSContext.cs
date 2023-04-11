using System;
using EPS.Data.Entities;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EPS.Data
{
    public partial class EPSContext : DbContext, IDataProtectionKeyContext
    {
        public EPSContext()
        {
            this.Database.SetCommandTimeout(600);
        }

        public EPSContext(DbContextOptions<EPSContext> options)
            : base(options)
        {
            this.Database.SetCommandTimeout(600);
        }

        public virtual DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
        public virtual DbSet<IdentityClient> IdentityClients { get; set; }
        public virtual DbSet<IdentityRefreshToken> IdentityRefreshTokens { get; set; }
        public virtual DbSet<Privilege> Privileges { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePrivilege> RolePrivileges { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UnitAncestor> UnitAncestors { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Company> Companys { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<GroupAccess> GroupAccess { get; set; }
        public virtual DbSet<AccessTimeSeg> AccessTimeSegs { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonGroup> PersonGroup { get; set; }
        public virtual DbSet<OTP> OTP { get; set; }
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<Face> Faces { get; set; }
        public virtual DbSet<AppParam> AppParam { get; set; }
        public virtual DbSet<v_Customer> v_Customer { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<GuessRegister> GuessRegister { get; set; }
        public virtual DbSet<PersonsAccess> PersonsAccess { get; set; }
        public virtual DbSet<v_GroupAccessDevices> v_GroupAccessDevices { get; set; }
        public virtual DbSet<v_ReportWorkingTime> v_ReportWorkingTime { get; set; }
        public virtual DbSet<ChartDataItem> ChartDataItem { get; set; }
        public virtual DbSet<ShiftTime> ShiftTime { get; set; }
        public virtual DbSet<p_TopLatePerson> p_TopLatePerson { get; set; }
        public virtual DbSet<p_PieChartData> p_PieChartData { get; set; }
        public virtual DbSet<WorkingShiftTimes> WorkingShiftTimes { get; set; }
        public virtual DbSet<Holidays> Holidays { get; set; }
        public virtual DbSet<DayOff> DayOff { get; set; }
        public virtual DbSet<WorkCalendar> WorkCalendar { get; set; }
        public virtual DbSet<WorkCalendarDetail> WorkCalendarDetail { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<NotificationDetail> NotificationDetail { get; set; }
        public virtual DbSet<NotificationSystem> NotificationSystem { get; set; }
        public virtual DbSet<OverTime> OverTime { get; set; }
        public virtual DbSet<TimeKeeping> TimeKeeping { get; set; }
        public virtual DbSet<RegisterWorkingShift> RegisterWorkingShift { get; set; }
        public virtual DbSet<RegisterWorkingShiftDetail> RegisterWorkingShiftDetail { get; set; }
        public virtual DbSet<WorkingHours> WorkingHours { get; set; }
        public virtual DbSet<FAQuestions> FAQuestions { get; set; }
        public virtual DbSet<View_ListEvent> View_ListEvent { get; set; }
        public virtual DbSet<View_ListEvent_RealTime> View_ListEvent_RealTime { get; set; }
        public virtual DbSet<OverTimeHours> OverTimeHours { get; set; }
        public virtual DbSet<MachineSynchronized> MachineSynchronized { get; set; }
        public virtual DbSet<ShowImfomation> ShowImfomation { get; set; }
        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<StudentParents> StudentParents { get; set; }
        public virtual DbSet<PersonQRUpdate> PersonQRUpdate { get; set; }
        public virtual DbSet<PersonQRCode> PersonQRCode { get; set; }
        public virtual DbSet<View_Event> View_Event { get; set; }
        public virtual DbSet<View_ListGuess> View_ListGuess { get; set; }
        public virtual DbSet<View_ListPerson> View_ListPerson { get; set; }
        public virtual DbSet<View_Machine> View_Machine { get; set; }
        public virtual DbSet<AreaMachine> AreaMachine { get; set; }
        public virtual DbSet<CustomerEvent> CustomerEvent { get; set; }
        public virtual DbSet<v_CustomerEvent> v_CustomerEvent { get; set; }
        public virtual DbSet<View_Person_NumberOfTimes> View_Person_NumberOfTimes { get; set; }
        public virtual DbSet<HomeCustomerComeStore> CustomerComeStore { get; set; }
        public virtual DbSet<HomeChartCustomerByDay> HomeChartCustomerByDay { get; set; }
        public virtual DbSet<HomePieCustomerByDay> HomePieCustomerByDay { get; set; }
        public virtual DbSet<Features> Features { get; set; }
        public virtual DbSet<DeviceFeature> DeviceFeature { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<ViewDeviceFeatures> ViewDeviceFeatures { get; set; }
        public virtual DbSet<HomeChartDay> HomeChartDay { get; set; }
        public virtual DbSet<HomePieByDay> HomePieByDay { get; set; }
        public virtual DbSet<CamViewConfig> CamViewConfig { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<PreOrder> PreOrder { get; set; }
        public virtual DbSet<HomeTopEmployeeInOut> HomeTopEmployeeInOut { get; set; }
        public virtual DbSet<HomeStatisticPersonInOut> HomeStatisticPersonInOut { get; set; }
        public virtual DbSet<HomeStatisticUnregistered> HomeStatisticUnregistered { get; set; }
        public virtual DbSet<HomePersonInfor> HomePersonInfor { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=.\\SQLEXPRESS;Database=EPS;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<UserPrivilege>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PrivilegeId });
            });

            modelBuilder.Entity<RolePrivilege>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PrivilegeId });
            });
        }
    }
}
