namespace Banks.Entities.ClientInformationStrategies
{
    public class TestInformationStrategy : IInformationStrategy
    {
        public string LastNotification { get; set; }
        public void Inform(string contactName, string eventText)
        {
            LastNotification = $"{contactName}";
        }
    }
}