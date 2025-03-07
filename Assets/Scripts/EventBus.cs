using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventBus : IService
{
    private Dictionary<string, List<Callback>> _callbacksForEvent = new Dictionary<string, List<Callback>>();

    public void Subscribe<T>(Action<T> action)
    {
        string key = typeof(T).Name;
        if (_callbacksForEvent.ContainsKey(key))
        {
            _callbacksForEvent[key].Add(new Callback(action));
        }
        else
        {
            _callbacksForEvent.Add(key, new List<Callback>() { new(action) });
        }
    }

    public void Invoke<T>(T signal)
    {
        string key = typeof(T).Name;
        if (_callbacksForEvent.ContainsKey(key))
        {
            foreach (var obj in _callbacksForEvent[key])
            {
                var callback = obj.Action as Action<T>;
                callback?.Invoke(signal);
            }
        }
        else
        {
            throw new Exception("Signal is not registed");
        }
    }

    public void Unsubscribe<T>(Action<T> action)
    {
        string key = typeof(T).Name;
        if (_callbacksForEvent.ContainsKey(key))
        {
            var callbackToDelete = _callbacksForEvent[key].FirstOrDefault(x => x.Action.Equals(action));
            if (callbackToDelete != null)
            {
                _callbacksForEvent[key].Remove(callbackToDelete);
            }
        }
        else
        {
            throw new Exception("Trying to unsubscribe for not existing key!");
        }
    }
}
