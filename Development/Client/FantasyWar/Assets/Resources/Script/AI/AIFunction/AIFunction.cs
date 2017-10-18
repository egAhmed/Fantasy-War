using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFunction{
	public static void BuildTest(KeyCode ds){
		Debug.Log ("按下v");
		AIBuild (Vector3.zero,"sdabsads");
	}

	public static void AIBuild(Vector3 pos,string prefabPath){
		//遍历单位列表
		foreach (RTSGameUnit item in RTSGameUnitManager.ShareInstance.SelectedUnits) {
			//选一个农民执行建造动作，建造建筑到pos
			if (item is RTSWorker) {
				RTSBuilding gameUnit = PrefabFactory.ShareInstance.createClone<RTSBuilding>(@"3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house", pos, Quaternion.identity);
				MeshCollider collider=gameUnit.gameObject.AddComponent<MeshCollider>();
				collider.convex = true;
				return;
			}
		}
	}

	public static void AIProduction(){
		//遍历单位列表
		foreach (RTSGameUnit item in RTSGameUnitManager.ShareInstance.SelectedUnits) {
			//找到一个可以生产且空闲的生产工厂
			//test
			if (item is RTSWorker) {
				//GameObject.Instantiate(Resources.Load<GameObject> ("Prefab/RTSCharacter/RTSWorker/RTSWorker"),item.transform.position,Quaternion.identity);
				item.GetComponent<Action_Production>().RunAction(KeyCode.A);
				return;
			}
		}
	}

	public static void AICollect(){
		foreach (RTSGameUnit item in RTSGameUnitManager.ShareInstance.SelectedUnits) {
			if (item is RTSWorker) {
				//TODO
				//找到最近的矿去采
				//item.GetComponent<Action_Collect>().collectDelegate(一个矿石的RTSGameUnit);
			}
		}
	}

	public static void AIAttack(RTSGameUnit target){
		foreach (RTSGameUnit item in RTSGameUnitManager.ShareInstance.SelectedUnits) {
			if (item is RTSWorker) {
				continue;
			}
			else {
				item.GetComponent<Action_Attack> ().attackDelegate (target);
			}
		}
	}

	public static void AIMove(Vector3 pos){
		foreach (RTSGameUnit item in RTSGameUnitManager.ShareInstance.SelectedUnits) {
			if (item is RTSWorker) {
				continue;
			}
			else {
				//TODO
				//移动到目标点
			}
		}
	}

}
