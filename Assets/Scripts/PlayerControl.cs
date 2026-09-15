using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Strictly sphere control: movement, jump, and what the sphere is currently touching.
public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float gravityScale = 1.5f; // default value, multiplies fall strength
    public float maxSpeed = 8f;       // horizontal speed cap
    public bool sphereGround = true;

    private Rigidbody rb;
    private bool jumpRequested;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVelocity.magnitude > maxSpeed)
        {
            Vector3 limited = flatVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(limited.x, rb.velocity.y, limited.z);
        }

        // Extra downward force so falls feel normal
        // gravityScale x normal gravity.
        rb.AddForce(Physics.gravity * (gravityScale - 1f), ForceMode.Acceleration);

        if (jumpRequested && sphereGround)
        {
            rb.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
            sphereGround = false;
        }
        jumpRequested = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("coin"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.CollectCoin();
        }

        if(other.gameObject.CompareTag("water"))
        {
            GameManager.Instance.RestartLevel();
        }

        if(other.gameObject.CompareTag("floor"))
        {
            sphereGround = true;
        }
    }
}
