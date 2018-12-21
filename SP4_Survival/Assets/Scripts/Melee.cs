using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour {

    public MeleeWeapons weaponHolding;
    public LayerMask layerMask = 1 << 8;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () 
    {
        debugMelee ();

        RaycastHit hit;

        if (Input.GetKeyDown (GetComponent<Controls>().meleeAttack)) 
        {
            if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask))
            {
                if (hit.transform.gameObject.tag == "Resource")
                {
                    hit.transform.GetComponent<ResourceFunctionality>().health -= weaponHolding.damage;
                    hit.transform.GetComponent<ResourceFunctionality>().GiveResources();
                }
                if (hit.transform.gameObject.tag == "Interactable")
                {
                    hit.transform.GetComponent<ScriptActiivation>().interacted = true;
                }
            }
        }
    }

    void debugMelee () 
    {
        RaycastHit hit;

        Debug.DrawRay(transform.GetChild(1).position, transform.GetChild(1).TransformDirection(Vector3.forward) * weaponHolding.meleeRange, Color.black);
        //if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask)) {
        //    Debug.DrawRay (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward) * hit.distance, Color.black);
        //}
        if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask)) {
            Debug.DrawRay (hit.point, transform.GetChild (1).TransformDirection (Vector3.forward) * weaponHolding.meleeRange, Color.green);
        }

    }
}