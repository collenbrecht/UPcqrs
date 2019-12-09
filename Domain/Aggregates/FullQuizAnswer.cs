using System;
using System.Collections.Generic;

namespace Domain
{
    public class FullQuizAnswer
    {
        public string QuizName { get; set; }
        public IEnumerable<Tuple<string,string>> Questions { get; set; }
        public string OwnerName { get; set; }
        public Guid QuizId { get; set; }
        public bool IsPublished { get; set; }
    }
}