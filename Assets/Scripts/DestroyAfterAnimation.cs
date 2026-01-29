using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get the first animation clip length and destroy the game object after the animation ends
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

}
