using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public class BaseScreen : ScreenBehaviour
    {
        public Canvas Canvas;
        private const string ScreenLayerName = "UI/Screen";
        public virtual void Show() => Canvas.enabled = true;
        public virtual void Hide() => Canvas.enabled = false;
        public bool IsShown() => Canvas.enabled;

        protected virtual void Awake()
        {
            Canvas = GetComponent<Canvas>();
            Canvas.sortingLayerName = ScreenLayerName;
        }

        protected virtual void Start()
        {
            //Canvas.worldCamera = GamePlay.uiCamera;
        }

        public void LocateScreen()
        {
            Pool.Set(GetType(), this);
        }
    }
}
