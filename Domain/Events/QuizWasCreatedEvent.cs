using System;

namespace Domain.Events
{
    public class QuizWasCreatedEvent : IEvent
    {
        public QuizWasCreatedEvent()
        {
        }

        public Guid QuizId { get; set; }
        public string QuizName { get; set; }
        public string PlayerName { get; set; }
    }
}