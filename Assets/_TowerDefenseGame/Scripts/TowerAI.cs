using UnityEngine;

public class TowerAI : MonoBehaviour
{
    public enum TowerState {Patrol, Attack, Die}
    public TowerState currentState = TowerState.Patrol;

    [Header("Patrol Settings")]
    public Transform turret;
    public float rotationSpeed = 30f;
    public float maxRotationAngle = 90f;
    public float range = 5f;
    
    [Header("Attacking Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2;

    float fireCooldown = 0;

    [Header("Death Settings")]
    public int health = 100;
    public GameObject destroyPrefab;

    bool isTowerDead = false;

    [Header("General Settings")]
    public GameObject buildPrefab;



    Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(buildPrefab)
                Instantiate(buildPrefab, transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState){
            case TowerState.Patrol:
                Patrol();
                break;
            case TowerState.Attack:
                Attack();
                break;
            case TowerState.Die:
                Die();
                break;
        }

    }

    void Patrol(){
        Debug.Log("State: Patrol");
        //turret.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        float angle = Mathf.PingPong(rotationSpeed * Time.time, maxRotationAngle * 2) - maxRotationAngle;
        turret.localRotation = Quaternion.Euler(0, angle, 0);

        LookForEnemies();
    }

    void LookForEnemies() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach(Collider collider in colliders) {
            if(collider.CompareTag("Enemy")){
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                if(distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = collider.transform;
                }
            }
        }

        if(nearestEnemy) {
            target = nearestEnemy;
            currentState = TowerState.Attack;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Attack() {
        Debug.Log("State: Attack");

        if(target == null || Vector3.Distance(transform.position, target.position) > range) {
            
            target = null;
            currentState = TowerState.Patrol;
            return;
        }

        //turret.LookAt(target);
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turret.rotation = Quaternion.Slerp(turret.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if(fireCooldown <= 0) {
            Shoot(); 
            fireCooldown = 1f / fireRate;  
        }

        fireCooldown -= Time.deltaTime;
        
    }

    void Shoot() {
        var bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();

        if(bulletBehavior)
            bulletBehavior.SetTarget(target);
    }

    public void TakeDamage(int damage){
        health -= damage;

        if (health <= 0)
        {
            currentState = TowerState.Die;
        }
    }

    void Die() {
        if(isTowerDead)
            return;

        Debug.Log("State: Die");

        if(destroyPrefab)
          Instantiate(destroyPrefab, transform.position, transform.rotation);
        isTowerDead = true;
        Destroy(gameObject, 1);
        
        
    }
}

