using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Events
{
    public class QuizWasCancelledEvent : IEvent
    {
        public Guid QuizId { get; set; }
    }
}
