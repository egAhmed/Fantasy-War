using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour {

	//public List<RTSWild> wildGroup =new List<RTSWild> ();
	public RTSWild rtsw;

	void Start(){
		StartCoroutine (CreatDog());
	}

	IEnumerator CreatDog(){
		yield return new WaitForSeconds (0.1f);
		rtsw = PrefabFactory.ShareInstance.createClone<RTSWild> ("Prefab/RTSCharacter/ORC/Dog/DOG", transform.position, Quaternion.identity);
		rtsw.GetComponent<UnitBloodBar> ().SetColor (Color.red);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == RTSLayerManager.ShareInstance.LayerNumberEnvironmentGround)
			return;
		RTSGameUnit gu = other.GetComponent<RTSGameUnit>();
		if (gu != null) {
			if (gu == rtsw)
				return;
			if (gu.GetType () != typeof(RTSWild)) {
				//Debug.Log ("有东西进来了");
//				foreach (RTSWild item in wildGroup) {
//					WildController wc = item.GetComponent<WildController> ();
//					wc.state = new WildChase (other.gameObject, wc);
//				}
				WildController wc = rtsw.GetComponent<WildController> ();
				wc.state = new WildChase (other.gameObject, wc);
			}
		}
	}
}
