using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_DestroyMe : MonoBehaviour
{
    //   This script is for removing any object in the world for optimization, if a timer will do.
    //   Projectiles, debris, effects, independent sounds, whatever you want, just add this component, set the timer, and check OnAwake.
    //   If OnAwake is false, you will need to start the timer yourself at some point using the StartDestroyTimer Function. 




    public float destroyTimer = 10.0f;

    public bool onAwake = false;

    void Awake()
    {
        if (onAwake)
            StartCoroutine(DestroyWait());
    }

    public void StartDestroyTimer()
    {
        StartCoroutine(DestroyWait());
    }

    IEnumerator DestroyWait()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(gameObject);
    }
}
