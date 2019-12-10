using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Aggregates
{
    public class QuizNames
    {
        internal List<IEvent> ReserveName(Guid quizId, List<IEvent> events)
        {
            if (!events.Any())
            {
                return new List<IEvent>()
                {
                    new QuizNameReservedEvent(){ QuizId = quizId}
                };
            }
            return new List<IEvent>();
        }
    }
}
