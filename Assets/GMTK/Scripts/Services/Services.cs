using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMTK.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _globalInstance = null;
        private static ServiceLocator _sceneInstance = null;

        public static ServiceLocator Instance => _sceneInstance;
        private Dictionary<Type, IService> _services = new();
        private Dictionary<Type, Action> _registerCallbacks = new();

        public static void Init()
        {
            if (_globalInstance)
                return;

            // Global Container
            var globalInstance = new GameObject("GlobalServicesContainer");
            _globalInstance = globalInstance.AddComponent<ServiceLocator>();
            DontDestroyOnLoad(_globalInstance);

            // Scene Container
            CreateSceneContainer();
            SceneManager.sceneLoaded += (Scene _, LoadSceneMode _) => CreateSceneContainer();
        }

        private static void CreateSceneContainer()
        {
            if (_sceneInstance)
                return;

            var sceneInstance = new GameObject("SceneServicesContainer");
            _sceneInstance = sceneInstance.AddComponent<ServiceLocator>();
        }

        public void RegisterGlobal<T>(T service) where T : IService
        {
            _globalInstance._services.Add(typeof(T), service);
        }

        public void Register<T>(T service) where T : IService
        {
            _services.Add(typeof(T), service);

            if (_registerCallbacks.ContainsKey(typeof(T)))
            {
                _registerCallbacks[typeof(T)]?.Invoke();
                _registerCallbacks.Remove(typeof(T));
            }

#if UNITY_EDITOR
            Debug.Log($"[MTK.Services] Register new service: {typeof(T)}");
#endif
        }

        public void SetCallback<T>(Action callback)
        {
            if (_registerCallbacks.ContainsKey(typeof(T)))
                _registerCallbacks[typeof(T)] += callback;
            else
                _registerCallbacks.Add(typeof(T), callback);
        }

        public bool Contains<T>() where T : IService
        {
            return _services.ContainsKey(typeof(T)) || _globalInstance._services.ContainsKey(typeof(T));
        }

        public T Get<T>() where T : IService
        {
            if (Contains<T>())
                return (T)_services[typeof(T)];
            return (T)_globalInstance._services[typeof(T)];
        }
    }
}