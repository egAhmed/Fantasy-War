using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed partial class InputManager
{
    private Dictionary<KeyCode, bool> _flagDict_IsKeyDown;
    private Dictionary<KeyCode, bool> FlagDict_IsKeyDown
    {
        get
        {
            if (_flagDict_IsKeyDown == null)
            {
                _flagDict_IsKeyDown = new Dictionary<KeyCode, bool>();
            }
            return _flagDict_IsKeyDown;
        }
    }

    private Dictionary<KeyCode, DInputManagerGetKey> _eventDict_GetKey;
    private Dictionary<KeyCode, DInputManagerGetKey> EventDict_GetKey
    {
        get
        {
            if (_eventDict_GetKey == null)
            {
                _eventDict_GetKey = new Dictionary<KeyCode, DInputManagerGetKey>();
            }
            return _eventDict_GetKey;
        }
    }

    private Dictionary<KeyCode, DInputManagerGetKeyDown> _eventDict_GetKeyDown;
    private Dictionary<KeyCode, DInputManagerGetKeyDown> EventDict_GetKeyDown
    {
        get
        {
            if (_eventDict_GetKeyDown == null)
            {
                _eventDict_GetKeyDown = new Dictionary<KeyCode, DInputManagerGetKeyDown>();
            }
            return _eventDict_GetKeyDown;
        }
    }

    private Dictionary<KeyCode, DInputManagerGetKeyUp> _eventDict_GetKeyUp;
    private Dictionary<KeyCode, DInputManagerGetKeyUp> EventDict_GetKeyUp
    {
        get
        {
            if (_eventDict_GetKeyUp == null)
            {
                _eventDict_GetKeyUp = new Dictionary<KeyCode, DInputManagerGetKeyUp>();
            }
            return _eventDict_GetKeyUp;
        }
    }

    private void isKeyDownDictionaryKeyCodesBinding()
    {
        //keyCodeDownUpStatusBinding(KeyCode.Q, false);
        //keyCodeDownUpStatusBinding(KeyCode.W, false);
        //keyCodeDownUpStatusBinding(KeyCode.E, false);
        //keyCodeDownUpStatusBinding(KeyCode.R, false);
        //keyCodeDownUpStatusBinding(KeyCode.A, false);
        //keyCodeDownUpStatusBinding(KeyCode.D, false);
        //keyCodeDownUpStatusBinding(KeyCode.F, false);
        //keyCodeDownUpStatusBinding(KeyCode.H, false);
        //keyCodeDownUpStatusBinding(KeyCode.Space, false);
        //keyCodeDownUpStatusBinding(KeyCode.Escape, false);
    }

    public bool isKeyCodeDownStatusMonitoring(KeyCode keyCode)
    {
        return FlagDict_IsKeyDown != null && FlagDict_IsKeyDown.Count > 0 && FlagDict_IsKeyDown.ContainsKey(keyCode);
    }

    public bool isKeyDown(KeyCode keyCode)
    {
        return FlagDict_IsKeyDown != null && FlagDict_IsKeyDown.Count > 0 && FlagDict_IsKeyDown.ContainsKey(keyCode) && FlagDict_IsKeyDown[keyCode];
    }

    public void keyCodeDownUpStatusBinding(KeyCode keyCode, bool isDown)
    {
        if (FlagDict_IsKeyDown == null)
        {
            return;
        }
        if (!FlagDict_IsKeyDown.ContainsKey(keyCode))
        {
            lock (FlagDict_IsKeyDown)
            {
                FlagDict_IsKeyDown.Add(keyCode, isDown);
            }
        }
        else
        {
            FlagDict_IsKeyDown[keyCode] = isDown;
        }
    }
}