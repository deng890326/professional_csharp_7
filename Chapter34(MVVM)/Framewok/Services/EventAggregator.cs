using System;

namespace Framewok.Services
{
    public class EventAggregator<TEvent>
        where TEvent : EventArgs
    {
        public static EventAggregator<TEvent> Instance { get; }

        static EventAggregator() =>
            Instance = new EventAggregator<TEvent>();

        public event EventHandler<TEvent>? Event;

        public void Publish(object? source, TEvent ev) =>
            Event?.Invoke(source, ev);
    }
}
