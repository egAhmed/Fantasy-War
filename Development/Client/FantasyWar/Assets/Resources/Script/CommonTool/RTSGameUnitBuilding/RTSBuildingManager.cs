using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RTSBuildingManager : UnitySingleton<RTSBuildingManager>
{
    bool RayCastingLocatingStarted
    {
        get;
        set;
    }
    string BuildingAssetResourcePath
    {
        get;
        set;
    }
    //
    RTSWorker CurrentWorker
    {
        get;
        set;
    }
    //
    bool IsBuildingModeAvailable
    {
        get
        {
            return CurrentWorker != null && BuildingAssetResourcePath != null && BuildingTempUnit != null;
        }
    }
    //
    bool IsBuildingMode
    {
        get;
        set;
    }
    //
    bool IsShiftDown
    {
        get;
        set;
    }
    //
    RTSBuildingTempUnit BuildingTempUnit
    {
        get;
        set;
    }
    //
    private bool isEnableToBuild()
    {
        if (BuildingTempUnit == null)
        {
            return false;
        }
        return !BuildingTempUnit.IsBlocked;
    }
    //
    void confirmBuilding()
    {
        // Debug.LogError("confirmBuilding...");
        //        
        if (!IsBuildingModeAvailable)
        {
            stopBuildingMode();
            // Debug.LogError("Fuck you...");
        }
        else
        {
            //
            RTSBuildingPendingToBuildTempUnit temp = createPendingBuildingInstance(BuildingAssetResourcePath, BuildingTempUnit.transform.position, Quaternion.identity);
            //
            if (temp == null)
            {
                Debug.LogError("RTSBuildingPendingToBuildTempUnit == null");
                return;
            }
            //
            temp.RealBuildingPrefabPath = BuildingAssetResourcePath;
            //
            CurrentWorker.addPendingBuildTask(temp);
            //
            if (!IsShiftDown)
            {
                //
                stopBuildingMode();
                //
            }
            //
        }
    }
    public RTSBuilding createRTSRealBuilding(string path, Vector3 pos, Quaternion quaternion, PlayerInfo info)
    {
        //
        switch (path)
        {
            case Action_Build.PATH:
                // Debug.LogError("Build home");
                return createRTSRealBuilding<RTSBuildingHomeBase>(path, pos, quaternion, info);
            case Action_BuildBarrack.PATH:
                // Debug.LogError("Build home");
                RTSBuildingBarrack barrackUnit = createRTSRealBuilding<RTSBuildingBarrack>(path, pos, quaternion, info);
                //
                if (info.isAI)
                {
                    info.AICon.registerDelCreatArmy(barrackUnit.CreatArmy, barrackUnit);
                }
                //
                return barrackUnit;
            default:
                // Debug.LogError("fuck here");
                return null;
        }
        //
    }
    //
    public T createRTSRealBuilding<T>(string path, Vector3 pos, Quaternion quaternion, PlayerInfo info) where T : RTSBuilding
    {
        //
        T buildingUnit = PrefabFactory.ShareInstance.createClone<T>(path, pos, quaternion);
        //
        buildingUnit.playerInfo = info;
        //
        if (info.gameUnitBelongSide == RTSGameUnitBelongSide.Player)
        {
            buildingUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberPlayerBuildingUnit;
            //
        }
        else if (info.gameUnitBelongSide == RTSGameUnitBelongSide.EnemyGroup)
        {
            buildingUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberEnemyGameUnit;
            //
        }
        else if (info.gameUnitBelongSide == RTSGameUnitBelongSide.FriendlyGroup)
        {
            buildingUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberFriendlyGameUnit;
            //
        }
        //
        return buildingUnit;
    }
    //
    public RTSBuildingPendingToBuildTempUnit createPendingBuildingInstance(string path, Vector3 pos, Quaternion quaternion)
    {
        return PrefabFactory.ShareInstance.createClone<RTSBuildingPendingToBuildTempUnit>(BuildingAssetResourcePath, pos, quaternion);
    }
    //
    private void OnMouseLeftDown(KeyCode keyCode)
    {
        //
        if (!IsBuildingMode)
        {
            stopBuildingMode();
            return;
        }
        //
        if (isEnableToBuild())
        {
            //
            confirmBuilding();
            //
        }
        else
        {
            Debug.LogError("Can't build here...");
        }
        //
    }
    //
    private void OnEscDown(KeyCode keyCode)
    {
        //
        stopBuildingMode();
        //
    }
    //
    IEnumerator buildingLocating()
    {
        //
        while (RayCastingLocatingStarted && RTSCameraController.RTSCamera && BuildingTempUnit != null)
        {
            Ray ray = RTSCameraController.RTSCamera.ScreenPointToRay(InputManager.ShareInstance.MousePosition);
            RaycastHit hitInfo = new RaycastHit();
            //
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 1000f, RTSLayerManager.ShareInstance.LayerMaskRayCastMouse1, QueryTriggerInteraction.Ignore))
            {
                //
                GameObject hitObj = hitInfo.collider.gameObject;
                //
                if (hitObj != null)
                {
                    //
                    //Debug.LogError("hit obj");
                    //
                    if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentGround)
                    {
                        //Debug.LogError("hit Ground");
                        //
                        BuildingTempUnit.transform.position = hitInfo.point;
                        //
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnemyGameUnit)
                    {
                        //Debug.LogError("hit EnemyGameUnit");
                        //
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentObstacle)
                    {
                        //Debug.LogError("hit Obstacle");
                    }
                }
            }
            //
            yield return null;
            //
        }
        //
    }
    //
    private void buildingTempUnitRelease()
    {
        BuildingTempUnit.gameObject.SetActive(false);
        DestroyImmediate(BuildingTempUnit.gameObject);
        BuildingTempUnit = null;
    }
    //
    public void startBuildingMode(string prefabPath, RTSWorker worker)
    {
        stopBuildingMode();
        //
        CurrentWorker = worker;
        BuildingAssetResourcePath = prefabPath;
        //
        BuildingTempUnit = PrefabFactory.ShareInstance.createClone<RTSBuildingTempUnit>(BuildingAssetResourcePath, Vector3.zero, Quaternion.identity);
        //
        if (IsBuildingModeAvailable)
        {
            startBuildingMode();
        }
    }

    void startBuildingMode()
    {
        //
        if (!IsBuildingMode)
        {
            //Debug.Log("!isBuildingMode");
            //
            IsBuildingMode = true;
            //
            RTSGameUnitSelectionManager.Enabled = false;
            RTSGameUnitActionManager.Enabled = false;
            //
            InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftDown);
            InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Escape, OnEscDown);
            InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.LeftShift, OnKeyShiftDown);
            InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.RightShift, OnKeyShiftDown);
            InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp(KeyCode.LeftShift, OnKeyShiftUp);
            InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp(KeyCode.RightShift, OnKeyShiftUp);
            //
            RayCastingLocatingStarted = true;
            //
            StartCoroutine(buildingLocating());
            //
        }
        //
    }

    //
    void OnKeyShiftDown(KeyCode keyCode)
    {
        IsShiftDown = true;
        //
        //Debug.Log("OnKeyShiftDown");
        //
    }

    void OnKeyShiftUp(KeyCode keyCode)
    {
        IsShiftDown = false;
        //
        //Debug.Log("OnKeyShiftUp");
        //
    }
    //
    public void stopBuildingMode()
    {
        //
        CurrentWorker = null;
        //
        if (IsBuildingMode)
        {
            //
            buildingTempUnitRelease();
            //
            IsBuildingMode = false;
            //
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftDown);
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.Escape, OnEscDown);
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp(KeyCode.LeftShift, OnKeyShiftUp);
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp(KeyCode.RightShift, OnKeyShiftUp);
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.LeftShift, OnKeyShiftDown);
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.RightShift, OnKeyShiftDown);
            //
            RTSGameUnitSelectionManager.Enabled = true;
            RTSGameUnitActionManager.Enabled = true;
            //
        }
    }
    //
    public bool isPosValidToBuild(Vector3 pos, string path)
    {
        //
        bool flag = false;
        //
        try
        {
            //
            RTSBuildingTempUnit unit = PrefabFactory.ShareInstance.createClone<RTSBuildingTempUnit>(path, pos, Quaternion.identity);
            //
            if (unit != null && !unit.IsBlocked)
            {
                flag = true;
            }
            //
            unit.gameObject.SetActive(false);
            Destroy(unit.gameObject);
            //
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        //
        return flag;
        //
    }
    //
}
