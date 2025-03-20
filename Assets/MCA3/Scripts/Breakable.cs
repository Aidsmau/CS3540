using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    public GameObject cratePieces;
    public GameObject chest;
    public GameObject potion;
    public bool hidingChest;

    public bool hidingPotion;

    void Start() {
        
    }
    void OnCollisionEnter(Collision collision) {
        if(hidingChest)
            Instantiate(chest, transform.position, transform.rotation);

        if(hidingPotion) {
            Instantiate(potion, transform.position, transform.rotation);
        }
        
        Instantiate(cratePieces, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
}
