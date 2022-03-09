using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.Repository;
using BackupsExtra.Services.FindRestorePointsStrategyService;
using BackupsExtra.Services.LoggerStrategyService;
using BackupsExtra.Services.StorageStrategyService;
using BackupsExtra.Tools.SpecificExceptions;

namespace BackupsExtra.Entities.JobStructure
{
    public class BackupJob
    {
        private List<JobObject> jobObjects;
        private List<RestorePoint> restorePoints;
        private IRepository repository;
        private int indexOfLastRestorePoint;

        public BackupJob(IRepository repository)
        {
            indexOfLastRestorePoint = 0;
            this.repository = repository;
            jobObjects = new List<JobObject>();
            restorePoints = new List<RestorePoint>();
        }

        private BackupJob(List<JobObject> jobObjects, List<RestorePoint> restorePoints, IRepository repository, int indexOfLastRestorePoint)
        {
            this.jobObjects = jobObjects;
            this.restorePoints = restorePoints;
            this.repository = repository;
            this.indexOfLastRestorePoint = indexOfLastRestorePoint;
        }

        public IStorageStrategy StorageStrategy { get; set; }
        public ILogger Logger { get; set; }
        public IFindRestorePointsStrategy FindRestorePoints { get; set; }

        public void SetRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public BackupJob AddJobObject(JobObject jobObject)
        {
            Logger.InformationLogging("Adding an " + jobObject.GetInformation() + " to " +
                                      GetInformationAboutBackupJob() + "\r\n");
            if (FindJobObject(jobObject.Id))
            {
                Logger.WarningLogging("This backup job: " + GetInformationAboutBackupJob() +
                                      " already have this job object " + jobObject.GetInformation() + "\r\n");
            }

            jobObjects.Add(jobObject);
            Logger.InformationLogging("Job object added to " + GetInformationAboutBackupJob() + "\r\n");
            return this;
        }

        public RestorePoint Backup(List<JobObject> jobObjects)
        {
            if (jobObjects.Count == 0)
            {
                Logger.WarningLogging("List of job objects is empty" + "\r\n");
            }

            string information = "Job objects: { " + jobObjects.Aggregate(
                string.Empty,
                (current, jobObject) => current + (jobObject.GetInformation() + ", ")) + " }";
            Logger.InformationLogging("Backup process with " + information + " in " + GetInformationAboutBackupJob() + "\r\n");

            List<Storage> storages = StorageStrategy.JobObjectsToStorages(jobObjects);

            var restorePoint = new RestorePoint(storages, indexOfLastRestorePoint);
            ++indexOfLastRestorePoint;
            Logger.InformationLogging("Created " + restorePoint.GetInformation() + "\r\n");

            restorePoints.Add(restorePoint);
            repository.AddRestorePoint(restorePoint);
            Logger.InformationLogging("Backup process finished, " + GetInformationAboutBackupJob() + "\r\n");
            return restorePoint;
        }

        public void ClearRestorePoints()
        {
            Logger.InformationLogging("Starting process of clean restore points in " + GetInformationAboutBackupJob() + "\r\n");
            List<RestorePoint> restorePoints = FindRestorePoints.ClearRestorePoints(new List<RestorePoint>(this.restorePoints));
            if (restorePoints.Count == 0)
            {
                Logger.ErrorLogging("It is not possible to delete all points" + "\r\n");
                throw new RestorePointException("Error: all restore points deleted");
            }

            var restorePointsNumbers = new List<int>(this.restorePoints.Select(restorePoint => restorePoint.Number)
                .Except(restorePoints.Select(restorePoint => restorePoint.Number)));
            repository.ClearRestorePoints(restorePointsNumbers);
            this.restorePoints = restorePoints;
            Logger.InformationLogging("Restore points cleared, " + GetInformationAboutBackupJob() + "\r\n");
        }

        private string GetInformationAboutJobObjects()
        {
            if (jobObjects.Count == 0)
            {
                return "Backup job contains 0 job objects";
            }

            string information = "Job objects: { " + jobObjects.Aggregate(
                string.Empty,
                (current, jobObject) => current + (jobObject.GetInformation() + ", "));
            return information.TrimEnd(',', ' ') + " }";
        }

        private string GetInformationAboutRestorePoints()
        {
            if (restorePoints.Count == 0)
            {
                return "Backup job contains 0 restore points";
            }

            string information = "Restore points: { " + restorePoints.Aggregate(
                string.Empty,
                (current, restorePoint) => current + (restorePoint.GetInformation() + ", "));
            return information.TrimEnd(',', ' ') + " }";
        }

        private string GetInformationAboutRepository()
        {
            return "Repository: { " + repository.Path + " }";
        }

        private string GetInformationAboutBackupJob()
        {
            return "Backup job: { " + GetInformationAboutJobObjects() + "; " + GetInformationAboutRestorePoints() + "; " +
                   GetInformationAboutRepository() + " }";
        }

        private bool FindJobObject(Guid id)
        {
            return jobObjects.Any(jobObject => Equals(jobObject.Id, id));
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
                IndexOfLastRestorePoint = backupJob.indexOfLastRestorePoint;
            }

            public List<JobObject.Snapshot> JobObjectsSnapshots { get; set; }
            public List<RestorePoint.Snapshot> RestorePointsSnapshots { get; set; }
            public IRepository RepositorySnapshot { get; set; }
            public int IndexOfLastRestorePoint { get; set; }

            public BackupJob Restore()
            {
                var jobObjects = JobObjectsSnapshots.Select(jobObject => jobObject.Restore()).ToList();
                var restorePoints = RestorePointsSnapshots.Select(restorePoint => restorePoint.Restore()).ToList();
                IRepository repository = RepositorySnapshot;
                return new BackupJob(jobObjects, restorePoints, repository, IndexOfLastRestorePoint);
            }
        }
    }
}