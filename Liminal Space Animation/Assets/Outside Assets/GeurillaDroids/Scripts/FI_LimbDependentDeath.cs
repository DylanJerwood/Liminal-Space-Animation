using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_LimbDependentDeath : MonoBehaviour
{
    //Use this code to trigger an animation, after a specific destructible limb has been fully destroyed. You can activate a ragdoll using the same method.
    //for example, in the demo scene, this is used to play a death animation, once the Droids head has been removed, which contains it's brain, or computer.

    public Animator animator;

    [Tooltip("You can stop the characters root motion if this is true. For example when the core is destroyed, and the entire robot is just free-falling pieces.")]
    public bool deactivateAnimatorInstead = false;
    public string animationTrigger = "Animation Trigger";
    public FI_DestructibleLimb limb;
    bool deathChecked = false;

    void Start()
    {
        //Make sure we don't activate the death dependency immediately
        deathChecked = false;
    }

    void Update()
    {
        if (!deathChecked)
        {
            //check for the limb being broken
            if (limb.limbBroken)
            {
                deathChecked = true;

                if (deactivateAnimatorInstead)
                {
                    if (animator != null)
                        animator.enabled = false;

                    return;
                }

                //--Change this section to activate a ragdoll instead if you like, or something else
                if (animator != null)
                    animator.SetTrigger(animationTrigger);
                //--

                //setting this bool here will prevent this code from running anything anymore
            }
        }
    }
}
