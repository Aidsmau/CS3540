using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 startPosition;
    public GameObject target;
    public float minDistance;
    public float rotationSpeed;
    public float zoomSpeed;
    
    private Vector3 minPosition;
    private bool isZoomed = false;

    void Start()
    {
        minPosition = new Vector3(0, 0, minDistance);
        startPosition = transform.position;
        
        var objects = GameObject.FindGameObjectsWithTag("Sun");
        if (target == null && objects.Length == 0)
        {
            Debug.LogWarning("There is no Sun object in the scene. Either assign the sun object in the inspector or change its tag to Sun");
            return;
        }
        else if (target == null)
        {
            target = objects[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isZoomed = !isZoomed;
        }
        
        if (isZoomed)
        {
            Zoom();
        }
        else
        {
            ZoomOut();
        }
        
        transform.LookAt(target.transform.position);
    }

    void Zoom()
    {
        transform.position = Vector3.MoveTowards(transform.position, minPosition, zoomSpeed * Time.deltaTime); 
        transform.RotateAround(target.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void ZoomOut()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, zoomSpeed * Time.deltaTime);
    }
}