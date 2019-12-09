using System;
using System.Collections.Generic;
using Domain;
using Domain.Commands;
using Domain.Events;
using Domain.ProcessManagers;

namespace HelloCQRS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 0 to cancel quiz");
            while (true)
            {
                var input = Console.ReadLine();
                int correctKey;
                if (int.TryParse(input, out correctKey))
                {
                    switch (correctKey)
                    {
                        case 0:
                            DoCancelQuiz();
                            break;
                    }
                }

                ;
            }


        }

        private static void DoCancelQuiz()
        {
            var quizId = Guid.NewGuid();
            var subject = new QuizCreationCancellationPolicy(Handle);
            var memoryEventStore = new MemoryEventStore();
            memoryEventStore.SubscribeToAll(subject.HandleEvent);
            memoryEventStore.Append(new List<IEvent>
            {
                new QuizWasCreatedEvent { QuizId = quizId },
                new DayWasPassedEvent { QuizId = quizId },
                new DayWasPassedEvent { QuizId = quizId }
            });
            memoryEventStore.Trigger();
        }

        public static void Handle(ICommand command)
        {
            Console.WriteLine("cancelled");
        }
    }
}
