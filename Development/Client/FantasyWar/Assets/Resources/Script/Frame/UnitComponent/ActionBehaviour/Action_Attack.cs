using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : ActionBehaviour {

	void Awake(){
		index = 2;
		shortCutKey = KeyCode.A;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			SelectTarget();
		};
	}

	/// <summary>
	/// 将鼠标左击事件转换为寻找攻击目标
	/// </summary>
	public void SelectTarget(){
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, FindAttackTarget);
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
		Debug.Log ("改委托");
	}

	public void FindAttackTarget(KeyCode k){
		//TODO
		//得到鼠标点击到的对象

		//将鼠标左击事件改回选取单位
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, FindAttackTarget);

		Vector3 mousePos = Input.mousePosition;

		Ray ray = Camera.main.ScreenPointToRay (mousePos);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;

		UnitInfo targetinfo = hit.transform.GetComponent<UnitInfo> ();
		if (targetinfo == null)
			return;

		//TODO
		//攻击方法
		Debug.Log(gameObject.GetComponent<UnitInfo>().belong.name + "攻击" + targetinfo.belong.name);
	}

}
