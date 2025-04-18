using HRMS.Management.BusinessAccessLayer.Master;
using HRMS.Management.Common.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Management.WebAPI.Controllers.Master
{
    [Route("api/Masters/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private DepartmentBL departmentBL;

        [HttpGet]
        [Route("getDepartmentDetails")]
        public DepartmentCollectionDataModel GetAllRoles()
        {
            departmentBL = new DepartmentBL();
            DepartmentCollectionDataModel LocationModel = departmentBL.b_GetDepartmentData();
            return LocationModel;
        }
    }
}
