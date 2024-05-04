using UnityEngine;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    public float moveSpeed = 5f;
    public float swipeThreshold = 50f; // Minimum swipe mesafesi
    private Vector2 touchStart;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (!isMoving && Input.touchCount > 0)
        {
            DetectSwipe();
        }
    }

    private void DetectSwipe()
    {
        Touch touch = Input.GetTouch(0);
        
        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Vector2 swipeDelta = touch.position - touchStart;
            
            // Swipe sağa yapıldı mı kontrol edin
            if (swipeDelta.x > swipeThreshold && Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                MoveToTarget(Vector3.right);
            }
        }
    }

    void MoveToTarget(Vector3 direction)
    {
        if (direction != Vector3.right) return; // Sadece sağa kaydırma işlemine yanıt ver

        isMoving = true;
        targetPosition = rb.position + direction * 10f; // Sağa doğru belirli bir mesafe hareket et

        float distance = Vector3.Distance(rb.position, targetPosition);
        float duration = distance / moveSpeed;

        rb.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            isMoving = false;
            Debug.Log("Movement completed");
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        });
    }
}
