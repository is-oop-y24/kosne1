using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.StorageStrategyService
{
    public class SingleStorageStrategy : IStorageStrategy
    {
        public List<Storage> JobObjectsToStorages(List<JobObject> jobObjects)
        {
            return new List<Storage>() { new Storage(jobObjects) };
        }
    }
}