using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]
    private float zoomSpeed = 1.2F;
    [SerializeField, Range(0f, 10f)]
    private float stretchSpeed = 5F;
    [SerializeField, Range(0f, 1f)]
    private float stretchAxis = 0.1F;
    //
    private float mouseDetectionLimitDistance = 50f;
    //
    private static Camera _rtsCamera;
    public static Camera RTSCamera
    {
        get
        {
            return _rtsCamera;
        }
    }
    //
    #region InputManager delegate
    //
    private void OnMouseScrolled(float scrollAxis)
    {
        Vector3 v3Zooming = transform.forward.normalized * scrollAxis * zoomSpeed;
        transform.Translate(v3Zooming);//Space.Self
    }
    //
    private void OnMouseMoved(float mouseXAxis, float mouseYAxis)
    {
        //Debug.Log("OnMouseMoved");
        Vector3 v3Moving = new Vector3(mouseXAxis, 0, mouseYAxis) * stretchSpeed;
        transform.Translate(v3Moving, Space.World);
    }
    #endregion
    //
    private void Awake()
    {
        _rtsCamera = GetComponent<Camera>();
    }
    //
    private void Start()
    {
        InputManager.ShareInstance.InputEventHandlerRegister_GetAxis_MouseScrollWheel(OnMouseScrolled);
        //InputManager.ShareInstance.InputEventHandlerRegister_GetAxis_MouseXMouseY(OnMouseMoved);
    }

    private void OnDestroy()
    {
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetAxis_MouseScrollWheel(OnMouseScrolled);
        //InputManager.ShareInstance.InputEventHandlerUnRegister_GetAxis_MouseXMouseY(OnMouseMoved);
    }

    private void Update()
    {
        float mouseX = InputManager.ShareInstance.MousePosition.x;
        float mouseY = InputManager.ShareInstance.MousePosition.y;
        //
        float mouseXAxis = 0f;
        float mouseYAxis = 0f;
        //
        float rightBorder = InputManager.ShareInstance.ScreenWidth - mouseDetectionLimitDistance;
        float leftBorder = mouseDetectionLimitDistance;
        float topBorder = InputManager.ShareInstance.ScreenHeight - mouseDetectionLimitDistance;
        float bottomBorder = mouseDetectionLimitDistance;
        //
        if (mouseX >= rightBorder)
        {
            mouseXAxis = stretchAxis;
        }
        else if (mouseX <= leftBorder)
        {
            mouseXAxis = -stretchAxis;
        }
        //
        if (mouseY >= topBorder)
        {
            mouseYAxis = stretchAxis;
        }
        else if (mouseY <= bottomBorder)
        {
            mouseYAxis = -stretchAxis;
        }
        //
        //
        if (mouseXAxis != 0f || mouseYAxis != 0f)
        {
            OnMouseMoved(mouseXAxis, mouseYAxis);
        }
        //
    }
}
