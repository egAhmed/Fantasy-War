using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 標記顏色
/// </summary>
public class MarkColor : MonoBehaviour {

	public MeshRenderer[] Renderers;

	// Use this for initialization
	void Start () {
		Color color = GetComponent<UnitInfo> ().belong.accentColor;
		foreach (var r in Renderers) {
			r.material.color = color;
		}
	}
}
