using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float duration = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, duration);   
    }

}
