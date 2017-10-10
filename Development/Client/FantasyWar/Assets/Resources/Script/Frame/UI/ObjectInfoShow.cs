using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoShow : MonoBehaviour {

	public static ObjectInfoShow Current;
	public Image unitIcon;
	public Text unitName,HP,maxHP;

	void Start () {
		Current = this;
		Clear ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 设置单位文本信息
	/// </summary>
	public void SetText(){
	}

	/// <summary>
	/// 设置单位图标信息
	/// </summary>
	public void SetIcon(){
		
	}

	/// <summary>
	/// 清空图标和文本
	/// </summary>
	public void Clear(){
	}
}
