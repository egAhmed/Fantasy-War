using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class InputManager : UnitySingleton<InputManager>
{
    #region axis property
    private float _axis_MouseY;
    public float Axis_MouseY
    {
        get
        {
            return _axis_MouseY;
        }
    }

    private float _axis_MouseX;
    public float Axis_MouseX
    {
        get
        {
            return _axis_MouseX;
        }
    }
    public bool isAxisMouseXChanged
    {
        get
        {
            return Axis_MouseX != 0;
        }
    }
    public bool isAxisMouseYChanged
    {
        get
        {
            return Axis_MouseY != 0;
        }
    }
    public bool isAxisMouseXYChanged
    {
        get
        {
            return isAxisMouseXChanged || isAxisMouseYChanged;
        }
    }

    private float _axis_Horizontal;
    public float Axis_Horizontal
    {
        get
        {
            return _axis_Horizontal;
        }
    }

    private float _axis_Vertical;
    public float Axis_Vertical
    {
        get
        {
            return _axis_Vertical;
        }
    }
    public bool isHorizontalAxisChanged
    {
        get
        {
            return Axis_Horizontal != 0;
        }
    }
    public bool isVerticalAxisChanged
    {
        get
        {
            return Axis_Vertical != 0;
        }
    }
    public bool isHorizontalOrVerticalAxisChanged
    {
        get
        {
            return isHorizontalAxisChanged || isVerticalAxisChanged;
        }
    }

    private float _axis_MouseScrollWheel;
    public float Axis_MouseScrollWheel
    {
        get
        {
            return _axis_MouseScrollWheel;
        }
    }
    public bool isAxisMouseScrollWheelChanged
    {
        get
        {
            return Axis_MouseScrollWheel != 0;
        }
    }

    #endregion

    #region screen property
    public float ScreenWidth
    {
        get
        {
            return Screen.width;
        }
    }

    public float ScreenHeight
    {
        get
        {
            return Screen.height;
        }
    }
    #endregion

    #region mouse position
    public Vector3 MousePosition
    {
        get {
            return Input.mousePosition;
        }
    }
    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        isKeyDownDictionaryKeyCodesBinding();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Input.mousePosition = >" + Input.mousePosition);
        //Debug.Log("Screen.width = >" + Screen.width);
        //Debug.Log("Screen.height = >" + Screen.height);
        //
        _axis_Horizontal = Input.GetAxis("Horizontal");
        _axis_Vertical = Input.GetAxis("Vertical");
        //
        #region Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")
        if (isHorizontalOrVerticalAxisChanged)
        {
            //Debug.Log("h = " + HorizontalAxisCurrentFrame + "," + "v = " + VerticalAxisCurrentFrame);
            //Debug.Log("Horizontal = " + Input.GetAxis("Horizontal") + "," + "Vertical = " + Input.GetAxis("Vertical"));
            if (!isHorizontalAxisChanged)
            {
                //Debug.Log("v = " + VerticalAxisCurrentFrame);
                if (EventGetAxis_Vertical != null)
                {
                    EventGetAxis_Vertical.Invoke(Axis_Vertical);
                }
            }
            else if (!isVerticalAxisChanged)
            {
                //Debug.Log("h = " + HorizontalAxisCurrentFrame);
                if (EventGetAxis_Horizontal != null)
                {
                    EventGetAxis_Horizontal.Invoke(Axis_Horizontal);
                }
            }
            else
            {
                //Debug.Log("h = " + Axis_Horizontal + "," + "v = " + Axis_Vertical);
                //Debug.Log("Horizontal = " + Input.GetAxis("Horizontal") + "," + "Vertical = " + Input.GetAxis("Vertical"));
                //
                if (EventGetAxis_HorizontalVertical != null)
                {
                    EventGetAxis_HorizontalVertical.Invoke(Axis_Horizontal, Axis_Vertical);
                }
            }
        }
        #endregion
        //
        #region Input.GetAxis("Mouse ScrollWheel")
        if (EventGetAxis_MouseScrollWheel != null)
        {
            _axis_MouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (isAxisMouseScrollWheelChanged)
            {
                EventGetAxis_MouseScrollWheel.Invoke(Axis_MouseScrollWheel);
            }
        }
        #endregion

        #region Input.GetAxis("Mouse X")&Input.GetAxis("Mouse Y")
        if (EventGetAxis_MouseXMouseY != null)
        {
            _axis_MouseX = Input.GetAxis("Mouse X");
            _axis_MouseY = Input.GetAxis("Mouse Y");
            if (isAxisMouseXYChanged)
            {
                EventGetAxis_MouseXMouseY.Invoke(Axis_MouseX, Axis_MouseY);
            }
        }
        #endregion
        //
        #region GetKey check
        if (Input.anyKey)
        {
            //Debug.Log("Input.anyKey");
            foreach (KeyCode keyCode in EventDict_GetKey.Keys)
            {
                if (Input.GetKey(keyCode))
                {
                    if (EventDict_GetKey[keyCode] != null)
                    {
                        EventDict_GetKey[keyCode].Invoke();
                    }
                }
            }
        }
        #endregion
        //
        #region GetKeyDown check
        if (Input.anyKeyDown)//Including mouse click
        {
            //Debug.Log("Input.anyKeyDown");
            foreach (KeyCode keyCode in EventDict_GetKeyDown.Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    if (EventDict_GetKeyDown[keyCode] != null)
                    {
                        //Debug.LogError(keyCode + "    is Down");
                        EventDict_GetKeyDown[keyCode].Invoke();
                    }

                    if (FlagDict_IsKeyDown.ContainsKey(keyCode))
                    {
                        keyCodeDownUpStatusBinding(keyCode, true);
                    }
                }
            }
        }
        #endregion
        //
        #region GetKeyUp check
        foreach (KeyCode keyCode in EventDict_GetKeyUp.Keys)
        {
            if (Input.GetKeyUp(keyCode))
            {
                if (EventDict_GetKeyUp[keyCode] != null)
                {
                    //Debug.LogError(keyCode + "    is Up");
                    EventDict_GetKeyUp[keyCode].Invoke();
                }

                if (FlagDict_IsKeyDown.ContainsKey(keyCode))
                {
                    keyCodeDownUpStatusBinding(keyCode, false);
                }
            }
        }
        #endregion
    }
}
