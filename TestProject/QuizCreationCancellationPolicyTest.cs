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
            Subject = new QuizCreationCancellationPolicy(Handle);
            _memoryEventStore = new MemoryEventStore();
            _memoryEventStore.SubscribeToAll(Subject.HandleEvent);
        }

        [TestMethod]
        public void QuizCancellationPolicy_1_DayWasPassed()
        {
            Given(new IEvent[]{new QuizWasCreatedEvent()});
            When(new DayWasPassedEvent());
            ThenNothing();
        }

        [TestMethod]
        public void QuizCancellationPolicy_2_DayWasPassed()
        {
            Given(new IEvent[] { new QuizWasCreatedEvent(), new DayWasPassedEvent() });
            When(new DayWasPassedEvent());
            Then(true);
        }

        public void Given(IEnumerable<IEvent> events)
        {
            _memoryEventStore.Append(events.ToList());
        }

        public void When(IEvent @event)
        {
            _memoryEventStore.Append(new List<IEvent>{@event});
        }

        public void Then(bool handled)
        {
            Assert.AreEqual(handled, commandHandled);
        }

        public void ThenNothing()
        {

        }
    }
}
