using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Scripting.APIUpdating;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;

    public AudioClip jumpSFX;
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
            PlaySound();
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

    void PlaySound() {
        if(jumpSFX) {
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = jumpSFX;
        audioSource.Play();
        }
    }
}
