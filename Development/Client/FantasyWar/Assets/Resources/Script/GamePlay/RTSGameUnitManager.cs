﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitManager : UnitySingleton<RTSGameUnitManager>
{
    Dictionary<RTSGameUnitBelongSide, List<RTSGameUnit>> _gameUnitsDic;
    private Dictionary<RTSGameUnitBelongSide, List<RTSGameUnit>> GameUnitsDic
    {
        get
        {
            if (_gameUnitsDic == null)
            {
                _gameUnitsDic = new Dictionary<RTSGameUnitBelongSide, List<RTSGameUnit>>();
            }
            return _gameUnitsDic;
        }
        set
        {
            _gameUnitsDic = value;
        }
    }
    //
    public List<RTSGameUnit> SelectedUnits
    {
        get
        {
            List<RTSGameUnit> playerUnits = GameUnitsDic[RTSGameUnitBelongSide.Player];
            if (playerUnits == null)
            {
                return null;
            }
            List<RTSGameUnit> selectedUnits = new List<RTSGameUnit>();
            //

            for (int i = 0; i < playerUnits.Count; i++)
            {
                RTSGameUnit unit = playerUnits[i];
                if (unit != null && unit.IsSelected)
                {
                    selectedUnits.Add(unit);
                }
            }
            return selectedUnits;
        }
    }
    //
    public List<RTSGameUnit> PlayerUnits
    {
        get
        {
            return GameUnitsDic[RTSGameUnitBelongSide.Player];
        }
    }
    //
    public List<RTSGameUnit> EnemyGroupUnits
    {
        get
        {
            return GameUnitsDic[RTSGameUnitBelongSide.EnemyGroup];
        }
    }
    //
    public List<RTSGameUnit> FriendlyGroupUnits
    {
        get
        {
            return GameUnitsDic[RTSGameUnitBelongSide.FriendlyGroup];
        }
    }
    //
    public bool unitRegister(RTSGameUnit unit)
    {
        if (unit == null || GameUnitsDic == null)
        {
            return false;
        }
        if (!GameUnitsDic.ContainsKey(unit.gameUnitBelongSide))
        {
            GameUnitsDic.Add(unit.gameUnitBelongSide, new List<RTSGameUnit>());
        }
        List<RTSGameUnit> unitsList = GameUnitsDic[unit.gameUnitBelongSide];
        lock (unitsList)
        {
            unitsList.Add(unit);
        }
        return true;
    }
    //
    public bool unitUnRegister(RTSGameUnit unit)
    {
        if (unit == null || GameUnitsDic == null)
        {
            return false;
        }
        if (GameUnitsDic.ContainsKey(unit.gameUnitBelongSide))
        {
            List<RTSGameUnit> unitsList = GameUnitsDic[unit.gameUnitBelongSide];
            lock (unitsList)
            {
                unitsList.Remove(unit);
            }
        }
        return true;
    }

    //private List<RTSGameUnit> multipleSelectionUnitsGeneration()
    //{
    //    List<RTSGameUnit> tempList = new List<RTSGameUnit>();
    //    for (int i = 0; i < PlayerUnits.Length; i++)
    //    {
    //        RTSGameUnit unit = PlayerUnits[i];
    //        if (unit != null && unit.IsVisible && unit.IsAllowMultipleSelection)
    //        {
    //            tempList.Add(unit);
    //        }
    //    }
    //    return tempList;
    //    //
    //}
    ////
    //private void multipleSelectionDone(RTSGameUnit[] units)
    //{
    //    _selectedUnits = units;
    //}

    //
    private void Start()
    {
        //RTSGameUnitSelectionManager.eventRegister(multipleSelectionUnitsGeneration);
        //RTSGameUnitSelectionManager.eventRegister(multipleSelectionDone);
        //
    }
    //
    private void OnDestroy()
    {
        //RTSGameUnitSelectionManager.eventUnRegister(multipleSelectionUnitsGeneration);
        //RTSGameUnitSelectionManager.eventUnRegister(multipleSelectionDone);
        //
    }
    //
}
