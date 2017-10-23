using System.Collections;
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
			RTSGamePlayManager.ShareInstance.build(pi);
		};
	}

//	private void buildingTesting(KeyCode key) {
//		RTSGamePlayManager.ShareInstance.build();
//	}

}
