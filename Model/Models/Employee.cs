using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Attendances = new HashSet<Attendance>();
            ChangeWorkDepartmentRequestEmployees = new HashSet<ChangeWorkDepartmentRequest>();
            ChangeWorkDepartmentRequestHrs = new HashSet<ChangeWorkDepartmentRequest>();
            InverseManager = new HashSet<Employee>();
            LeaveRequestEmployees = new HashSet<LeaveRequest>();
            LeaveRequestHrs = new HashSet<LeaveRequest>();
            OtrequestApprovers = new HashSet<Otrequest>();
            OtrequestEmployees = new HashSet<Otrequest>();
            ResignationRequestApprovers = new HashSet<ResignationRequest>();
            ResignationRequestEmployees = new HashSet<ResignationRequest>();
            TaxRequestApprovers = new HashSet<TaxRequest>();
            TaxRequestEmployees = new HashSet<TaxRequest>();
            UpdateAttendanceRequestEmployees = new HashSet<UpdateAttendanceRequest>();
            UpdateAttendanceRequestHrs = new HashSet<UpdateAttendanceRequest>();
            UpdateEmployeeInforRequestApprovers = new HashSet<UpdateEmployeeInforRequest>();
            UpdateEmployeeInforRequestEmployees = new HashSet<UpdateEmployeeInforRequest>();
        }
        public string EmployeeId { get; set; } = null!;
        [MinLength(10)]
        [Required(ErrorMessage = " Please enter this field")]
        public string? EmplyeeName { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@+[a-z0-9.-]+.+[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? DepartmentId { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Please enter a valid phone number starting with '0' and having 10 digits")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        public string? Address { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        public decimal? Salary { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        public string? TaxCode { get; set; }
        [Required(ErrorMessage = " Please enter this field")]
        public string? Level { get; set; }
        public string? ManagerId { get; set; }
        public DateTime? DayOne { get; set; }
        public DateTime? LastDay { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Employee? Manager { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<ChangeWorkDepartmentRequest> ChangeWorkDepartmentRequestEmployees { get; set; }
        public virtual ICollection<ChangeWorkDepartmentRequest> ChangeWorkDepartmentRequestHrs { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequestEmployees { get; set; }
        public virtual ICollection<LeaveRequest> LeaveRequestHrs { get; set; }
        public virtual ICollection<Otrequest> OtrequestApprovers { get; set; }
        public virtual ICollection<Otrequest> OtrequestEmployees { get; set; }
        public virtual ICollection<ResignationRequest> ResignationRequestApprovers { get; set; }
        public virtual ICollection<ResignationRequest> ResignationRequestEmployees { get; set; }
        public virtual ICollection<TaxRequest> TaxRequestApprovers { get; set; }
        public virtual ICollection<TaxRequest> TaxRequestEmployees { get; set; }
        public virtual ICollection<UpdateAttendanceRequest> UpdateAttendanceRequestEmployees { get; set; }
        public virtual ICollection<UpdateAttendanceRequest> UpdateAttendanceRequestHrs { get; set; }
        public virtual ICollection<UpdateEmployeeInforRequest> UpdateEmployeeInforRequestApprovers { get; set; }
        public virtual ICollection<UpdateEmployeeInforRequest> UpdateEmployeeInforRequestEmployees { get; set; }
    }
}
