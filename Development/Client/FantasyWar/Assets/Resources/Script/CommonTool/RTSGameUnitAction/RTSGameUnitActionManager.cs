using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public delegate void DGameUnitActionTargetPosition(Vector3 pos);
public delegate void DGameUnitActionTargetUnit(RTSGameUnit rtsGameUnit);
//
public class RTSGameUnitActionManager : UnitySingleton<RTSGameUnitActionManager>
{
    private static bool _enabled = true;
    public static bool Enabled
    {
        get
        {
            return _enabled;
        }
        set
        {
            _enabled = value;
        }
    }
    //
    private bool _isRayCastingOnGUI = false;
    private bool IsRayCastingOnGUI
    {
        get
        {
            return _isRayCastingOnGUI;
        }
        set
        {
            _isRayCastingOnGUI = value;
        }
    }
    //
    //
    private event DGameUnitActionTargetPosition targetPositionEvent;
    private event DGameUnitActionTargetUnit targetUnitEvent;
    //
    #region eventRegister&eventUnRegister
    //
    public static void eventRegister(DGameUnitActionTargetPosition eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (eventHandler == null)
        {
            return;
        }
        if (ShareInstance.targetPositionEvent == null)
        {
            ShareInstance.targetPositionEvent = eventHandler;
        }
        else
        {
            ShareInstance.targetPositionEvent += eventHandler;
        }
    }
    //
    public static void eventUnRegister(DGameUnitActionTargetPosition eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (eventHandler == null)
        {
            return;
        }
        if (ShareInstance.targetPositionEvent == null)
        {
            return;
        }
        else
        {
            ShareInstance.targetPositionEvent -= eventHandler;
        }
    }
    //
    public static void eventRegister(DGameUnitActionTargetUnit eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (eventHandler == null)
        {
            return;
        }
        if (ShareInstance.targetUnitEvent == null)
        {
            ShareInstance.targetUnitEvent = eventHandler;
        }
        else
        {
            ShareInstance.targetUnitEvent += eventHandler;
        }
    }
    //
    public static void eventUnRegister(DGameUnitActionTargetUnit eventHandler)
    {
        if (ShareInstance == null)
        {
            return;
        }
        if (eventHandler == null)
        {
            return;
        }
        if (ShareInstance.targetUnitEvent == null)
        {
            return;
        }
        else
        {
            ShareInstance.targetUnitEvent -= eventHandler;
        }
    }
    //
    #endregion
    //
    private bool TargetPositionEventNeedInvoke
    {
        get
        {
            return targetPositionEvent != null && targetPositionEvent.GetInvocationList() != null && targetPositionEvent.GetInvocationList().Length > 0;
        }
    }
    //
    private bool TargetUnitEventNeedInvoke
    {
        get
        {
            return targetUnitEvent != null && targetUnitEvent.GetInvocationList() != null && targetUnitEvent.GetInvocationList().Length > 0;
        }
    }
    //
    private void OnMouseRightKeyPressing(KeyCode keyCode)
    {
        if (!Enabled)
        {
            return;
        }
        //
        //
        if (!isMouseRightKeyRayCasting)
        {
            IsRayCastingOnGUI = UIRayCastInterceptor.ShareInstance.IsRayCastingGUI(InputManager.ShareInstance.MousePosition);
            //
            if (!IsRayCastingOnGUI)
            {
                StartCoroutine(mouseRightKeyRayCast());
            }
        }
        //
    }
    //
    //private void OnMouseRightKeyPressed()
    //{
    //    //
    //}
    ////
    //private void OnMouseRightKeyRelease()
    //{
    //    //
    //}
    //
    private bool isMouseRightKeyRayCasting = false;
    private float mouseRightKeyRayCastingWaitTime = 0.2f;
    //
    private IEnumerator mouseRightKeyRayCast()
    {
        isMouseRightKeyRayCasting = true;
        //
        if (RTSCameraController.RTSCamera && (TargetUnitEventNeedInvoke || TargetPositionEventNeedInvoke))
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
                    if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberUI)
                    {
                        Debug.LogError("hit UI");
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentGround)
                    {
                        //Debug.LogError("hit Ground");
                        //
                        if (TargetPositionEventNeedInvoke)
                        {
                            RTSGameUnitActionCursorController.ShareInstance.showMove(hitInfo.point);
                            //
                            targetPositionEvent.Invoke(hitInfo.point);
                        }
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnemyGameUnit)
                    {
                        //Debug.LogError("hit EnemyGameUnit");
                        //
                        RTSGameUnit gameUnit = (RTSGameUnit)hitObj.GetComponent("RTSGameUnit");
                        //
                        if (gameUnit)
                        {
                            if (TargetUnitEventNeedInvoke)
                            {
                                RTSGameUnitActionCursorController.ShareInstance.showAttack();
                                //
                                targetUnitEvent.Invoke(gameUnit);
                            }
                        }
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentObstacle)
                    {
                        Debug.LogError("hit Obstacle");
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentResource)
                    {
                        Debug.LogError("hit Resource");
                        //
                        RTSGameUnit gameUnit = (RTSGameUnit)hitObj.GetComponent("RTSGameUnit");
                        //
                        if (gameUnit)
                        {
                            if (TargetUnitEventNeedInvoke)
                            {
                                //RTSGameUnitActionCursorController.ShareInstance.showAttack();
                                //
                                targetUnitEvent.Invoke(gameUnit);
                            }
                        }
                    }
                    else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberPlayerBuildingUnit)
                    {
                        Debug.LogError("hit Building");
                        //
                        RTSGameUnit gameUnit = (RTSGameUnit)hitObj.GetComponent("RTSGameUnit");
                        //
                        if (gameUnit)
                        {
                            if (TargetUnitEventNeedInvoke)
                            {
                                //RTSGameUnitActionCursorController.ShareInstance.showAttack();
                                //
                                targetUnitEvent.Invoke(gameUnit);
                            }
                        }
                    }
                }

            }
        }
        //
        yield return new WaitForSeconds(mouseRightKeyRayCastingWaitTime);
        //
        isMouseRightKeyRayCasting = false;
    }
    //
    //
    // Use this for initialization
    private void Start()
    {
        //InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Mouse1, OnMouseRightKeyPressed);
        InputManager.ShareInstance.InputEventHandlerRegister_GetKey(KeyCode.Mouse1, OnMouseRightKeyPressing);
        //InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp(KeyCode.Mouse1, OnMouseRightKeyRelease);
        //
    }

    private void OnDestroy()
    {
        //
        //InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.Mouse1, OnMouseRightKeyPressed);
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetKey(KeyCode.Mouse1, OnMouseRightKeyPressing);
        //InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp(KeyCode.Mouse1, OnMouseRightKeyRelease);
    }
}
