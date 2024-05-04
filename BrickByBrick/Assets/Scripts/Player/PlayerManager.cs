using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    internal bool shield;
    public int starCount = 0;

    void OnEnable()
    {
    }
    public void TakeDamage()
    {
        if (shield)
        {
            return;
        }
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        GameManager.Instance.ChangeHealth();
    }

    public void CollectCoin()
    {
        GameManager.Instance.CollectCoin();
    }
    public void CollectStar()
    {
        starCount++;
    }

    public void ActivateShield()
    {
        shield = true;
        GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 1); // Kırmızı renk

        Invoke("DeactivateShield", 10f);
    }

    private void DeactivateShield()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1); // Beyaz renk
        shield = false;
    }

    
}
