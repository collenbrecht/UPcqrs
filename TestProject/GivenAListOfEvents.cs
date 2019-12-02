using Domain;
using Domain.Events;
using Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    //Given When Then
    //List<Events> QueryObject Answer

    [TestClass]
    public class GivenAListOfEvents
    {
        [TestMethod]
        public void ThenNumberOfRegisteredPlayersIsReturned()
        {
            //Given
            var testRegisteredPlayers = new RegisteredEvents();

            var testHowManyPlayersRegistered = new HowManyEventsRegisteredQuery()
            {
                TypeOfEvent = typeof(PlayerHasRegisteredEvent)
            };

            //When
            testRegisteredPlayers.Stream(new IEvent[] { new PlayerHasRegisteredEvent(), new PlayerHasRegisteredEvent(), new QuizWasCreatedEvent() });

            var result = testRegisteredPlayers.Query(testHowManyPlayersRegistered);

            //Then
            Assert.AreEqual(2,result);
        }
    }
}
