using System.Collections.Generic;
using BackupsExtra.Entities.Repository;
using BackupsExtra.Services.StorageStrategyService;

namespace BackupsExtra.Entities.JobStructure
{
    public class BackupJob
    {
        private List<JobObject> jobObjects;
        private List<RestorePoint> restorePoints;
        private IRepository repository;

        public BackupJob(IRepository repository)
        {
            this.repository = repository;
            jobObjects = new List<JobObject>();
            restorePoints = new List<RestorePoint>();
        }

        public IStorageStrategy StorageStrategy { get; set; }

        public void SetRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public JobObject AddJobObject(JobObject jobObject)
        {
            jobObjects.Add(jobObject);
            return jobObject;
        }

        public RestorePoint Backup(List<JobObject> jobObjects)
        {
            List<Storage> storages = StorageStrategy.JobObjectsToStorages(jobObjects);

            var restorePoint = new RestorePoint(storages, restorePoints.Count + 1);

            this.restorePoints.Add(restorePoint);
            repository.AddRestorePoint(restorePoint);
            return restorePoint;
        }
    }
}