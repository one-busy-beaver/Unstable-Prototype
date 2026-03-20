// using UnityEngine;
// using Cinemachine;

// public class CameraTargetConnector : MonoBehaviour
// {
//     private CinemachineVirtualCamera vcam;

//     void Start()
//     {
//         vcam = GetComponent<CinemachineVirtualCamera>();
//     }

//     void Update()
//     {
//         // If the camera lost its target or never had one
//         if (vcam.Follow == null)
//         {
//             // Look for the player by Tag
//             GameObject player = GameObject.FindWithTag("Player");
            
//             if (player != null)
//             {
//                 vcam.Follow = player.transform;
//                 vcam.LookAt = player.transform;
//             }
//         }
//     }
// }