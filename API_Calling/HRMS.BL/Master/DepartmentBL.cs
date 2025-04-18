using HRMS.Management.Common.Master;
using HRMS.Management.DataAccessLayer.Master;

namespace HRMS.Management.BusinessAccessLayer.Master
{
    public class DepartmentBL
    {
        private DepartmentData _departmentData;

        public DepartmentCollectionDataModel b_GetDepartmentData()
        {
            _departmentData = new DepartmentData();
            return _departmentData.d_GetDepartmentData();
        }
    }
}
