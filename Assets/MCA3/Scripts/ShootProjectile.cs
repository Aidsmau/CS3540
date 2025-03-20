using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShootProjectile : MonoBehaviour
{

    public GameObject patronusProjectile;
    public GameObject reductoProjectile;
    public GameObject defaultProjectile;
    public GameObject alohomoraProjectile;

    public Transform camera;

    public float projectileSpeed = 100;

    public float spellRange = 20;

    public AudioClip spellSFX;

    public TextMeshProUGUI spellCount;
    public int spellAmmo = 20;



    [Header("Reticle Settings")]
    public Image reticleImage;
    public Color targetColorDementor;
    public float animationSpeed = 5;

    Color originalReticleColor;
    Vector3 originalReticleScale;

    GameObject currentProjectile;

    Color currentReticleColor; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalReticleColor = reticleImage.color;
        originalReticleScale = reticleImage.transform.localScale;
        spellCount.text = "" + spellAmmo;

        if(defaultProjectile)
            currentProjectile = defaultProjectile;

        UpdateReticleColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
            Shoot();

        if(!reticleImage) {
            return;
        }
        InteractiveEffect();
    }

    void FixedUpdate(){
        if(!reticleImage) {
            return;
        }
        
    }

    void Shoot(){
        if(spellAmmo > 0) {
            spellAmmo--;
        if(currentProjectile){
            GameObject spell = Instantiate(currentProjectile, camera.position, camera.rotation);
        

        Rigidbody rb = spell.GetComponent<Rigidbody>();

        if(rb){
            rb.AddForce(camera.forward * projectileSpeed, ForceMode.VelocityChange);
        }
        if(spellSFX){
            AudioSource.PlayClipAtPoint(spellSFX, camera.position);
        }
        spell.transform.SetParent(camera);
        }
        }
        spellCount.text = "" + spellAmmo;
    }

    void InteractiveEffect() {
        RaycastHit hit;

        if(Physics.Raycast(camera.position, camera.forward, out hit, spellRange)) {
            Debug.Log("Hit Something: " + hit.collider.name);
            if(hit.collider.CompareTag("Dementor")) {
                currentProjectile = patronusProjectile;
                UpdateReticleColor();
                ReticleAnimation(originalReticleScale / 2, currentReticleColor, animationSpeed);
            }
            else if(hit.collider.CompareTag("Reducto")){
                currentProjectile = reductoProjectile;
                UpdateReticleColor();
                ReticleAnimation(originalReticleScale / 2, currentReticleColor, animationSpeed);

            }
            else if(hit.collider.CompareTag("Chest")) {
                currentProjectile = alohomoraProjectile;
                UpdateReticleColor();
                ReticleAnimation(originalReticleScale / 2, currentReticleColor, animationSpeed);
            }
        }
        else {
            currentProjectile = defaultProjectile; 
            UpdateReticleColor();
            ReticleAnimation(originalReticleScale, originalReticleColor, animationSpeed);
        }
    }

    void ReticleAnimation(Vector3 targetScale, Color targetColor, float speed) {
        var step = speed * Time.deltaTime;
        reticleImage.color = Color.Lerp(reticleImage.color, targetColor, step);
        reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, targetScale, step); 
    }

    void UpdateReticleColor() {
    currentReticleColor = currentProjectile.GetComponent<Renderer>().sharedMaterial.color;   
    }

    public void TakeSpell(int spellAmount) {
        spellAmmo += spellAmount;
        spellCount.text = "" + spellAmmo;

        
        Debug.Log("Spell taken " + spellAmmo);
    }
}