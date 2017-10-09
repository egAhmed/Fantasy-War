﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

public class RTSGamePlayManager : UnitySingleton<RTSGamePlayManager>
{

    private void enterBuildingMode()
    {
        //

    }

    private void exitBuildingMode(bool status, Vector3 pos)
    {
        RTSGameUnitSelectionManager.Enabled = true;
        RTSGameUnitActionManager.Enabled = true;
        //
        if (status)
        {
            beginToBuildTheBuilding(pos);
        }
        else
        {
            returnTheBuildingCost();
        }
        //
    }

    private void beginToBuildTheBuilding(Vector3 pos)
    {
        RTSGameUnit gameUnit=PrefabFactory.ShareInstance.createClone<RTSGameUnit>(@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", pos, Quaternion.identity);
        gameUnit.gameObject.AddComponent<RVOObstacle>();
        MeshCollider collider=gameUnit.gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        //collider.isTrigger = true;
        //

    }

    private void returnTheBuildingCost()
    {

    }

    public void build()
    {
        //
        RTSGameUnitSelectionManager.Enabled = false;
        RTSGameUnitActionManager.Enabled = false;
        //
        RTSBuildingManager.ShareInstance.startBuildingMode(PrefabFactory.ShareInstance.createClone<RTSBuildingTempUnit>(@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", Vector3.zero, Quaternion.identity));
        //
    }

    // Use this for initialization
    void Start()
    {
        //
        RTSBuildingManager.eventRegister(enterBuildingMode);
        RTSBuildingManager.eventRegister(exitBuildingMode);
        //
        bool b=RTSGameUnitTeamingManager.ShareInstance == null;
    }

    private void OnDestroy()
    {
        //
        RTSBuildingManager.eventUnRegister(enterBuildingMode);
        RTSBuildingManager.eventUnRegister(exitBuildingMode);
        //
    }
}