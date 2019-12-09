using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Quiz
    {
        internal List<IEvent> CreateQuiz(Guid quizId, IList<IEvent> events)
        {
            if (!events.Any())
            {
                return new List<IEvent>() { new QuizWasCreatedEvent() { QuizId = quizId } };
            }
            else
            {
                return new List<IEvent>();
            }
        }

        internal List<IEvent> Publish(Guid quizId, IList<IEvent> events)
        {
            if(!events.Any(e=>e is QuizWasPublished))
            {
                return new List<IEvent>() { new QuizWasPublished() { QuizId = quizId } };
            }
            else
            {
                return new List<IEvent>();
            }
        }
    }

}
