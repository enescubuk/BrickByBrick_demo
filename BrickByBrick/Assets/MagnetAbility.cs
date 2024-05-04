using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAbility : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            GetComponentInParent<PlayerManager>().CollectCoin();
            Destroy(other.gameObject);
        }
    }
}
