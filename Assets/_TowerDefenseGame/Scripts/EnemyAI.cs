using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    NavMeshAgent agent;

    public enum EnemyState {Navigate, Attack, Die}
    
    [Header("General Settings")]
    public EnemyState currentState = EnemyState.Navigate;
    public Transform targetBase;

    [Header("Navigation Settings")]
    public Transform turret;
    public float rotationSpeed = 30f;
    public float range = 5f;
    
    [Header("Attacking Settings")]
    public bool canAttack = true;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2;

    float fireCooldown = 0;

    [Header("Death Settings")]
    public int health = 100;
    public GameObject destroyPrefab;
    Transform attackTarget;

    bool isEnemyDead = false;

    Quaternion originalTurretRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(turret)
            originalTurretRotation = turret.localRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState){
            case EnemyState.Navigate:
                Navigate();
                break;
            case EnemyState.Attack:
            if(canAttack)
                Attack();
            else
                currentState = EnemyState.Navigate;
            break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    void Navigate(){
        agent.SetDestination(targetBase.position);

        if(canAttack)
            LookForTower();

    if(turret)
        turret.localRotation = Quaternion.Slerp(turret.localRotation, originalTurretRotation, rotationSpeed * Time.deltaTime);
    }

    void Attack(){
        if(attackTarget == null || Vector3.Distance(transform.position, attackTarget.position) > range) {
            
            attackTarget = null;
            currentState = EnemyState.Navigate;
            return;
        }

        Vector3 direction = attackTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        turret.rotation = Quaternion.Slerp(turret.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        if(fireCooldown <= 0) {
            if(HasLineOfSight(attackTarget)) 
                Shoot(); 
            fireCooldown = 1f / fireRate;  
        }

        fireCooldown -= Time.deltaTime;

    }

    void Shoot() {
        var bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();

        if(bulletBehavior)
        {   var targetTowerTurret = attackTarget.transform.Find("Turret").transform;
            if(targetTowerTurret)
                bulletBehavior.SetTarget(targetTowerTurret);
            else
                bulletBehavior.SetTarget(attackTarget);
        }
    }


    void Die() {
        if(isEnemyDead)
            return;

        Debug.Log("State: Die");

        if(destroyPrefab)
          Instantiate(destroyPrefab, transform.position, transform.rotation);
        isEnemyDead = true;
        Destroy(gameObject, 1);
        
        agent.isStopped = true;
        
    }

    public void TakeDamage(int damage){
        health -= damage;

        if (health <= 0)
        {
            currentState = EnemyState.Die;
        }
    }


    void LookForTower() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach(Collider collider in colliders) {
            if(collider.CompareTag("Tower")){
                float distanceToEnemy = Vector3.Distance(transform.position, collider.transform.position);

                if(distanceToEnemy < shortestDistance) {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = collider.transform;
                }
            }
        }

        if(nearestEnemy) {
            attackTarget = nearestEnemy;
            currentState = EnemyState.Attack;
            return;
        }
    }

     private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Bullet")) {
            var bulletBehavior = other.gameObject.GetComponent<BulletBehavior>();
            TakeDamage(bulletBehavior.ReturnBulletDamage());
        }
    }

    bool HasLineOfSight(Transform target)
    {
        RaycastHit hit;
        Vector3 direction = (target.position - transform.position).normalized;
        if (Physics.Raycast(firePoint.position, direction, out hit, range))
        {
            if (hit.collider.CompareTag("Tower"))
            {
                Debug.Log("Tower in sight");
                return true;
            }
        }
        return false;
    }

}
