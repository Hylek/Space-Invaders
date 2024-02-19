using System;
using System.Collections.Generic;
using UnityEngine;

// Made by Daniel Cumbor in 2023.

namespace Utils
{
    public static class Locator
    {
        public static bool IsReady = false;
        
        private static Dictionary<Type, object> _services;

        public static void InitCoreServices()
        {
            _services = new Dictionary<Type, object>
            {
                { typeof(ITinyMessengerHub), new TinyMessengerHub() }
            };

            Application.quitting += OnQuit;

            IsReady = true;
        }

        public static void CleanUp() => _services.Clear();

        public static T Find<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch
            {
                throw new ApplicationException("The requested service could not be found!");
            }
        }

        // Defined explicit helper methods for simplicity.
        public static ITinyMessengerHub EventHub => Find<ITinyMessengerHub>();
        
        private static void OnQuit()
        {
            CleanUp();
            Application.quitting -= OnQuit;
        }
    }
}