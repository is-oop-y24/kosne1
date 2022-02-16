using System.Collections.Generic;
using Backups.Entities.JobStructure;

namespace Backups.Services.StorageStrategyService
{
    public interface IStorageStrategy
    {
        List<Storage> JobObjectsToStorages(List<JobObject> jobObjects);
    }
}