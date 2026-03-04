using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMaster : MonoBehaviour
{
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
}
