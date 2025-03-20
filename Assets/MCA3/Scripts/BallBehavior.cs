using UnityEngine;
using System.Collections;

public class BallBehavior : MonoBehaviour
{
    public float ballSpeed = 1f; // Speed of the ball
    public int limit = 5; // Maximum number of targets to attack
    private GameObject[] enemies; // Array of Dementor enemies
    public Transform target; // Current target
    private int targetsHit = 0; // Number of targets hit so far
    private Rigidbody rb; // Rigidbody of the ball
    private AudioSource audioSource; // AudioSource for sound effects

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // Set up sound effects
        audioSource.Play(); // Start playing the sound

        // Find all Dementor enemies
        enemies = GameObject.FindGameObjectsWithTag("Dementor");

        // Start attacking
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (targetsHit < limit)
        {
            // Select a random target
            SelectTarget();

            // Move towards the target
            while (target != null && Vector3.Distance(transform.position, target.position) > 0.1f)
            {
                MoveTowardsTarget();
                yield return null;
            }

            // If the target is destroyed, select a new one
            if (target == null)
            {
                if (enemies.Length > 0)
                {
                    SelectTarget();
                }
                else
                {
                    // If no enemies are left, self-destruct
                    Destroy(gameObject);
                    yield break;
                }
            }

            // Increment targets hit
            targetsHit++;
        }

        // Self-destruct after reaching the attack limit
        Destroy(gameObject);
    }

    void SelectTarget()
    {
        // Update enemies array in case some were destroyed
        enemies = GameObject.FindGameObjectsWithTag("Dementor");

        // Check if there are any enemies left
        if (enemies.Length == 0)
        {
            // If no enemies are left, self-destruct
            Destroy(gameObject);
            return;
        }

        // Randomly select a target from the enemies array
        int i = Random.Range(0, enemies.Length);
        target = enemies[i].transform;
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            // Calculate direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Apply force to move towards the target
            rb.AddForce(direction * ballSpeed, ForceMode.VelocityChange);
        }
        else
        {
            // If target is null, select a new one
            SelectTarget();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the ball hits an enemy, destroy it
        if (collision.gameObject.CompareTag("Dementor"))
        {
            Destroy(collision.gameObject);
            target = null; // Reset target

            // Select a new target if there are remaining enemies
            if (enemies.Length > 0)
            {
                SelectTarget();
            }
        }
    }
}
