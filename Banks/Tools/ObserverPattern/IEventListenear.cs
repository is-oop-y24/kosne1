namespace Banks.Tools.ObserverPattern
{
    public interface IEventListenear
    {
        void ReactToEvent(string eventName);
    }
}