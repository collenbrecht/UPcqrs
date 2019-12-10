using Domain.Events;
using System;

namespace Domain.ProcessManagers
{
    public class QuizUniqueNamePolicy
    {
        private readonly QuizUseCases _quizUseCases;
        public QuizUniqueNamePolicy(QuizUseCases useCases)
        {
            _quizUseCases = useCases;
        }

        public void HandleEvent(IEvent @event)
        {
            Console.WriteLine($"registered {@event.GetType()}");
            switch (@event)
            {
                case QuizWasCreatedEvent e:
                    _quizUseCases.HandleCommand(new Commands.ReserveQuizNameCommand() { Name = e.QuizName, QuizId = e.QuizId });
                    break;
                case QuizNameReservedEvent e:
                    _quizUseCases.HandleCommand(new Commands.ApproveQuizNameCommand() { QuizId = e.QuizId });
                        break;
                default:
                    break;
            }
        }
    }
}
