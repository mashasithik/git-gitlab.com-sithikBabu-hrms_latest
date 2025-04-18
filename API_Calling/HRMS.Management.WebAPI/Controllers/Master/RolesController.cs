using HRMS.Management.BusinessAccessLayer.Master;
using HRMS.Management.Common.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Management.WebAPI.Controllers.Master
{
    [Route("api/Masters/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private RolesBL _rolesBL;

        [HttpGet]
        [Route("getRolesDetails")]
        public RolesCollectionDataModel GetAllRoles()
        {
            _rolesBL = new RolesBL();
            RolesCollectionDataModel LocationModel = _rolesBL.b_GetRolesData();
            return LocationModel;
        }
    }
}
