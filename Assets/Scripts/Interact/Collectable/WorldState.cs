using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public static WorldState Instance;
    private HashSet<CollectID> collectedItems = new HashSet<CollectID>();

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
}