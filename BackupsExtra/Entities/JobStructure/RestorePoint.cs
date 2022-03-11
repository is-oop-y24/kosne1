using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Services.LoggerStrategyService;

namespace BackupsExtra.Entities.JobStructure
{
    public class RestorePoint
    {
        private List<Storage> storages;

        public RestorePoint(List<Storage> storages, int number)
        {
            this.storages = storages;
            Number = number;
            Id = Guid.NewGuid();
            DateTime = DateTime.Now;
        }

        private RestorePoint(List<Storage> storages, int number, Guid id, DateTime dateTime, ILogger logger)
        {
            this.storages = storages;
            Number = number;
            Id = id;
            DateTime = dateTime;
            Logger = logger;
        }

        public ILogger Logger { get; set; }
        public int Number { get; }
        public Guid Id { get; }
        public DateTime DateTime { get; }

        public List<Storage> GetStorages()
        {
            return new List<Storage>(storages);
        }

        public void AddJobObjects(List<JobObject> jobObjects)
        {
            storages.Add(new Storage(jobObjects));
        }

        public void Merge(RestorePoint restorePoint)
        {
            if (storages.Count == 1) return;

            var jobObjects = restorePoint.GetJobObjects().Except(GetJobObjects()).ToList();
            if (jobObjects.Count == 0) return;

            storages.Add(new Storage(jobObjects));
        }

        public string GetInformation()
        {
            string information = "Restore point: { " + storages.
                Aggregate(string.Empty, (current, storage) => current + (storage.GetInformation() + ", "));
            return information.TrimEnd(',', ' ') + " }";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            var other = (RestorePoint)obj;
            return Equals(other);
        }

        public bool Equals(RestorePoint other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(storages, Logger, Number, Id, DateTime);
        }

        private List<JobObject> GetJobObjects()
        {
            var jobObjects = new List<JobObject>();
            foreach (Storage storage in storages)
            {
                jobObjects.AddRange(storage.GetJobObjects());
            }

            return jobObjects;
        }

        public class Snapshot
        {
            public Snapshot()
            {
            }

            public Snapshot(RestorePoint restorePoint)
            {
                StoragesSnapshots = restorePoint.storages.Select(storage => new Storage.Snapshot(storage)).ToList();
                NumberSnapshot = restorePoint.Number;
                IdSnapshot = restorePoint.Id;
                DateTime = restorePoint.DateTime;
                Logger = restorePoint.Logger;
            }

            public List<Storage.Snapshot> StoragesSnapshots { get; set; }
            public int NumberSnapshot { get; set; }
            public Guid IdSnapshot { get; set; }
            public DateTime DateTime { get; set; }
            public ILogger Logger { get; set; }

            public RestorePoint Restore()
            {
                return new RestorePoint(StoragesSnapshots.Select(storage => storage.Restore()).ToList(), NumberSnapshot, IdSnapshot, DateTime, Logger);
            }
        }
    }
}