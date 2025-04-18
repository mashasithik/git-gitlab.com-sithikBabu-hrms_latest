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
    public class JobHistoryBL
    {
        private JobHistoryData jobHistoryData;

        //Get all data
        public JobHistoryCollectionDataModel b_GetJobHistoryData(int employeeID)
        {
            jobHistoryData = new JobHistoryData();
            return jobHistoryData.d_GetJobHistoryData(employeeID);
        }

        //Post All Data
        public ICoreResponse b_PostJobHistoryData(JobHistory jobHistory)
        {
            jobHistoryData = new JobHistoryData();
            return jobHistoryData.d_PostJobHistoryData(jobHistory);
        }

        //Delete Data
        public ICoreResponse b_DeleteRoleHistoryData(int jobhistoryID)
        {
            jobHistoryData = new JobHistoryData();
            return jobHistoryData.d_DeletJobHistoryData(jobhistoryID);
        }
    }
}
