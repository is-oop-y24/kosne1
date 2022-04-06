using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Services.StorageStrategyService
{
    public interface IStorageStrategy
    {
        List<Storage> JobObjectsToStorages(List<JobObject> jobObjects);
    }
}