using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusSpikeManager : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerManager>().TakeDamage();
        }
    }
}
