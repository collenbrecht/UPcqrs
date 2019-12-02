using System;

namespace Domain.Queries
{
    public class HowManyEventsRegisteredQuery : IQuery
    {
        public Type TypeOfEvent { get; set; } = typeof(HowManyEventsRegisteredQuery);
    }
}
