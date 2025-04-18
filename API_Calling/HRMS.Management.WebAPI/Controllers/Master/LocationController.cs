using HRMS.Management.BusinessAccessLayer.Master;
using HRMS.Management.BusinessAccessLayer.Transaction;
using HRMS.Management.Common.Master;
using HRMS.Management.Common.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Management.WebAPI.Controllers.Master
{
    [Route("api/Masters/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private LocationBL? _locationeBL;

        [HttpGet]
        [Route("getLocationDetails")]
        public LocationCollectionDataModel GetAllLocation()
        {
            _locationeBL = new LocationBL();
            LocationCollectionDataModel LocationModel = _locationeBL.b_GetLocationData();
            return LocationModel;
        }
    }
}
