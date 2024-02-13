using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sideMoveDistance = 2f;
    public float jumpForce = 1f;
    public float swipeCooldown = 0.5f;
    private bool isGrounded=true;

    private Vector2 touchStartPos;
    private bool isSwipe;
    private float lastSwipeTime;

    private Vector3 initialPosition;
    public int coinCount;

    public Text winText;

    void Start()
    {
        initialPosition = transform.position;
        coinCount = 0;

    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        DetectSwipe();
    }

    void DetectSwipe()
    {
        isSwipe = false;

        float timeSinceLastSwipe = Time.time - lastSwipeTime;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    Vector2 swipeDelta = touch.position - touchStartPos;

                    if (swipeDelta.magnitude > 50f && timeSinceLastSwipe > swipeCooldown)
                    {
                        isSwipe = true;
                        touchStartPos = touch.position;
                        lastSwipeTime = Time.time;
                    }
                    break;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            if (Mathf.Abs(mouseDelta.x) > 0.1f && timeSinceLastSwipe > swipeCooldown)
            {
                isSwipe = true;

                MoveToSide(mouseDelta.x > 0 ? Vector3.right : Vector3.left);

                lastSwipeTime = Time.time;
            }
        }
    }

    void MoveToSide(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * sideMoveDistance;
        transform.position = targetPosition;
    }


    void ResetPosition()
    {
        transform.position = initialPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Collided with a coin!");
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), true);
            coinCount++;
            Destroy(collision.gameObject);

            if (coinCount == 8)
            {

            }
        }
        if (collision.gameObject.CompareTag("Obs"))
        {
            Debug.Log("Obs!");
            ResetPosition();
        }

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
