using UnityEngine;

public class TileBehavior : MonoBehaviour
{
    public Material highlightMat;
    Renderer renderer;
    public GameObject towerPrefab;

    GameObject tileTower;

    Material originalMat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalMat = renderer.material;
    }

    void OnMouseOver() {
        HighlightTile();
    }

    void OnMouseExit() {
        if(!tileTower)
        {
            renderer.sharedMaterial = originalMat;
        }
    }

    void OnMouseDown() {
        if(!tileTower)
        {if(towerPrefab)
            {
                var tower = Instantiate(towerPrefab, transform.parent.position, transform.parent.rotation);
                tileTower = tower;
            }
        }
    }

    void HighlightTile(){
        if(highlightMat)
            renderer.sharedMaterial = highlightMat;
    }
}
