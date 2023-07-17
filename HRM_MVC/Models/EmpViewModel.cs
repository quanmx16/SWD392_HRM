using Model.Models;

namespace HRM_MVC.Models
{
    public class EmpViewModel
    {
        public IEnumerable<Employee> listEmp {get; set;}
        public string search { get; set;}
    }
}
