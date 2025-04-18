
using HRMS.Management.CommonLayer;

namespace HRMS.Management.Common.Transaction
{
    public class EmployeesModel
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }

    public class EmployeesCollectionDataModel : CoreResponse
    {
        public EmployeesModel[] employeeModelList { get; set; }
    }
}
