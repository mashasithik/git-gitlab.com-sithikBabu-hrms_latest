using HRMS.Management.Common.Master;
using HRMS.Management.Common.Transaction;
using HRMS.Management.DataAccessLayer.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.BusinessAccessLayer.Master
{
    public class LocationBL
    {
        private LocationData? _locationData;

        public LocationCollectionDataModel b_GetLocationData()
        {
            _locationData =new LocationData();
            return _locationData.d_GetLocationData();
        }
    }
}
