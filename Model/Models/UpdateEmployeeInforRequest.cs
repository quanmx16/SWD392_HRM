using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class UpdateEmployeeInforRequest
    {
        public int RequestId { get; set; }
        public string EmployeeId { get; set; } = null!;
        public string? EmplyeeName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? DepartmentId { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public decimal? Salary { get; set; }
        public string? TaxCode { get; set; }
        public string? Level { get; set; }
        public string? ManagerId { get; set; }
        public DateTime? DayOne { get; set; }
        public DateTime? LastDay { get; set; }
        public string? ApproverId { get; set; }
        public string? Status { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string? Comment { get; set; }

        public virtual Employee? Approver { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
}
