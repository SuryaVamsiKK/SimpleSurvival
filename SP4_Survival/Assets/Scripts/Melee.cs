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
            if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask)) {
                
                hit.transform.GetComponent<TreeFunctionality>().health -= weaponHolding.damage;
                hit.transform.GetComponent<TreeFunctionality>().GiveWood();
                //Debug.Log ("Did Hit");
            }
        }
    }

    void debugMelee () 
    {
        RaycastHit hit;

        if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask)) {
            Debug.DrawRay (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward) * hit.distance, Color.black);
            //Debug.Log ("Did Hit");
        }
        if (Physics.Raycast (transform.GetChild (1).position, transform.GetChild (1).TransformDirection (Vector3.forward), out hit, weaponHolding.meleeRange, layerMask)) {
            Debug.DrawRay (hit.point, transform.GetChild (1).TransformDirection (Vector3.forward) * weaponHolding.meleeRange, Color.green);
        }

    }
}