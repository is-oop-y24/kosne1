using System.Collections.Generic;
using Backups.Entities.JobStructure;
using Backups.Entities.Repository;
using Backups.Services.StorageStrategyService;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            IRepository repository = new RepositoryWithFileSystem(@"D:\Users\akoli\RiderProjects\kosne1\Backups\backups");
            var backupJob = new BackupJob(repository);
            var jobObjectA = new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\Backups\TestDirectory\FileA.txt");
            var jobObjectB = new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\Backups\TestDirectory\FileB.txt");
            var jobObjectC = new JobObject(@"D:\Users\akoli\RiderProjects\kosne1\Backups\TestDirectory\FileC.txt");
            backupJob.AddJobObject(jobObjectA);
            backupJob.AddJobObject(jobObjectB);
            backupJob.AddJobObject(jobObjectC);
            var jobObjects = new List<JobObject> { jobObjectA, jobObjectB, jobObjectC };
            backupJob.StorageStrategy = new SingleStorageStrategy();
            backupJob.Backup(jobObjects);
        }
    }
}
