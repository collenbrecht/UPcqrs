using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class QuizNameReservedEvent : IEvent
    {
        public Guid QuizId { get; internal set; }
    }
}
