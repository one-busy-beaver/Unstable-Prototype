using UnityEngine;

public abstract class GatedInteraction: MonoBehaviour
{
    [SerializeField] public bool autoTrigger = false; 

    // Add this so child classes can define what happens on interaction
    public abstract void Execute();
}