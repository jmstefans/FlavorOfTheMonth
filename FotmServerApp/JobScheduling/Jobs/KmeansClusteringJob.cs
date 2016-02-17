using System;
using Quartz;

namespace Fotm.Server.JobScheduling.Jobs
{
    public class KmeansClusteringJob : IJob
    {
        #region Members

        private DateTime _startDate;
        private DateTime _endDate;

        #endregion

        #region Constructor

        public KmeansClusteringJob(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        #endregion

        #region IJob

        public void Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
     
        #endregion
    }
}
