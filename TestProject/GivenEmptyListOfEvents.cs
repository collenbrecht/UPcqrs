using Domain;
using Domain.Events;
using Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    //Given When Then
    //List<Events> QueryObject Answer

    [TestClass]
    public class GivenEmptyListOfEvents
    {

        [TestMethod]
        public void ThenNoPlayersRegistered()
        {
            //Given
            var testRegisteredPlayers = new RegisteredPlayers();
            var testHowManyPlayersRegistered = new HowManyPlayersRegistered();

            //When
            //testRegisteredPlayers.Stream(new IEvent[0]);

            var result = testRegisteredPlayers.Query(testHowManyPlayersRegistered);

            //Then
            Assert.AreEqual(0,result);
        }

    }
}
