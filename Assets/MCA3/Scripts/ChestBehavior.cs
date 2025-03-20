using UnityEngine;
using System.Collections;

public class ChestBehavior : MonoBehaviour
{
    public AudioClip openingSFX;
    public Transform lid;
    public GameObject balls;

    private RoomManager roomManager;

    private Quaternion startRotation;
    private Quaternion endRotation;
    public Collider collider;

    void Start() {
        startRotation = lid.rotation;
        endRotation =  Quaternion.Euler(90, 0, 0);
        roomManager = GameObject.FindWithTag("RoomManager").GetComponent<RoomManager>();
        collider.enabled = false;
        StartCoroutine(ActivateCollider());

    }

    void OnCollisionEnter(Collision collision) {
            OpenChest();
    }

    void OpenChest(){
        if(openingSFX){
            AudioSource.PlayClipAtPoint(openingSFX, transform.position);
        }

        lid.rotation = Quaternion.Slerp(startRotation, endRotation, 1);
        balls.SetActive(true);
        roomManager.chestOpened = true;
    }



    IEnumerator ActivateCollider(){

        yield return new WaitForSeconds(1);
        collider.enabled = true;
    }

}
