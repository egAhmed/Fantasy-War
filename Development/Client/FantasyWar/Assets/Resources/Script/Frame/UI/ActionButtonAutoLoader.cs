using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 挂载在按钮上，会自动将自己添加到ActionManager.Buttons[]中
/// </summary>
[RequireComponent(typeof(Button))]
public class ActionButtonAutoLoader : MonoBehaviour {
	public int index;
	public Button btn;
	// Use this for initialization
	void Start () {
		btn = gameObject.GetComponent<Button> ();
		ActionManager.ShareInstance.AddBtn (index, btn);
	}

}
