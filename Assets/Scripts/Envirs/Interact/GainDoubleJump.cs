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
            PlayerMovements player = other.GetComponent<PlayerMovements>();
            
            if (player != null)
            {
                player.SetMaxAirJumps(1);

                if (panel != null)
                {
                    panel.GetComponent<UIMessageTrigger>().ShowMessage();
                }

                Destroy(gameObject);
            }
        }
    }
}
