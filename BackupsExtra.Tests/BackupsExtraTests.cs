using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Entities.JobStructure;
using BackupsExtra.Entities.Repository;
using BackupsExtra.Services.ClearRestorePointsStrategyService;
using BackupsExtra.Services.FindRestorePointsStrategyService;
using BackupsExtra.Services.FindRestorePointsStrategyService.Strategies;
using BackupsExtra.Services.LoggerStrategyService;
using BackupsExtra.Services.LoggerStrategyService.TimeStrategyService;
using BackupsExtra.Services.StorageStrategyService;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class BackupsExtraTests
    {
        [Test]
        public void DeleteTwoRestorePointsByDate_Success()
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem())
            {
                Logger = new ConsoleLogger()
                {
                    TimeStrategy = new WithoutTime(),
                },
                StorageStrategy = new SplitStorageStrategy(),
                FindRestorePoints = new FindRestorePointsByIntersection(),
                ClearingRestorePoints = new DeleteRestorePoints(),
            };
            
            var jobObjectA =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileA.txt");
            var jobObjectB =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileB.txt");
            var jobObjectC =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA).AddJobObject(jobObjectB).AddJobObject(jobObjectC);
            
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB});
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByDate(DateTime.Now));
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB,jobObjectC});
            
            backupJob.ClearRestorePoints();
            
            Assert.IsTrue(backupJob.GetRestorePoints().Count.Equals(1));
        }

        [Test]
        public void DeleteTwoRestorePointsByNumber_Success()
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem())
            {
                Logger = new ConsoleLogger()
                {
                    TimeStrategy = new WithoutTime(),
                },
                StorageStrategy = new SplitStorageStrategy(),
                FindRestorePoints = new FindRestorePointsByIntersection(),
                ClearingRestorePoints = new DeleteRestorePoints(),
            };
            
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByNumber(1));
            
            var jobObjectA =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileA.txt");
            var jobObjectB =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileB.txt");
            var jobObjectC =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA).AddJobObject(jobObjectB).AddJobObject(jobObjectC);
            
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB});
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB,jobObjectC});
            
            backupJob.ClearRestorePoints();
            
            Assert.IsTrue(backupJob.GetRestorePoints().Count.Equals(1));
        }

        [Test]
        public void DeleteTwoRestorePointsByDateAndNumber_Success()
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem())
            {
                Logger = new ConsoleLogger()
                {
                    TimeStrategy = new WithoutTime(),
                },
                StorageStrategy = new SplitStorageStrategy(),
                FindRestorePoints = new FindRestorePointsByIntersection(),
                ClearingRestorePoints = new DeleteRestorePoints(),
            };
            
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByNumber(1));
            
            var jobObjectA =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileA.txt");
            var jobObjectB =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileB.txt");
            var jobObjectC =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA).AddJobObject(jobObjectB).AddJobObject(jobObjectC);
            
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB});
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByDate(DateTime.Now));
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB,jobObjectC});
            
            backupJob.ClearRestorePoints();
            
            Assert.IsTrue(backupJob.GetRestorePoints().Count.Equals(1));
        }
        
        [Test]
        public void DeleteTwoRestorePointsByDateOrNumber_Success()
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem())
            {
                Logger = new ConsoleLogger()
                {
                    TimeStrategy = new WithoutTime(),
                },
                StorageStrategy = new SplitStorageStrategy(),
                FindRestorePoints = new FindRestorePointsByUnion(),
                ClearingRestorePoints = new DeleteRestorePoints(),
            };
            
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByNumber(1));
            
            var jobObjectA =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileA.txt");
            var jobObjectB =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileB.txt");
            var jobObjectC =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA).AddJobObject(jobObjectB).AddJobObject(jobObjectC);
            
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB});
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByDate(DateTime.Now));
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB,jobObjectC});
            
            backupJob.ClearRestorePoints();
            
            Assert.IsTrue(backupJob.GetRestorePoints().Count.Equals(1));
        }

        [Test]
        public void MergeThreeRestorePointsInOneRestorePoint_FileAMerged()
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem())
            {
                Logger = new ConsoleLogger()
                {
                    TimeStrategy = new WithoutTime(),
                },
                StorageStrategy = new SplitStorageStrategy(),
                FindRestorePoints = new FindRestorePointsByUnion(),
                ClearingRestorePoints = new DeleteRestorePoints(),
            };
            
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByNumber(1));
            
            var jobObjectA =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileA.txt");
            var jobObjectB =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileB.txt");
            var jobObjectC =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA).AddJobObject(jobObjectB).AddJobObject(jobObjectC);
            
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB});
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByDate(DateTime.Now));
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            
            backupJob.ClearRestorePoints();
            
            Assert.IsTrue(backupJob.GetRestorePoints().Count.Equals(1));
            Assert.IsTrue(backupJob.GetRestorePoints().First().GetStorages().Any(storage => storage.GetJobObjects().First().Id == jobObjectA.Id));
        }

        [Test]
        public void MergeThreeRestorePointsInOneRestorePoint_FirstAndSecondRestorePointsDeleted()
        {
            var backupJob = new BackupJob(new RepositoryWithoutFileSystem())
            {
                Logger = new ConsoleLogger()
                {
                    TimeStrategy = new WithoutTime(),
                },
                StorageStrategy = new SingleStorageStrategy(),
                FindRestorePoints = new FindRestorePointsByUnion(),
                ClearingRestorePoints = new DeleteRestorePoints(),
            };
            
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByNumber(1));
            
            var jobObjectA =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileA.txt");
            var jobObjectB =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileB.txt");
            var jobObjectC =
                new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\BackupsExtra\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA).AddJobObject(jobObjectB).AddJobObject(jobObjectC);
            
            backupJob.Backup(new List<JobObject> {jobObjectA,jobObjectB});
            backupJob.FindRestorePoints.AddFindRestorePointStrategy(new FindRestorePointsByDate(DateTime.Now));
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            backupJob.Backup(new List<JobObject> {jobObjectB,jobObjectC});
            
            backupJob.ClearRestorePoints();
            
            Assert.IsTrue(backupJob.GetRestorePoints().Count.Equals(1));
            Assert.IsTrue(backupJob.GetRestorePoints().First().GetStorages().Count.Equals(1));
        }
    }
}