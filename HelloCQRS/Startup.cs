using Domain;
using Domain.Commands;
using Domain.Events;
using Domain.ProcessManagers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloCQRS
{
    public class Startup
    {
        public QuizCreationCancellationPolicy ProcesManager { get; }
        public EventStore MemoryEventStore { get; set; }
        public QuizUseCases QuizUseCases { get; set; }
        public Startup()
        {
            MemoryEventStore = new MemoryEventStore();
            QuizUseCases = new QuizUseCases(MemoryEventStore);
            ProcesManager = new QuizCreationCancellationPolicy(QuizUseCases);
            var r = new RegisteredPlayers();
            MemoryEventStore.SubscribeToAll(ProcesManager.HandleEvent);
            MemoryEventStore.SubscribeToAll(r.HandleEvent);
        }

    }
}
