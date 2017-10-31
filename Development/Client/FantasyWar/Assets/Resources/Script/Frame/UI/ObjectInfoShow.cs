using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoShow : MonoBehaviour {

	public static ObjectInfoShow Current;
	public RawImage unitIcon;
	public Text unitName,HP,maxHP,ATKTitle,ATK;
	public GameObject theUI;
	public GameObject Camerax;

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
	public void SetText(string name,string hp,string maxhp,string Atk){
//		Debug.Log (name);
		unitName.text = name;
		HP.text = hp;
		maxHP.text = maxhp;
		if (Atk != null) {
			ATKTitle.gameObject.SetActive (true);
			ATK.gameObject.SetActive (true);
			ATK.text = Atk;
		}
		else {
			ATKTitle.gameObject.SetActive (false);
			ATK.gameObject.SetActive (false);
		}
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
		theUI.SetActive (false);
		unitName.text = "";
		HP.text = "";
		maxHP.text = "";
		ATK.text = "";
	}
}
