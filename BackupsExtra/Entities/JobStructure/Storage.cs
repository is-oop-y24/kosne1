using System.Collections.Generic;
using System.Linq;

namespace BackupsExtra.Entities.JobStructure
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

        public class Snapshot
        {
            public Snapshot()
            {
            }

            public Snapshot(Storage storage)
            {
                JobObjectsSnapshots = storage.jobObjects.Select(jobObject => new JobObject.Snapshot(jobObject)).ToList();
            }

            public List<JobObject.Snapshot> JobObjectsSnapshots { get; set; }

            public Storage Restore()
            {
                return new Storage(JobObjectsSnapshots.Select(jobObject => jobObject.Restore()).ToList());
            }
        }
    }
}