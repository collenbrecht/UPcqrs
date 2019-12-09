using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class PublishQuizCommand : ICommand
    {
        public Guid QuizId { get; set; }
    }
}
