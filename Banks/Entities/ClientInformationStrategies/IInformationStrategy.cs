namespace Banks.Entities.ClientInformationStrategies
{
    public interface IInformationStrategy
    {
        void Inform(string contactName, string eventText);
    }
}