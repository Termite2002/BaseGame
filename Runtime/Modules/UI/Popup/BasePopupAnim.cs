using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace Termite.BaseGame
{
    public class BasePopupAnim : BasePopup
    {
        private const float ShowDuration = 0.6f;
        private const float HideDuration = 0.1f;
        [SerializeField] private Transform panel;
        private GraphicRaycaster m_graphicRaycaster;
        private CanvasGroup m_canvasGroup;
        private TweenerCore<Vector3, Vector3, VectorOptions> m_tweenClose;

        protected override void Awake()
        {
            base.Awake();
            m_graphicRaycaster = GetComponent<GraphicRaycaster>();
            m_canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public override void Show()
        {
            if (!IsShown) AnimShow();
            base.Show();
        }

        public void ShowNoAnim()
        {
            base.Show();
        }

        public override void Hide()
        {
            AnimHide(BaseHide);
            // base.Hide();
        }

        public void HideNoAnim()
        {
            BaseHide();
        }

        private void AnimShow()
        {
            m_graphicRaycaster.enabled = true;
            m_canvasGroup.alpha = 1;
            panel.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            panel.DOScale(1, ShowDuration).SetEase(Ease.OutElastic);
            // SoundManager.I.PlaySound(Sound.PopupAppear, 0.5f);
        }

        public void AnimHide(System.Action onDone)
        {
            if (m_tweenClose.IsActive())
            {
                Debug.Log($"Popup {GetType().Name} is already being closed.");
                return;
            }

            m_graphicRaycaster.enabled = false;
            m_canvasGroup.DOFade(0, HideDuration);
            // SoundManager.I.PlaySound(Sound.PopupDisappear, 0.5f);
            m_tweenClose = panel.DOScale(0.5f, HideDuration).OnComplete(() =>
            {
                onDone?.Invoke();
                m_tweenClose = null;
            });
        }
    }
}
