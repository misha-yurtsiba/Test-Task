using System;
using System.Collections.Generic;
using UnityEngine;
public class ServiceLocator
{
    private readonly Dictionary<Type, IService> _services = new();

    public static ServiceLocator Current { get; private set; }

    public static void Init() => Current = new ServiceLocator();

    public void Register<T>(T service) where T : IService
    {
        if (_services.ContainsKey(typeof(T)))
        {
            Debug.LogError($"Service {nameof(T)} was registered");
            return;
        }

        _services[typeof(T)] = service;
    }

    public T GetService<T> () where T : IService
    {
        if (!_services.ContainsKey(typeof(T)))
        {
            Debug.LogError($"{nameof(T)} not registered");
            throw new InvalidOperationException();
        }

        return (T)_services[typeof(T)];
    }
}
