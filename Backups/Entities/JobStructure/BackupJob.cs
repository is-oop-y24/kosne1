using System.Collections.Generic;

namespace Backups.Entities.JobStructure
{
    public class BackupJob
    {
        private List<JobObject> jobObjects;
        private List<RestorePoint> restorePoints;

        public BackupJob()
        {
            jobObjects = new List<JobObject>();
            restorePoints = new List<RestorePoint>();
        }

        public JobObject AddJobObject(JobObject jobObject)
        {
            jobObjects.Add(jobObject);
            return jobObject;
        }
    }
}