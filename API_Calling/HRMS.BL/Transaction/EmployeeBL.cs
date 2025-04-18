using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;
using HRMS.Management.DataAccessLayer.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Management.BusinessAccessLayer.Transaction
{
    public class EmployeeBL
    {
        private EmployeeData? employeeData;

        //Get all data
        public EmployeesCollectionDataModel b_GetEmployeeData(string name)
        {
            employeeData = new EmployeeData();
            return employeeData.d_GetEmployeedData(name);
        }

        //Update data
        public ICoreResponse b_PostEmployeeData(EmployeesModel employeeInputs)
        {
            employeeData = new EmployeeData();
            return employeeData.d_PostEmployeeData(employeeInputs);
        }

        //Delete data
        public ICoreResponse b_DeleteEmployeeData(int employeeID)
        {
            employeeData = new EmployeeData();
            return employeeData.d_DeleteEmployeeData(employeeID);
        }
    }
}
