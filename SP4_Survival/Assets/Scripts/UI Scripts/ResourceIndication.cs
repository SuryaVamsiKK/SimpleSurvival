using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceIndication : MonoBehaviour
{
    public float detctionrange;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).gameObject;
    }
    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < detctionrange)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(1).gameObject.SetActive(false);
        }

        this.transform.GetChild(1).LookAt(player.transform);

        this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = this.GetComponent<PickUp>().amount.ToString();
        this.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = this.GetComponent<PickUp>().typeOf.icon;
    }
}
