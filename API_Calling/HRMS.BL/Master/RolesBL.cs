using HRMS.Management.Common.Master;
using HRMS.Management.DataAccessLayer.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.BusinessAccessLayer.Master
{
    public class RolesBL
    {
        private RolesData _rolesData;

        public RolesCollectionDataModel b_GetRolesData()
        {
            _rolesData = new RolesData();
            return _rolesData.d_GetRolesData();
        }
    }
}
