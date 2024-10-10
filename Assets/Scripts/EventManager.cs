using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static class Events
    {
        public static UnityEvent PlayerLevelUp = new UnityEvent();
    }

    static public void InvokeEvent(UnityEvent anUnityEvent)
    {
        anUnityEvent?.Invoke();
    }

    static public void RegisterToPlayerLevelUp(UnityAction aCall)
    {
        Events.PlayerLevelUp.AddListener(aCall);
	}

    static public void UnregisterFromPlayerLevelUp(UnityAction aCall)
    {
        Events.PlayerLevelUp.RemoveListener(aCall);
	}
}