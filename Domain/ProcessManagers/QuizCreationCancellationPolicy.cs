using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Commands;
using Domain.Events;

namespace Domain.ProcessManagers
{
    public class QuizCreationCancellationPolicy
    {
        private readonly CommandHandler _commandHandler;
        private IList<CreatedQuiz> _createdQuizzes = new List<CreatedQuiz>();

        public QuizCreationCancellationPolicy(CommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        public void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case QuizWasCreatedEvent e:
                    _createdQuizzes.Add(new CreatedQuiz()
                    {
                        QuizId = e.QuizId,
                        NumberOfDays = 0
                    });
                    break;
                case QuizWasPublished e:
                    //_createdQuizzes.(x => x.QuizId == e.QuizId); Delete from list
                    break;
                case DayWasPassedEvent e:
                    var quiz = _createdQuizzes.First(x => x.QuizId == e.QuizId);
                    quiz.NumberOfDays = quiz.NumberOfDays++;
                    if (quiz.NumberOfDays > 2)
                    {
                        //sendCommand
                        _commandHandler(new CancelQuizCreationCommand());
                    }
                    break;
                default:
                    break;
            }
        }

        class CreatedQuiz
        {
            public Guid QuizId { get; set; }
            public int NumberOfDays  { get; set; }
        }
    }
}
