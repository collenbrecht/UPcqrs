using Domain;
using Domain.Commands;
using Domain.Events;
using Domain.ProcessManagers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestClass]
    public class QuizUseCasesTests
    {
        protected QuizCreationCancellationPolicy _processManager;
        private MemoryEventStore _memoryEventStore;
        private QuizUseCases _quizUseCases;



        [TestInitialize]
        public void Initialize()
        {
            _memoryEventStore = new MemoryEventStore();
            _quizUseCases = new QuizUseCases(_memoryEventStore);
            _processManager = new QuizCreationCancellationPolicy(_quizUseCases);
            _memoryEventStore.SubscribeToAll(_processManager.HandleEvent);
        }

        [TestMethod]
        public void WhenCreatedQuizCanBeCancelled()
        {
            var quizId = Guid.NewGuid();
            Given(quizId, new IEvent[] { 
                new QuizWasCreatedEvent(){QuizId = quizId },
                new QuestionAddedToQuiz{QuizId = quizId, QuestionAndAnswer = new Tuple<string, string>("q","a")}
            });
            When(new CancelQuizCreationCommand() { QuizId = quizId });
            ThenQuizIsCancelled();
        }


        [TestMethod]
        public void WhenPublishedQuizCanNotBeCancelled()
        {
            var quizId = Guid.NewGuid();
            Given(quizId, new IEvent[] {
                new QuizWasCreatedEvent(){QuizId = quizId },
                new QuestionAddedToQuiz{QuizId = quizId, QuestionAndAnswer = new Tuple<string, string>("q","a")},
                new QuizWasPublished{QuizId = quizId}
            });
            When(new CancelQuizCreationCommand() { QuizId = quizId });
            ThenQuizIsNotCancelled();
        }

        public void Given(Guid quizId, IEnumerable<IEvent> events)
        {
            _memoryEventStore.AppendToStream($"Quiz{quizId}", events.ToList());
        }

        public void When(CancelQuizCreationCommand command)
        {
            _quizUseCases.HandleCommand(command);
        }

        public void ThenQuizIsCancelled()
        {
            _memoryEventStore.Trigger();
            Assert.IsTrue(_memoryEventStore.Events.Any(e => e is QuizWasCancelledEvent));
        }

        public void ThenQuizIsNotCancelled()
        {
            _memoryEventStore.Trigger();
            Assert.IsTrue(!_memoryEventStore.Events.All(e => e is QuizWasCancelledEvent));
        }

    }
}
