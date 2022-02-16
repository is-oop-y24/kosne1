using System.Collections.Generic;
using Backups.Entities.JobStructure;
using Backups.Entities.Repository;
using Backups.Services.StorageStrategyService;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTests
    {
        private BackupJob backupJob;

        [SetUp]
        public void Setup()
        {
            backupJob = new BackupJob(new RepositoryWithoutFileSystem());
        }
        
        [Test]
        public void AddThreeRestorePoints_CheckingRestorePointsCreatedFiles()
        {
            var jobObjectA = new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\Backups\TestDirectory\FileA.txt");
            var jobObjectB = new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\Backups\TestDirectory\FileB.txt");
            var jobObjectC = new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\Backups\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA);
            backupJob.AddJobObject(jobObjectB);
            backupJob.AddJobObject(jobObjectC);
            var jobObjects = new List<JobObject> { jobObjectA, jobObjectB, jobObjectC };
            backupJob.StorageStrategy = new SingleStorageStrategy();
            RestorePoint restorePoint1 = backupJob.Backup(jobObjects);
            
            var jobObjects1 = new List<JobObject> { jobObjectA, jobObjectB };
            backupJob.StorageStrategy = new SplitStorageStrategy();
            RestorePoint restorePoint2 = backupJob.Backup(jobObjects);
            
            RestorePoint restorePoint3 = backupJob.Backup(jobObjects1);

            bool isRestorePoint1Good = restorePoint1.GetStorages().Count == 1;
            bool isRestorePoint2Good = restorePoint2.GetStorages().Count == 3;
            bool isRestorePoint3Good = restorePoint3.GetStorages().Count == 2;
            
            Assert.IsTrue(isRestorePoint1Good && isRestorePoint2Good && isRestorePoint3Good);
        }
    }
}