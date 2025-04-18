using HRMS.Management.CommonLayer;

namespace HRMS.Management.Common.Master
{
    public class Department
    {
        public string DepartmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
    public class DepartmentCollectionDataModel : CoreResponse
    {
        public Department[] DepartmentModelList { get; set; }
    }
}
