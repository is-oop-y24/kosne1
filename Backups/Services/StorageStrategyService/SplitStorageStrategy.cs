using System.Collections.Generic;
using System.Linq;
using Backups.Entities.JobStructure;

namespace Backups.Services.StorageStrategyService
{
    public class SplitStorageStrategy : IStorageStrategy
    {
        public List<Storage> JobObjectsToStorages(List<JobObject> jobObjects)
        {
            return jobObjects.Select(jobObject => new Storage(new List<JobObject>() { jobObject })).ToList();
        }
    }
}