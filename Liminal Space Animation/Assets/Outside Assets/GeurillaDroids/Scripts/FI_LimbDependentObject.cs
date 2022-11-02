using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_LimbDependentObject : MonoBehaviour
{
    //Use this code to deactivate an object depending on a destructible limb, that it needs to be fully in tact
    //For example, in the demo scene, this is used to deactivate the lights on the head, so it doesn't look strange when a piece has been removed
    
    public FI_DestructibleLimb limb;

    void Update()
    {
        //check the health of the limb, and deactivate this entire object if it isn't fully intact
        if (limb.health != limb.maxHealth)
            gameObject.SetActive(false);
    }
}
