using System.Collections.Generic;
using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public string Path { get; set; }

        public void AddRestorePoint(RestorePoint restorePoint)
        {
        }

        public void ClearRestorePoints(List<int> restorePointsNumbers)
        {
        }
    }
}