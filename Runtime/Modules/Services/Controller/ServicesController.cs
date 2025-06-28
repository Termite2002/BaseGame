using UnityEngine;
using static UnityEditor.FilePathAttribute;

namespace Termite.BaseGame
{
    public class ServicesController : MonoBehaviour, IController
    {
        bool isInit = false;

        public void LocateController()
        {
            Controllers.Add(this);
        }

        IJsonService m_Json;
        IDataService m_DataSecure;
        IDataService m_DataNormal;

        public IDataService DataSecure()
        {
            Init();
            return m_DataSecure;
        }

        void Init()
        {
            if (isInit)
                return;
            isInit = true;

            InitBindingInjection();
        }
        void InitBindingInjection()
        {
            m_Json = new JsonDotNetModule();

            m_DataNormal = new PlayerPrefsModule(m_Json);
            if (Application.isEditor)
            {
                m_DataSecure = new SecurePlayerPrefsUtilModule(m_Json);
            }
            else
            {
                m_DataSecure = new SecurePlayerPrefsModule(m_Json);
            }
        }

        public void SaveData()
        {
            Init();

            m_DataNormal.Save();
            m_DataSecure.Save();
        }
    }
}
