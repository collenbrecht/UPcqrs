using Domain.Aggregates;
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

        public void HandleCommand(CreateQuizCommand command)
        {
            var streamId = $"Quiz{command.QuizId}";
            var events = EventStore.ReadStreamForward(streamId);
            var quiz = new Quiz();
            var newEvents = quiz.Create(command.QuizId, events);
            EventStore.AppendToStream(streamId, newEvents);
            Console.WriteLine($"handled {command.GetType()}");
        }

        public void HandleCommand(AddQuestionToQuizCommand command)
        {
            var streamId = $"Quiz{command.QuizId}";
            var events = EventStore.ReadStreamForward(streamId);
            var quiz = new Quiz();
            var newEvents = quiz.AddQuestion(command.QuizId, new Tuple<string, string>(command.Question, command.Answer), events);
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

        public void HandleCommand(CancelQuizCreationCommand command)
        {
            var streamId = $"Quiz{command.QuizId}";
            var events = EventStore.ReadStreamForward(streamId);
            var quiz = new Quiz();
            var newEvents = quiz.Cancel(command.QuizId, events);
            EventStore.AppendToStream(streamId, newEvents);
            Console.WriteLine($"handled {command.GetType()}");
        }

        public void HandleCommand(ReserveQuizNameCommand command)
        {
            var streamId = $"QuizNames{command.Name}";
            var events = EventStore.ReadStreamForward(streamId);
            var quizNames = new QuizNames();
            var newEvents = quizNames.ReserveName(command.QuizId, events);
            EventStore.AppendToStream(streamId, newEvents);
            Console.WriteLine($"handled {command.GetType()}");
        }

        public void HandleCommand(ApproveQuizNameCommand command)
        {
            var streamId = $"Quiz{command.QuizId}";
            //var events = EventStore.ReadStreamForward(streamId);
            var quiz = new Quiz();
            var newEvents = quiz.ApproveName(command.QuizId);
            EventStore.AppendToStream(streamId, newEvents);
            Console.WriteLine($"handled {command.GetType()}");
        }

    }
}
