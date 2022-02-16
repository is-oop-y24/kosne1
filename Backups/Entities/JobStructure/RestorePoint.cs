using System;
using System.Collections.Generic;

namespace Backups.Entities.JobStructure
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

        public int Number { get; }
        public Guid Id { get; }

        public List<Storage> GetStorages()
        {
            return new List<Storage>(storages);
        }
    }
}