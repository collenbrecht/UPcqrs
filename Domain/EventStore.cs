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
    }

    public class MemoryEventStore : EventStore
    {
        public IEnumerable<IEvent> Events { get; set; } = new List<IEvent>();
        public IEnumerable<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
        public IEnumerable<CommandHandler> CommandHandlers { get; set; } = new List<CommandHandler>();
        public void SubscribeToAll(Subscriber subscriber)
        {
            Subscribers = Subscribers.Append(subscriber);
        }

        public void SubscribeToCommands(CommandHandler commandHandler)
        {
            CommandHandlers.Append(commandHandler);
        }

        public void Append(IList<IEvent> events)
        {
            Events = Events.Concat(events);
        }

        public void Trigger()
        {
            foreach (var @event in Events)
            {
                foreach (var subscriber in Subscribers)
                {
                    subscriber(@event);
                }
            }
        }
    }
}
