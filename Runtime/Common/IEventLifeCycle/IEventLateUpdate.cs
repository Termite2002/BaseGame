using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public interface IEventLateUpdate : IEventBehaviour
    {
        void BehaviourLateUpdate(float deltaTime);
    }

}
