using Unity.VisualScripting;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;
    public float airControlMultiplier = 0.5f;
    private Transform groundCheck;
    [SerializeField] float groundDistance = 0.3f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float stopSmoothness = 0.9f;
    
    private Rigidbody rb;
    [SerializeField] private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // find child named "GroundCheck"
        groundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        // Check if Sphere is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // read input
        float xValue = Input.GetAxis("Horizontal");
        float zValue = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(xValue, 0f,zValue).normalized;

        //Determine move force based on grounded state
        float forceMultiplier = isGrounded ? 1f : airControlMultiplier;
        Vector3 move = input * forceMultiplier;
        if (move.sqrMagnitude > 0.01f * 0.01f)
        {
            
            rb.AddForce(move * moveSpeed, ForceMode.Force);
        }
        else
        {
            Vector3 horizontalVelocity = new(rb.linearVelocity.x, 0f,
                rb.linearVelocity.z);
            horizontalVelocity *= stopSmoothness; //smooth damping
            rb.linearVelocity = new Vector3(horizontalVelocity.x,
                rb.linearVelocity.y, horizontalVelocity.z);
            rb.angularVelocity *= stopSmoothness;
        }
        if(Input.GetKey(KeyCode.Space)&& isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //transform.Translate(xValue, 0, zValue); 
    }
}
