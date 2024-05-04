using UnityEngine;
using DG.Tweening;

public class SwipeMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 targetPosition;
    private float rayDistance = 10f;
    public float moveSpeed = 5f;
    public float swipeThreshold = 50f; // Minimum swipe mesafesi
    private Vector2 touchStart;
    private bool isMoving = false;
    [SerializeField, HideInInspector] private LayerMask wallLayer;
    private GameObject verticalTriggerObject;
    private float lastTapTime = 0f; 
    public float doubleTapThreshold = 0.3f;

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
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        // Eğer double tap olduysa, onu işle.
        if (touch.tapCount == 2)
        {
            if (Time.time - lastTapTime < doubleTapThreshold)
            {
                // Double tap olayını işleyin.
                GetComponent<PlayerManager>().ActivateShield();
                lastTapTime = Time.time; // Double tap zamanını güncelleyin.
            }
        }

        // Swipe kontrolü.
        else if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
            lastTapTime = Time.time; // Dokunmanın zamanını kaydedin.
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            Vector2 swipeDelta = touch.position - touchStart;

            // Eğer swipe minimum mesafeden daha büyükse, swipe'ı işle.
            if (swipeDelta.magnitude > swipeThreshold)
            {
                Vector3 direction = Vector3.zero;

                // Swipe yönünü belirleyin.
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    direction = swipeDelta.x > 0 ? Vector3.right : Vector3.left;
                }
                else
                {
                    direction = swipeDelta.y > 0 ? Vector3.forward : Vector3.back;
                }

                // Raycast kullanarak hedef pozisyonu belirleyin ve ilgili yönde hareketi başlatın.
                RaycastHit hit;
                if (Physics.Raycast(rb.position, direction, out hit, rayDistance, wallLayer))
                {
                    targetPosition = hit.point + direction * -0.5f;
                    if (hit.collider.GetComponent<ITriggerable>() != null)
                    {
                        verticalTriggerObject = hit.collider.gameObject;
                    }
                    else
                    {
                        verticalTriggerObject = null;
                    }
                }
                else
                {
                    targetPosition = rb.position + direction * rayDistance;
                }

                // Hedefe hareketi başlatın.
                MoveToTarget(direction);
            }
        }
    }
}

    void MoveToTarget(Vector3 direction)
    {
        isMoving = true;

        float distance = Vector3.Distance(rb.position, targetPosition);
        float duration = distance / moveSpeed;

        rb.DOMove(targetPosition, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            verticalTriggerObject?.GetComponent<ITriggerable>().Trigger();
            isMoving = false;
        });

        float tiltAngle = 10f;
        Quaternion targetRotation = Quaternion.identity;

        if (direction.x != 0)
        {
            float zTilt = direction.x > 0 ? tiltAngle : -tiltAngle;
            targetRotation = Quaternion.Euler(0, 0, zTilt);
        }
        else if (direction.z != 0)
        {
            float xTilt = direction.z > 0 ? -tiltAngle : tiltAngle;
            targetRotation = Quaternion.Euler(xTilt, 0, 0);
        }

        rb.DORotate(targetRotation.eulerAngles, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            Quaternion originalRotation = Quaternion.Euler(0, 0, 0);
            rb.DORotate(originalRotation.eulerAngles, duration).SetEase(Ease.Linear);
        });
    }
}
