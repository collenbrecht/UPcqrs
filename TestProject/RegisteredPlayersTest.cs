using System;
using System.IO.Pipes;
using Domain.Events;
using Domain.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class RegisteredPlayersTest : RegisteredPlayersTestBase
    {
        [TestMethod]
        public void NoEvents()
        {
            Given(Guid.NewGuid(), new IEvent[]{})
                .When(new HowManyPlayersRegistered())
                .Then(0);
        }

        [TestMethod]
        public void PlayerHasRegisteredEvent()
        {
            Given(Guid.NewGuid(), new IEvent[] {new QuizWasCreatedEvent()})
                .When(new HowManyPlayersRegistered())
                .Then(0);
        }

        [TestMethod]
        public void AValidQuiz()
        {
            Given(Guid.NewGuid(), new IEvent[] { new PlayerHasRegisteredEvent(), new QuizWasCreatedEvent(), new QuestionAddedToQuiz(), new QuizWasPublished() })
                .When(new HowManyPlayersRegistered())
                .Then(1);
        }
    }
}
