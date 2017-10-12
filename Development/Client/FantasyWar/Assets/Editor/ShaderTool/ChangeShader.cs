using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class ChangeShader : EditorWindow
{

    [MenuItem("Window/ChangeShader/BuidingDestoryShader")]
    static void BuidingShader()
    {
        changeShader(0, @"LCH/BuidingProgressShader");
        changeShader(1, @"LCH/UnitDestoryShader");
    }

    //[MenuItem("ChangeShader /BuidingDestoryShader")]
    //static void BuidingProgressShader()
    //{

    //}

    //[MenuItem("ChangeShader/UnitBornShader")]
    //static void UnitBornShader()
    //{

    //}

    //[MenuItem("ChangeShader/UnitDestoryShader")]
    //static void UnitDestoryShader()
    //{

    //}

    /// <summary>
    /// 传入shader路径，执行shader替换
    /// </summary>
    /// <param name="shaderName"></param>
    /// <returns></returns>
    static void changeShader(int materialIndex, string shaderName)
    {
        if (Selection.activeObject == null)
        {
            Debug.LogError("请选择对象");
            return;
        }

        string path;
        path = AssetDatabase.GetAssetPath(Selection.activeObject);
        Debug.Log(path);
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("请在Project中选择对象");
            return;
        }

        if (!path.EndsWith("prefab"))
        {
            Debug.LogError("请选择perfab对象");
            return;
        }

        //标记预设为修改状态 这步很重要，不执行这步操作。关闭Unity后。预设并不会保存。
        EditorUtility.SetDirty(Selection.activeObject);
        GameObject obj = Resources.Load<GameObject>((path.Replace(@"Assets/Resources/", "")).Replace(".prefab", "")) as GameObject;
        Debug.Log((path.Replace(@"Assets/Resources/", "0")).Replace(".prefab", ""));
        if (obj == null)
        {
            Debug.LogError("无法加载该prefab");
            return;
        }
        MeshRenderer tmpMeshR = obj.GetComponent<MeshRenderer>();
        if (tmpMeshR == null)
        {
            Debug.LogError("该prefab没有MeshRenderer组件");
            return;
        }
        MeshFilter tmpMeshF = obj.GetComponent<MeshFilter>();
        if (tmpMeshF == null)
        {
            Debug.LogError("该prefab没有MeshFilter组件");
            return;
        }

        if (Shader.Find(shaderName) == null)
        {
            Debug.LogError("找不到create时用的Shader");
            return;
        }
        try
        {
            tmpMeshR.sharedMaterials[materialIndex].shader = Shader.Find(shaderName);

        }
        catch
        {
            Debug.LogError("无法获取对应Index下的Material");
            return;
        }

        float MaxY = -Mathf.Infinity;
        float MinY = Mathf.Infinity;
        foreach (var item in tmpMeshF.sharedMesh.vertices)
        {
            if (item.y > MaxY)
            {
                MaxY = item.y;
            }

            if (item.y < MinY)
            {
                MinY = item.y;
            }
        }
        tmpMeshR.sharedMaterial.SetFloat("_MeshVertexYMax", MaxY);
        tmpMeshR.sharedMaterial.SetFloat("_MeshVertexYMin", MinY);
        //如何获取default贴图
        // tmpMeshR.sharedMaterial.SetTexture("",)
        obj = null;
        //清不掉obj
        Resources.UnloadUnusedAssets();
        Debug.Log(Selection.activeObject.name + "修改Shader成功");
    }
}
