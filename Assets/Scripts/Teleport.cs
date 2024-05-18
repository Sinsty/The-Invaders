using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform TeleportPoint;

    private void OnTriggerStay (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.position = TeleportPoint.transform.position;
        }
        
    }
}
