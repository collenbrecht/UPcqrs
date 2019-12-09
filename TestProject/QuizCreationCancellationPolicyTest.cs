using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Events;
using Domain.ProcessManagers;
using Domain.Queries;
using Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class QuizCreationCancellationPolicyTest
    {
        protected QuizCreationCancellationPolicy Subject;
        private MemoryEventStore _memoryEventStore;
        private bool commandHandled;

        public void Handle(ICommand command)
        {
            commandHandled = true;
        }


        [TestInitialize]
        public void Initialize()
        {
            _memoryEventStore = new MemoryEventStore();
            Subject = new QuizCreationCancellationPolicy(new QuizUseCases(_memoryEventStore));
            _memoryEventStore.SubscribeToAll(Subject.HandleEvent);
        }

        [TestMethod]
        public void QuizCancellationPolicy_1_DayWasPassed()
        {
            var quizId = Guid.NewGuid();
            Given(quizId, new IEvent[] { new QuizWasCreatedEvent() });
            When(new DayWasPassedEvent() { QuizId =  quizId});
            ThenNothing();
        }

        [TestMethod]
        public void QuizCancellationPolicy_2_DayWasPassed()
        {
            var quizId = Guid.NewGuid();
            Given(quizId, new IEvent[] { new QuizWasCreatedEvent() { QuizId = quizId }, new DayWasPassedEvent() { QuizId = quizId } });
            When(new DayWasPassedEvent() { QuizId = quizId });
            Then(true);
        }

        public void Given(Guid quizId, IEnumerable<IEvent> events)
        {
            _memoryEventStore.AppendToStream($"Quiz{quizId}",events.ToList());
        }

        public void When(DayWasPassedEvent @event)
        {
            _memoryEventStore.AppendToStream($"Quiz{@event.QuizId}",new List<IEvent> { @event });
        }

        public void Then(bool handled)
        {
            _memoryEventStore.Trigger();
            Assert.IsTrue(_memoryEventStore.Events.Any(e => e is QuizWasCancelledEvent));
            //Assert.AreEqual(handled, commandHandled);
        }

        public void ThenNothing()
        {
            Assert.IsFalse(_memoryEventStore.Events.Any(e => e is QuizWasCancelledEvent));
        }
    }
}
