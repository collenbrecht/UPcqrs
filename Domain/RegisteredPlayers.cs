using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Events;
using Domain.Queries;

namespace Domain
{
    public class RegisteredPlayers
    {
        private int _count;

        public int Query(IQuery query)
        {
            switch (query)
            {
                case HowManyPlayersRegistered e:
                    return _count;
            }

            throw new ArgumentException();
        }


        public void HandleEvent(IEvent @event)
        {
            switch (@event)
            {
                case PlayerHasRegisteredEvent e:
                    _count += 1;
                    break;
            }
        }
    }
}
