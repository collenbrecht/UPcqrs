﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public class CancelQuizCreationCommand : ICommand
    {
        public Guid QuizId { get; set; }
    }
}
