using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public interface IEventUpdate : IEventBehaviour
    {
        void BehaviourUpdate(float deltaTime);
    }

}
