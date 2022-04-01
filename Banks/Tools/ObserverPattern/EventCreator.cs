using System.Collections.Generic;

namespace Banks.Tools.ObserverPattern
{
    public class EventCreator<T>
    where T : IEventListenear
    {
        private readonly Dictionary<string, List<T>> listenears;

        public EventCreator()
        {
            listenears = new Dictionary<string, List<T>>();
        }

        public void Listen(string eventName, T listenear)
        {
            if (listenears.ContainsKey(eventName))
            {
                if (!listenears[eventName].Contains(listenear))
                    listenears[eventName].Add(listenear);
            }
            else
            {
                listenears[eventName] = new List<T> { listenear };
            }
        }

        public void Unlisten(string eventName, T listenear)
        {
            if (!listenears.ContainsKey(eventName)) return;
            listenears[eventName].Remove(listenear);
        }

        public void Inform(string eventName)
        {
            if (!listenears.ContainsKey(eventName)) return;
            listenears[eventName].ForEach(l => l.ReactToEvent(eventName));
        }
    }
}