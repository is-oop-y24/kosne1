using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Entities.JobStructure;
using BackupsExtra.Tools.SpecificExceptions;

namespace BackupsExtra.Entities.Repository
{
    public class RepositoryWithFileSystem : IRepository
    {
        public RepositoryWithFileSystem(string pathToRepository)
        {
            PathToRepository = pathToRepository;
        }

        public string PathToRepository { get; set; }

        public void AddRestorePoint(RestorePoint restorePoint)
        {
            string pathToRestorePoint = PathToRepository + @"\RestorePoint" + restorePoint.Number;
            Directory.CreateDirectory(pathToRestorePoint);
            int i = 0;
            foreach (Storage storage in restorePoint.GetStorages())
            {
                ++i;
                string pathToArchiveStorage = pathToRestorePoint + @"\ArchiveStorage";
                Directory.CreateDirectory(pathToArchiveStorage);
                foreach (JobObject jobObject in storage.GetJobObjects())
                {
                    string pathToArchiveFile =
                        Path.Combine(
                            pathToArchiveStorage,
                            Path.GetFileName(jobObject.Path) ?? string.Empty);
                    File.Copy(jobObject.Path, pathToArchiveFile, true);
                }

                ZipFile.CreateFromDirectory(pathToArchiveStorage, pathToRestorePoint + @"\Storage" + i + ".zip");
                Directory.Delete(pathToArchiveStorage, true);
            }
        }

        public void DeleteRestorePoints(List<int> restorePointsNumbers)
        {
            foreach (int restorePointNumber in restorePointsNumbers)
            {
                Directory.Delete(PathToRepository + @"\RestorePoint" + restorePointNumber, true);
            }
        }

        public void Restore(RestorePoint restorePoint, string location = null)
        {
            string fullRestorePointPath = PathToRepository + @"\RestorePoint" + restorePoint.Number;
            int i = 0;
            restorePoint.GetStorages().ForEach(storage =>
            {
                i++;
                string storageArchivePath = fullRestorePointPath + @"\Storage" + i + ".zip";
                if (!File.Exists(storageArchivePath)) throw new FileException("Error: File does not exist");

                string temporaryDirectory = fullRestorePointPath + @"\Storage" + i + @"_tmp\";
                ZipFile.ExtractToDirectory(storageArchivePath, temporaryDirectory);
                foreach (string jobObjectFile in Directory.GetFiles(temporaryDirectory))
                {
                    string jobObjectFileName = Path.GetFileNameWithoutExtension(jobObjectFile) + Path.GetExtension(jobObjectFile);
                    string originalFile = !string.IsNullOrWhiteSpace(location) ? location + Path.PathSeparator + jobObjectFileName
                        : storage.GetJobObjects().First(jobObject => Path.GetFileName(jobObject.Path) == jobObjectFileName).Path;

                    File.Copy(jobObjectFile, originalFile, true);
                }

                Directory.Delete(temporaryDirectory, true);
            });
        }
    }
}