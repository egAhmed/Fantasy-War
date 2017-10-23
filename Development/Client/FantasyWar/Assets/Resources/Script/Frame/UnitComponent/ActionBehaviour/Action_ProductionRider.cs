using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ProductionRider : ActionBehaviour {
	
	GameObject workerPrefeb;

	void Awake(){
		index = 1;
		shortCutKey = KeyCode.R;
		actionIcon = Resources.Load<Sprite> ("Texture/RiderIcon");
		canRepeat = false;
		workerPrefeb = Resources.Load<GameObject> ("Prefab/RTSCharacter/RTSCavalryman/RTSCavalryman");
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//生产方法
			PlayerInfo pi = gameObject.GetComponent<RTSGameUnit>().playerInfo;
			// GameObject go = GameObject.Instantiate(workerPrefeb,transform.position+new Vector3 (2,0,0),Quaternion.identity);
			// RTSMelee rtsm = go.GetComponent<RTSMelee>();
			RTSMelee rtsm = PrefabFactory.ShareInstance.createClone<RTSMelee>("Prefab/RTSCharacter/RTSCavalryman/RTSCavalryman", transform.position + new Vector3(2, 0, 0), Quaternion.identity);
            GameObject go = rtsm.gameObject;
			rtsm.playerInfo = pi;
			//rtsm.homeBuilding = (RTSBuilding)pi.BuildingUnits["Base"][0];
			//pi.ArmyUnits["worker"].Add(go.GetComponent<RTSGameUnit>());
//			Debug.Log(pi.ArmyUnits[Settings.ResourcesTable.Get(1002).type].Count);
			if(pi.isAI){
				//Debug.Log("是AI");
				go.AddComponent<MoveUnitAIController>();
				//Debug.Log("AI添加成功");
			}
			//Debug.Log("现在有"+RTSGameUnitManager.ShareInstance.PlayerUnits.Count +"个单位");
		};
	}
}
