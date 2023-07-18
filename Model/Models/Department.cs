namespace Model.Models
{
    public partial class Department
    {
        public Department()
        {
            ChangeWorkDepartmentRequestCurrentDepartments = new HashSet<ChangeWorkDepartmentRequest>();
            ChangeWorkDepartmentRequestDepartmentMoveToes = new HashSet<ChangeWorkDepartmentRequest>();
            Employees = new HashSet<Employee>();
        }

        public string DepartmentId { get; set; } = null!;
        public string? DepartmentName { get; set; }

        public virtual ICollection<ChangeWorkDepartmentRequest> ChangeWorkDepartmentRequestCurrentDepartments { get; set; }
        public virtual ICollection<ChangeWorkDepartmentRequest> ChangeWorkDepartmentRequestDepartmentMoveToes { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
