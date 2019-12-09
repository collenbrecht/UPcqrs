using System;
using System.Collections.Generic;
using Domain.Commands;
using Domain.Events;

namespace Domain.ProcessManagers
{
    public partial class QuizCreationCancellationPolicy
    {
        private readonly QuizUseCases _quizUseCases;
        private Dictionary<Guid, int> _createdQuizzes = new Dictionary<Guid, int>();

        public QuizCreationCancellationPolicy(QuizUseCases useCases)
        {
            _quizUseCases = useCases;
        }

        public void HandleEvent(IEvent @event)
        {
            Console.WriteLine($"registered {@event.GetType()}");
            switch (@event)
            {
                case QuizWasCreatedEvent e:
                    _createdQuizzes.Add(e.QuizId, 0);
                    break;
                case QuizWasPublished e:
                    _createdQuizzes.Remove(e.QuizId);
                    break;
                case DayWasPassedEvent e:
                    if (++_createdQuizzes[e.QuizId] > 2)
                    {
                        _quizUseCases.HandleCommand(new CancelQuizCreationCommand() { QuizId = e.QuizId });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
