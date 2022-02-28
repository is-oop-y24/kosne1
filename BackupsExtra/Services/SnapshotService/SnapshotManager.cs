using System.IO;
using BackupsExtra.Entities.JobStructure;
using Newtonsoft.Json;

namespace BackupsExtra.Services.SnapshotService
{
    public class SnapshotManager
    {
        private string pathToSnapshotJson;

        public SnapshotManager(string pathToSnapshotJson)
        {
            this.pathToSnapshotJson = pathToSnapshotJson;
        }

        public void Save(BackupJob backupJob)
        {
            var snapshot = new BackupJob.Snapshot(backupJob);
            var setting = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            };
            string snapshotJson = JsonConvert.SerializeObject(snapshot, setting);

            File.WriteAllText(pathToSnapshotJson, snapshotJson);
        }

        public BackupJob Restore()
        {
            StreamReader sr = File.OpenText(pathToSnapshotJson);
            string snapshotJson = sr.ReadLine();
            sr.Close();

            var setting = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            };
            BackupJob.Snapshot snapshot = JsonConvert.DeserializeObject<BackupJob.Snapshot>(snapshotJson, setting);
            return snapshot.Restore();
        }
    }
}