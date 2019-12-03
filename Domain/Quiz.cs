using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Events;
using Domain.Queries;

namespace Domain
{
    public class Quiz
    {
        private IList<FullQuizAnswer> _quizAnswers;

        public FullQuizAnswer Query(FullQuizQuery query)
        {
            return _quizAnswers.FirstOrDefault(x => x.QuizId == query.QuizId);
        }

        
        public void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case QuizWasCreatedEvent e:
                    _quizAnswers.Add(new FullQuizAnswer()
                    {
                        OwnerName = e.PlayerName,
                        QuizName = e.QuizName,
                        QuizId = e.QuizId
                    });
                    break;
                case QuestionAddedToQuiz e:
                    _quizAnswers.FirstOrDefault(x => x.QuizId == e.QuizId)?.Questions.Append(e.QuestionAndAnswer);
                    break;
                case QuizWasPublished e:
                    _quizAnswers.First(x => x.QuizId == e.QuizId).IsPublished = true;
                    break;

            }
        }
    }

    public class FullQuizAnswer
    {
        public string QuizName { get; set; }
        public IEnumerable<Tuple<string,string>> Questions { get; set; }
        public string OwnerName { get; set; }
        public Guid QuizId { get; set; }
        public bool IsPublished { get; set; }
    }
}