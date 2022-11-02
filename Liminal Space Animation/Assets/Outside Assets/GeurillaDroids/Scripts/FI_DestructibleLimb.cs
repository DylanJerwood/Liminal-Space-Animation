using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_DestructibleLimb : MonoBehaviour
{
    #region Heads Up and Tips

    //            Bare in mind, this is only one simple way 
    //            to achieve this effect. There are many ways 
    //            to add gore, destruction, and mutilation to your 
    //            Unity games. Although this asset is very well
    //            performant on PC, it may not be the right method
    //            for your platform. In the "Models" folder you'll
    //            find a fully skinned Droid with LOD's, a fully
    //            skinned Droid with individual parts, and a fully 
    //            non-skinned Droid with individual parts. You can
    //            separate the prefabs and create whatever you need!

    //**********      PLEASE ENJOY!     ************//

    #endregion

    #region Variables

    [Tooltip("(Optional) You can use an override for 'Max Health', instead of setting each limbs 'Max Health'")]
    public FI_DroidStatistics stats;

    [Tooltip ("The amount of health to lose before the limb falls off entirely")]
    public int maxHealth = 3;
    public int health;

    [Tooltip("When this bool is set to true, the limb can no longer take damage")]
    public bool limbBroken = false;

    [Tooltip("(Optional) A list of smaller parts that make up the limb, these will randomly be broken off when non-lethal damage is taken")]
    public GameObject[] childParts;

    [Tooltip("(Optional) The corresponding parts to the destructible character. These should be listed in order, exactly the same as 'Child Parts', and will be deactivated for the effect.")]
    public GameObject[] characterParts;

    [Tooltip("(Optional) The corresponding limb(Armature Bone) to the destructible character. This transforms scale will be set to Vector3.zero upon losing the limb")]
    public GameObject characterLimb;

    [Tooltip("(Optional) An effect to be played upon taking damage")]
    public GameObject sparksPrefab;

    [Tooltip("(Optional) A simple script that randomizes a list of sound effects. In this case, the sound plays when damage is taken")]
    public FI_ClipRandomizer hitAudio;

    [Tooltip("(Optional) If any limbs should be removed along with this one, they should be listed here. For example, if this is a forearm, you should list the hand as a child limb.")]
    public FI_DestructibleLimb[] childLimbs;

    #endregion

    private void Awake()
    {
        //Override this parts max health if we've assigned stats in the inspector
        if (stats != null)
            maxHealth = stats.partsHealth;

        //Fill the parts health to max
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!limbBroken)
        {
            //Inflict the recieved damage
            health -= damage;

            //Play a clip if we're using the Clip Randomizer
            if (hitAudio != null)
                hitAudio.RandomizeClip();

            //Instantiate an object. By default, this is a custom sparks effect that is destroyed from the world in 3 seconds. You can find it in the Effects folder
            if (sparksPrefab != null)
                Instantiate(sparksPrefab, transform.position, Quaternion.identity);

            //If by inflicting damage, our health is now zero or less, call the DropLimb function, and stop this function.
            if (health <= 0)
            {
                DropLimb(true);
                return;
            }
            else
            {
                //otherwise, check our list of individual parts
                if (childParts != null)
                {
                    // Randomize through our list. As an array, the length of the list would be 1 higher than the maximum array element,-
                    // -however, using Random.Range will never select an integer of its maximum value, only lower values. You must use the arrays-
                    // -Length or an equal value, to randomize the entire list.
                    int partIndex = Random.Range(0, childParts.Length);

                    //if the part hasn't been erased for optimization
                    if (childParts[partIndex] != null)
                    {
                        GameObject part = childParts[partIndex];

                        //Activate the visibility of the broken piece
                        part.GetComponent<MeshRenderer>().enabled = true;

                        //Make sure the following process was never done to that piece before
                        if (!part.TryGetComponent(out Rigidbody rb))
                        {
                            //Here we add a rigidbody, and the most performant collider for simulated physics
                            part.AddComponent<Rigidbody>();
                            part.AddComponent<BoxCollider>();
                            //Adding an explosion force to the rigidbody component will give it the impression of impact, while randomizing spherically which direction the force will resonate
                            part.GetComponent<Rigidbody>().AddExplosionForce(350, transform.position, 1);

                            //unparent the object, so it is no longer bound to the transform of the Droids armature, releasing it into the game world to collide with objects on it's own
                            part.transform.SetParent(null);

                            //unparent the object, so it is no longer bound to the transform of the Droids armature, releasing it into the game world to collide with objects on it's own
                            if (part.TryGetComponent(out FI_DestroyMe dm))
                            {
                                //Override the parts destruction timer if we are using Stats, and start the timer
                                if (stats != null)
                                    dm.destroyTimer = stats.eraseDebrisTimeInSeconds;

                                dm.StartDestroyTimer();
                            }
                        }
                    }

                    //Finally, deactivate the corresponding skinned mesh on our animated character.
                    if (characterParts != null)
                    {
                        if (characterParts[partIndex] != null)
                            characterParts[partIndex].SetActive(false);
                    }
                }
            }
        }
    }

    public void DropLimb(bool dropChildren)
    {
        //Preparing to drop the limb, so make sure this process never happens twice. Easily done setting a bool
        limbBroken = true;

        //Double check that this process has never been done,
        if (!gameObject.TryGetComponent(out Rigidbody rb))
        {
            //and begin adding the requried components for simulated physics, once again adding a small explosion force, and unparenting the object from the Droid
            gameObject.AddComponent<Rigidbody>();
            gameObject.AddComponent<BoxCollider>();
            gameObject.GetComponent<Rigidbody>().AddExplosionForce(350, transform.position, 2);
            transform.SetParent(null);

            //override the optimization timer if we're using stats, and start the timer
            if (TryGetComponent(out FI_DestroyMe dm))
            {
                if (stats != null)
                    dm.destroyTimer = stats.eraseDebrisTimeInSeconds;

                dm.StartDestroyTimer();
            }

            //activate the visibility of this limb
            GetComponent<MeshRenderer>().enabled = true;

            //activate the visibility of any remaining pieces we haven't broken off
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
            if (children != null)
            {
                foreach (MeshRenderer mr in children)
                {
                    if (mr != null)
                        mr.enabled = true;
                }
            }

            //This bool can be used if you do not wish to remove any connected parts fully, for example if it is wire, pipe, or a tail that is connected to the character from both ends.
            //I probably was overthinking by adding this feature, but it might be cool to have those examples wave around while still attached to the character. Just an idea!
            if (dropChildren)
            {
                //Check for any child limbs we've assigned, and break them off to avoid floating objects around the character
                if (childLimbs != null)
                {
                    foreach (FI_DestructibleLimb limb in childLimbs)
                    {
                        if (limb != null)
                            limb.DropLimb(true);
                    }
                }
            }

            //Finally, set the armature's corresponding bones scale to 0. This will cause it, and all of it's dependently connected bones seemingly to disappear. Colliders on the bone and child objects of it,
            //are still active, but are for the most part useless. You will still want to deactivate any components you've attached to the corresponding heirarchy of the armature, so your game knows the limb
            //has been removed. The easiest way to check for this in your own code, is use the bool on this script - 'limbBroken'.
            if (characterLimb != null)
                characterLimb.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
