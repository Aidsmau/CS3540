using UnityEngine;

public class LootBehavior : MonoBehaviour {

    public int healthAmount = 5;
    public int spellAmount = 5;

    private GameObject camera;

    private ShootProjectile shootProjectile;

    public bool isHealth;
    public bool isSpell;
    public AudioClip lootSFX;

    void Start(){
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        shootProjectile = camera.transform.GetComponent<ShootProjectile>();
    }
    void Update() {

        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        if(transform.position.y < Random.Range(1.0f, 3.0f)){
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")) {
            var playerHealth = other.transform.GetComponent<PlayerHealth>();

            if(isHealth){
                if(playerHealth) {
                    if(lootSFX){
                    AudioSource.PlayClipAtPoint(lootSFX, transform.position);
                    }
                    playerHealth.TakeHealth(healthAmount);
                    Destroy(gameObject);
                }
            }
            else if(isSpell) {
                if(shootProjectile){
                    if(lootSFX){
                    AudioSource.PlayClipAtPoint(lootSFX, transform.position);
                    }
                    shootProjectile.TakeSpell(spellAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}