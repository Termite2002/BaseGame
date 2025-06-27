using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public class PopupGroup : _BaseBehaviour, IGroup
    {
        private bool m_isLocated;
        public static PopupGroup I
        {
            get
            {
                var popupGroup = Pool.Get<PopupGroup>();
                if (popupGroup.m_isLocated == false)
                    throw new Exception(
                        "PopupGroup is not located yet. You cannot use it before EventController Awake()");
                return popupGroup;
            }
        }
        [SerializeField] private string resourcesPath = "PopupResources/";
        [SerializeField] private string resourcesPreloadPath = "PopupResourcesPreload/";
        private readonly Dictionary<string, BasePopup> _dictPopup = new();
        public int activesPopupCount = 0;

        public void LocateGroup()
        {
            m_isLocated = true;
            Pool.Set(this);
            AutoPreloadPopups();
        }

        public T ShowPopup<T>() where T : BasePopup
        {
            //Popup is already loaded
            if (_dictPopup.TryGetValue(typeof(T).Name, out var popup))
            {
                popup.Show();
                return (T)popup;
            }

            //Load popup for the first Time
            popup = LoadPopup<T>();
            popup.Show();
            _dictPopup.Add(typeof(T).Name, popup);

            return (T)popup;
        }

        private BasePopup LoadPopup<T>() where T : BasePopup
        {
            return Instantiate(Resources.Load<BasePopup>($"{resourcesPath}{typeof(T).Name}"), transform);
        }

        /// <summary>
        /// Scans and preloads all popups marked with `ShouldPreload = true`
        /// </summary>
        private void AutoPreloadPopups()
        {
            var popupPrefabs = Resources.LoadAll<BasePopup>(resourcesPreloadPath);

            foreach (BasePopup popupPrefab in popupPrefabs)
            {
                var popup = Instantiate(popupPrefab, transform);
                _dictPopup.Add(popupPrefab.GetType().Name, popup);
                ((IPreloadPopup)popup).OnPreload();
            }
        }

        public void HidePopup<T>()
        {
            _dictPopup[typeof(T).Name].Hide();
        }

        public T GetPopup<T>() where T : BasePopup
        {
            if (_dictPopup.TryGetValue(typeof(T).Name, out var popup))
                return (T)popup;

            popup = LoadPopup<T>();
            _dictPopup.Add(typeof(T).Name, popup);
            return (T)popup;
        }
    }
}
