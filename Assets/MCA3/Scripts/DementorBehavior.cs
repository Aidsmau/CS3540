using UnityEngine;

public class DementorBehavior : MonoBehaviour
{

    public Transform player;
    public float moveSpeed = 5f;
    public float minDistance = 1f;
    public GameObject lootPrefab;

    public int damageAmount = 10;

    public bool randomSpeed = false;
    public GameObject particleEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(randomSpeed) {
            moveSpeed = Random.Range(2.0f ,5.0f);
        }
        if (!player){
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!player) {
            return;
        }
        float step = moveSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > minDistance){
            transform.LookAt(player);
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

     void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Projectile") || other.CompareTag("Quaffle") || other.CompareTag("Bludger") || other.CompareTag("Protego")) {
            DestroyDementor();
        }
        else if(other.CompareTag("Player")) {
            var playerHealth = other.transform.GetComponent<PlayerHealth>();

            if(playerHealth) {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }

    void DestroyDementor() {
        if(particleEffect) {
        Instantiate(particleEffect, transform.position, transform.rotation);   
        }
        
        if(lootPrefab) {
            Instantiate(lootPrefab, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
