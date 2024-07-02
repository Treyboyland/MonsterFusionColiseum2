using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventSingleVar<T> : ScriptableObject
{
    List<GameEventListenerSingleVar<T>> listeners = new List<GameEventListenerSingleVar<T>>();

    public T Value;

    public void AddListener(GameEventListenerSingleVar<T> listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void RemoveListener(GameEventListenerSingleVar<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Invoke()
    {
        foreach (var listener in listeners)
        {
            listener.Response.Invoke(Value);
        }
    }

    public void Invoke(T value)
    {
        Value = value;
        Invoke();
    }
}

public class GameEventListenerSingleVar<T> : MonoBehaviour
{
    [SerializeField]
    GameEventSingleVar<T> gameEvent;

    public UnityEvent<T> Response;

    protected virtual void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    protected virtual void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }
}