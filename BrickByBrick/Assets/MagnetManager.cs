using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetManager : MonoBehaviour
{
    private float rotationSpeed = 100f;
    public GameObject MagnetAbility;
    private void FixedUpdate()
    {
        Rotate();
    }
    
    private void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(MagnetAbility, other.transform);
            Destroy(gameObject);
        }
    }
}
