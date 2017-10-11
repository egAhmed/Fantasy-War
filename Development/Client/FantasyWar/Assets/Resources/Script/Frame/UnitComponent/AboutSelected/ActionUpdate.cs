using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUpdate : Interaction {

	public override void Deselect ()
	{
		ActionManager.ShareInstance.ClearButtons ();
	}

	public override void Select ()
	{
		ActionManager.ShareInstance.ClearButtons ();
		foreach (ActionBehaviour ab in GetComponents<ActionBehaviour>()) {
			ActionManager.ShareInstance.AddButton(
				ab.index,
				ab.actionIcon, 
				ab.GetClickAction());
		}
	}
}
