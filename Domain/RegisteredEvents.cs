using System.Collections.Generic;
using System.Linq;
using Domain.Events;
using Domain.Queries;

namespace Domain
{
    public class RegisteredEvents
    {
        private IList<IEvent> _registeredEvents = new List<IEvent>();

        public int Query(HowManyEventsRegisteredQuery query)
        {
            return _registeredEvents.Count(e => e.GetType() == query.TypeOfEvent);
        }

        public void Stream(IEnumerable<IEvent> events)
        {
            _registeredEvents = _registeredEvents.Concat(events).ToList();
        }
    }
}
