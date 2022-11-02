using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_DroidStatistics : MonoBehaviour
{
    //Use this code on the parent of your character, and drag it into any destructible limbs you want to override values for



    [Tooltip("Drag this into the 'Stats' slot in the inspector of 'FI_DestructibleLimb' component to override that limb's Max Health, and any optimization timers used within it")]
    public int partsHealth = 5;

    [Tooltip("Drag this into the 'Stats' slot in the inspector of 'FI_DestructibleLimb' component to override that limb's Max Health, and any optimization timers used within it")]
    public int eraseDebrisTimeInSeconds = 10;
}
