using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Termite.BaseGame
{
    public class BaseBehaviour : MonoBehaviour
    {
        private static readonly Dictionary<Type, object> FindDictionary = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> GetDictionary = new Dictionary<Type, object>();
        private readonly Dictionary<Type, object> GetChildDictionary = new Dictionary<Type, object>();

        protected static T SingleFind<T>() where T : Object
        {
            var type = typeof(T);
            if (!FindDictionary.ContainsKey(type)) FindDictionary[type] = FindObjectOfType<T>();
            return (T)FindDictionary[type];
        }

        protected T SingleGet<T>() where T : Object
        {
            var type = typeof(T);
            if (!GetDictionary.ContainsKey(type)) GetDictionary[type] = GetComponent<T>();
            return (T)GetDictionary[type];
        }

        protected T SingleGetChild<T>(bool includeInactive = true) where T : Object
        {
            var type = typeof(T);
            if (!GetChildDictionary.ContainsKey(type))
                GetChildDictionary[type] = GetComponentInChildren<T>(includeInactive);
            return (T)GetChildDictionary[type];
        }


        protected void SingleClearGetChild()
        {
            GetChildDictionary.Clear();
        }

        protected void SingleClearGet()
        {
            GetDictionary.Clear();
        }

        protected void SingleClearFind()
        {
            FindDictionary.Clear();
        }
    }
}