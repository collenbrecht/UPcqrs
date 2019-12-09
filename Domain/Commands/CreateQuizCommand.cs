using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class CreateQuizCommand : ICommand
    {
        public Guid QuizId { get; set; }
    }
}
