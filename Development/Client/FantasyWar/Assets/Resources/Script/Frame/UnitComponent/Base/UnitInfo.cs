using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitInfo : MonoBehaviour {

	protected List<Interaction> interactionList = new List<Interaction> ();
	public List<ActionBehaviour> ActionList = new List<ActionBehaviour> ();

	public bool canSelected = true;
	public int HP;
	public int maxHP;
	public string unitName;
	public Sprite unitIcom;
	public PlayerInfo belong;
	//public UnitType unitType;

	protected virtual void Start(){
		
		gameObject.AddComponent<DieInNoLife> ();
		gameObject.AddComponent<MapBip> ();
		gameObject.AddComponent<MarkColor> ();
		Debug.Log ("我是" + this.GetType ().ToString());

		if (belong.IsCurrentPlayer) {
			Debug.Log (this.GetType () + "是自己人");
			Interaction au = gameObject.AddComponent<ActionUpdate> ();
			interactionList.Add (au);
		}

		if (canSelected) {
			gameObject.AddComponent<Interactive> ();
		}

		Interaction hl = gameObject.AddComponent<HightLight> ();
		interactionList.Add (hl);
		Interaction si = gameObject.AddComponent<ShowInfoUI> ();
		interactionList.Add (si);


	}

	public void ActiveInteractions(){
		Debug.Log ("激活");
		foreach (Interaction selection in interactionList) {
			selection.Select();
		}
	}

	public void InactiveInteractions(){
		foreach (Interaction selection in interactionList) {
			Debug.Log ("取消"+selection.GetType());
			selection.Deselect();
		}
	}
}
