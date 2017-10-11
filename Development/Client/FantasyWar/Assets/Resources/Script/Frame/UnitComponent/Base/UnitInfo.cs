using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitInfo : MonoBehaviour {

	protected List<Interaction> interactionList = new List<Interaction> ();
	protected List<ActionBehaviour> ActionList = new List<ActionBehaviour> ();

	public bool selected = true;
	public int HP;
	public int maxHP;
	public string unitName;
	public Sprite unitIcom;
	public PlayerInfo belong;
	//public UnitType unitType;

	protected virtual void Start(){
		Interaction hl = gameObject.AddComponent<HightLight> ();
		interactionList.Add (hl);
		Interaction si = gameObject.AddComponent<ShowInfoUI> ();
		interactionList.Add (si);

	}
}
