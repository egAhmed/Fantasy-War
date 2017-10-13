using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Collect : ActionBehaviour {

	void Awake(){
		index = 3;
		shortCutKey = KeyCode.G;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			SelectTarget();
		};
	}

	public void SelectTarget(){
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, FindCollectTarget);
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
		Debug.Log ("改委托");
	}

	public void FindCollectTarget(KeyCode k){
		//TODO
		//得到鼠标点击到的对象

		//将鼠标左击事件改回选取单位
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, FindCollectTarget);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;

		string tag = hit.transform.tag;
		if (tag != "mine")
			return;

		//TODO
		//采集方法
		Debug.Log(gameObject.GetComponent<UnitInfo>().belong.name+"挖矿");
	}

}
