using System;
using System.Collections.Generic;
using System.Linq;

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
        }

        private RestorePoint(List<Storage> storages, int number, Guid id)
        {
            this.storages = storages;
            Number = number;
            Id = id;
        }

        public int Number { get; }
        public Guid Id { get; }

        public List<Storage> GetStorages()
        {
            return new List<Storage>(storages);
        }

        public string GetInformation()
        {
            string information = storages.
                Aggregate(string.Empty, (current, storage) => current + ("Storage: " + storage.GetInformation() + ", "));
            return information.TrimEnd(new[] { ',', ' ' });
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
            }

            public List<Storage.Snapshot> StoragesSnapshots { get; set; }
            public int NumberSnapshot { get; set; }
            public Guid IdSnapshot { get; set; }

            public RestorePoint Restore()
            {
                return new RestorePoint(StoragesSnapshots.Select(storage => storage.Restore()).ToList(), NumberSnapshot, IdSnapshot);
            }
        }
    }
}