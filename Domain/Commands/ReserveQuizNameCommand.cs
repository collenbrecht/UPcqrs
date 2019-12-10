using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class ReserveQuizNameCommand
    {
        public string Name { get; set; }
        public Guid QuizId { get; internal set; }
    }
}
