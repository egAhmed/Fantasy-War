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

        // Debug.Log (gameObject.GetComponent<RTSGameUnit>().GetType()+"检测"+TeamManager.ShareInstance.currentSelections.Count);

        // if(TeamManager.ShareInstance.currentSelections[0] == this.gameObject){
        //Debug.Log("selected数量"+RTSGameUnitManager.ShareInstance.SelectedUnits.Count);
        if (RTSGameUnitManager.ShareInstance.SelectedUnits != null && RTSGameUnitManager.ShareInstance.SelectedUnits.Count == 0) { 
			 //Debug.Log("清空面板1");
        //if(RTSGameUnitManager.ShareInstance.SelectedUnits[0].gameObject == this.gameObject){
			 //Debug.Log("清空面板");
			ActionManager.ShareInstance.ClearButtons ();
		//}

		}

		hasIntoDelete.Clear ();
	}

	/// <summary>
	/// 激活操作面板
	/// </summary>
	public override void Select ()
	{
        //Debug.Log("组件数量:"+gameObject.GetComponent<RTSGameUnit>().ActionList.Count);
        // if (TeamManager.ShareInstance.currentSelections [0] == this.gameObject) {
        //Debug.Log(RTSGameUnitManager.ShareInstance.SelectedUnits.Count);
        // if (RTSGameUnitManager.ShareInstance.SelectedUnits [0] == this.gameObject) {
		//Debug.Log(GetComponent<RTSGameUnit>().GetType());
		if (RTSGameUnitManager.ShareInstance.SelectedUnits [0].gameObject == this.gameObject) {
			ActionManager.ShareInstance.ClearButtons ();
			//将所有ActionBehaviour组件映射到操作面板上
			foreach (ActionBehaviour ab in gameObject.GetComponent<RTSGameUnit>().ActionList) {
				ActionManager.ShareInstance.ActionBehaviours [ab.index] = ab;
                //Debug.Log("加面板");
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
		// if (TeamManager.ShareInstance.currentSelections [0] != this.gameObject) {
		if (RTSGameUnitManager.ShareInstance.SelectedUnits [0].gameObject != this.gameObject) {
			foreach (ActionBehaviour ab in gameObject.GetComponent<RTSGameUnit>().ActionList) {
				if (ActionManager.ShareInstance.ActionBehaviours [ab.index] != null) {
					if (ActionManager.ShareInstance.ActionBehaviours [ab.index].GetType () == ab.GetType ()) {
						if (ab.canRepeat) {
							ActionManager.ShareInstance.AddButtonDelegate (ab.index, ab.GetClickAction ());
							InputManager.ShareInstance.InputEventHandlerRegister_GetKeyDown (ab.shortCutKey, ab.RunAction);
							hasIntoDelete.Add (ab);
						}
					}
				}
			}
		}
	}

	void OnDestroy(){
		if (!gameObject.GetComponent<Interactive> ().Selected)
			return;

        if (RTSGameUnitManager.ShareInstance.SelectedUnits == null) {
            return;
        }

        // if (TeamManager.ShareInstance.currentSelections.Count > 0) {
        if (RTSGameUnitManager.ShareInstance.SelectedUnits.Count > 0) {
			
			// if (TeamManager.ShareInstance.currentSelections [0] == this.gameObject) {
			if (RTSGameUnitManager.ShareInstance.SelectedUnits [0] == this.gameObject) {
				
				ActionManager.ShareInstance.ClearButtons ();
			}
		}
	}
}
