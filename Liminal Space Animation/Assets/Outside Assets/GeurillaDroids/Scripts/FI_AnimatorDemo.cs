using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FI_AnimatorDemo : MonoBehaviour
{
    //    THIS CODE IS NOT DESIGNED TO BE USED FOR ANYTHING BUT THE DEMO SCENES FUNCTIONALITY AND IT'S UI -  Quite simple and not great efficiency   //
    //    That being said, maybe there is something useful to you here so give it a quick read!                                                      //


    public Animator animator;
    public RuntimeAnimatorController defaultPose;

    int skinIndex = 1;

    public Material demoLightsMaterial;
    public Material demoSkinMaterial;
    public Material demoHeadSkinMaterial;

    public Color32 lightsV1;
    public Color32 lightsV2;
    public Color32 lightsV3;

    public Texture2D armorV1Albedo;
    public Texture2D armorV1Metallic;
    public Texture2D armorV1Normal;
    public Texture2D armorV1AO;

    public Texture2D armorV2Albedo;
    public Texture2D armorV2Metallic;
    public Texture2D armorV2Normal;
    public Texture2D armorV2AO;
 
    public Texture2D armorV3Albedo;
    public Texture2D armorV3Metallic;
    public Texture2D armorV3Normal;
    public Texture2D armorV3AO;

    public Texture2D headV1Albedo;
    public Texture2D headV1Metallic;
    public Texture2D headV1Normal;
    public Texture2D headV1AO;

    public Texture2D headV2Albedo;
    public Texture2D headV2Metallic;
    public Texture2D headV2Normal;
    public Texture2D headV2AO;

    public Texture2D headV3Albedo;
    public Texture2D headV3Metallic;
    public Texture2D headV3Normal;
    public Texture2D headV3AO;

    void Start()
    {
        animator.runtimeAnimatorController = defaultPose;
        skinIndex = 0;
        ChangeSkin();
    }

    public void ChangeSkin()
    {
        skinIndex++;

        if (skinIndex >= 4)
            skinIndex = 1;

        if(skinIndex == 1)
        {
            demoSkinMaterial.SetTexture("_MainTex", armorV1Albedo);
            demoSkinMaterial.SetTexture("_MetallicGlossMap", armorV1Metallic);
            demoSkinMaterial.SetTexture("_BumpMap", armorV1Normal);
            demoSkinMaterial.SetTexture("_OcclusionMap", armorV1AO);

            demoHeadSkinMaterial.SetTexture("_MainTex", headV1Albedo);
            demoHeadSkinMaterial.SetTexture("_MetallicGlossMap", headV1Metallic);
            demoHeadSkinMaterial.SetTexture("_BumpMap", headV1Normal);
            demoHeadSkinMaterial.SetTexture("_OcclusionMap", headV1AO);

            demoLightsMaterial.SetColor("_EmissionColor", lightsV1);
            return;
        }
        if (skinIndex == 2)
        {
            demoSkinMaterial.SetTexture("_MainTex", armorV2Albedo);
            demoSkinMaterial.SetTexture("_MetallicGlossMap", armorV2Metallic);
            demoSkinMaterial.SetTexture("_BumpMap", armorV2Normal);
            demoSkinMaterial.SetTexture("_OcclusionMap", armorV2AO);

            demoHeadSkinMaterial.SetTexture("_MainTex", headV2Albedo);
            demoHeadSkinMaterial.SetTexture("_MetallicGlossMap", headV2Metallic);
            demoHeadSkinMaterial.SetTexture("_BumpMap", headV2Normal);
            demoHeadSkinMaterial.SetTexture("_OcclusionMap", headV2AO);

            demoLightsMaterial.SetColor("_EmissionColor", lightsV2);
            return;
        }
        if (skinIndex == 3)
        {
            demoSkinMaterial.SetTexture("_MainTex", armorV3Albedo);
            demoSkinMaterial.SetTexture("_MetallicGlossMap", armorV3Metallic);
            demoSkinMaterial.SetTexture("_BumpMap", armorV3Normal);
            demoSkinMaterial.SetTexture("_OcclusionMap", armorV3AO);

            demoHeadSkinMaterial.SetTexture("_MainTex", headV3Albedo);
            demoHeadSkinMaterial.SetTexture("_MetallicGlossMap", headV3Metallic);
            demoHeadSkinMaterial.SetTexture("_BumpMap", headV3Normal);
            demoHeadSkinMaterial.SetTexture("_OcclusionMap", headV3AO);

            demoLightsMaterial.SetColor("_EmissionColor", lightsV3);
            return;
        }
    }
}
