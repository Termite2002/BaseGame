using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Termite.BaseGame
{
    [ExecuteAlways]
    public static class GlobalEventManager
    {
        private static Dictionary<Type, List<GlobalEventListenerBase>> _subscribersList;

        static GlobalEventManager()
        {
            _subscribersList = new Dictionary<Type, List<GlobalEventListenerBase>>();
        }

        public static void AddListener<MMEvent>(IGlobalEventListener<MMEvent> listener) where MMEvent : struct
        {
            Type eventType = typeof(MMEvent);

            if (!_subscribersList.ContainsKey(eventType))
            {
                _subscribersList[eventType] = new List<GlobalEventListenerBase>();
            }

            if (!SubscriptionExists(eventType, listener))
            {
                _subscribersList[eventType].Add(listener);
            }
        }

        public static void RemoveListener<MMEvent>(IGlobalEventListener<MMEvent> listener) where MMEvent : struct
        {
            Type eventType = typeof(MMEvent);

            if (!_subscribersList.ContainsKey(eventType))
            {
                return;
            }

            List<GlobalEventListenerBase> subscriberList = _subscribersList[eventType];


            for (int i = subscriberList.Count - 1; i >= 0; i--)
            {
                if (subscriberList[i] == listener)
                {
                    subscriberList.Remove(subscriberList[i]);

                    if (subscriberList.Count == 0)
                    {
                        _subscribersList.Remove(eventType);
                    }

                    return;
                }
            }
        }

        public static void TriggerEvent<GlobalEvent>(GlobalEvent newEvent) where GlobalEvent : struct
        {
            List<GlobalEventListenerBase> list;
            if (!_subscribersList.TryGetValue(typeof(GlobalEvent), out list))
                return;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                (list[i] as IGlobalEventListener<GlobalEvent>)?.OnGlobalEventTriggered(newEvent);
            }
        }

        private static bool SubscriptionExists(Type type, GlobalEventListenerBase receiver)
        {
            List<GlobalEventListenerBase> receivers;

            if (!_subscribersList.TryGetValue(type, out receivers)) return false;

            bool exists = false;

            for (int i = receivers.Count - 1; i >= 0; i--)
            {
                if (receivers[i] == receiver)
                {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
    }

    public static class EventRegister
    {
        public delegate void Delegate<T>(T eventType);

        public static void GlobalEventRegister<EventType>(this IGlobalEventListener<EventType> caller)
            where EventType : struct
        {
            GlobalEventManager.AddListener<EventType>(caller);
        }

        public static void GlobalEventUnregister<EventType>(this IGlobalEventListener<EventType> caller)
            where EventType : struct
        {
            GlobalEventManager.RemoveListener<EventType>(caller);
        }
    }

    public interface GlobalEventListenerBase
    {
    };

    public interface IGlobalEventListener<T> : GlobalEventListenerBase
    {
        void OnGlobalEventTriggered(T eventType);
    }

    public class GlobalEventListenerWrapper<TOwner, TTarget, TEvent> : IGlobalEventListener<TEvent>, IDisposable
        where TEvent : struct
    {
        private Action<TTarget> _callback;

        private TOwner _owner;

        public GlobalEventListenerWrapper(TOwner owner, Action<TTarget> callback)
        {
            _owner = owner;
            _callback = callback;
            RegisterCallbacks(true);
        }

        public void Dispose()
        {
            RegisterCallbacks(false);
            _callback = null;
        }

        protected virtual TTarget OnEvent(TEvent eventType) => default;

        public void OnGlobalEventTriggered(TEvent eventType)
        {
            var item = OnEvent(eventType);
            _callback?.Invoke(item);
        }

        private void RegisterCallbacks(bool b)
        {
            if (b)
            {
                this.GlobalEventRegister<TEvent>();
            }
            else
            {
                this.GlobalEventUnregister<TEvent>();
            }
        }
    }
}
