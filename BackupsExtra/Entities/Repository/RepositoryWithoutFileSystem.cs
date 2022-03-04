using BackupsExtra.Entities.JobStructure;

namespace BackupsExtra.Entities.Repository
{
    public class RepositoryWithoutFileSystem : IRepository
    {
        public string Path { get; set; }

        public void AddRestorePoint(RestorePoint restorePoint)
        {
        }
    }
}