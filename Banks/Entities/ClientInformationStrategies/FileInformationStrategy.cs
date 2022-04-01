using System.IO;

namespace Banks.Entities.ClientInformationStrategies
{
    public class FileInformationStrategy : IInformationStrategy
    {
        public FileInformationStrategy(string filepath)
        {
            Filepath = filepath;
        }

        public string Filepath { get; }

        public void Inform(string contactName, string eventText)
        {
            var text = $"{contactName}, {eventText}";
            File.WriteAllText(Filepath, text);
        }
    }
}