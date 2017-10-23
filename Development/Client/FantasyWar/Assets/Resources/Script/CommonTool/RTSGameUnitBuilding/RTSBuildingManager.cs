using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

//
// public delegate void DGameUnitBuildingModeStarted();
public delegate void DGameUnitBuildingModeStopped(bool unitBuildingIsCompleted, Vector3 buildingPos,PlayerInfo info);
//
public class RTSBuildingManager : UnitySingleton<RTSBuildingManager>
{
    //
    // private event DGameUnitBuildingModeStarted buildingStartedEvent;
    private event DGameUnitBuildingModeStopped buildingStoppedEvent;
    //
    // public static void eventRegister(DGameUnitBuildingModeStarted eventHandler)
    // {
    //     if (ShareInstance == null)
    //     {
    //         return;
    //     }
    //     if (eventHandler == null) {
    //         return;
    //     }
    //     if (ShareInstance.buildingStartedEvent == null)
    //     {
    //         ShareInstance.buildingStartedEvent = eventHandler;
    //     }
    //     else
    //     {
    //         ShareInstance.buildingStartedEvent += eventHandler;
    //     }
    // }
    //
    // public static void eventUnRegister(DGameUnitBuildingModeStarted eventHandler)
    // {
    //     if (ShareInstance == null)
    //     {
    //         return;
    //     }
    //     if (eventHandler == null) {
    //         return;
    //     }
    //     if (ShareInstance.buildingStartedEvent == null)
    //     {
    //         return;
    //     }
    //     else
    //     {
    //         ShareInstance.buildingStartedEvent -= eventHandler;
    //     }
    // }
    //
    public static void eventRegister(DGameUnitBuildingModeStopped eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (eventHandler == null) {
            return;
        }
        if (ShareInstance.buildingStoppedEvent == null)
        {
            ShareInstance.buildingStoppedEvent = eventHandler;
        }
        else
        {
            ShareInstance.buildingStoppedEvent += eventHandler;
        }
    }
    //
    public static void eventUnRegister(DGameUnitBuildingModeStopped eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (eventHandler == null) {
            return;
        }
        if (ShareInstance.buildingStoppedEvent == null)
        {
            return;
        }
        else
        {
            ShareInstance.buildingStoppedEvent -= eventHandler;
        }
    }
    //
    private PlayerInfo playerRequestToBuild;
    //
    private bool isBuildingMode;
    //
    private RTSBuildingTempUnit buildingTempUnit;
    //
    private bool isEnableToBuild()
    {
        if (buildingTempUnit == null)
        {
            return false;
        }
        return !buildingTempUnit.IsBlocked;
    }
    //
    private void confirmBuilding()
    {
        if (buildingStoppedEvent != null)
        {
            if (buildingTempUnit != null)
            {
                buildingStoppedEvent.Invoke(true, buildingTempUnit.transform.position,playerRequestToBuild);
            }
            else
            {
                buildingStoppedEvent.Invoke(false, Vector3.zero,playerRequestToBuild);
            }
        }
        //
        stopBuildingMode();
    }
    //
    private void OnMouseLeftDown(KeyCode keyCode)
    {
        if (!isBuildingMode)
        {
            stopBuildingMode();
            return;
        }
        //
        if (isEnableToBuild())
        {
            confirmBuilding();
        }
        else
        {
            Debug.LogError("Can't build here...");
        }
    }
    //
    private void OnEscDown(KeyCode keyCode)
    {
        if (buildingStoppedEvent != null)
        {
            buildingStoppedEvent.Invoke(false, Vector3.zero,playerRequestToBuild);
        }
        stopBuildingMode();
    }
    //
    private bool rayCastingLocatingStarted = false;
    //private float rayCastingWaitTime = 0.05f;
    //
    private IEnumerator buildingLocating()
    {
        //
        while (rayCastingLocatingStarted && RTSCameraController.RTSCamera && buildingTempUnit != null)
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
                        buildingTempUnit.transform.position = hitInfo.point;
                        //
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnemyGameUnit)
                    {
                        //Debug.LogError("hit EnemyGameUnit");
                        //
                        RTSGameUnit gameUnit = (RTSGameUnit)hitObj.GetComponent("RTSGameUnit");
                        //
                        if (gameUnit)
                        {
                            //
                        }
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentObstacle)
                    {
                        //Debug.LogError("hit Obstacle");
                    }
                }
            }
            //
            //yield return new WaitForSeconds(rayCastingWaitTime);
            yield return null;
            //
        }
        //
    }
    //
    private void buildingTempUnitRelease()
    {
        buildingTempUnit.gameObject.SetActive(false);
        DestroyImmediate(buildingTempUnit.gameObject);
        buildingTempUnit = null;
    }
    //
    public void startBuildingMode(string prefabPath, PlayerInfo playerInfo) {
        startBuildingMode(PrefabFactory.ShareInstance.createClone<RTSBuildingTempUnit>(prefabPath, Vector3.zero, Quaternion.identity),playerInfo);
    }

    //
    public void startBuildingMode(RTSBuildingTempUnit tempUnit,PlayerInfo playerInfo)
    {
        // Debug.Log("startBuildingMode");
        if (playerInfo == null) {
            return;
        }
        //
        playerRequestToBuild = playerInfo;
		//Debug.Log (playerRequestToBuild.name);
        //
        if (tempUnit == null)
        {
            Debug.Log("tempUnit == null");
            //
            return;
        }
        if (!isBuildingMode)
        {
            Debug.Log("!isBuildingMode");
            //
                isBuildingMode = true;
                //
                RTSGameUnitSelectionManager.Enabled = false;
                RTSGameUnitActionManager.Enabled = false;
                //
                InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftDown);
                InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Escape, OnEscDown);
                //
                buildingTempUnit = tempUnit;
                rayCastingLocatingStarted = true;
                //
                StartCoroutine(buildingLocating());
                //
            //     if (buildingStartedEvent != null)
            // {
            //     Debug.Log("buildingStartedEvent != null");
            //     buildingStartedEvent.Invoke();
            // }else { 
            //     Debug.Log("buildingStartedEvent == null");
            // }
            //
        }
    }
    //
    public void stopBuildingMode()
    {
        if (isBuildingMode)
        {
            //
            buildingTempUnitRelease();
            //
            isBuildingMode = false;
            //
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftDown);
            InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.Escape, OnEscDown);
            //
            RTSGameUnitSelectionManager.Enabled = true;
            RTSGameUnitActionManager.Enabled = true;
            //
        }
    }

    public bool isPosValidToBuild(Vector3 pos, string path) {
        //
        bool flag = false;
        //
        Vector3 validBuildPos;
        //
        try { 
            //
        RTSBuildingTempUnit unit=PrefabFactory.ShareInstance.createClone<RTSBuildingTempUnit>(path,Vector3.zero,Quaternion.identity);
        //
        Debug.Log("fucking here");
        //
        if (RTSCameraController.RTSCamera && unit != null)
        {
                Debug.Log("ready to cast");
                //
                Vector3 direction = pos - RTSCameraController.RTSCamera.transform.position;
            RaycastHit hitInfo = new RaycastHit();
            //
            if (Physics.Raycast(RTSCameraController.RTSCamera.transform.position, direction, out hitInfo, 1000f, RTSLayerManager.ShareInstance.LayerMaskRayCastMouse1, QueryTriggerInteraction.Ignore))
            {
                //
                Debug.Log("raycast hit something");
                //
                GameObject hitObj = hitInfo.collider.gameObject;
                //
                if (hitObj != null)
                {
                    //
                    Debug.LogError("hit obj");
                    //
                    if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentGround)
                    {
                        Debug.LogError("hit Ground");
                        //
                        unit.transform.position = hitInfo.point;
                        //
                        flag = !buildingTempUnit.IsBlocked;
                        //
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnemyGameUnit)
                    {
                        Debug.LogError("hit LayerNumberEnemyGameUnit");
                    }else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberFriendlyGameUnit)
                    {
                        Debug.LogError("hit LayerNumberFriendlyGameUnit");
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentObstacle)
                    {
                        Debug.LogError("hit LayerNumberEnvironmentObstacle");
                    }
                }
            }
            //
        }
        //
        }catch(Exception e){ 
            Debug.Log(e.Message);
        }
        //
        return flag;
        //
    }
}
