using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;
    public float speed = 2f;
    public float distance = 5f; // How far the platform moves before reversing

    private Vector3 startPosition;
    private Vector3 lastPosition;
    public Vector3 platformVelocity { get; private set; }
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Move platform back and forth between start position and target position
        float step = speed * Time.deltaTime;
        Vector3 targetPosition = movingForward ?
            startPosition + moveDirection * distance :
            startPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Check if reached target and need to reverse
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            movingForward = !movingForward;
        }

        // Calculate velocity for the ball to use
        platformVelocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Parent the ball to the platform when it lands
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Unparent the ball when it leaves
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}