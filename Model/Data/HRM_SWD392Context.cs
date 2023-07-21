using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Models;

namespace Model.Data
{
    public partial class HRM_SWD392Context : DbContext
    {
        public HRM_SWD392Context()
        {
        }

        public HRM_SWD392Context(DbContextOptions<HRM_SWD392Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<ChangeWorkDepartmentRequest> ChangeWorkDepartmentRequests { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<LeaveRequest> LeaveRequests { get; set; } = null!;
        public virtual DbSet<Otrequest> Otrequests { get; set; } = null!;
        public virtual DbSet<ResignationRequest> ResignationRequests { get; set; } = null!;
        public virtual DbSet<TaxRequest> TaxRequests { get; set; } = null!;
        public virtual DbSet<UpdateAttendanceRequest> UpdateAttendanceRequests { get; set; } = null!;
        public virtual DbSet<UpdateEmployeeInforRequest> UpdateEmployeeInforRequests { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseSqlServer("Server=(local);Database=HRM_SWD392;Trusted_Connection=True;User Id=sa;Password=12345;");
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }
        private string GetConnectionString()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config["ConnectionStrings:HRM_SWD392"];
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("Attendance");

                entity.Property(e => e.AttendanceDate).HasColumnType("date");

                entity.Property(e => e.CheckInTime).HasColumnType("datetime");

                entity.Property(e => e.CheckOutTime).HasColumnType("datetime");

                entity.Property(e => e.DayIncome).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EmployeeId).HasMaxLength(10);

                entity.Property(e => e.OttimeIn)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTimeIn");

                entity.Property(e => e.OttimeOut)
                    .HasColumnType("datetime")
                    .HasColumnName("OTTimeOut");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_Employee");
            });

            modelBuilder.Entity<ChangeWorkDepartmentRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("ChangeWorkDepartmentRequest");

                entity.Property(e => e.CurrentDepartmentId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DateCreateRequest).HasColumnType("datetime");

                entity.Property(e => e.DateMoveToNewDepartment).HasColumnType("date");

                entity.Property(e => e.DateResponeRequest).HasColumnType("datetime");

                entity.Property(e => e.DateWantToMove).HasColumnType("date");

                entity.Property(e => e.DepartmentMoveToId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Hrid)
                    .HasMaxLength(10)
                    .HasColumnName("HRID")
                    .IsFixedLength();

                entity.Property(e => e.ReasonForMoving).HasMaxLength(300);

                entity.HasOne(d => d.CurrentDepartment)
                    .WithMany(p => p.ChangeWorkDepartmentRequestCurrentDepartments)
                    .HasForeignKey(d => d.CurrentDepartmentId)
                    .HasConstraintName("FK_ChangeWorkDepartmentRequest_Department1");

                entity.HasOne(d => d.DepartmentMoveTo)
                    .WithMany(p => p.ChangeWorkDepartmentRequestDepartmentMoveToes)
                    .HasForeignKey(d => d.DepartmentMoveToId)
                    .HasConstraintName("FK_ChangeWorkDepartmentRequest_Department");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ChangeWorkDepartmentRequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChangeWorkDepartmentRequest_Employee");

                entity.HasOne(d => d.Hr)
                    .WithMany(p => p.ChangeWorkDepartmentRequestHrs)
                    .HasForeignKey(d => d.Hrid)
                    .HasConstraintName("FK_ChangeWorkDepartmentRequest_Employee1");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.DepartmentName).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DayOne).HasColumnType("date");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.EmplyeeName).HasMaxLength(100);

                entity.Property(e => e.LastDay).HasColumnType("date");

                entity.Property(e => e.Level)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<LeaveRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("LeaveRequest");

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.DateOff).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Hrid)
                    .HasMaxLength(10)
                    .HasColumnName("HRID")
                    .IsFixedLength();

                entity.Property(e => e.Reason).HasMaxLength(300);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveRequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveRequest_Employee");

                entity.HasOne(d => d.Hr)
                    .WithMany(p => p.LeaveRequestHrs)
                    .HasForeignKey(d => d.Hrid)
                    .HasConstraintName("FK_LeaveRequest_Employee1");
            });

            modelBuilder.Entity<Otrequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("OTRequest");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproverId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.DateRequest).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RequestStatus).HasMaxLength(20);

                entity.Property(e => e.TaskOt)
                    .HasMaxLength(50)
                    .HasColumnName("TaskOT");

                entity.Property(e => e.TimeEndOt)
                    .HasColumnType("datetime")
                    .HasColumnName("TimeEndOT");

                entity.Property(e => e.TimeStartOt)
                    .HasColumnType("datetime")
                    .HasColumnName("TimeStartOT");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.OtrequestApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_OTRequest_Employee1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.OtrequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OTRequest_Employee");
            });

            modelBuilder.Entity<ResignationRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("ResignationRequest");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproverId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Comment).HasMaxLength(100);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LastWorkingDate).HasColumnType("date");

                entity.Property(e => e.Reason).HasMaxLength(300);

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestStatus).HasMaxLength(20);

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.ResignationRequestApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_ResignationRequest_Employee1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ResignationRequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ResignationRequest_Employee");
            });

            modelBuilder.Entity<TaxRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("TaxRequest");

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproverId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.Deductions).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestStatus).HasMaxLength(20);

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TaxableIncome).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalIncome).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.TaxRequestApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_TaxRequest_Employee1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TaxRequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaxRequest_Employee");
            });

            modelBuilder.Entity<UpdateAttendanceRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("UpdateAttendanceRequest");

                entity.Property(e => e.RequestId).ValueGeneratedNever();

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Hrid)
                    .HasMaxLength(10)
                    .HasColumnName("HRID")
                    .IsFixedLength();

                entity.Property(e => e.Reason).HasMaxLength(300);

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.TimeIn).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.UpdateAttendanceRequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UpdateAtt__EmployeeID");

                entity.HasOne(d => d.Hr)
                    .WithMany(p => p.UpdateAttendanceRequestHrs)
                    .HasForeignKey(d => d.Hrid)
                    .HasConstraintName("FK__UpdateAtt__HRID");
            });

            modelBuilder.Entity<UpdateEmployeeInforRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("UpdateEmployeeInforRequest");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.ApproveDate).HasColumnType("datetime");

                entity.Property(e => e.ApproverId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Comment).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DayOne).HasColumnType("date");

                entity.Property(e => e.DepartmentId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.EmplyeeName).HasMaxLength(100);

                entity.Property(e => e.LastDay).HasColumnType("date");

                entity.Property(e => e.Level)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Status).HasMaxLength(20);

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.Approver)
                    .WithMany(p => p.UpdateEmployeeInforRequestApprovers)
                    .HasForeignKey(d => d.ApproverId)
                    .HasConstraintName("FK_UpdateEmployeeInforRequest_Employee1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.UpdateEmployeeInforRequestEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UpdateEmployeeInforRequest_Employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
