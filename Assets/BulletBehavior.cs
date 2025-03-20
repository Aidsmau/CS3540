using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 20f;
    public float rotationSpeed = 5f;
    public float lifetime = 5f;
    public int damage = 10;

    public GameObject bulletHitPrefab;

    private Transform target;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
            return;

        
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        rb.linearVelocity = transform.forward * speed;
    }

    public void SetTarget(Transform currentTarget){
        target = currentTarget;
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("Hit " + other.transform.name);
        if(bulletHitPrefab) {
            var pos = other.contacts[0].point;
            Instantiate(bulletHitPrefab, pos, Quaternion.identity);
        }
    }
}
