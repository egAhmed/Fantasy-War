using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitBloodBarManager : UnitySingleton<RTSGameUnitBloodBarManager> {
    public GameObject BloodBarPref;
    private bool isHide = false;
    public List<UnitBloodBar> unitList = new List<UnitBloodBar>();
    private UnitBloodBar BloodBar;
    // Use this for initialization
    void Start () {
        if (BloodBarPref==null)
        {
            BloodBarPref = Resources.Load<GameObject>("Prefab/BloodBar/BloodBar");
        }
    }

    // Update is called once per frame
    void Update () {
        addBloodBar();
    }

    public void hideBloodbar()
    {
        isHide = !isHide;
        foreach (var item in unitList)
        {

            item.bloodBar.SetActive(isHide);
        }
        
    }

    public void addBloodBar()
    {
        foreach (var item in RTSGameUnitManager.ShareInstance.PlayerUnits)
        {
            if (item.GetComponent<UnitBloodBar>() == null)
            {
                //Checkmate:Added testing code here, using for first basic demo, may enhance implement in next version >>>
              //
                if (item is RTSBuilding) {
                      //Bug binding
                    continue;
                }
                UnitBloodBar temp= item.gameObject.AddComponent<UnitBloodBar>();
                temp.SetColor(Color.green, Color.gray);
                unitList.Add(temp);
                //<<<

                //szmalqp:original desgin>>>
                // BloodBar = item.gameObject.AddComponent<UnitBloodBar>();
                // unitList.Add(BloodBar);
            }

        }
        foreach (var item in RTSGameUnitManager.ShareInstance.EnemyGroupUnits)
        {
            if (item.GetComponent<UnitBloodBar>() == null)
            {
                //Checkmate:Added testing code here, using for first basic demo, may enhance implement in next version >>>
                 if (item is RTSBuilding) {
                     //Bug binding
                    continue;
                }
                UnitBloodBar temp= item.gameObject.AddComponent<UnitBloodBar>();
                temp.SetColor(Color.red, Color.gray);
                unitList.Add(temp);
                //<<<
                //
                //szmalqp:original desgin>>>
                // BloodBar = item.gameObject.AddComponent<UnitBloodBar>();
                // //BloodBar.SetColor(Color.white,Color.yellow);
                // //BloodBar.SetHide(true);
                // unitList.Add(BloodBar);
            }

        }
        foreach (var item in RTSGameUnitManager.ShareInstance.FriendlyGroupUnits)
        {
            if (item.GetComponent<UnitBloodBar>() == null)
            {
                //Checkmate:Added testing code here, using for first basic demo, may enhance implement in next version >>>
                UnitBloodBar temp= item.gameObject.AddComponent<UnitBloodBar>();
                temp.SetColor(Color.yellow, Color.gray);
                unitList.Add(temp);
                //<<<
                 //
                //szmalqp:original desgin>>>
                // BloodBar = item.gameObject.AddComponent<UnitBloodBar>();
                // unitList.Add(BloodBar);
            }

            //}
            //foreach (KeyValuePair<PlayerInfo, List<GameObject>> dic in UnitManager.ShareInstance.Armys)
            //{
            //    foreach (var item in dic.Value)
            //    {
            //        if (item.GetComponent<UnitBloodBar>() == null)
            //        {
            //            unitList.Add(item.gameObject.AddComponent<UnitBloodBar>());
            //        }
            //    }
            //}
            //foreach (var item in UnitManager.ShareInstance.Buildings)
            //{
            //    foreach (var _item in item.Value)
            //    {
            //        if (_item.GetComponent<UnitBloodBar>() == null)
            //        {
            //            unitList.Add(_item.gameObject.AddComponent<UnitBloodBar>());
            //        }
            //    }
            //}
            //foreach (var item in UnitManager.ShareInstance.InitialBuilds)
            //{
            //    foreach (var _item in item.Value)
            //    {
            //        if (_item.GetComponent<UnitBloodBar>() == null)
            //        {
            //            unitList.Add(_item.gameObject.AddComponent<UnitBloodBar>());
            //        }
            //    }
        }
    }
}
