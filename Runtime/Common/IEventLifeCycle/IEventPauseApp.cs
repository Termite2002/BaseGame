using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public interface IEventPauseApp : IEventBehaviour
    {
        void BehaviourPauseApp(bool isPause);
    }

}
