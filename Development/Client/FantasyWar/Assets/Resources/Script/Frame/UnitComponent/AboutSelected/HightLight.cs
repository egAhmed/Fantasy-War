using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HightLight : Interaction {

	GameObject tgo;
	public GameObject circle;

	public override void Select ()
	{
		//Debug.Log ("显示圈圈");
		circle.gameObject.SetActive (true);
	}

	public override void Deselect ()
	{
		//Debug.Log ("不显示圈圈");
		circle.gameObject.SetActive (false);
	}

	void Start(){
		GameObject tgo;
		RTSGameUnit un = GetComponent<RTSGameUnit> ();
		if (un != null) {
			//gameUnitBelongSide==RTSGameUnitBelongSide.Player
			if (un.playerInfo.gameUnitBelongSide == RTSGameUnitBelongSide.Player) {
				tgo = Resources.Load<GameObject> ("Prefab/SelectedEffect/SelectedGreed");
			} else if (un.playerInfo.gameUnitBelongSide == RTSGameUnitBelongSide.EnemyGroup) {
				tgo = Resources.Load<GameObject> ("Prefab/SelectedEffect/SelectedRed");
			} else if (un.playerInfo.gameUnitBelongSide == RTSGameUnitBelongSide.FriendlyGroup) {
				tgo = Resources.Load<GameObject> ("Prefab/SelectedEffect/SelectedYellow");
			} else {
				tgo = Resources.Load<GameObject> ("Prefab/SelectedEffect/Selected");
			}
		}
		else {
			tgo = Resources.Load<GameObject> ("Prefab/SelectedEffect/Selected");
		}
		//Debug.Log (tgo == null);
		GameObject go = GameObject.Instantiate (tgo);
		go.transform.SetParent (transform);
		go.SetActive (false);
		circle = go;
		//Vector3 bottomPos = new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z);
		circle.transform.position = transform.position + new Vector3 (0,0.1f,0);

	}

}
