using System.Collections.Generic;

namespace Backups.Entities.JobStructure
{
    public class Storage
    {
        private List<JobObject> jobObjects;

        public Storage(List<JobObject> jobObjects)
        {
            this.jobObjects = jobObjects;
        }

        public List<JobObject> GetJobObjects()
        {
            return new List<JobObject>(jobObjects);
        }
    }
}