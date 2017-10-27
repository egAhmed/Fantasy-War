using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Collect : ActionBehaviour {

	public delegate void CollectDelegate(RTSGameUnit unit);
	public CollectDelegate collectDelegate;

	void Awake(){
		index = 3;
		shortCutKey = KeyCode.G;
		actionIcon = Resources.Load<Sprite> ("Texture/CollectIcon");
		canRepeat = true;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			SelectTarget();
		};
	}

	public void SelectTarget(){
		RTSGameUnitSelectionManager.Enabled = false;
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, FindCollectTarget);
		//InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
		Debug.Log ("改委托");
	}

	public void FindCollectTarget(KeyCode k){
		//TODO
		//得到鼠标点击到的对象
		//将鼠标左击事件改回选取单位
		//InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, FindCollectTarget);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;
		
		RTSGameUnit targetUnit = hit.transform.GetComponent<RTSGameUnit> ();
		if (targetUnit != null&&targetUnit is RTSResource){
        //TODO
		//采集方法
			Debug.Log (gameObject.GetComponent<RTSGameUnit> ().playerInfo.name + "挖矿");
			collectDelegate (targetUnit);
			//
		}else{
			Debug.Log ("目标不合法");
		}

		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp (KeyCode.Mouse0, activateSelection);
	}

	private void activateSelection(KeyCode key) { 
		Debug.Log("Mouse 0 released");
		RTSGameUnitSelectionManager.Enabled = true;
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp (KeyCode.Mouse0, activateSelection);
	}
}
