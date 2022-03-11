using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public string PathToRepository { get; set; }

        public void AddRestorePoint(RestorePoint restorePoint)
        {
        }

        public void DeleteRestorePoints(List<int> restorePointsNumbers)
        {
        }
    }
}