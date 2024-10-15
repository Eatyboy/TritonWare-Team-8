using UnityEngine.Events;

public static class EventManager
{
    public static class Events
    {
        public static UnityEvent PlayerLevelUp = new UnityEvent();
        public static UnityEvent ChoosePerk = new UnityEvent();
    }

    static public void InvokeEvent(UnityEvent anUnityEvent)
    {
        anUnityEvent?.Invoke();
    }

    static public void RegisterToEvent(UnityEvent anEvent, UnityAction aCall)
    {
        anEvent.AddListener(aCall);
	}

    static public void UnregisterFromEvent(UnityEvent anEvent, UnityAction aCall)
    {
        anEvent.RemoveListener(aCall);
	}
}