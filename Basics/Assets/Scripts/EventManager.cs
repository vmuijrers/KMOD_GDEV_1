using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    ON_ENEMY_KILLED = 0,
    ON_ENEMY_HIT = 1,
}

public static class EventManager
{
    private static Dictionary<EventType, System.Action> eventDictionary = new Dictionary<EventType, System.Action>();

    public static void AddListener(EventType type, System.Action function)
    {
        if (!eventDictionary.ContainsKey(type))
        {
            eventDictionary.Add(type, null);
        }
        eventDictionary[type] += function;
    }

    public static void RemoveListener(EventType type, System.Action function)
    {
        if(eventDictionary.ContainsKey(type) && eventDictionary[type] != null)
        {
            eventDictionary[type] -= function;
        }
    }

    public static void RaiseEvent(EventType type)
    {
        if(eventDictionary.ContainsKey(type))
        {
            eventDictionary[type].Invoke();
        }
    }
}

public static class EventManager<T>
{
    private static Dictionary<EventType, System.Action<T>> eventDictionary = new Dictionary<EventType, System.Action<T>>();

    public static void AddListener(EventType type, System.Action<T> function)
    {
        if (!eventDictionary.ContainsKey(type))
        {
            eventDictionary.Add(type, null);
        }
        eventDictionary[type] += function;
    }

    public static void RemoveListener(EventType type, System.Action<T> function)
    {
        if (eventDictionary.ContainsKey(type) && eventDictionary[type] != null)
        {
            eventDictionary[type] -= function;
        }
    }

    public static void RaiseEvent(EventType type, T arg1)
    {
        if (eventDictionary.ContainsKey(type))
        {
            eventDictionary[type]?.Invoke(arg1);
        }
    }
}


