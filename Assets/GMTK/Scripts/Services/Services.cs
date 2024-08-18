using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();

        private Dictionary<Type, IService> _services = new();

        public void Reset()
        {
            _services.Clear();
        }

        public void Register<T>(T service) where T : IService
        {
            _services.Add(typeof(T), service);

#if UNITY_EDITOR
            Debug.Log($"Register new service: {typeof(T)}");
#endif
        }

        public T Get<T>() where T : IService
        {
            return (T)_services[typeof(T)];
        }

        public bool TryGet<T>(out T service) where T : IService
        {
            bool r = _services.TryGetValue(typeof(T), out IService value);
            service = (T)value;
            return r;
        }
    }
}