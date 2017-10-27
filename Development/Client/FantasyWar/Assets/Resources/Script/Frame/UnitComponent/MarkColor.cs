using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 標記顏色
/// </summary>
public class MarkColor : MonoBehaviour {

	public MeshRenderer render;

	// Use this for initialization
	void Start () {
		render = gameObject.GetComponent<MeshRenderer> ();
		if (GetComponent<RTSGameUnit> () != null) {
			Color color = GetComponent<RTSGameUnit> ().playerInfo.accentColor;
			render.material.color = color;
		}
		else {
			render.material.color = Color.white;
		}
	}
}
