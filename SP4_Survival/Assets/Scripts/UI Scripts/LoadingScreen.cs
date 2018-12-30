using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public bool loadGame;
    public Slider Bar;
    public float speed = 10f;

    void Start()
    {
        if (loadGame)
        {
            Bar.value = 0;
            this.transform.root.GetComponent<PlayerStats>().InventoryStatus = true;
            this.transform.parent.GetChild(0).gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
    }
    
    void Update()
    {
        if (loadGame)
        {
            Bar.value += Time.deltaTime * speed;
            if (Bar.value >= 100)
            {
                this.transform.root.GetComponent<PlayerStats>().InventoryStatus = false;
                this.transform.parent.GetChild(0).gameObject.SetActive(true);
                this.gameObject.SetActive(false);
                loadGame = false;
            }
        }
    }
}
