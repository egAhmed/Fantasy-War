using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Production : ActionBehaviour  {

	GameObject workerPrefeb;

	bool isProducting = false;
	PlayerInfo pi = null;

	void Awake(){
		pi = gameObject.GetComponent<RTSGameUnit>().playerInfo;
		index = 0;
		shortCutKey = KeyCode.W;
		actionIcon = Resources.Load<Sprite> ("Texture/WorkerIcon");
		canRepeat = false;
		workerPrefeb = Resources.Load<GameObject> ("Prefab/RTSCharacter/RTSWorker/RTSWorker");
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//生产方法
			if(!isProducting){
				if(pi.Resources>=50){
					isProducting = true;
					pi.Resources -= 50;
					Debug.Log(pi.name + pi.Resources.ToString());
					StartCoroutine(Producting());
				}
			}
            //Debug.Log("现在有"+RTSGameUnitManager.ShareInstance.PlayerUnits.Count +"个单位");
        };
	}

	IEnumerator Producting(){
		yield return new WaitForSeconds (5);
		RTSWorker rtsw = PrefabFactory.ShareInstance.createClone<RTSWorker>("Prefab/RTSCharacter/RTSWorker/RTSWorker", transform.position + new Vector3(2, 0, 0), Quaternion.identity);
		GameObject go = rtsw.gameObject;
		// GameObject go = GameObject.Instantiate(workerPrefeb,transform.position+new Vector3 (2,0,0),Quaternion.identity);
		// RTSWorker rtsw = go.GetComponent<RTSWorker>();
		rtsw.playerInfo = pi;
		rtsw.homeBuilding = (RTSBuilding)pi.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0];
		//pi.ArmyUnits["worker"].Add(go.GetComponent<RTSGameUnit>());
		//			Debug.Log(pi.ArmyUnits[Settings.ResourcesTable.Get(1009).type].Count);
		if(pi.isAI){
			WorkerAIController AICon = go.AddComponent<WorkerAIController>();
			//AICon.DelAIBuild = rtsw.CreatBuilding;
		}
		isProducting = false;
	}
}
