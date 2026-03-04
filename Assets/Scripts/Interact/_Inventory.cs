using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private HashSet<string> _items = new HashSet<string>();

    private void Awake() 
    { 
        if (Instance == null) Instance = this; 
    }

    public void AddItem(string id) => _items.Add(id);
    public bool HasItem(string id) => _items.Contains(id);
}