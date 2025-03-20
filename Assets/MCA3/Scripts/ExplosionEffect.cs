using UnityEngine;

public class ExplosionEffect : MonoBehaviour 
{

    public float forceMagnitude = 100f;

    public float explosionRadius = 5f;
    void Start() {
        Reducto();
    }

    void Reducto() {
       Rigidbody [] pieces = GetComponentsInChildren<Rigidbody>();

       foreach(Rigidbody rb in pieces){
            rb.AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
       }

       Debug.Log("Rigidbodies: " + pieces);
    }
}