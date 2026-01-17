using System;
using System.Collections.Generic;
using Case.UnityWaveTest.EventBus;

public static class SimpleEventBus
{
    private static readonly Dictionary<Type, List<Delegate>> _listeners = new();

    public static void Subscribe<T>(Action<T> listener) where T : IEvent
    {
        var type = typeof(T);

        if (!_listeners.TryGetValue(type, out var listeners))
        {
            listeners = new List<Delegate>();
            _listeners[type] = listeners;
        }

        listeners.Add(listener);
    }

    public static void Unsubscribe<T>(Action<T> listener) where T : IEvent
    {
        var type = typeof(T);

        if (_listeners.TryGetValue(type, out var listeners))
        {
            listeners.Remove(listener);
        }
    }

    public static void Publish<T>(T evt) where T : IEvent
    {
        var type = typeof(T);

        if (!_listeners.TryGetValue(type, out var listeners))
            return;

        
        var snapshot = listeners.ToArray();

        foreach (var listener in snapshot)
        {
            ((Action<T>)listener)?.Invoke(evt);
        }
    }
}