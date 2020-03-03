using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler {
    public interface IContentJobScheduler {
        Task<bool> CreateJob(ContentJobDto contentJobDto);
        bool DeleteJob(ContentJobDto contentJobDto);
        IEnumerable<ContentJobDto> GetAllJobs();
    }
}