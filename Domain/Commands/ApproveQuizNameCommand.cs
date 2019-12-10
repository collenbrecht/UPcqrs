using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class ApproveQuizNameCommand
    {
        public Guid QuizId { get; internal set; }
    }
}
