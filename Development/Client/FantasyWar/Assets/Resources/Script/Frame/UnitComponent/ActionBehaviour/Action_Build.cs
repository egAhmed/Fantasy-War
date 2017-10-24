﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Build : ActionBehaviour {

	void Awake(){
		index = 6;
		shortCutKey = KeyCode.B;
		actionIcon = Resources.Load<Sprite> ("Texture/BuildIcon");
		canRepeat = false;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//建造方法
			Debug.Log("加载建筑");
			PlayerInfo pi = gameObject.GetComponent<RTSGameUnit>().playerInfo;
			//Debug.Log(pi.name);
			//InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.B,buildingTesting);
			//RTSGamePlayManager.ShareInstance.build(pi);
			buildBarr(pi,@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house");
			//
		};
	}


	string path;

	private void exitBuildingMode(bool status, Vector3 pos,PlayerInfo info)
	{
		RTSBuildingManager.eventUnRegister (exitBuildingMode);
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
		RTSBuildingHomeBase gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuildingHomeBase>(path, pos, Quaternion.identity);
		gameUnit.GetComponent<RTSBuildingHomeBase> ().playerInfo = info;
		//

		//
		if(info.gameUnitBelongSide==RTSGameUnitBelongSide.Player){
			gameUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberPlayerBuildingUnit;
		}
		//
	}

	private void returnTheBuildingCost()
	{
		//返回建造资源
	}

	private void buildBarr(PlayerInfo info,string buildingPrefabPath)
	{
		if (info == null) {
			return;
		}
		//
		if (buildingPrefabPath == null) {
			return;
		}
		//
		RTSBuildingManager.eventRegister(exitBuildingMode);
		//
		path=buildingPrefabPath;
		//
		Debug.Log("当前需要建造的建筑："+path);
		//
		RTSBuildingManager.ShareInstance.startBuildingMode(path,info);
		//
	}
}
