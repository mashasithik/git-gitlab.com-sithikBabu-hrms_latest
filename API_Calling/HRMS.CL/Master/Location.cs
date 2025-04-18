using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;

namespace HRMS.Management.Common.Master
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class LocationCollectionDataModel : CoreResponse
    {
        public Location[] LocationModelList { get; set; }
    }
}
