using UnityEngine;
using UnityEngine.UI;

namespace Termite.BaseGame
{
    public class BasePopup : ScreenBehaviour
    {
        [SerializeField] protected Canvas canvas;
        [SerializeField] protected GameObject parent;
        [SerializeField] protected Button closeButton;
        public bool IsShown { private set; get; }
        private const string PopupLayer = "UI/Popup";

        protected virtual void Awake()
        {
            SetCamera();
            parent.SetActive(false);
            SetEvent(closeButton, Hide);
            canvas.sortingLayerName = PopupLayer;
            LocatePopup();
        }

        private void LocatePopup()
        {
            Pool.Set(GetType(), this);
        }

        protected virtual void SetCamera()
        {
            //canvas.worldCamera = GamePlay.uiCamera;
        }

        public virtual void Show()
        {
            if (IsShown)
            {
                Debug.Log($"Popup {GetType().Name} has already been shown.");
                return;
            }

            IsShown = true;
            PopupGroup.I.activesPopupCount++;
            canvas.sortingOrder = PopupGroup.I.activesPopupCount;
            canvas.enabled = true;
            parent.SetActive(true);
        }

        public virtual void Hide()
        {
            BaseHide();
        }

        protected void BaseHide()
        {
            if (!IsShown)
            {
                Debug.Log($"Popup {gameObject.name} is already closed");
                return;
            }

            IsShown = false;

            PopupGroup.I.activesPopupCount--;
            if (PopupGroup.I.activesPopupCount < 0)
            {
                PopupGroup.I.activesPopupCount = 0;
                Debug.Log("Is less than zero " + gameObject.name);
                // throw new ArgumentException("active pop up is less than 1");
            }

            canvas.enabled = false;
            parent.SetActive(false);
        }
    }
}
