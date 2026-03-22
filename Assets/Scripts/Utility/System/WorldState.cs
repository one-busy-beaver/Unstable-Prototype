using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public static WorldState Instance;
    private HashSet<CollectID> collectedItems = new HashSet<CollectID>();
    private HashSet<EventID> worldEvent = new HashSet<EventID>();

    // An event that other scripts can subscribe to
    public static event Action<EventID, bool> OnStateChanged;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void MarkAsCollected(CollectID id) => collectedItems.Add(id);
    public bool IsCollected(CollectID id) => collectedItems.Contains(id);

    public void SetFlag(EventID eventName, bool value) {
        if (value) worldEvent.Add(eventName);
        else worldEvent.Remove(eventName);

        // Notify anyone listening that a state changed
        OnStateChanged?.Invoke(eventName, value);
    }

    public bool GetFlag(EventID eventName) => worldEvent.Contains(eventName);
}