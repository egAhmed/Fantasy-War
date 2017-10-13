using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理對象下所有與選定相關的類
/// </summary>
public class Interactive : MonoBehaviour {

	private bool _Selected = false;

	public bool Selected { get { return _Selected; } }

	/// <summary>
	/// 当被选定时调用，将所有interaction激活
	/// </summary>
	public void Select()
	{
		_Selected = true;
		TeamManager.ShareInstance.currentSelections.Add (this.gameObject);
		gameObject.GetComponent<UnitInfo> ().ActiveInteractions ();
	}

	/// <summary>
	/// 取消选定时调用，将所有interaction失活
	/// </summary>
	public void Deselect()
	{
		if (Selected) {
			Debug.Log ("进取消选择函数");
			_Selected = false;
			gameObject.GetComponent<UnitInfo> ().InactiveInteractions ();
			if (TeamManager.ShareInstance.currentSelections.Contains (gameObject)) {
				TeamManager.ShareInstance.currentSelections.Remove (gameObject);
			}
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Deselect ();
		}
	}

}
