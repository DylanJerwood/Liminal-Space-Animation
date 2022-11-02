using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_FrameRateManager : MonoBehaviour
{
    //This script sets the FPS of your game.
    //You can access it from anywhere using the static instance,
    //and it will call the function to set the frame rate at the start of the game

    public int maxFrameRate = 300;
    public int minFrameRate = 24;
    public int desiredFrameRate = 60;

    //Access this and change the framerate from any other code by using - FI_FrameRateManager.instance.ChangeFrameRate(an fps integer value);
    #region Static Access
    public static FI_FrameRateManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    private void Start()
    {
        //Set the frame rate to the value in desiredFrameRate field
        ChangeFrameRate(desiredFrameRate);
    }

    public void ChangeFrameRate(int frameRate)
    {
        //Adjust if we've somehow gone below or above our min/max fps
        if (frameRate >= maxFrameRate)
            frameRate = maxFrameRate;

        if (frameRate <= minFrameRate)
            frameRate = minFrameRate;

        //Set the adjusted or checked framerate
        Application.targetFrameRate = frameRate;
    }
}
