using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalBus
{
    private class Event<T> : UnityEvent<T> { }
    private static readonly Dictionary<System.Type, object> evDictionary = new Dictionary<System.Type, object>();

    /// <summary>
    /// Method to subscribe to a certain event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="listener"></param>
    public static void Subscribe<T>(UnityAction<T> listener)
    {
        object thisEvent;
        if (evDictionary.TryGetValue(typeof(T), out thisEvent))
            ((UnityEvent<T>)thisEvent).AddListener(listener);
        else
        {
            var ev = new Event<T>();
            ev.AddListener(listener);
            evDictionary.Add(typeof(T), ev);
        }
    }

    /// <summary>
    /// Method to unsubscribe to a certain event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="listener"></param>
    public static void Unsubscribe<T>(UnityAction<T> listener)
    {
        object thisEvent;
        if (evDictionary.TryGetValue(typeof(T), out thisEvent))
            ((UnityEvent<T>)thisEvent).RemoveListener(listener);
    }

    /// <summary>
    /// Method to send out an event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    public static void Broadcast<T>(T context) where T : struct
    {
        object thisEvent;
        if (evDictionary.TryGetValue(typeof(T), out thisEvent))
            ((UnityEvent<T>)thisEvent).Invoke(context);
    }

    /// <summary>
    /// Method to clean up all the events
    /// </summary>
    public static void CleanEvents() { evDictionary.Clear(); }
}
