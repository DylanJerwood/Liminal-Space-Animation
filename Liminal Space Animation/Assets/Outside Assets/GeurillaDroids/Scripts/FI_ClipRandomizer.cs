using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_ClipRandomizer : MonoBehaviour
{
    [Tooltip("If you do not assign an audioSource, this script will try to use the one from this gameObject")]
    [SerializeField] AudioSource audioSource;

    [Tooltip("Assign at least one audio clip. You can use any amount more")]
    [SerializeField] AudioClip[] clips;

    [Tooltip("Plays a random clip once, as soon as this object is active. If you do not use this, you will have to call the function 'RandomizeClip' from wherever, and whenever you like.")]
    public bool useOnAwake = false;

    private void Awake()
    {
        //If no audio source has been assigned, try to pull one from this gameobject
        if (audioSource == null)
        {
            if (TryGetComponent(out AudioSource source))
            {
                audioSource = source;
            }
        }

        //if useOnAwake is checked, randomize an audio clip immediately
        if (useOnAwake)
            RandomizeClip();
    }

    public void RandomizeClip()
    {
        if (audioSource == null)
        {
            Debug.Log("You are trying to randomize an audio clip, but no audiosource has been assigned, or found on this gameObject. Please assign or add one.");
            return;
        }

        //Choose a random value from our entire list of clips, and play it once.
        int Index = Random.Range(0, clips.Length);
        audioSource.PlayOneShot(clips[Index]);
    }
}
