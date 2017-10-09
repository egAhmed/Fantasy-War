using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void DInputManagerGetAxis_MouseX(float axis_mouseX);
public delegate void DInputManagerGetAxis_MouseY(float axis_mouseY);
public delegate void DInputManagerGetAxis_MouseXMouseY(float axis_mouseX, float axis_mouseY);
public delegate void DInputManagerGetAxis_MouseScrollWheel(float axis_mouseScrollWheel);
//
public delegate void DInputManagerGetAxis_Horizontal(float axis_horizontal);
public delegate void DInputManagerGetAxis_Vertical(float axis_vertical);
public delegate void DInputManagerGetAxis_HorizontalVertical(float axis_horizontal, float axis_vertical);
//
public delegate void DInputManagerGetKeyDown(KeyCode key);
public delegate void DInputManagerGetKeyUp(KeyCode key);
public delegate void DInputManagerGetKey(KeyCode key);

public sealed partial class InputManager
{
}
