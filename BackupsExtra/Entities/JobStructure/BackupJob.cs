using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.Repository;
using BackupsExtra.Services.LoggerStrategyService;
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

        private BackupJob(List<JobObject> jobObjects, List<RestorePoint> restorePoints, IRepository repository)
        {
            this.jobObjects = jobObjects;
            this.restorePoints = restorePoints;
            this.repository = repository;
        }

        public IStorageStrategy StorageStrategy { get; set; }
        public ILogger Logger { get; set; }

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

        public string GetInformationAboutJobObjects()
        {
            string information = "Job objects: { " + jobObjects.Aggregate(
                string.Empty,
                (current, jobObject) => current + (jobObject.GetInformation() + ", "));
            return information.TrimEnd(',', ' ') + " }";
        }

        public string GetInformationAboutRestorePoints()
        {
            string information = "Restore points: { " + restorePoints.Aggregate(
                string.Empty,
                (current, restorePoint) => current + (restorePoint.GetInformation() + ", "));
            return information.TrimEnd(',', ' ') + " }";
        }

        public string GetInformationAboutRepository()
        {
            return "Repository: { " + repository.Path + " }";
        }

        public string GetInformationAboutBackupJob()
        {
            return GetInformationAboutJobObjects() + "; " + GetInformationAboutRestorePoints() + "; " +
                   GetInformationAboutRepository();
        }

        public class Snapshot
        {
            public Snapshot()
            {
            }

            public Snapshot(BackupJob backupJob)
            {
                JobObjectsSnapshots =
                    backupJob.jobObjects.Select(jobObject => new JobObject.Snapshot(jobObject)).ToList();
                RestorePointsSnapshots = backupJob.restorePoints
                    .Select(restorePoint => new RestorePoint.Snapshot(restorePoint)).ToList();
                RepositorySnapshot = backupJob.repository;
            }

            public List<JobObject.Snapshot> JobObjectsSnapshots { get; set; }
            public List<RestorePoint.Snapshot> RestorePointsSnapshots { get; set; }
            public IRepository RepositorySnapshot { get; set; }

            public BackupJob Restore()
            {
                var jobObjects = JobObjectsSnapshots.Select(jobObject => jobObject.Restore()).ToList();
                var restorePoints = RestorePointsSnapshots.Select(restorePoint => restorePoint.Restore()).ToList();
                IRepository repository = RepositorySnapshot;
                return new BackupJob(jobObjects, restorePoints, repository);
            }
        }
    }
}