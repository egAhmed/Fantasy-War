using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUpdate : Interaction {

	List<ActionBehaviour> hasIntoDelete = new List<ActionBehaviour> ();

	public override void Deselect ()
	{
		//TODO
		//注销快捷键

		foreach (ActionBehaviour ab in hasIntoDelete){
			ActionManager.ShareInstance.RemoveDelegate (ab.index, ab.GetClickAction ());
			InputManager.ShareInstance.InputEventHandlerUnRegister_GetKeyDown(ab.shortCutKey,ab.RunAction);
		}

		Debug.Log (gameObject.GetComponent<UnitInfo>().GetType()+"检测"+TeamManager.ShareInstance.currentSelections.Count);

		if(TeamManager.ShareInstance.currentSelections[0] == this.gameObject){
			ActionManager.ShareInstance.ClearButtons ();
		}

		hasIntoDelete.Clear ();
	}

	/// <summary>
	/// 激活操作面板
	/// </summary>
	public override void Select ()
	{
		if (TeamManager.ShareInstance.currentSelections [0] == this.gameObject) {
			ActionManager.ShareInstance.ClearButtons ();
			//将所有ActionBehaviour组件映射到操作面板上
			foreach (ActionBehaviour ab in gameObject.GetComponent<UnitInfo>().ActionList) {
				ActionManager.ShareInstance.ActionBehaviours [ab.index] = ab;
				ActionManager.ShareInstance.AddButton (
					ab.index,
					ab.actionIcon, 
					ab.GetClickAction ());
				//TODO
				//注册快捷键
				hasIntoDelete.Add (ab);
				InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (ab.shortCutKey, ab.RunAction);
			}
		}
		if (TeamManager.ShareInstance.currentSelections [0] != this.gameObject) {
			foreach (ActionBehaviour ab in gameObject.GetComponent<UnitInfo>().ActionList) {
				if (ActionManager.ShareInstance.ActionBehaviours [ab.index] != null) {
					if (ActionManager.ShareInstance.ActionBehaviours [ab.index].GetType () == ab.GetType ()) {
						ActionManager.ShareInstance.AddButtonDelegate (ab.index, ab.GetClickAction ());
						InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (ab.shortCutKey, ab.RunAction);
						hasIntoDelete.Add(ab);
					}
				}
			}
		}
	}

	void OnDestroy(){
		if (!gameObject.GetComponent<Interactive> ().Selected)
			return;

		if (TeamManager.ShareInstance.currentSelections.Count > 0) {
			if (TeamManager.ShareInstance.currentSelections [0] == this.gameObject) {
				ActionManager.ShareInstance.ClearButtons ();
			}
		}
	}
}
