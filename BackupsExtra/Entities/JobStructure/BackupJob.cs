using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.Repository;
using BackupsExtra.Services.ClearRestorePointsStrategyService;
using BackupsExtra.Services.FindRestorePointsStrategyService;
using BackupsExtra.Services.FindRestorePointsStrategyService.Strategies;
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

        private BackupJob(
            List<JobObject> jobObjects,
            List<RestorePoint> restorePoints,
            IRepository repository,
            int indexOfLastRestorePoint,
            IStorageStrategy storageStrategy,
            ILogger logger,
            IFindRestorePointsAbstractStrategy findRestorePoints,
            IClearRestorePointsStrategy clearingRestorePoints)
        {
            this.jobObjects = jobObjects;
            this.restorePoints = restorePoints;
            this.repository = repository;
            this.indexOfLastRestorePoint = indexOfLastRestorePoint;
            StorageStrategy = storageStrategy;
            Logger = logger;
            FindRestorePoints = findRestorePoints;
            ClearingRestorePoints = clearingRestorePoints;
        }

        public IStorageStrategy StorageStrategy { get; set; }
        public ILogger Logger { get; set; }
        public IFindRestorePointsAbstractStrategy FindRestorePoints { get; set; }
        public IClearRestorePointsStrategy ClearingRestorePoints { get; set; }

        public void SetRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return new List<RestorePoint>(restorePoints);
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

        public void Backup(List<JobObject> jobObjects)
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
        }

        public void ClearRestorePoints()
        {
            if (restorePoints.Count == 1)
            {
                Logger.ErrorLogging("It is not possible to clear restore points, because BackupJob contains only one restore point");
                throw new RestorePointException("It is not possible to clear restore points, because BackupJob contains only one restore point");
            }

            Logger.InformationLogging("Starting process of clean restore points in " + GetInformationAboutBackupJob() + "\r\n");
            List<RestorePoint> newRestorePoints = FindRestorePoints.ClearRestorePoints(new List<RestorePoint>(restorePoints));
            if (newRestorePoints.Count == 0)
            {
                Logger.ErrorLogging("It is not possible to delete all points" + "\r\n");
                throw new RestorePointException("Error: all restore points deleted");
            }

            repository.DeleteRestorePoints(restorePoints.Select(restorePoint => restorePoint.Number).ToList());

            restorePoints = ClearingRestorePoints.ClearRestorePoints(restorePoints, newRestorePoints);

            newRestorePoints.ForEach(restorePoint => repository.AddRestorePoint(restorePoint));
            Logger.InformationLogging("Restore points was cleared, " + GetInformationAboutBackupJob() + "\r\n");
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
            return "Repository: { " + repository.PathToRepository + " }";
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
                StorageStrategy = backupJob.StorageStrategy;
                Logger = backupJob.Logger;
                FindRestorePoints = backupJob.FindRestorePoints;
                ClearingRestorePoints = backupJob.ClearingRestorePoints;
            }

            public List<JobObject.Snapshot> JobObjectsSnapshots { get; set; }
            public List<RestorePoint.Snapshot> RestorePointsSnapshots { get; set; }
            public IRepository RepositorySnapshot { get; set; }
            public int IndexOfLastRestorePoint { get; set; }
            public IStorageStrategy StorageStrategy { get; set; }
            public ILogger Logger { get; set; }
            public IFindRestorePointsAbstractStrategy FindRestorePoints { get; set; }
            public IClearRestorePointsStrategy ClearingRestorePoints { get; set; }

            public BackupJob Restore()
            {
                var jobObjects = JobObjectsSnapshots.Select(jobObject => jobObject.Restore()).ToList();
                var restorePoints = RestorePointsSnapshots.Select(restorePoint => restorePoint.Restore()).ToList();
                return new BackupJob(jobObjects, restorePoints, RepositorySnapshot, IndexOfLastRestorePoint, StorageStrategy, Logger, FindRestorePoints, ClearingRestorePoints);
            }
        }
    }
}