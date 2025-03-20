using UnityEngine;

public class ThirdPersonConrtroller : MonoBehaviour {
    

    public float speed = 10f;
    public float jumpHeight = 0.4f;
    public float gravity = 9.81f;

    public float airControl = 10f;

    public Transform cameraTransform;

    public float rotationSpeed = 5f;

    Vector3 input;
    Vector3 moveDirection;
    CharacterController controller;
    Animator animator;
    int animState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if(!cameraTransform)
            cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input = new Vector3(moveHorizontal, 0f, moveVertical);
        input.Normalize();

        if(controller.isGrounded) {
            moveDirection = input;

            if(input.magnitude >= 0.1f) {
                animState = 1;
                
                float rotationAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
                Quaternion smoothRotation = Quaternion.Euler(0f, rotationAngle, 0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, smoothRotation, Time.deltaTime * rotationSpeed);

                Vector3 moveDir = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

                moveDirection = moveDir.normalized * rotationSpeed;

                if(Input.GetButtonDown("Fire1")) {
                    animState = 3;
                }
            }
            else {
                //animState = 0;
                if(Input.GetButtonDown("Fire1")) {
                    animState = 4;
                }
            }

            if (Input.GetButton("Jump")) {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
                animState = 2;
            }
            else {
                moveDirection.y = 0.0f;
            }
        }
        else {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }

        animator.SetInteger("animState", animState);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}