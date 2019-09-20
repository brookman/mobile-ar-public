using System;
using System.Collections.Generic;
using UnityEngine;

public class AlertObservers : MonoBehaviour
{
    private List<Action<string>> callbacks = new List<Action<string>>();

    public void Subscribe(Action<string> callback)
    {
        callbacks.Add(callback);
    }

    public void Unsubscribe(Action<string> callback)
    {
        callbacks.Remove(callback);
    }

    public void Trigger(string message)
    {
        callbacks.ForEach(c => c?.Invoke(message));
    }
}