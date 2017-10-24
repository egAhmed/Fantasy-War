using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Production : ActionBehaviour  {

	GameObject workerPrefeb;

	void Awake(){
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
			PlayerInfo pi = gameObject.GetComponent<RTSGameUnit>().playerInfo;
            RTSWorker rtsw = PrefabFactory.ShareInstance.createClone<RTSWorker>("Prefab/RTSCharacter/RTSWorker/RTSWorker", transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            GameObject go = rtsw.gameObject;
            // GameObject go = GameObject.Instantiate(workerPrefeb,transform.position+new Vector3 (2,0,0),Quaternion.identity);
            // RTSWorker rtsw = go.GetComponent<RTSWorker>();
            rtsw.playerInfo = pi;
			rtsw.homeBuilding = (RTSBuilding)pi.BuildingUnits[Settings.ResourcesTable.Get(1101).type][0];
			//pi.ArmyUnits["worker"].Add(go.GetComponent<RTSGameUnit>());
//			Debug.Log(pi.ArmyUnits[Settings.ResourcesTable.Get(1009).type].Count);
			if(pi.isAI){
//				Debug.Log("是AI");
				go.AddComponent<WorkerAIController>();
//				Debug.Log("添加成功");
			}else { 
				Debug.Log("rtsw.playerInfo =>"+rtsw.playerInfo.name);
			}
            //Debug.Log("现在有"+RTSGameUnitManager.ShareInstance.PlayerUnits.Count +"个单位");
        };
	}
}
