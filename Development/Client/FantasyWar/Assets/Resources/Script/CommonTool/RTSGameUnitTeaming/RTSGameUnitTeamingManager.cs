﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RTSGameUnitTeamingManager : UnitySingleton<RTSGameUnitTeamingManager>
{
    private bool isTeamingMode = false;
    // private const KeyCode TEAMING_CONTROL_KEY = KeyCode.T;
    private const KeyCode TEAMING_CONTROL_KEY = KeyCode.LeftShift;
    private readonly KeyCode[] TEAMING_TEAM_KEYS = new KeyCode[] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };
    private Dictionary<KeyCode, RTSGameUnit[]> _teamDict;
    private Dictionary<KeyCode, RTSGameUnit[]> TeamDict
    {
        get
        {
            if (_teamDict == null)
            {
                _teamDict = new Dictionary<KeyCode, RTSGameUnit[]>();
            }
            return _teamDict;
        }
        set
        {
            _teamDict = value;
        }
    }

    void setTeam(KeyCode key)
    {
        // Debug.Log("setting team");
        List<RTSGameUnit> sl = RTSGameUnitManager.ShareInstance.SelectedUnits;
        if (sl == null || sl.Count <= 0)
        {
            return;
        }
        RTSGameUnit[] copy = new RTSGameUnit[sl.Count];
        sl.CopyTo(copy);
        TeamDict[key] = copy;
    }

    void callTeam(KeyCode key)
    {
        // Debug.Log("calling team");
        //
        RTSGameUnit[] calling = TeamDict[key];
        //
        if (calling != null)
        {
            foreach (RTSGameUnit unit in calling)
            {
                if (unit != null)
                {
                    unit.IsSelected = true;
                }
            }
        }
    }

    private void OnTeamingTeamKeyDown(KeyCode key)
    {

        // Debug.Log("TEAMING_CONTROL_KEY is down =>" + InputManager.ShareInstance.isKeyDown(TEAMING_CONTROL_KEY));
        //
        if (isTeamingMode)
        {
            setTeam(key);
        }
        else
        {
            callTeam(key);
        }
    }

    private void OnControlKeyDown(KeyCode keyCode)
    {
        // Debug.Log("OnLeftControlKeyDown");
        isTeamingMode = true;
    }

    private void OnControlKeyUp(KeyCode keyCode)
    {
        // Debug.Log("OnLeftControlKeyUp");
        isTeamingMode = false;
    }

    // Use this for initialization
    public void Start()
    {
        //
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(TEAMING_CONTROL_KEY, OnControlKeyDown);
        InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp(TEAMING_CONTROL_KEY, OnControlKeyUp);
        //
        if (TEAMING_TEAM_KEYS != null && TEAMING_TEAM_KEYS.Length > 0)
        {
            foreach (KeyCode key in TEAMING_TEAM_KEYS)
            {
                if (!TeamDict.ContainsKey(key))
                {
                    lock (TeamDict)
                    {
                        TeamDict.Add(key, null);
                    }
                }
                //
                InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(key, OnTeamingTeamKeyDown);
            }
        }
        //
    }

    private void OnDestroy()
    {
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(TEAMING_CONTROL_KEY, OnControlKeyDown);
        InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp(TEAMING_CONTROL_KEY, OnControlKeyUp);
        //
        TeamDict.Clear();
        TeamDict = null;
        //
        if (TEAMING_TEAM_KEYS != null && TEAMING_TEAM_KEYS.Length > 0)
        {
            foreach (KeyCode key in TEAMING_TEAM_KEYS)
            {
                InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(key, OnTeamingTeamKeyDown);
            }
        }
    }
}
