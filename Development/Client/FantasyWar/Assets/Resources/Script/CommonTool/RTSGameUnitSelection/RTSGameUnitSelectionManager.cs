using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//
//public delegate void DGameUnitMultipleSelectionSelectedUnitsNotification(RTSGameUnit[] selectedUnits);
//public delegate RTSGameUnit[] DGameUnitMultipleSelectionValidUnitsListGeneration();
//public delegate void DGameUnitSingleSelectionSelected(RTSGameUnit rtsGameUnit);
public delegate void DGameUnitSelectionRelease();
//
public class RTSGameUnitSelectionManager : MonoBehaviour
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
            // if (!value)
            // {
            //     selectionRelease();
            // }
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
    static RTSGameUnit singleSelectedUnit = null;
    //
    #region event define
    //
    //private static event DGameUnitMultipleSelectionValidUnitsListGeneration multipleSelectionValidListGenerationEvent;
    //private static event DGameUnitMultipleSelectionSelectedUnitsNotification multipleSelectionSelectedDoneNotificationEvent;
    //private static event DGameUnitSingleSelectionSelected singleSelectionSelectedEvent;
    //private static event DGameUnitSelectionRelease selectionReleaseEvent;
    //
    #endregion
    //
    #region multiple selection screen functional properties
    //
    [SerializeField]
    private Color rectBorderColor;
    private Color rectContentColor;
    //
    [SerializeField]
    private float rectContentColorAlpha = 0.1f;
    private Vector3 mousePositionStartPos = Vector3.zero;
    private Vector3 mousePositionEndPos = Vector3.zero;
    //
    [SerializeField]
    //The texture of the drawing line. If using default material, result may be out of your expectation
    private Material rectMaterial = null;
    //
    private int mouseLeftPressingCounter = 0;
    private int mouseLeftPressingCounterMultipleSelectionLimit = 7;
    //
    private bool _isMouseLeftDown;
    private bool IsMouseLeftDown
    {
        get
        {
            return _isMouseLeftDown;
        }
        set
        {
            _isMouseLeftDown = value;
        }
    }
    //
    #endregion
    //
    //private static List<RTSGameUnit> _unitsSelectionAvailable;
    //private static List<RTSGameUnit> UnitsSelectionAvailable
    //{
    //    get
    //    {
    //        if (_unitsSelectionAvailable == null)
    //        {
    //            _unitsSelectionAvailable = new List<RTSGameUnit>();
    //        }
    //        return _unitsSelectionAvailable;
    //    }
    //}
    //
    //private static List<RTSGameUnit> _unitsSelected;
    //private static List<RTSGameUnit> UnitsSelected
    //{
    //    get
    //    {
    //        if (_unitsSelected == null)
    //        {
    //            _unitsSelected = new List<RTSGameUnit>();
    //        }
    //        return _unitsSelected;
    //    }
    //}
    //
    private bool IsSingleSelection
    {
        get
        {
            return !IsMouseLeftDown && mouseLeftPressingCounter < mouseLeftPressingCounterMultipleSelectionLimit;
        }
    }
    //
    private bool IsMultipleSelection
    {
        get
        {
            return IsMouseLeftDown && mouseLeftPressingCounter > mouseLeftPressingCounterMultipleSelectionLimit;
        }
    }
    //
    #region event register&unregister
    //
    //public static void eventRegister(DGameUnitMultipleSelectionValidUnitsListGeneration eventHandler)
    //{
    //    if (multipleSelectionValidListGenerationEvent == null)
    //    {
    //        multipleSelectionValidListGenerationEvent = new DGameUnitMultipleSelectionValidUnitsListGeneration(eventHandler);
    //    }
    //    else
    //    {
    //        multipleSelectionValidListGenerationEvent += eventHandler;
    //    }
    //}
    ////
    //public static void eventUnRegister(DGameUnitMultipleSelectionValidUnitsListGeneration eventHandler)
    //{
    //    if (multipleSelectionValidListGenerationEvent == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        multipleSelectionValidListGenerationEvent -= eventHandler;
    //    }
    //}
    ////
    ////
    //public static void eventRegister(DGameUnitMultipleSelectionSelectedUnitsNotification eventHandler)
    //{
    //    if (multipleSelectionValidListGenerationEvent == null)
    //    {
    //        multipleSelectionSelectedDoneNotificationEvent = new DGameUnitMultipleSelectionSelectedUnitsNotification(eventHandler);
    //    }
    //    else
    //    {
    //        multipleSelectionSelectedDoneNotificationEvent += eventHandler;
    //    }
    //}
    ////
    //public static void eventUnRegister(DGameUnitMultipleSelectionSelectedUnitsNotification eventHandler)
    //{
    //    if (multipleSelectionSelectedDoneNotificationEvent == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        multipleSelectionSelectedDoneNotificationEvent -= eventHandler;
    //    }
    //}
    //
    //public static void eventRegister(DGameUnitSelectionRelease eventHandler)
    //{
    //    if (selectionReleaseEvent == null)
    //    {
    //        selectionReleaseEvent = new DGameUnitSelectionRelease(eventHandler);
    //    }
    //    else
    //    {
    //        selectionReleaseEvent += eventHandler;
    //    }
    //}
    ////
    //public static void eventUnRegister(DGameUnitSelectionRelease eventHandler)
    //{
    //    if (selectionReleaseEvent == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        selectionReleaseEvent -= eventHandler;
    //    }
    //}

    //public static void eventRegister(DGameUnitSingleSelectionSelected eventHandler)
    //{
    //    if (singleSelectionSelectedEvent == null)
    //    {
    //        singleSelectionSelectedEvent = new DGameUnitSingleSelectionSelected(eventHandler);
    //    }
    //    else
    //    {
    //        singleSelectionSelectedEvent += eventHandler;
    //    }
    //}
    ////
    //public static void eventUnRegister(DGameUnitSingleSelectionSelected eventHandler)
    //{
    //    if (singleSelectionSelectedEvent == null)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        singleSelectionSelectedEvent -= eventHandler;
    //    }
    //}
    //
    #endregion
    //
    //public static void addSelectionListener(RTSGameUnit unit)
    //{
    //    if (unit == null)
    //    {
    //        return;
    //    }
    //    if (!unit.IsAllowMultipleSelection)
    //    {
    //        return;
    //    }
    //    lock (UnitsSelectionAvailable)
    //    {
    //        UnitsSelectionAvailable.Add(unit);
    //    }
    //}
    ////
    //public static void removeSelectionListener(RTSGameUnit unit)
    //{
    //    if (unit == null)
    //    {
    //        return;
    //    }
    //    lock (UnitsSelectionAvailable)
    //    {
    //        if (UnitsSelectionAvailable.Contains(unit))
    //        {
    //            UnitsSelectionAvailable.Remove(unit);
    //        }
    //    }
    //}
    //
    public static void selectionRelease()
    {
        if (singleSelectedUnit != null)
        {
            singleSelectedUnit.IsSelected = false;
            singleSelectedUnit = null;
        }
        //

		List<RTSGameUnit> selectedUnits =PlayerInfoManager.ShareInstance.currentPlayer.SelectedUnits;
        if (selectedUnits == null || selectedUnits.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            RTSGameUnit unit = selectedUnits[i];
            if (unit != null)
            {
                unit.IsSelected = false;
            }
        }
        selectedUnits.Clear();
    }

    //public static void addSelectionReleaseListener(RTSGameUnit unit)
    //{
    //    if (unit == null)
    //    {
    //        return;
    //    }
    //    UnitsSelected.Add(unit);
    //}

    //public static void removeSelectionReleaseListener(RTSGameUnit unit)
    //{
    //    if (unit == null)
    //    {
    //        return;
    //    }
    //    if (UnitsSelected.Contains(unit))
    //    {
    //        UnitsSelected.Remove(unit);
    //    }
    //}
    //
    //private static void selectionReleaseEventInvoke()
    //{
    //    if (selectionReleaseEvent != null)
    //    {
    //        selectionReleaseEvent.Invoke();
    //    }
    //}
    //
    private void singleSelectionEventInvoke()
    {
        // Debug.Log("singleSelectionEventInvoke");
        if (RTSCameraController.RTSCamera)
        {
            Ray ray = RTSCameraController.RTSCamera.ScreenPointToRay(InputManager.ShareInstance.MousePosition);
            RaycastHit hitInfo = new RaycastHit();
            //
            // Debug.DrawLine(ray.origin, ray.origin+1000f*ray.direction, Color.red);
            // Debug.Log("RTSLayerManager.ShareInstance.LayerMaskRayCastMouse0 = "+RTSLayerManager.ShareInstance.LayerMaskRayCastMouse0);
            //
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, 2000f, RTSLayerManager.ShareInstance.LayerMaskRayCastMouse0, QueryTriggerInteraction.Ignore))
            {
                GameObject hitObj = hitInfo.collider.gameObject;
                // Debug.Log("hit");
                // Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
                //if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberUI) {
                //    Debug.LogError("hit UI");
                //    return;
                //}else {
                //
                //Debug.LogError("hit => " + hitObj.name);
                //
                //selectionRelease();
                RTSGameUnit gameUnit = hitObj.GetComponent<RTSGameUnit>();
                // RTSGameUnit gameUnit = (RTSGameUnit)hitObj.GetComponent("RTSGameUnit");
                //            
                //
                if (gameUnit)
                {
                    if (gameUnit.IsAllowSingleSelection)
                    {
                        gameUnit.IsSelected = true;
                        singleSelectedUnit = gameUnit;
                        //lock (UnitsSelected)
                        //{
                        //    UnitsSelected.Add(gameUnit);
                        //}
                    }
                }
                // if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberPlayerGameUnit)
                // {
                //     //Debug.LogError("hit ground");
                // }
                // else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberEnemyGameUnit)
                // { 
                // }else if (hitObj.layer == RTSLayerManager.ShareInstance.LayerNumberFriendlyGameUnit) { 
                // }
                //}
            }
        }
    }
    //
    private void multipleSelectionEventInvoke()
    {
        if (!RTSCameraController.RTSCamera)
        {
            return;
        }
        //
        //if (multipleSelectionValidListGenerationEvent == null)
        //{
        //    return;
        //}
        if (mousePositionStartPos.x == mousePositionEndPos.x || mousePositionStartPos.y == mousePositionEndPos.y)
        {
            //not a rect
            return;
        }
        //
        float xMin = 0f;
        float xMax = 0f;
        float yMin = 0f;
        float yMax = 0f;
        //
        if (mousePositionStartPos.x > mousePositionEndPos.x)
        {
            xMax = mousePositionStartPos.x;
            xMin = mousePositionEndPos.x;
        }
        else
        {
            xMax = mousePositionEndPos.x;
            xMin = mousePositionStartPos.x;
        }
        //
        if (mousePositionStartPos.y > mousePositionEndPos.y)
        {
            yMax = mousePositionStartPos.y;
            yMin = mousePositionEndPos.y;
        }
        else
        {
            yMax = mousePositionEndPos.y;
            yMin = mousePositionStartPos.y;
        }
        ////
        //
		List<RTSGameUnit> UnitsSelectionAvailable = PlayerInfoManager.ShareInstance.currentPlayer.AllUnits;
        if (UnitsSelectionAvailable == null)
        {
            return;
        }
        for (int i = 0; i < UnitsSelectionAvailable.Count; i++)
        {
            RTSGameUnit unit = UnitsSelectionAvailable[i];
            //
            if (unit != null && unit.IsAllowMultipleSelection && unit.IsVisible)
            {
                //
                Vector3 screenPos = RTSCameraController.RTSCamera.WorldToScreenPoint(unit.transform.position);
                //
                if (screenPos.x >= xMin && screenPos.x <= xMax && screenPos.y >= yMin && screenPos.y <= yMax)
                {
                    //inside the rect
                    unit.IsSelected = true;
                    //lock (UnitsSelected)
                    //{
                    //    UnitsSelected.Add(unit);
                    //}
                }
                else
                {
                    //outside the rect
                    unit.IsSelected = false;
                }
            }
        }
        //
        //RTSGameUnit[] temp = multipleSelectionValidListGenerationEvent.Invoke();
        //if (temp == null)
        //{
        //    return;
        //}
        //List<RTSGameUnit> selectedUnits = new List<RTSGameUnit>();
        //for (int i = 0; i < temp.Length; i++)
        //{
        //    RTSGameUnit unit = temp[i];
        //    //
        //    if (unit != null && unit.IsAllowMultipleSelection && unit.IsVisible)
        //    {
        //        //
        //        Vector3 screenPos = RTSCameraController.RTSCamera.WorldToScreenPoint(unit.transform.position);
        //        //
        //        if (screenPos.x >= xMin && screenPos.x <= xMax && screenPos.y >= yMin && screenPos.y <= yMax)
        //        {
        //            //inside the rect
        //            unit.IsSelected = true;
        //        }
        //        else
        //        {
        //            //outside the rect
        //            unit.IsSelected = false;
        //        }
        //    }
        //}
        ////
        //if (selectedUnits != null && multipleSelectionSelectedDoneNotificationEvent != null && selectedUnits.Count > 0)
        //{
        //    RTSGameUnit[] copy = new RTSGameUnit[selectedUnits.Count];
        //    selectedUnits.CopyTo(copy);
        //    multipleSelectionSelectedDoneNotificationEvent.Invoke(copy);
        //}
    }
    //
    private void OnMouseLeftKeyPressing(KeyCode keyCode)
    {
        if (!Enabled)
        {
            return;
        }
        //
        mouseLeftPressingCounter++;
    }
    //
    private void OnMouseLeftKeyPressed(KeyCode keyCode)
    {
        IsMouseLeftDown = true;
        //
        if (!Enabled)
        {
            return;
        }
        //
        mousePositionStartPos = InputManager.ShareInstance.MousePosition;
        //
        IsRayCastingOnGUI = UIRayCastInterceptor.ShareInstance.IsRayCastingGUI(mousePositionStartPos);
        //
        if (!IsRayCastingOnGUI) {
            selectionRelease();
        }
        //
    }
    //
    private void OnMouseLeftKeyRelease(KeyCode keyCode)
    {
        //
        IsMouseLeftDown = false;
        //
        if (Enabled)
        {
            if (!IsRayCastingOnGUI)
            {
                if (IsSingleSelection)
                {
                    singleSelectionEventInvoke();
                }
                else
                {
                    multipleSelectionEventInvoke();
                }
            }
        }
        //
        IsRayCastingOnGUI = false;
        //
        mouseLeftPressingCounter = 0;
        //
    }
    //
    void OnPostRender()
    {
        if (IsMultipleSelection)
        {
            mousePositionEndPos = InputManager.ShareInstance.MousePosition;
            //
            //Saving camer matrix
            GL.PushMatrix();
            //
            if (!rectMaterial)
            {
                Debug.LogError("Missing RTS Selection Rectangle Drawing Material");
                return;
            }
            //
            rectMaterial.SetPass(0);
            //Setting use screen pos to draw
            GL.LoadPixelMatrix();
            GL.Begin(GL.QUADS);
            //rectContentColor should be transparent
            GL.Color(rectContentColor);
            GL.Vertex3(mousePositionStartPos.x, mousePositionStartPos.y, 0);
            GL.Vertex3(mousePositionEndPos.x, mousePositionStartPos.y, 0);
            GL.Vertex3(mousePositionEndPos.x, mousePositionEndPos.y, 0);
            GL.Vertex3(mousePositionStartPos.x, mousePositionEndPos.y, 0);
            GL.End();
            GL.Begin(GL.LINES);
            //rectBorderColor should be opaque
            GL.Color(rectBorderColor);
            GL.Vertex3(mousePositionStartPos.x, mousePositionStartPos.y, 0);
            GL.Vertex3(mousePositionEndPos.x, mousePositionStartPos.y, 0);
            GL.Vertex3(mousePositionEndPos.x, mousePositionStartPos.y, 0);
            GL.Vertex3(mousePositionEndPos.x, mousePositionEndPos.y, 0);
            GL.Vertex3(mousePositionEndPos.x, mousePositionEndPos.y, 0);
            GL.Vertex3(mousePositionStartPos.x, mousePositionEndPos.y, 0);
            GL.Vertex3(mousePositionStartPos.x, mousePositionEndPos.y, 0);
            GL.Vertex3(mousePositionStartPos.x, mousePositionStartPos.y, 0);
            GL.End();
            //Recover camer matrix
            GL.PopMatrix();
        }
    }
    //
    private void Awake()
    {
        //
        if (!rectMaterial)
        {
            Destroy(this);
            return;
        }
        rectContentColor = new Color(rectMaterial.color.r, rectMaterial.color.g, rectMaterial.color.b, rectContentColorAlpha);
        rectBorderColor = new Color(rectMaterial.color.r, rectMaterial.color.g, rectMaterial.color.b);
    }
    //
    private void Start()
    {
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftKeyPressed);
        InputManager.ShareInstance.InputEventHandlerRegister_GetKey(KeyCode.Mouse0, OnMouseLeftKeyPressing);
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp(KeyCode.Mouse0, OnMouseLeftKeyRelease);
        //
    }
    //
    private void OnDestroy()
    {
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(KeyCode.Mouse0, OnMouseLeftKeyPressed);
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetKey(KeyCode.Mouse0, OnMouseLeftKeyPressing);
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp(KeyCode.Mouse0, OnMouseLeftKeyRelease);
    }
    //
}
