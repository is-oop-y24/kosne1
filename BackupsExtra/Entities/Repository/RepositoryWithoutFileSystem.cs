using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public void AddRestorePoint(RestorePoint restorePoint)
        {
        }
    }
}