using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public interface IRepository
    {
        public string PathToRepository { get; set; }
        void AddRestorePoint(RestorePoint restorePoint);
        void DeleteRestorePoints(List<int> restorePointsNumbers);
        void Restore(RestorePoint restorePoint, string location = null);
    }
}