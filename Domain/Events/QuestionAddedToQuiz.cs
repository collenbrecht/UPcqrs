using System;

namespace Domain.Events
{
    public class QuestionAddedToQuiz : IEvent
    {
        public Guid QuizId { get; set; }
        public Tuple<string,string> QuestionAndAnswer { get; set; }
    }
}