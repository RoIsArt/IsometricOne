using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

    private static readonly ServiceLocator _instance;

    static ServiceLocator()
    {
        if (_instance == null)
        {
            _instance = new ServiceLocator();
        }
    }

    public static ServiceLocator Instance { get { return _instance; } }

    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            throw new InvalidOperationException($"{key} not registered with {GetType().Name}");
        }

        return (T)_services[key];
    }

    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            throw new InvalidOperationException($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
        }

        _services.Add(key, service);
    }

    public void Unregister<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            throw new InvalidOperationException($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
        }

        _services.Remove(key);
    }
}
