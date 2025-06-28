using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    public class DataController : _BaseBehaviour, IController, IEventListener, IEventAwake, IEventPauseApp, IEventQuitApp
    {
        public const string PLAYER_DATA_KEY = "PlayerData";

        private bool m_IsNeedSaveData;

        private readonly Dictionary<string, object> m_DictAutoSave = new Dictionary<string, object>();

        public void LocateController()
        {
            Controllers.Add(this);
        }

        public bool IsActive()
        {
            return gameObject.activeSelf;
        }

        public void OnEventRaise(string gameEvent, EventParam param)
        {
            switch (gameEvent)
            {
                //case EventList.SAVE_DATA:
                //    m_IsNeedSaveData = true;
                //    break;
            }
        }

        public void BehaviourAwake()
        {
            LoadData();
        }



        public void SetAutoSaveObject(string saveDataKey, object obj)
        {
            m_DictAutoSave[saveDataKey] = obj;
        }

        public void SaveData()
        {
            m_IsNeedSaveData = false;

            foreach (var kv in m_DictAutoSave)
            {
                Services.DataSecure().SetObject(kv.Key, kv.Value);
            }

            Services.SaveData();
        }

        public T LoadData<T>(string saveDataKey) where T : class, new()
        {
            return Services.DataSecure().GetObject<T>(saveDataKey, Pool.Get<T>());
        }

        void LoadData()
        {

        }

        public void BehaviourPauseApp(bool pause)
        {
            if (pause)
            {
                SaveData();
            }
        }

        public void BehaviourQuitApp()
        {
            SaveData();
        }

        void LateUpdate()
        {
            if (m_IsNeedSaveData)
            {
                SaveData();
            }
        }


    }
}

