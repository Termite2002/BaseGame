using UnityEngine;

namespace Termite.BaseGame
{
    public class _BaseBehaviour : BaseBehaviour
    {
        protected static ServicesController Services => Controllers.Get<ServicesController>();

        protected static DataController DataController => Controllers.Get<DataController>();

        protected static SoundController SoundController => Controllers.Get<SoundController>();



        public virtual void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public virtual void SetActive(GameObject gameObject, bool isActive)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(isActive);
            }
        }

        public virtual void SetActive(Component component, bool isActive)
        {
            if (component != null)
            {
                component.gameObject.SetActive(isActive);
            }
        }
    }
}