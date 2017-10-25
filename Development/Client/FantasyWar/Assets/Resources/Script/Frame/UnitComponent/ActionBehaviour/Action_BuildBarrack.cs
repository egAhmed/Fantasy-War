using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_BuildBarrack : ActionBehaviour {
		//
	PlayerInfo pi;
	RTSWorker rtsw;

	void Awake(){
		rtsw = gameObject.GetComponent<RTSWorker> ();
		pi = rtsw.playerInfo;
		index = 7;
		shortCutKey = KeyCode.R;
		actionIcon = Resources.Load<Sprite> ("Texture/BarrIcon");
		canRepeat = false;
	}
	
	public override Action GetClickAction ()
	{
		//
		return delegate() {
			//TODO
			//建造方法
			Debug.Log("加载建筑");
			//
			//Debug.Log(pi.name);
			if(pi.Resources>150){
				buildBarr(pi,@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/barracks");
			}
		};
		//
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

	/// <summary>
	/// 真正建出来调用这个
	/// </summary>
	/// <param name="pos">Position.</param>
	/// <param name="info">Info.</param>
	public void beginToBuildTheBuilding(Vector3 pos,PlayerInfo info)
	{
		rtsw.move (pos);
		StartCoroutine (BuildNew(pos,info));

		if (info == null)
			return;
		//
		StartCoroutine (BuildNew(pos,info));
	}

	IEnumerator BuildNew(Vector3 pos,PlayerInfo info){
		while (true) {
			if (Vector3.Distance (transform.position, pos) < 1) {
				yield return new WaitForSeconds (5);
				RTSBuildingBarrack gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuildingBarrack> (path, pos, Quaternion.identity);
				gameUnit.GetComponent<RTSBuildingBarrack> ().playerInfo = info;
				if (info.gameUnitBelongSide == RTSGameUnitBelongSide.Player) {
					gameUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberPlayerBuildingUnit;
				}
				if (info.isAI) {
					info.AICon.registerDelCreatArmy (gameUnit.CreatArmy,gameUnit);
				}
				break;
			}
			yield return null;
		}
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
