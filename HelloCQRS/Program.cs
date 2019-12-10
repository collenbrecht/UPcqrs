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
        //public static Startup startup;

        static void Main(string[] args)
        {
            var startup = new Startup();
            var eventStore = startup.MemoryEventStore;
            var quizId = Guid.NewGuid();
            Console.WriteLine("Press 0 to cancel quiz");
            Console.WriteLine("Press 1 to create quiz");
            Console.WriteLine("Press 2 to add Question quiz");
            Console.WriteLine("Press 3 to publish quiz");
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out var correctKey))
                {
                    switch (correctKey)
                    {
                        case 0:
                            startup.QuizUseCases.HandleCommand(new CancelQuizCreationCommand { QuizId = quizId });
                            break;
                        case 1:
                            startup.QuizUseCases.HandleCommand(new CreateQuizCommand { QuizId = quizId });
                            break;
                        case 2:
                            AddQuestion(quizId, startup);
                            break;
                        case 3:
                            startup.QuizUseCases.HandleCommand(new PublishQuizCommand() { QuizId = quizId });
                            break;
                        default:
                            Console.WriteLine($"command {input} not recognized");
                            break;

                    }
                };
                //na de transactie de events Triggeren. wordt normaal door iets anders gedaan. 
                ((MemoryEventStore)eventStore).Trigger();
            }


        }

        private static void AddQuestion(Guid quizId, Startup startup)
        {

            Console.WriteLine("type a question");
            var question = Console.ReadLine();
            Console.WriteLine("type the answer");
            var answer = Console.ReadLine();
            startup.QuizUseCases.HandleCommand(new 
                AddQuestionToQuizCommand() { 
                QuizId = quizId,
                Question = question,
                Answer = answer
            });
        }
    }
}
