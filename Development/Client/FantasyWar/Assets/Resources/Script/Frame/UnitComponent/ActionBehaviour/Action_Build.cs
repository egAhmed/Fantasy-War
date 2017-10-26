using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Build : ActionBehaviour {

	PlayerInfo pi;
	RTSWorker rtsw;
	public string pathh = @"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house";

	void Awake(){
		rtsw = gameObject.GetComponent<RTSWorker> ();
		pi = rtsw.playerInfo;
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
			if(pi.Resources>200){
			//Debug.Log(pi.name);
			//InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown(KeyCode.B,buildingTesting);
			//RTSGamePlayManager.ShareInstance.build(pi);
				buildBarr(pi,pathh);
			//
			}
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
			//Debug.Log (info.name);
		}
		else
		{
			returnTheBuildingCost();
		}
		//
	}

	public void beginToBuildTheBuilding(Vector3 pos,PlayerInfo info)
	{
		rtsw.move (pos);
		if (info == null)
			return;
		StartCoroutine (BuildNew(pos,info));
		//
	}

    public void AIBuild(Vector3 pos, PlayerInfo info) {
        RTSBuildingHomeBase gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuildingHomeBase>(path, pos, Quaternion.identity);
        gameUnit.GetComponent<RTSBuildingHomeBase>().playerInfo = info;
        //
        if (info.gameUnitBelongSide == RTSGameUnitBelongSide.Player)
        {
            gameUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberPlayerBuildingUnit;
        }
    }

	IEnumerator BuildNew(Vector3 pos,PlayerInfo info){
		while (true) {
			if (Vector3.Distance (transform.position, pos) < 1) {
				yield return new WaitForSeconds (5);
				RTSBuildingHomeBase gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuildingHomeBase>(path, pos, Quaternion.identity);
				gameUnit.GetComponent<RTSBuildingHomeBase> ().playerInfo = info;
				//
				if(info.gameUnitBelongSide==RTSGameUnitBelongSide.Player){
					gameUnit.gameObject.layer = RTSLayerManager.ShareInstance.LayerNumberPlayerBuildingUnit;
				}
                //懒得写回调，直接调用建造成功时的函数
                if (pi.isAI)
                {
                    var list = transform.GetComponent<MoveUnitAIController>().fsmStates;
                    MoveUnitBuildState buildstate = null;
                    foreach (var item in list)
                    {
                        if (item.StateID == MoveUnitFSMStateID.Building)
                        {
                            buildstate = item as MoveUnitBuildState;
                            break;
                        }
                    }
                    if (buildstate != null)
                        buildstate.buildSuccess();
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
