using UnityEngine;


public class CastShield : MonoBehaviour
{
    public GameObject shield;

    void Update()
    {
        if (!shield)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            shield.SetActive(true);
        }
    }

}