using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using Domain.Events;
using Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    public class RegisteredPlayersTestBase
    {
        protected RegisteredPlayers Subject;
        private IQuery query;
        private MemoryEventStore _memoryEventStore;

        [TestInitialize]
        public void Initialize()
        {
            Subject = new RegisteredPlayers();
            _memoryEventStore = new MemoryEventStore();
            _memoryEventStore.SubscribeToAll(Subject.HandleEvent);
        }

        public RegisteredPlayersTestBase Given(Guid quizId, IEnumerable<IEvent> events)
        { 
           _memoryEventStore.AppendToStream($"QuizId{quizId}", events.ToList());
           return this;
        }

        public RegisteredPlayersTestBase When(IQuery query)
        {
            this.query = query;
            return this;
        }

        internal void Then(int expected)
        {
            _memoryEventStore.Trigger();
            Assert.AreEqual(expected, Subject.Query(query));
        }
    }
}
