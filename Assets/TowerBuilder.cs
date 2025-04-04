using JetBrains.Annotations;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{

    public GameObject[] towers;
    int selectedTowerIndex = 0;
    bool selectedTower = false;

    public static TowerBuilder Instance { get; private set; }
    

    private void Awake()
    {
        if(Instance != null & Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

 

    public void SelectTower(int index)
    {
        if(index >= 0 && index < towers.Length)
        {
            selectedTowerIndex = index;
            selectedTower = true;
        }
        else
        {
            Debug.LogWarning("Invalid tower index");
            selectedTower = false;
        }
       
    }

    public GameObject GetSelectedTower()
    {
        return towers[selectedTowerIndex];
    }

    public bool HasTowerSelected()
    {
        return selectedTower;
    }

    public void ClearSelection()
    {
        selectedTower = false;
        selectedTowerIndex = -100;
    }


}
