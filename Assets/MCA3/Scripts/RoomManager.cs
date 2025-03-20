using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{

    public bool chestOpened;
    public GameObject enemySpawners;
    public GameObject[] crates;

    public int numberOfPotions = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crates = GameObject.FindGameObjectsWithTag("Reducto");
        HideChest();
        HidePotions();
    }

    // Update is called once per frame
    void Update()
    {
        if(chestOpened) {
            enemySpawners.SetActive(false);
        }
    }

    void HideChest() {
        int index = RandomInt(0,crates.Length);
        crates[index].GetComponent<Breakable>().hidingChest = true;
    }

    void HidePotions(){
        int index = 0;
        int i = 0; 
        while(i < numberOfPotions) {
            index = RandomInt(0,crates.Length);
            if(crates[index].GetComponent<Breakable>().hidingChest == true) {
                index = RandomInt(0,crates.Length);
            }
            else if(crates[index].GetComponent<Breakable>().hidingPotion == true) {
                index = RandomInt(0,crates.Length);
            }
            else {
                crates[index].GetComponent<Breakable>().hidingPotion = true;
                i++;
            }
        }
    }

    int RandomInt(int start, int end) {
        return Random.Range(start,end);
    }
}
