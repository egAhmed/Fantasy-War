using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public sealed partial class InputManager
{
    #region Event Handler Register here
    /// <summary>
    /// Input.GetAxis("Vertical")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_Vertical(DInputManagerGetAxis_Vertical eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_Vertical == null)
        {
            EventGetAxis_Vertical = new DInputManagerGetAxis_Vertical(eventHandler);
        }
        else
        {
            EventGetAxis_Vertical += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetAxis("Horizontal")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_Horizontal(DInputManagerGetAxis_Horizontal eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_Horizontal == null)
        {
            EventGetAxis_Horizontal = new DInputManagerGetAxis_Horizontal(eventHandler);
        }
        else
        {
            EventGetAxis_Horizontal += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetAxis("Horizontal") and Input.GetAxis("Vertical")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_HorizontalVertical(DInputManagerGetAxis_HorizontalVertical eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_HorizontalVertical == null)
        {
            EventGetAxis_HorizontalVertical = new DInputManagerGetAxis_HorizontalVertical(eventHandler);
        }
        else
        {
            EventGetAxis_HorizontalVertical += eventHandler;
        }
    }

    /// <summary>
    /// Input.GetAxis("Mouse Y")&Input.GetAxis("Mouse X")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_MouseXMouseY(DInputManagerGetAxis_MouseXMouseY eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_MouseXMouseY == null)
        {
            EventGetAxis_MouseXMouseY = new DInputManagerGetAxis_MouseXMouseY(eventHandler);
        }
        else
        {
            EventGetAxis_MouseXMouseY += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetAxis("Mouse X")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_MouseX(DInputManagerGetAxis_MouseX eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_MouseX == null)
        {
            EventGetAxis_MouseX = new DInputManagerGetAxis_MouseX(eventHandler);
        }
        else
        {
            EventGetAxis_MouseX += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetAxis("Mouse Y")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_MouseY(DInputManagerGetAxis_MouseY eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_MouseY == null)
        {
            EventGetAxis_MouseY = new DInputManagerGetAxis_MouseY(eventHandler);
        }
        else
        {
            EventGetAxis_MouseY += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetKey()
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetKey(KeyCode keyCode, DInputManagerGetKey eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventDict_GetKey == null)
        {
            return;
        }
        if (!EventDict_GetKey.ContainsKey(keyCode))
        {
            lock (EventDict_GetKey)
            {
                EventDict_GetKey.Add(keyCode, new DInputManagerGetKey(eventHandler));
            }
        }
        else
        {
            EventDict_GetKey[keyCode] += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetKeyDown()
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetKeyDown(KeyCode keyCode, DInputManagerGetKeyDown eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventDict_GetKeyDown == null)
        {
            return;
        }
        if (FlagDict_IsKeyDown != null && !FlagDict_IsKeyDown.ContainsKey(keyCode))
        {
            lock (FlagDict_IsKeyDown)
            {
                FlagDict_IsKeyDown.Add(keyCode, false);
            }
        }
        if (!EventDict_GetKeyDown.ContainsKey(keyCode))
        {
            lock (EventDict_GetKeyDown)
            {
                EventDict_GetKeyDown.Add(keyCode, new DInputManagerGetKeyDown(eventHandler));
            }
        }
        else
        {
            EventDict_GetKeyDown[keyCode] += eventHandler;
        }
    }
    /// <summary>
    /// Input.GetKeyUp()
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetKeyUp(KeyCode keyCode, DInputManagerGetKeyUp eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventDict_GetKeyUp == null)
        {
            return;
        }
        if (FlagDict_IsKeyDown != null && !FlagDict_IsKeyDown.ContainsKey(keyCode))
        {
            lock (FlagDict_IsKeyDown)
            {
                FlagDict_IsKeyDown.Add(keyCode, false);
            }
        }

        if (!EventDict_GetKeyUp.ContainsKey(keyCode))
        {
            lock (EventDict_GetKeyUp)
            {
                EventDict_GetKeyUp.Add(keyCode, new DInputManagerGetKeyUp(eventHandler));
            }
        }
        else
        {
            EventDict_GetKeyUp[keyCode] += eventHandler;
        }
    }

    /// <summary>
    /// Input.GetAxis("Mouse ScrollWheel")
    /// </summary>
    /// <param name="eventHandler"></param>
    public void InputEventHandlerRegister_GetAxis_MouseScrollWheel(DInputManagerGetAxis_MouseScrollWheel eventHandler)
    {
        if (eventHandler == null)
        {
            return;
        }
        if (EventGetAxis_MouseScrollWheel == null)
        {
            EventGetAxis_MouseScrollWheel = new DInputManagerGetAxis_MouseScrollWheel(eventHandler);
        }
        else
        {
            EventGetAxis_MouseScrollWheel += eventHandler;
        }
    }
    #endregion

}
