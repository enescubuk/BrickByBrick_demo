using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlPoolManager : MonoBehaviour, ITriggerable
{
    private GameObject player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Trigger()
    {
        player?.GetComponent<PlayerManager>().TakeDamage();
    }
}
