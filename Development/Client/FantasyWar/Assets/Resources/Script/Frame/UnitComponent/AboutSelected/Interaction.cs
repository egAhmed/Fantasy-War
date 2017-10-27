using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 與選定相關的類
/// </summary>
public abstract class Interaction : MonoBehaviour {

	public abstract void Select();		//選擇時
	public abstract void Deselect();	//取消選擇時
}
