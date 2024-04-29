using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum MyEventType
{
    ReStart,
    PlayerChangeObject
}

public class EventCentreManager : NormSingleton<EventCentreManager>
{
    private static readonly IDictionary<MyEventType, UnityEvent> Events =
        new Dictionary<MyEventType, UnityEvent>(); //Events字典装有若干个事件，一一对应事件类型，
        
    public void Subscribe(MyEventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent; //事件
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(listener); //向事件中添加函数
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Events.Add(eventType, thisEvent);
        }
    }

    public void Unsubscribe(MyEventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public void Publish(MyEventType eventType)
    {
        UnityEvent thisEvent;
        if (Events.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.Invoke();
        }
        else
        {
            Debug.LogError(eventType + "isn't Exist");
        }
    }
}
