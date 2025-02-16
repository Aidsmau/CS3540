using UnityEngine;

public class PlayerController : MonoBehaviour
{

    RigidBody rb;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<RigidBody>();
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
}
