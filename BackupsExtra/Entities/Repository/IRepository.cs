using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public interface IRepository
    {
        void AddRestorePoint(RestorePoint restorePoint);
    }
}