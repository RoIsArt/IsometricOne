using System;
using Infrastructure.Services;

namespace GameEvents
{
    public interface IEventBus : IService
    { 
        void Subscribe<T>(Action<T> action);
        void Unsubscribe<T>(Action<T> action);
        void Invoke<T>(T signal);
    }
}