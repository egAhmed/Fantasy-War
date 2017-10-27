using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Skill1 : ActionBehaviour {

	public delegate void Skill1Delegate(RTSGameUnit attackTarget);
	public Skill1Delegate skill1Delegate;


	void Awake () {
		index = 7;
		shortCutKey = KeyCode.Q;
		actionIcon = Resources.Load<Sprite> ("Texture/Skill1Icon");
		canRepeat = true;
	}
	
	public override Action GetClickAction ()
	{
		return delegate() {
			Debug.Log ("找释放目标");
			RTSGameUnitSelectionManager.Enabled = false;
			InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (KeyCode.Mouse0, FindSkillTarget);
		};
	}

	public void FindSkillTarget(KeyCode k){
		InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown (KeyCode.Mouse0, FindSkillTarget);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (!Physics.Raycast (ray, out hit))
			return;
		RTSGameUnit targetinfo = hit.transform.GetComponent<RTSGameUnit> ();
		if (targetinfo != null){
			Debug.Log(gameObject.GetComponent<RTSGameUnit>().playerInfo.name + "放技能");
			RTSMovableUnit rtsm = gameObject.GetComponent<RTSMovableUnit> ();
			rtsm.move (targetinfo.transform.position);
			transform.position = targetinfo.transform.position;
			if (skill1Delegate != null) {
				skill1Delegate (targetinfo);
			}
		}
		else{
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
