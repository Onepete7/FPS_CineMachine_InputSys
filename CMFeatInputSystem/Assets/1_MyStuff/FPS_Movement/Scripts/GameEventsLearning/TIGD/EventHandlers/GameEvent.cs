using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CMFeatInputSystem/GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListenerMB> listeners = new List<GameEventListenerMB>();

    //Raise event through different methods signatures
    public void Raise(Component sender, object data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }
    }

    //Manage Listeners
    public void RegisterListener(GameEventListenerMB listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerMB listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
