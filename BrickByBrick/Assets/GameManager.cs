using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int Coin;
    public int Health;
    public static event Action CompletedLevelEvent;
    public static event Action GameOverEvent;
    public static event Action<int> CoinCollectedEvent;
    public static event Action<int> HealthChangedEvent;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void NextLevel()
    {
        CompletedLevelEvent?.Invoke();
    }

    public void PlayerDead()
    {
        GameOverEvent?.Invoke();
    }

    public void CollectCoin()
    {
        Coin++;
        CoinCollectedEvent?.Invoke(Coin);
    }

    public void ChangeHealth()
    {
        Health--;
            HealthChangedEvent?.Invoke(Health);
        if (Health <= 0)
        {
            PlayerDead();
        }
    }
}
