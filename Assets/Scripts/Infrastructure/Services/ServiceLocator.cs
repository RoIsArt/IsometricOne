using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Android.Gradle.Manifest;

namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Container => _instance ?? (_instance = new ServiceLocator());
        public void RegisterSingle<TService>(TService service) where TService : IService => 
            Implementation<TService>.Service = service;

        public TService Single<TService>() where TService : IService => 
            Implementation<TService>.Service;

        private static class Implementation<TService> where TService : IService
        {
            public static TService Service;
        }
    }
}
