using UnityEngine;
using System.Collections;

public class BloodHit : MonoBehaviour
{

    public Shader SCShader;
    public static BloodHit current;
    private float TimeX = 1.0f;
    [Range(0, 1)]
    public float Hit_Full = 0f;
    [Range(0, 1f)]
    public float LightReflect = 0.5f;
    private Material SCMaterial;
    private Texture2D Texture2;
    Material material
    {
        get
        {
            if (SCMaterial == null)
            {
                SCMaterial = new Material(SCShader);
                SCMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return SCMaterial;
        }
    }

    public BloodHit()
    {
        current = this;
    }
    public void BloodScreen()
    {
        Hit_Full = 0.5f;
        LightReflect = 0.5f;
    }
    public void CloseBloodScreen()
    {
        Hit_Full = 0.0f;
        LightReflect = 0.0f;
    }
    /// <summary>
    /// 损血屏幕特效,效果强度由两个参数的积决定,范围0~1
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    public void BloodScreen(float arg1, float arg2)
    {
        Hit_Full = arg1;
        LightReflect = arg2;
    }
    void Start()
    {
        Texture2 = Resources.Load("Texture/BloodHit") as Texture2D;
        SCShader = Shader.Find("ImageEffects/BloodHit");
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
        CloseBloodScreen();
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (SCShader != null)
        {
            TimeX += Time.deltaTime;
            if (TimeX > 100) TimeX = 0;
            material.SetFloat("_TimeX", TimeX);
            material.SetFloat("_Value", LightReflect);
            material.SetFloat("_Value10", Mathf.Clamp(Hit_Full, 0, 1));
            material.SetTexture("_MainTex2", Texture2);
            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }


    }

    void OnDisable()
    {
        if (SCMaterial)
        {
            DestroyImmediate(SCMaterial);
        }

    }

   

}