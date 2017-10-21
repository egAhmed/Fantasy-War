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
			GameObject go = GameObject.Instantiate(workerPrefeb,transform.position,Quaternion.identity);
			go.GetComponent<RTSGameUnit>().playerInfo = gameObject.GetComponent<RTSGameUnit>().playerInfo;
			//Debug.Log("现在有"+RTSGameUnitManager.ShareInstance.PlayerUnits.Count +"个单位");
		};
	}
}
