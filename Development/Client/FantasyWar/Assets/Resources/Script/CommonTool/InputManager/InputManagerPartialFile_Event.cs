using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class InputManager
{
    #region Input Event
    private event DInputManagerGetAxis_Vertical _eventGetAxis_Vertical;
    private DInputManagerGetAxis_Vertical EventGetAxis_Vertical
    {
        get { return _eventGetAxis_Vertical; }
        set { _eventGetAxis_Vertical = value; }
    }
    private event DInputManagerGetAxis_Horizontal _eventGetAxis_Horizontal;
    private DInputManagerGetAxis_Horizontal EventGetAxis_Horizontal
    {
        get { return _eventGetAxis_Horizontal; }
        set { _eventGetAxis_Horizontal = value; }
    }
    private event DInputManagerGetAxis_HorizontalVertical _eventGetAxis_HorizontalVertical;
    private DInputManagerGetAxis_HorizontalVertical EventGetAxis_HorizontalVertical
    {
        get { return _eventGetAxis_HorizontalVertical; }
        set { _eventGetAxis_HorizontalVertical = value; }
    }
    private event DInputManagerGetAxis_MouseX _eventGetAxis_MouseX;
    private DInputManagerGetAxis_MouseX EventGetAxis_MouseX
    {
        get { return _eventGetAxis_MouseX; }
        set { _eventGetAxis_MouseX = value; }
    }
    private event DInputManagerGetAxis_MouseY _eventGetAxis_MouseY;
    private DInputManagerGetAxis_MouseY EventGetAxis_MouseY
    {
        get { return _eventGetAxis_MouseY; }
        set { _eventGetAxis_MouseY = value; }
    }
    private event DInputManagerGetAxis_MouseXMouseY _eventGetAxis_MouseXMouseY;
    private DInputManagerGetAxis_MouseXMouseY EventGetAxis_MouseXMouseY
    {
        get { return _eventGetAxis_MouseXMouseY; }
        set { _eventGetAxis_MouseXMouseY = value; }
    }
    private event DInputManagerGetAxis_MouseScrollWheel _eventGetAxis_MouseScrollWheel;
    private DInputManagerGetAxis_MouseScrollWheel EventGetAxis_MouseScrollWheel
    {
        get { return _eventGetAxis_MouseScrollWheel; }
        set { _eventGetAxis_MouseScrollWheel = value; }
    }
    #endregion

    
}
