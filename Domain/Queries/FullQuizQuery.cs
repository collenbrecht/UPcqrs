using System;
using Domain.Events;

namespace Domain.Queries
{
    public class FullQuizQuery : IQuery
    {
        public Guid QuizId { get; set; }
    }

}