using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    private Renderer renderer;

    public float changeSpeed = 3f;

    public Color color1;
    public Color color2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = Mathf.PingPong(Time.time, changeSpeed);
        renderer.material.color = Color.Lerp(color1, color2, step);
    }
}
