using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : ActionBehaviour {

	public delegate void AttackDelegate(RTSGameUnit attackTarget);
	public AttackDelegate attackDelegate;

	void Awake(){
		//
		index = 2;
		shortCutKey = KeyCode.A;
		actionIcon = Resources.Load<Sprite> ("Texture/AtkIcon");
		attackDelegate += DebugAttack;
		canRepeat = true;
		info = "攻击" +"\n"+"快捷键:A";
		//
	}

	void DebugAttack(RTSGameUnit attackTarget){
		//Debug.Log ("要攻击了");
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
		RTSGameUnitSelectionManager.Enabled = false;
		//
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, FindAttackTarget);
        // InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);
        //
        //
        //Debug.Log ("寻找攻击目标");
	}

	public void FindAttackTarget(KeyCode k){
		//TODO
		//得到鼠标点击到的对象

		//将鼠标左击事件改回选取单位
		// InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, RTSOperations.PointSelect);

		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, FindAttackTarget);
		//
        //
		//		

		Vector3 mousePos = Input.mousePosition;

		Ray ray = Camera.main.ScreenPointToRay (mousePos);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;

		RTSGameUnit targetinfo = hit.transform.GetComponent<RTSGameUnit> ();
		if (targetinfo != null){

        //
        // if (targetinfo.gameUnitBelongSide == RTSGameUnitBelongSide.Player) { 
		// }
        //TODO
        //攻击方法
			Debug.Log(gameObject.GetComponent<RTSGameUnit>().playerInfo.name + "攻击" + targetinfo.playerInfo.name);
			if (attackDelegate != null) {
				attackDelegate (targetinfo);
			}
			//
		}else{
			Debug.Log ("目标不合法");
		}
		InputManager.ShareInstance.InputEventHandlerRegister_GetKeyUp (KeyCode.Mouse0, activateSelection);
		//
	}

    private void activateSelection(KeyCode key) { 
		Debug.Log("Mouse 0 released");
		RTSGameUnitSelectionManager.Enabled = true;
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyUp (KeyCode.Mouse0, activateSelection);
	}

}
