using System;

namespace Domain.Events
{
    public class QuizNameApprovedEvent : IEvent
    {
        public Guid QuizId { get; internal set; }
    }
}
