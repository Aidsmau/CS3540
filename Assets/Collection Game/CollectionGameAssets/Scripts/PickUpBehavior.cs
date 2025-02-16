using UnityEngine;

public class PickUpBehavior : MonoBehaviour
{

    public float rotSpeed = 5f;

    public static int pickUpCount = 0;

    public int scoreValue = 1;

    public static int totalScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickUpCount++;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate() 
    {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        DestroyPickUp();
    }

    void DestroyPickUp() {
        pickUpCount--;
        Destroy(gameObject);
    }
}
