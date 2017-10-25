using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ProductionRider : ActionBehaviour {
	
	PlayerInfo pi = null;
	RTSBuilding rtsb;
	float scheduleTime =0;

	void Awake(){
		rtsb = gameObject.GetComponent<RTSBuilding> ();
		pi = gameObject.GetComponent<RTSGameUnit>().playerInfo;
		index = 1;
		shortCutKey = KeyCode.R;
		actionIcon = Resources.Load<Sprite> ("Texture/RiderIcon");
		canRepeat = false;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//生产方法

			if(!rtsb.isProducting){
				if(pi.Resources>=100){
					rtsb.isProducting = true;
					pi.Resources -= 100;
					Debug.Log(pi.name +"  resources  "+ pi.Resources.ToString());
					StartCoroutine(Producting());
				}
			}
			//Debug.Log("现在有"+RTSGameUnitManager.ShareInstance.PlayerUnits.Count +"个单位");
		};
	}

	IEnumerator Producting(){
		scheduleTime = 0;
		while (scheduleTime < 5) {
			scheduleTime += 0.2f;
			yield return new WaitForSeconds(0.2f);
			rtsb.schedule = scheduleTime / 5;
		}
		rtsb.schedule = 0;
		RTSMelee rtsm = PrefabFactory.ShareInstance.createClone<RTSMelee>("Prefab/RTSCharacter/RTSCavalryman/RTSCavalryman", transform.position + new Vector3(6, 0, 0), Quaternion.identity);
		GameObject go = rtsm.gameObject;
		// GameObject go = GameObject.Instantiate(workerPrefeb,transform.position+new Vector3 (2,0,0),Quaternion.identity);
		// RTSWorker rtsw = go.GetComponent<RTSWorker>();
		rtsm.playerInfo = pi;
		//pi.ArmyUnits["worker"].Add(go.GetComponent<RTSGameUnit>());
		//			Debug.Log(pi.ArmyUnits[Settings.ResourcesTable.Get(1009).type].Count);
		if(pi.isAI){
			//Debug.Log("是AI");
			go.AddComponent<MeleeAIController>();
			//Debug.Log("AI添加成功");
		}
		rtsb.isProducting = false;
	}
}
