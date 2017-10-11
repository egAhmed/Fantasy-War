using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUpdate : Interaction {

	public override void Deselect ()
	{
		ActionManager.Current.ClearButtons ();
	}

	public override void Select ()
	{
		ActionManager.Current.ClearButtons ();
		foreach (ActionBehaviour ab in GetComponents<ActionBehaviour>()) {
			ActionManager.Current.AddButton(
				ab.index,
				ab.actionIcon, 
				ab.GetClickAction());
		}
	}
}
