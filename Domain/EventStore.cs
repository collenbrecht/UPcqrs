using System.Collections.Generic;
using System.Linq;
using Domain.Commands;
using Domain.Events;

namespace Domain
{
    public delegate void Subscriber(IEvent @event);

    public delegate void CommandHandler(ICommand command);

    public interface EventStore
    {
        void SubscribeToAll(Subscriber subscriber);
        void AppendToStream(string streamId, IList<IEvent> events);
        List<IEvent> ReadStreamForward(string streamId);
    }

    public class MemoryEventStore : EventStore
    {
        public IEnumerable<IEvent> Events { get; set; } = new List<IEvent>();
        public IEnumerable<IEvent> EventsToPublish { get; set; } = new List<IEvent>();
        public IDictionary<string, IEnumerable<IEvent>> StreamEvents { get; set; } = new Dictionary<string, IEnumerable<IEvent>>();

        public IEnumerable<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
        public IEnumerable<CommandHandler> CommandHandlers { get; set; } = new List<CommandHandler>();
        public void SubscribeToAll(Subscriber subscriber)
        {
            Subscribers = Subscribers.Append(subscriber);
        }

        public void AppendToStream(string streamId, IList<IEvent> events)
        {
            Events = Events.Concat(events);
            EventsToPublish = EventsToPublish.Concat(events);
            if (StreamEvents.ContainsKey(streamId))
            {
                StreamEvents[streamId] = StreamEvents[streamId].Concat(events);
            }
            else
            {
                StreamEvents.Add(streamId, events);
            }
        }

        public void Trigger()
        {
            var events = EventsToPublish.ToArray();
            EventsToPublish = new List<IEvent>();
            foreach (var @event in events)
            {
                foreach (var subscriber in Subscribers)
                {
                    subscriber(@event);
                }
            }
        }

        public List<IEvent> ReadStreamForward(string streamId)
        {
            return StreamEvents[streamId].ToList();
        }
    }
}
