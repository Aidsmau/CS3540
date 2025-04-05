using UnityEngine;
using UnityEngine.UI;

public class TowerButtonBehavior : MonoBehaviour
{
    public int towerIndex;

    int towerCost;
    Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        towerCost = TowerBuilder.Instance.GetSelectedTowerCost(towerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(!MoneyManager.Instance){
            return;
        }
        if(MoneyManager.Instance.GetCurrentMoney() >= towerCost){
            button.interactable = true;
        }
        else {
            button.interactable = false;
        }
    }
}
