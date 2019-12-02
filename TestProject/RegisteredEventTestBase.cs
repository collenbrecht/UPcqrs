using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Events;
using Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    public class RegisteredEventTestBase
    {
        protected RegisteredEvents Subject;
        private IQuery query;

        [TestInitialize]
        public void Initialize()
        {
            Subject = new RegisteredEvents();
        }

        public RegisteredEventTestBase Given(IEnumerable<IEvent> events)
        { 
            Subject.Stream(events);
            return this;
        }

        public RegisteredEventTestBase When(IQuery query)
        {
            query = this.query;
            return this;
        }

        internal bool Then(Func<int, bool> answerFunc)
        {
            return answerFunc(
                Subject.Query(query as HowManyEventsRegisteredQuery));
        }
    }
}
