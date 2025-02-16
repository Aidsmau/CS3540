using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    public bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    void Update() 
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movenment = new Vector3(horizontal, 0, vertical).normalized;

        rb.AddForce(movenment * speed);
    }

    void Jump() 
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    
    }

    void OnCollisionEnter(Collision collison)
    {
        ContactPoint contact = collison.contacts[0];

        if(contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
