using UnityEngine;
using System.Collections;
using TMPro;


public class CastShield : MonoBehaviour
{
    public GameObject shield;
    public int shieldLimit = 3;
    public TextMeshProUGUI shieldCount;

    public AudioClip castSFX;
    public AudioClip failedSFX;

    void Start(){
        shield.SetActive(false);
        shieldCount.text = "" + shieldLimit;
    }

    void Update()
    {
        if (!shield)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q) && shieldLimit != 0)
        {
            AudioSource.PlayClipAtPoint(castSFX, transform.position);
            StartCoroutine(Shield());
            shieldLimit--;
        }
        else if(Input.GetKeyDown(KeyCode.Q) && shieldLimit == 0) {
            AudioSource.PlayClipAtPoint(failedSFX, transform.position);
        }
        shieldCount.text = "" + shieldLimit;
    }



    IEnumerator Shield(){
        shield.SetActive(true);
        yield return new WaitForSeconds(3);
        shield.SetActive(false);
    }

}