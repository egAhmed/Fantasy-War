using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_ProductCar : ActionBehaviour {

	GameObject workerPrefeb;
	float scheduleTime =0;
	RTSBuilding rtsb;
	PlayerInfo pi = null;

	void Awake(){
		rtsb = gameObject.GetComponent<RTSBuilding> ();
		pi = gameObject.GetComponent<RTSGameUnit>().playerInfo;
		index = 2;
		shortCutKey = KeyCode.C;
		//改图标路径
		actionIcon = Resources.Load<Sprite> ("Texture/WorkerIcon");
		canRepeat = false;
		//workerPrefeb = Resources.Load<GameObject> ("Prefab/RTSCharacter/RTSWorker/RTSWorker");
		info = "生产投石车" +"\n"+"快捷键:C";
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//生产方法
			if(!rtsb.isProducting){
				if(pi.Resources>=80){
					rtsb.isProducting = true;
					pi.Resources -= 80;
					//Debug.Log(pi.name + pi.Resources.ToString());
					StartCoroutine(Producting());
				}
			}
			//Debug.Log("现在有"+RTSGameUnitManager.ShareInstance.PlayerUnits.Count +"个单位");
		};
	}

	IEnumerator Producting(){
		scheduleTime = 0;
		while(scheduleTime < 5)
		{
			scheduleTime += 0.2f;
			yield return new WaitForSeconds(0.2f);
			rtsb.schedule = scheduleTime / 5;
		}
		rtsb.schedule = 0;

		//改一下路径
		RTSCamion scriptObj = PrefabFactory.ShareInstance.createClone<RTSCamion> ("Prefab/RTSCharacter/RTSCamion/Camion", transform.position + new Vector3 (3, 0, 0), Quaternion.identity);
		GameObject go = scriptObj.gameObject;
		// GameObject go = GameObject.Instantiate(workerPrefeb,transform.position+new Vector3 (2,0,0),Quaternion.identity);
		// RTSWorker rtsw = go.GetComponent<RTSWorker>();
		scriptObj.playerInfo = pi;
		//
		if (pi.isAI) {
			//Add Camion AI
		}
		//
		rtsb.isProducting = false;
	}
}
