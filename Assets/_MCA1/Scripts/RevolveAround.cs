using UnityEngine;

public class RevolveAround : MonoBehaviour
{
    public GameObject target;
    public float speed = 5f;

    void Start()
    {
        var objects = GameObject.FindGameObjectsWithTag("Sun");
        if (target == null && objects.Length == 0)
        {
            Debug.LogWarning(
                "There is no Sun object in the scene. Either assign the sun object in" +
                "the inspector or change its tag to Sun");
            return;
        }
        else if (target == null)
        {
            target = objects[0];
        }
    }
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
