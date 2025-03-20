using UnityEngine;

public class SpinBehavior : MonoBehaviour
{

    public float rotateSpeed = 5f;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
