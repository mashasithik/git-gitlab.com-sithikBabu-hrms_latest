using HRMS.Management.CommonLayer;

namespace HRMS.Management.Common.Master
{
    public class Roles
    {
        public int RoleID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
    public class RolesCollectionDataModel : CoreResponse
    {
        public Roles[] RolesModelList { get; set; }
    }
}
