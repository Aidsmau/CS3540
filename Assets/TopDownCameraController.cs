using UnityEngine;
using UnityEngine.Rendering;

public class TopDownCameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float paddingSize = 20f;
    public float scrollSpeed = 3000f;

    public Vector2 panLimitX = new Vector2(-20, 20);
    public Vector2 panLimitZ = new Vector2(-20, 20);
    public Vector2 zoomLimitY = new Vector2(5, 20);

    bool controlCamera = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            controlCamera = !controlCamera;
        }
        if (!controlCamera)
        {
            return;
        }
        Vector3 pos = transform.position;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - paddingSize)
        {
            pos.z += (panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= paddingSize )
        {
            pos.z -= (panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= paddingSize)
        {
            pos.x -= (panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - paddingSize)
        {
            pos.x += (panSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * Time.deltaTime * scrollSpeed;

        pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
        pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);
        pos.y = Mathf.Clamp(pos.y, zoomLimitY.x, zoomLimitY.y);
        transform.position = pos;
    }
}
