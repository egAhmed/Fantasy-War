using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public delegate void DGameUnitBuildingModeStarted();
public delegate void DGameUnitBuildingModeStopped(bool unitBuildingIsCompleted, Vector3 buildingPos);
//
public class RTSBuildingManager : UnitySingleton<RTSBuildingManager>
{
    //
    private event DGameUnitBuildingModeStarted buildingStartedEvent;
    private event DGameUnitBuildingModeStopped buildingStoppedEvent;
    //
    public static void eventRegister(DGameUnitBuildingModeStarted eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (ShareInstance.buildingStartedEvent == null)
        {
            ShareInstance.buildingStartedEvent = new DGameUnitBuildingModeStarted(eventHandler);
        }
        else
        {
            ShareInstance.buildingStartedEvent += eventHandler;
        }
    }
    //
    public static void eventUnRegister(DGameUnitBuildingModeStarted eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (ShareInstance.buildingStartedEvent == null)
        {
            return;
        }
        else
        {
            ShareInstance.buildingStartedEvent -= eventHandler;
        }
    }
    //
    public static void eventRegister(DGameUnitBuildingModeStopped eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (ShareInstance.buildingStoppedEvent == null)
        {
            ShareInstance.buildingStoppedEvent = new DGameUnitBuildingModeStopped(eventHandler);
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
                buildingStoppedEvent.Invoke(true, buildingTempUnit.transform.position);
            }
            else
            {
                buildingStoppedEvent.Invoke(false, Vector3.zero);
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
            buildingStoppedEvent.Invoke(false, Vector3.zero);
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
    public void startBuildingMode(RTSBuildingTempUnit tempUnit)
    {
        if (tempUnit == null)
        {
            return;
        }
        if (!isBuildingMode)
        {
            //
            if (buildingStartedEvent != null)
            {
                isBuildingMode = true;
                //
                InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftDown);
                InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Escape, OnEscDown);
                //
                buildingTempUnit = tempUnit;
                rayCastingLocatingStarted = true;
                //
                StartCoroutine(buildingLocating());
                //
                buildingStartedEvent.Invoke();
            }
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
        }
    }

}
