using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    /// <summary>
    /// A base class that extends from MonoBehaviour but with some common added behaviours.
    /// </summary>
    public class ExtendedBehaviour : MonoBehaviour
    {
        private Dictionary<Type, TinyMessageSubscriptionToken> _tokens;

        protected virtual void Awake()
        {
            _tokens = new Dictionary<Type, TinyMessageSubscriptionToken>();
        }

        protected void Subscribe<T>(Action<T> action) where T : class, ITinyMessage
        {
            if (!Locator.IsReady)
            {
                Locator.InitCoreServices();
            }
            
            _tokens.Add(typeof(T), Locator.EventHub.Subscribe(action));
        }

        protected void Unsubscribe(Type type)
        {
            foreach (var token in
                     _tokens.Where(token => token.Key == type))
            {
                Debug.Log($"{gameObject.name} has unsubscribed from message type {token.Key.Name}");
                Locator.EventHub.Unsubscribe(token.Value);
                _tokens.Remove(token.Key);

                break;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_tokens.Count <= 0) return;
            
            Debug.Log($"{gameObject.name} is cleaning up it's messages.");
            
            foreach (var token in _tokens)
            {
                Locator.EventHub.Unsubscribe(token.Value);
            }
            _tokens.Clear();
        }
    }
}