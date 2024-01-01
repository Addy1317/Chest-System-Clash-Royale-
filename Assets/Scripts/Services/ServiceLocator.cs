using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChestSystem
{
    public class ServiceLocator : GenericSingleton<ServiceLocator>
    {
        private Dictionary<TypesOfServices, IGameService> services;

        protected override void Awake()
        {
            base.Awake();
            services = new Dictionary<TypesOfServices, IGameService>();
        }

        public void Register<T>(TypesOfServices type, T service) where T : IGameService
        {
            if(!services.ContainsKey(type))
            {
                services.Add(type, service);
            }
        }

        public T GetService<T>(TypesOfServices type) where T : class, IGameService
        {
            if(!services.ContainsKey(type))
            {
                Debug.LogError("Services Does not Exixts");
                return null;
            }
            return (T)services[type];
        }
    }
}