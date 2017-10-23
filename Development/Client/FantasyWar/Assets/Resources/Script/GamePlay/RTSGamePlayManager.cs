using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Pathfinding.RVO;

public class RTSGamePlayManager : UnitySingleton<RTSGamePlayManager>
{
    private void exitBuildingMode(bool status, Vector3 pos,PlayerInfo info)
    {
        // RTSGameUnitSelectionManager.Enabled = true;
        // RTSGameUnitActionManager.Enabled = true;
        //
        if (status)
        {
            beginToBuildTheBuilding(pos,info);
			Debug.Log (info.name);
        }
        else
        {
            returnTheBuildingCost();
        }
        //
    }

    private void beginToBuildTheBuilding(Vector3 pos,PlayerInfo info)
    {
        RTSBuilding gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuilding>(@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", pos, Quaternion.identity);
		gameUnit.GetComponent<RTSBuilding> ().playerInfo = info;
		// MeshCollider collider=gameUnit.gameObject.AddComponent<MeshCollider>();
        // collider.convex = true;
        //collider.isTrigger = true;
        //
    }

    private void returnTheBuildingCost()
    {
        //
    }

    public void build(PlayerInfo info)
    {
        //
        // RTSGameUnitSelectionManager.selectionRelease();
        // RTSGameUnitSelectionManager.Enabled = false;
        // RTSGameUnitActionManager.Enabled = false;
        //
		Debug.Log("建造方法");
        //
        RTSBuildingManager.ShareInstance.startBuildingMode(@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house",info);
        //
    }

    // Use this for initialization
    void Start()
    {
        //
        // RTSBuildingManager.eventRegister(enterBuildingMode);
        RTSBuildingManager.eventRegister(exitBuildingMode);
        //
        bool b=RTSGameUnitTeamingManager.ShareInstance == null;
    }

    private void OnDestroy()
    {
        //
        // RTSBuildingManager.eventUnRegister(enterBuildingMode);
        RTSBuildingManager.eventUnRegister(exitBuildingMode);
        //
    }
}
