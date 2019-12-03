using System;

namespace Domain.Events
{
    public class DayWasPassedEvent : IEvent
    {
        public DayWasPassedEvent()
        {
        }

        public Guid QuizId { get; set; }
    }
}