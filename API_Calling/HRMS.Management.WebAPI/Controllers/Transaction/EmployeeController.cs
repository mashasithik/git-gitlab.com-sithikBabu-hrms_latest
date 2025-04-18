using HRMS.Management.BusinessAccessLayer.Transaction;
using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Management.WebAPI.Controllers.Transaction
{
    [Route("api/Transaction/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeBL? _employeeBL;

        [HttpGet]
        [Route("getEmployeeDetails")]
        public EmployeesCollectionDataModel GetAllEmployee(string name = "")
        {
            _employeeBL = new EmployeeBL();
            EmployeesCollectionDataModel EmployeeModel = _employeeBL.b_GetEmployeeData(name);
            return EmployeeModel;
        }

        [HttpPost]
        [Route("postEmployeeDetails")]
        public ICoreResponse PostEmployee(EmployeesModel employeeData)
        {
            _employeeBL = new EmployeeBL();
            return _employeeBL.b_PostEmployeeData(employeeData);
        }

        [HttpDelete]
        [Route("deleteEmployeeDetails")]
        public ICoreResponse DeleteEmployee(int employeeID)
        {
            _employeeBL = new EmployeeBL();
            return _employeeBL.b_DeleteEmployeeData(employeeID);
        }
    }
}
