using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Termite.BaseGame
{
    public class ScreenGroup : _BaseBehaviour, IGroup
    {
        private bool m_isLocated;

        public static ScreenGroup I
        {
            get
            {
                var screenGroup = Pool.Get<ScreenGroup>();
                if (screenGroup.m_isLocated == false)
                    throw new Exception(
                        "ScreenGroup is not located yet. You cannot use it before EventController Awake()");
                return screenGroup;
            }
        }

        public List<BaseScreen> ListScreen =>
            SingleCreateIfNotExist("ListScreen_YasaOre",
                () => transform.GetComponentsInChildren<BaseScreen>(true).ToList());


        public void LocateGroup()
        {
            Pool.Set(this);
            m_isLocated = true;
            foreach (var screen in ListScreen)
            {
                screen.LocateScreen();
            }
        }
        public T ShowScreen<T>() where T : BaseScreen
        {
            T panel = null;
            foreach (var p in ListScreen)
            {
                if (p is T matchedPanel)
                {
                    p.Show();
                    panel = matchedPanel;
                }
                else p.Hide();
            }

            return panel;
        }

    }
}
