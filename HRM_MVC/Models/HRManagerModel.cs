using Model.Models;

namespace HRM_MVC.Models
{
    public class HRManagerModel
    {
        public List<RequestDTO> listRequests { get; set; }
        public string search { get; set; }
    }
}
