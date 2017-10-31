using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSBuildingPendingToBuildTempUnit : MonoBehaviour
{
	public string RealBuildingPrefabPath{
        get;
        set;
    }
    //
    const string MATERIAL_PATH = @"Material/RTSGameUnitBuildingPendingToBuild";
    //
    Material[] materialsOriginal;
    Material[] materialsPending;
    //
    Rigidbody rigid;
    MeshCollider locatingTrigger;
    MeshRenderer meshRenderer;
    //
    static Material materialPending;
    //
    bool _isBlocked = false;
    public bool IsBlocked
    {
        get
        {
            return _isBlocked;
        }
    }
    //
    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        locatingTrigger = gameObject.GetComponent<MeshCollider>();
        //
        if (locatingTrigger == null)
        {
            locatingTrigger = gameObject.AddComponent<MeshCollider>();
        }
        //
        locatingTrigger.convex = true;
        // locatingTrigger.isTrigger = true;
        // rigid = gameObject.AddComponent<Rigidbody>();
        // rigid.useGravity = false;
        // rigid.isKinematic = true;
        // rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    //
    private void Start()
    {
        //
        materialsOriginal = meshRenderer.materials;
        //
        if (materialsOriginal != null)
        {
            if (materialPending == null)
            {
                materialPending = Resources.Load<Material>(MATERIAL_PATH);
            }
            if (materialPending != null)
            {
                materialsPending = new Material[materialsOriginal.Length + 1];
                materialsOriginal.CopyTo(materialsPending, 0);
                materialsPending[materialsPending.Length - 1] = materialPending;
            }
        }
        //
        meshRenderer.materials = materialsPending;
        //
    }
}
