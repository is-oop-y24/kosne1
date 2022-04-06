using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Services.LoggerStrategyService;

namespace BackupsExtra.Entities.JobStructure
{
    public class Storage
    {
        private List<JobObject> jobObjects;

        public Storage(List<JobObject> jobObjects)
        {
            this.jobObjects = jobObjects;
        }

        public ILogger Logger { get; set; }

        public List<JobObject> GetJobObjects()
        {
            return new List<JobObject>(jobObjects);
        }

        public string GetInformation()
        {
            string information = "Storage: { " + jobObjects.
                Aggregate(string.Empty, (current, jobObject) => current + (jobObject.GetInformation() + ", "));
            return information.TrimEnd(',', ' ') + " }";
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