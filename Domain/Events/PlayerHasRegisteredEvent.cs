using System.ComponentModel;

namespace Domain.Events
{
    public class PlayerHasRegisteredEvent : IEvent
    {
        public PlayerHasRegisteredEvent()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
