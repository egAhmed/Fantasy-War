using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public sealed partial class InputManager
{
    #region Event Handler UnRegister here
    public void InputEventHandlerUnRegister_GetAxis_HorizontalVertical(DInputManagerGetAxis_HorizontalVertical eventHandler)
    {
        if (eventHandler == null || EventGetAxis_HorizontalVertical == null)
        {
            return;
        }
        EventGetAxis_HorizontalVertical -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_Vertical(DInputManagerGetAxis_Vertical eventHandler)
    {
        if (eventHandler == null || EventGetAxis_Vertical == null)
        {
            return;
        }
        EventGetAxis_Vertical -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_Horizontal(DInputManagerGetAxis_Horizontal eventHandler)
    {
        if (eventHandler == null || EventGetAxis_Horizontal == null)
        {
            return;
        }
        EventGetAxis_Horizontal -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_MouseX(DInputManagerGetAxis_MouseX eventHandler)
    {
        if (eventHandler == null || EventGetAxis_MouseX == null)
        {
            return;
        }
        EventGetAxis_MouseX -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_MouseY(DInputManagerGetAxis_MouseY eventHandler)
    {
        if (eventHandler == null || EventGetAxis_MouseY == null)
        {
            return;
        }
        EventGetAxis_MouseY -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_MouseXMouseY(DInputManagerGetAxis_MouseXMouseY eventHandler)
    {
        if (eventHandler == null || EventGetAxis_MouseXMouseY == null)
        {
            return;
        }
        EventGetAxis_MouseXMouseY -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetKey(KeyCode keyCode, DInputManagerGetKey eventHandler)
    {
        if (eventHandler == null || EventDict_GetKey == null || EventDict_GetKey.Count <= 0 || !EventDict_GetKey.ContainsKey(keyCode))
        {
            return;
        }
        DInputManagerGetKey d = EventDict_GetKey[keyCode];
        if (d == null)
        {
            return;
        }
        d -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetKeyDown(KeyCode keyCode, DInputManagerGetKeyDown eventHandler)
    {
        if (eventHandler == null || EventDict_GetKeyDown == null || EventDict_GetKeyDown.Count <= 0 || !EventDict_GetKeyDown.ContainsKey(keyCode))
        {
            return;
        }
        DInputManagerGetKeyDown d = EventDict_GetKeyDown[keyCode];
        if (d == null)
        {
            return;
        }
        d -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetKeyUp(KeyCode keyCode, DInputManagerGetKeyUp eventHandler)
    {
        if (eventHandler == null || EventDict_GetKeyUp == null || EventDict_GetKeyUp.Count <= 0 || !EventDict_GetKeyUp.ContainsKey(keyCode))
        {
            return;
        }
        DInputManagerGetKeyUp d = EventDict_GetKeyUp[keyCode];
        if (d == null)
        {
            return;
        }
        d -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_MouseScrollWheel(DInputManagerGetAxis_MouseScrollWheel eventHandler)
    {
        if (eventHandler == null || EventGetAxis_MouseScrollWheel == null)
        {
            return;
        }
        EventGetAxis_MouseScrollWheel -= eventHandler;
    }
    #endregion

}
