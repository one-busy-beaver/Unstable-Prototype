using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainDoubleJump : MonoBehaviour
{
    public GameObject panel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerJump jumpScript = other.GetComponent<PlayerJump>();
            
            if (jumpScript != null)
            {
                jumpScript.SetMaxAirJumps(1);

                if (panel != null)
                {
                    panel.GetComponent<UIMessageTrigger>().ShowMessage();
                }

                Destroy(gameObject);
            }
        }
    }
}
