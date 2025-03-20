using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SnitchBehavior : MonoBehaviour
{
    public float riseHeight = 2f; // Height to rise before orbiting
    public float orbitSpeed = 2f; // Speed of orbiting around the player
    public float approachSpeed = 5f; // Speed of approaching the player
    public float stoppingDistance = 1f; // Distance to stop in front of the player
    public Transform playerTransform; // Reference to the player

    public Transform targetPosition; // Target position for rising
    private Rigidbody rb; // Rigidbody of the Snitch
    private bool isOrbiting = false; // Flag to check if the Snitch is orbiting
    private bool enemiesDestroyed = false; // Flag to check if all enemies are destroyed

    void Start()
    {
        // Initialize components
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        targetPosition = new GameObject().transform;

        // Start rising and orbiting
        StartCoroutine(RiseAndOrbit());
    }

    IEnumerator RiseAndOrbit()
    {
        // Calculate target position for rising
        targetPosition.position = transform.position + new Vector3(0, riseHeight, 0);

        // Move towards the target position
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
        {
            MoveTowardsTarget(targetPosition);
            yield return null;
        }

        // Start orbiting
        isOrbiting = true;
        while (isOrbiting)
        {
            OrbitPlayer();
            yield return null;
        }
    }

        void MoveTowardsTarget(Transform target)
        {
        // Move towards the target position smoothly
        transform.position = Vector3.MoveTowards(transform.position, target.position, approachSpeed * Time.deltaTime);
        }

    void OrbitPlayer()
    {
        if (playerTransform != null)
        {
            transform.RotateAround(playerTransform.position, Vector3.up, orbitSpeed * Time.deltaTime);

            // Face the player
            transform.LookAt(playerTransform);
        }
        else
        {
            Debug.LogError("Player transform is null.");
        }
    }

    void StopOrbiting()
    {
        isOrbiting = false;
        StartCoroutine(ApproachPlayer());
    }

    IEnumerator ApproachPlayer()
    {
        // Calculate target position in front of the player
        targetPosition.position = playerTransform.position + playerTransform.forward * stoppingDistance;

        // Move towards the target position
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
        {
            MoveTowardsTarget(targetPosition);
            yield return null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If the Snitch is caught by the player, end the game
        if (collision.gameObject.CompareTag("Player") && enemiesDestroyed)
        {
            // Reload the scene to end the game
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else if(collision.gameObject.CompareTag("Projectile")) {
            // Reload the scene to end the game
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

    void Update()
    {
        // Check if all enemies are destroyed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Dementor");
        if (enemies.Length == 0)
        {
            enemiesDestroyed = true;
        }
        if (enemiesDestroyed)
        {
            StopOrbiting();
        }
    }
}
