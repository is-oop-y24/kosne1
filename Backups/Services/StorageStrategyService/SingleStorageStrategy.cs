using System.Collections.Generic;
using Backups.Entities.JobStructure;

namespace Backups.Services.StorageStrategyService
{
    public class SingleStorageStrategy : IStorageStrategy
    {
        public List<Storage> JobObjectsToStorages(List<JobObject> jobObjects)
        {
            return new List<Storage>() { new Storage(jobObjects) };
        }
    }
}