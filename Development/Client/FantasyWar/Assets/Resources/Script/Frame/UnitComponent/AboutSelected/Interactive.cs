using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理對象下所有與選定相關的類
/// </summary>
public class Interactive : MonoBehaviour {

	private bool _Selected = false;

	public bool Selected { get { return _Selected; } }

	public void Select()
	{
		_Selected = true;
		foreach (Interaction selection in GetComponents<Interaction>()) {
			selection.Select();
		}
	}

	public void Deselect()
	{
		_Selected = false;
		foreach (var selection in GetComponents<Interaction>()) {
			selection.Deselect();
		}
	}
}
