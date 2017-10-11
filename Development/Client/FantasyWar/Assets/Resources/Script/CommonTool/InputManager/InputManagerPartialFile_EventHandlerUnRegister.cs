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
        //lock (EventGetAxis_HorizontalVertical)
        EventGetAxis_HorizontalVertical -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_Vertical(DInputManagerGetAxis_Vertical eventHandler)
    {
        if (eventHandler == null || EventGetAxis_Vertical == null)
        {
            return;
        }
        //lock (EventGetAxis_Vertical)
        EventGetAxis_Vertical -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetAxis_Horizontal(DInputManagerGetAxis_Horizontal eventHandler)
    {
        if (eventHandler == null || EventGetAxis_Horizontal == null)
        {
            return;
        }
        //lock (EventGetAxis_Horizontal) 
        EventGetAxis_Horizontal -= eventHandler;
        
    }
    public void InputEventHandlerUnRegister_GetAxis_MouseX(DInputManagerGetAxis_MouseX eventHandler)
    {
        if (eventHandler == null || EventGetAxis_MouseX == null)
        {
            return;
        }
        //lock (EventGetAxis_MouseX) 
        EventGetAxis_MouseX -= eventHandler;
        
    }
    public void InputEventHandlerUnRegister_GetAxis_MouseY(DInputManagerGetAxis_MouseY eventHandler)
    {
        if (eventHandler == null || EventGetAxis_MouseY == null)
        {
            return;
        }
        //lock (EventGetAxis_MouseY)  
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
        EventDict_GetKey[keyCode] -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetKeyDown(KeyCode keyCode, DInputManagerGetKeyDown eventHandler)
    {
        if (eventHandler == null || EventDict_GetKeyDown == null || EventDict_GetKeyDown.Count <= 0 || !EventDict_GetKeyDown.ContainsKey(keyCode))
        {
            return;
        }
        EventDict_GetKeyDown[keyCode] -= eventHandler;
    }
    public void InputEventHandlerUnRegister_GetKeyUp(KeyCode keyCode, DInputManagerGetKeyUp eventHandler)
    {
        if (eventHandler == null || EventDict_GetKeyUp == null || EventDict_GetKeyUp.Count <= 0 || !EventDict_GetKeyUp.ContainsKey(keyCode))
        {
            return;
        }
        EventDict_GetKeyUp[keyCode] -= eventHandler;
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
