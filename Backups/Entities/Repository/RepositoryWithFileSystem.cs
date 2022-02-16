﻿using System.IO;
using System.IO.Compression;
using Backups.Entities.JobStructure;

namespace Backups.Entities.Repository
{
    public class RepositoryWithFileSystem : IRepository
    {
        public RepositoryWithFileSystem(string path)
        {
            Path = path;
        }

        public string Path { get; }

        public void AddRestorePoint(RestorePoint restorePoint)
        {
            string pathToRestorePoint = Path + @"\RestorePoint" + restorePoint.Number;
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
                        System.IO.Path.Combine(
                            pathToArchiveStorage,
                            System.IO.Path.GetFileNameWithoutExtension(jobObject.Path) + "_" + restorePoint.Number + System.IO.Path.GetExtension(jobObject.Path) ?? string.Empty);
                    File.Copy(jobObject.Path, pathToArchiveFile, true);
                }

                ZipFile.CreateFromDirectory(pathToArchiveStorage, pathToRestorePoint + @"\Storage" + i + ".zip");
                Directory.Delete(pathToArchiveStorage, true);
            }
        }
    }
}