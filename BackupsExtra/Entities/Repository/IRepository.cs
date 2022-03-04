using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public interface IRepository
    {
        public string Path { get; set; }
        void AddRestorePoint(RestorePoint restorePoint);
    }
}