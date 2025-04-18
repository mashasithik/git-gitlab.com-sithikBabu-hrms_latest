using HRMS.Adecco.Management.BusinessAccessLayer.Master;
using HRMS.Adecco.Management.MSSQLDataAccess;
using HRMS.Management.WebAPI.Controllers.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Adecco.WebAPI.xUnitTest.Fixtures
{
    public class ControllerFixture : IDisposable
    {
        private LocationBL _locationeBL;
        public LocationController locationController { get; private set; }

        private DBMSSQLCommand _DBCommand;
        public ControllerFixture()
        {
            // Create UserService with Memory Database
            _locationeBL = new LocationBL();
            _DBCommand = new DBMSSQLCommand("Server=LAPTOP-T5JC6V3P/SQLEXPRESS;Database=HRMS_Addeco;Integrated Security=SSPI;");
            // Create Controller
            locationController = new LocationController();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ControllerFixture()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _locationeBL = null;
                locationController = null;
            }
        }
    }
}
        
