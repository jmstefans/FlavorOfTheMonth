using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace FotmServerApp.JobScheduling.Jobs
{
    public class RatingChangeJob: IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
