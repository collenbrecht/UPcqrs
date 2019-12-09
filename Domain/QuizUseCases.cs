using Domain.Commands;
using Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class QuizUseCases
    {
        public EventStore EventStore { get; set; }

        public QuizUseCases(EventStore store)
        {
            EventStore = store;
        }
        public void HandleCommand(CancelQuizCreationCommand command)
        {
            //get events for quiz and make decision
            EventStore.AppendToStream($"Quiz{command.QuizId}", new List<IEvent>() { new QuizWasCancelledEvent() { QuizId = command.QuizId } });
            Console.WriteLine($"handled {command.GetType()}");
        }

        public void HandleCommand(CreateQuizCommand command)
        {
            var streamId = $"Quiz{command.QuizId}";
            var events = EventStore.ReadStreamForward(streamId);
            var quiz = new Quiz();
            var newEvents = quiz.CreateQuiz(command.QuizId, events);
            EventStore.AppendToStream(streamId, newEvents);
            Console.WriteLine($"handled {command.GetType()}");
        }

        public void HandleCommand(PublishQuizCommand command)
        {
            var streamId = $"Quiz{command.QuizId}";
            var events = EventStore.ReadStreamForward(streamId);
            var quiz = new Quiz();
            var newEvents = quiz.Publish(command.QuizId, events);
            EventStore.AppendToStream(streamId, newEvents); 
            Console.WriteLine($"handled {command.GetType()}");
        }

        public void HandleCommand(AddQuestionToQuizCommand command)
        {
            EventStore.AppendToStream($"Quiz{command.QuizId}", new List<IEvent>() { new QuestionAddedToQuiz() { 
                QuizId = command.QuizId,
                QuestionAndAnswer = new Tuple<string, string>(command.Question, command.Answer)
            } });
            Console.WriteLine($"handled {command.GetType()}");
        }



    }
}
