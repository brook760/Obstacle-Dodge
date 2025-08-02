using UnityEngine;

public class StickToPlatform : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Ensure physics interactions are smooth
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            // Get platform velocity and adjust the ball's velocity smoothly
            MovingPlatform platform = collision.gameObject.GetComponent<MovingPlatform>();

            if (platform != null)
            {
                // Preserve the ball's vertical velocity (for jumping etc)
                Vector3 currentVelocity = rb.linearVelocity;
                Vector3 platformVelocity = platform.platformVelocity;

                // Blend the velocities smoothly
                    rb.linearVelocity = new Vector3(
                    Mathf.Lerp(currentVelocity.x, platformVelocity.x, 0.5f),
                    currentVelocity.y,
                    Mathf.Lerp(currentVelocity.z, platformVelocity.z, 0.5f)
                );
            }
        }
    }
}