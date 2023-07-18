namespace Model.Models
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Typename { get; set; }

        public virtual ChangeWorkDepartmentRequest? ChangeWorkDepartmentRequest { get; set; }

        public virtual LeaveRequest? LeaveRequest { get; set; }
        public virtual Otrequest? Otrequest { get; set; }
        public virtual ResignationRequest? ResignationRequest { get; set; }
        public virtual TaxRequest? TaxRequest { get; set; }

        public virtual UpdateEmployeeInforRequest? UpdateEmployeeInforRequest { get; set; }

        public RequestDTO() { }

        public RequestDTO(int id, string typename)
        {
            Id = id;
            Typename = typename;
        }

        public RequestDTO(int id, string typename, ChangeWorkDepartmentRequest? changeWorkDepartmentRequest, LeaveRequest? leaveRequest, Otrequest? otrequest, ResignationRequest? resignationRequest, TaxRequest? taxRequest, UpdateEmployeeInforRequest? updateEmployeeInforRequest) : this(id, typename)
        {
            ChangeWorkDepartmentRequest = changeWorkDepartmentRequest;
            LeaveRequest = leaveRequest;
            Otrequest = otrequest;
            ResignationRequest = resignationRequest;
            TaxRequest = taxRequest;
            UpdateEmployeeInforRequest = updateEmployeeInforRequest;
        }
    }
}
