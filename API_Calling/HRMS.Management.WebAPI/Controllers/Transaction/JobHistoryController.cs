using HRMS.Management.BusinessAccessLayer.Transaction;
using HRMS.Management.Common.Transaction;
using HRMS.Management.CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Management.WebAPI.Controllers.Transaction
{
    [Route("api/Transaction/[controller]")]
    [ApiController]
    public class JobHistoryController : ControllerBase
    {
        private JobHistoryBL _jobHistoryBL;

        [HttpGet]
        [Route("getJobHistoryDetails")]
        public JobHistoryCollectionDataModel GetJobHistoryDetails(int employeeID)
        {
            _jobHistoryBL = new JobHistoryBL();
            //_paramJobHistory.StartDate = (string.IsNullOrEmpty(_paramJobHistory.StartDate)) ? new DateTime().ToString("yyyyy-mm-dd") : _paramJobHistory.StartDate;
            //_paramJobHistory.EndDate = (string.IsNullOrEmpty(_paramJobHistory.EndDate)) ? new DateTime().ToString("yyyyy-mm-dd") : _paramJobHistory.EndDate;
            JobHistoryCollectionDataModel JobHistoryModel = _jobHistoryBL.b_GetJobHistoryData(employeeID);
            return JobHistoryModel;
        }

        [HttpPost]
        [Route("postJobHistoryDetails")]
        public ICoreResponse PostJobHistoryDetails(JobHistory jobHistory)
        {
            _jobHistoryBL = new JobHistoryBL();
            return _jobHistoryBL.b_PostJobHistoryData(jobHistory);
        }

        [HttpDelete]
        [Route("deleteJobHistoryDetails")]
        public ICoreResponse DeleteJobHistoryDetails(int jobHistoryId)
        {
            _jobHistoryBL = new JobHistoryBL();
            return _jobHistoryBL.b_DeleteRoleHistoryData(jobHistoryId);
        }
    }
}
