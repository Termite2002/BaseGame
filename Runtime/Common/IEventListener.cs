using System;


namespace Termite.BaseGame
{
    public interface IEventListener : IEventBehaviour
    {
        void OnEventRaise(string gameEvent, EventParam param);
    }
}
