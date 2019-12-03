using System;

namespace Domain.Events
{
    public class QuizWasPublished : IEvent
    {
        public Guid QuizId { get; set; }
    }
}