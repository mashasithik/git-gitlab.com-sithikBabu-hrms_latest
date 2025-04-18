using HRMS.Management.CommonLayer;

namespace HRMS.Management.Common.Transaction
{
    public class JobHistory
    {
        public int JobHistoryID { get; set; } = 0;
        public int EmployeeID { get; set; } = 0;
        public string EmployeeName { get; set; } = "";
        public int ManagerID { get; set; } = 0;
        public string ManagerName { get; set; } = "";
        public int RoleID { get; set; } = 0;
        public string RoleName { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public string Status { get; set; } = "";
        public string Comments { get; set; } = "";
    }
    public class JobHistoryCollectionDataModel : CoreResponse
    {
        public JobHistory[] JobHistoryModelList { get; set; }
    }
}
