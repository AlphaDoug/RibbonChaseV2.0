﻿using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 控制开始飞行时候的马赛克效果
/// </summary>
[ExecuteInEditMode]
[AddComponentMenu("PengLu/ImageEffect/Mosaic")]

public class ImageEffect_Mosaic : MonoBehaviour
{
    #region Variables  
    public Shader MosaicShader = null;
    private Material MosaicMaterial = null;
    public int MosaicSize = 8;

    #endregion


    //创建材质和shader  
    Material material
    {
        get
        {
            if (MosaicMaterial == null)
            {
                MosaicMaterial = new Material(MosaicShader);
                MosaicMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return MosaicMaterial;
        }
    }

    // Use this for initialization  
    void Start()
    {

        MosaicShader = Shader.Find("PengLu/Unlit/MosaicVF");

        // Disable if we don't support image effects  
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }

        // Disable the image effect if the shader can't  
        // run on the users graphics card  
        if (!MosaicShader || !MosaicShader.isSupported)
            enabled = false;
        return;

    }



    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (MosaicSize > 0 && MosaicShader != null)
        {

            material.SetInt("_MosaicSize", MosaicSize);//将马赛克尺寸传递给shader  

            Graphics.Blit(sourceTexture, destTexture, material);//将抓取的图像传递给gpu并用shader处理后，传回来  


        }

        else
        {
            Graphics.Blit(sourceTexture, destTexture);

        }


    }

    // Update is called once per frame  
    void Update()
    {
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            MosaicShader = Shader.Find("PengLu/Unlit/MosaicVF");
        }
#endif

    }

    public void OnDisable()
    {
        if (MosaicMaterial)
            DestroyImmediate(MosaicMaterial);
    }
}