using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public interface IEventAwake : IEventBehaviour
    {
        void BehaviourAwake();
    }

}
