using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public interface IEventFixedUpdate : IEventBehaviour
    {
        void BehaviourFixedUpdate(float deltaTime);
    }

}
