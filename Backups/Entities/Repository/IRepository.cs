using Backups.Entities.JobStructure;

namespace Backups.Entities.Repository
{
    public interface IRepository
    {
        void AddRestorePoint(RestorePoint restorePoint);
    }
}