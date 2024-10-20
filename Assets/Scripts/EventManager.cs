using UnityEngine.Events;

public static class EventManager
{
    public static class Events
    {
        public static UnityEvent PlayerLevelUp = new();
        public static UnityEvent ChoosePerk = new();
        public static UnityEvent<float> EnemyDie = new();
    }

    static public void InvokeEvent(UnityEvent anUnityEvent)
    {
        anUnityEvent?.Invoke();
    }

    static public void InvokeEvent<T>(UnityEvent<T> anUnityEvent, T aValue)
    {
        anUnityEvent?.Invoke(aValue);
    }

    static public void RegisterToEvent(UnityEvent anEvent, UnityAction aCall)
    {
        anEvent.AddListener(aCall);
	}

    static public void RegisterToEvent<T>(UnityEvent<T> anEvent, UnityAction<T> aCall)
    {
        anEvent.AddListener(aCall);
	}

    static public void UnregisterFromEvent(UnityEvent anEvent, UnityAction aCall)
    {
        anEvent.RemoveListener(aCall);
	}
    
    static public void UnregisterFromEvent<T>(UnityEvent<T> anEvent, UnityAction<T> aCall)
    {
        anEvent.RemoveListener(aCall);
	}
}