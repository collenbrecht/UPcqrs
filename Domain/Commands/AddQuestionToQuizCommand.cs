using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class AddQuestionToQuizCommand
    {
        public Guid QuizId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
