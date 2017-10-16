using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSBuildingTempUnit : MonoBehaviour
{
    //
    private Material[] materialsOriginal;
    private Material[] materialsValid;
    private Material[] materialsInvalid;
    //
    private Rigidbody rigid;
    private MeshCollider locatingTrigger;
    private MeshRenderer meshRenderer;
    //
    private static Material materialRed;
    private static Material materialGreen;
    //
    private bool _isBlocked = false;
    public bool IsBlocked
    {
        get
        {
            return _isBlocked;
        }
    }
    //
    private void setValid()
    {
        _isBlocked = false;
        if (materialsValid != null)
        {
            meshRenderer.materials = materialsValid;
        }
    }
    //
    private void setInvalid()
    {
        _isBlocked = true;
        if (materialsInvalid != null)
        {
            meshRenderer.materials = materialsInvalid;
        }
    }
    //
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("OnTriggerEnter");
        if (other.gameObject.layer != RTSLayerManager.ShareInstance.LayerNumberEnvironmentGround)
        {
            setInvalid();
        }
    }
    //
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != RTSLayerManager.ShareInstance.LayerNumberEnvironmentGround)
        {
            setInvalid();
        }
    }
    //
    private void OnTriggerExit(Collider other)
    {
        //Debug.LogError("OnTriggerExit");
        setValid();
    }
    //
    private void Awake()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        locatingTrigger = gameObject.AddComponent<MeshCollider>();
        locatingTrigger.convex = true;
        locatingTrigger.isTrigger = true;
        rigid = gameObject.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.isKinematic = true;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }
    //
    private void Start()
    {
        //
        materialsOriginal = meshRenderer.materials;
        //
        if (materialsOriginal != null)
        {
            if (materialRed == null)
            {
                materialRed = Resources.Load<Material>("Material/RTSGameUnitBuildingTempUnitLocationInvalid");
            }
            if (materialGreen == null)
            {
                materialGreen = Resources.Load<Material>("Material/RTSGameUnitBuildingTempUnitLocationValid");
            }
            //
            if (materialRed != null)
            {
                materialsInvalid = new Material[materialsOriginal.Length + 1];
                materialsOriginal.CopyTo(materialsInvalid, 0);
                materialsInvalid[materialsInvalid.Length - 1] = materialRed;
            }
            if (materialGreen != null)
            {
                materialsValid = new Material[materialsOriginal.Length + 1];
                materialsOriginal.CopyTo(materialsValid, 0);
                materialsValid[materialsValid.Length - 1] = materialGreen;
            }
        }
        //
        setValid();
        //
    }
}
