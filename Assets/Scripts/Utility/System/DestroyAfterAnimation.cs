using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    void Start()
    {
        // Get the first animation clip length and destroy the game object after the animation ends
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

}