using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OctopusManager : MonoBehaviour
{
    public bool canHit;
    public float Threshold;
    private Vector3 position;
    public GameObject EffectObject;
    public float effectOffset = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canHit = true;
            position = CalculateEffectPosition(other.transform.position); 
            Debug.Log(position);
            StartCoroutine(DelayedHit(Threshold)); 
        }
    }

    private Vector3 CalculateEffectPosition(Vector3 playerPosition)
    {
        Vector3 relativePos = playerPosition - transform.position;
        Vector3 effectDirection = DetermineEffectDirection(relativePos);
        return transform.position + effectDirection * effectOffset;
    }

    private Vector3 DetermineEffectDirection(Vector3 relativePos)
    {
        if (Mathf.Abs(relativePos.x) > Mathf.Abs(relativePos.z))
        {
            return relativePos.x > 0 ? Vector3.right : Vector3.left;
        }
        else
        {
            return relativePos.z > 0 ? Vector3.forward : Vector3.back;
        }
    }

    private IEnumerator DelayedHit(float delay)
{
    yield return new WaitForSeconds(delay);
    GameObject instantiatedObject = Instantiate(EffectObject, transform.position, Quaternion.identity, transform);
    // İlk konuma doğru hızlı bir şekilde hareket et
    Sequence sequence = DOTween.Sequence();
    sequence.Append(instantiatedObject.transform.DOMove(position, 0.1f).SetEase(Ease.OutQuad));
    // 1 saniye bekle
    sequence.AppendInterval(1f);
    // Orijinal pozisyona geri dön
    sequence.Append(instantiatedObject.transform.DOMove(transform.position, 0.1f).SetEase(Ease.InQuad));
    // Hareket tamamlandığında nesneyi yok et
    sequence.OnComplete(() => Destroy(instantiatedObject));

    if (canHit)
    {
        Debug.Log("Octopus Hit");
    }
}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canHit = false;
        }
    }
}
