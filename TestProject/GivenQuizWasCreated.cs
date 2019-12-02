using Domain.Events;
using Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class GivenQuizWasCreated : RegisteredEventTestBase
    {
        [TestMethod]
        public void NoEvents()
        {
            Given(new IEvent[]{})
                .When(new HowManyEventsRegisteredQuery())
                .Then((answer)=> answer.Equals(0));
        }

        [TestMethod]
        public void PlayerHasRegisteredEvent()
        {
            Given(new IEvent[] { new PlayerHasRegisteredEvent()})
                .When(new HowManyEventsRegisteredQuery())
                .Then((answer) => answer.Equals(1));
        }
    }
}
