using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class InputManager : UnitySingleton<InputManager>
{
    private bool _isIputWork=true;
    public bool IsInputWork
    {
        get { return _isIputWork; }
        set { _isIputWork = value; }
    }

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
            return !Mathf.Approximately(0, Axis_MouseX);
        }
    }
    public bool isAxisMouseYChanged
    {
        get
        {
            return !Mathf.Approximately(0, Axis_MouseY);
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
            return !Mathf.Approximately(0, Axis_Horizontal);
        }
    }
    public bool isVerticalAxisChanged
    {
        get
        {
            return !Mathf.Approximately(0, Axis_Vertical);
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
            return !Mathf.Approximately(0, Axis_MouseScrollWheel);
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
        get
        {
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
            //
            if (isAxisMouseXYChanged)
            {
                EventGetAxis_MouseXMouseY.Invoke(Axis_MouseX, Axis_MouseY);
            }
        }
        #endregion
        //
        #region GetKey check
        if (Input.anyKey&& IsInputWork)
        {
            //Debug.Log("Input.anyKey");
            KeyCode[] keyCodes=new KeyCode[EventDict_GetKey.Keys.Count];
            EventDict_GetKey.Keys.CopyTo(keyCodes, 0);
            for (int i = 0; i < keyCodes.Length; i++) {
                KeyCode keyCode = keyCodes[i];
               if (Input.GetKey(keyCode))
                    {
                        if (EventDict_GetKey[keyCode] != null)
                        {
                            EventDict_GetKey[keyCode].Invoke(keyCode);
                        }
                    }
            }
            //
        }
        #endregion
        //
        #region GetKeyDown check
        if (Input.anyKeyDown)//Including mouse click
        {
            //Debug.Log("Input.anyKeyDown");
            KeyCode[] keyCodes=new KeyCode[EventDict_GetKeyDown.Keys.Count];
            EventDict_GetKeyDown.Keys.CopyTo(keyCodes, 0);
            for (int i = 0; i < keyCodes.Length; i++) {
                KeyCode keyCode = keyCodes[i];
               //
                    if (Input.GetKeyDown(keyCode))
                {
                    if (EventDict_GetKeyDown[keyCode] != null)
                    {
                        //Debug.LogError(keyCode + "    is Down");
                        EventDict_GetKeyDown[keyCode].Invoke(keyCode);
                    }

                    if (FlagDict_IsKeyDown.ContainsKey(keyCode))
                    {
                        keyCodeDownUpStatusBinding(keyCode, true);
                    }
                }
            }
            //
        }
        #endregion
        //
        #region GetKeyUp check
        //Debug.Log("GetKeyUp check");
        //
         KeyCode[] keyCodesArr=new KeyCode[EventDict_GetKeyUp.Keys.Count];
            EventDict_GetKeyUp.Keys.CopyTo(keyCodesArr, 0);
            for (int i = 0; i < keyCodesArr.Length; i++) {
                KeyCode keyCode = keyCodesArr[i];
               //
            if (FlagDict_IsKeyDown.ContainsKey(keyCode))
            {
                if (FlagDict_IsKeyDown[keyCode] && !Input.GetKey(keyCode))
                {
                    keyCodeDownUpStatusBinding(keyCode, false);
                    if (EventDict_GetKeyUp[keyCode] != null)
                    {
                        //Debug.LogError(keyCode + "    is Up");
                        EventDict_GetKeyUp[keyCode].Invoke(keyCode);
                    }
                }
            }
                //
            }
            //
        #endregion

    }
}
