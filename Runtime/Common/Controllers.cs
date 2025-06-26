using System;
using System.Collections.Generic;


namespace Termite.BaseGame
{
    public static class Controllers
    {
        static Dictionary<Type, object> m_Dict = new Dictionary<Type, object>();

        public static void Add<T>(T controller) where T : class
        {
            if (controller == null)
            {
                UnityEngine.Debug.LogError("add null controller");
            }
            m_Dict[typeof(T)] = controller;
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);
            if (m_Dict.ContainsKey(type))
            {
                return m_Dict[type] as T;
            }
            return null;
        }
    }
}
