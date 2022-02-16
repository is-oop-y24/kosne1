using Backups.Entities.JobStructure;

namespace Backups.Entities.Repository
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public void AddRestorePoint(RestorePoint restorePoint)
        {
        }
    }
}