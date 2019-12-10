using Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Quiz
    {
        internal List<IEvent> Create(Guid quizId, IList<IEvent> events)
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

        internal List<IEvent> AddQuestion(Guid quizId, Tuple<string,string> questionAnswer, IList<IEvent> events)
        {
            if (!events.Any(e => e is QuizWasPublished || e is QuizWasCancelledEvent) && events.Any(e => e is QuizWasCreatedEvent))
            {
                return new List<IEvent>() { new QuestionAddedToQuiz()
                {
                    QuizId = quizId ,
                    QuestionAndAnswer = questionAnswer
                } };
            }
            else
            {
                return new List<IEvent>();
            }
        }

        internal List<IEvent> Publish(Guid quizId, IList<IEvent> events)
        {
            if(!events.Any(e=>e is QuizWasPublished || e is QuizWasCancelledEvent) && events.Any(e => e is QuestionAddedToQuiz))
            {
                return new List<IEvent>() { new QuizWasPublished() { QuizId = quizId } };
            }
            else
            {
                return new List<IEvent>();
            }
        }

        internal List<IEvent> Cancel(Guid quizId, IList<IEvent> events)
        {
            if (!events.Any(e => e is QuizWasPublished || e is QuizWasCancelledEvent) && events.Any(e=> e is QuizWasCreatedEvent))
            {
                return new List<IEvent>() { new QuizWasCancelledEvent() { QuizId = quizId } };
            }
            else
            {
                return new List<IEvent>();
            }
        }
    }

}
