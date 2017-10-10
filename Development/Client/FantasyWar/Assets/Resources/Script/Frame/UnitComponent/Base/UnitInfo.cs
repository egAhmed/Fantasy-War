using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitInfo : MonoBehaviour {

	public bool selected = true;
	public int HP;
	public int maxHP;
	public string unitName;
	public Sprite unitIcom;
	public PlayerInfo belong;
	//public UnitType unitType;

	protected virtual void Start(){
		gameObject.AddComponent<HightLight> ();
		gameObject.AddComponent<ShowInfoUI> ();

	}
}
