using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour {

	public List<RTSWild> wildGroup =new List<RTSWild> ();

	void OnTriggerEnter(Collider other){
		RTSGameUnit gu = other.GetComponent<RTSGameUnit>();
		if (gu != null) {
			if (gu.GetType () != typeof(RTSWild)) {
				foreach (RTSWild item in wildGroup) {
					WildController wc = item.GetComponent<WildController> ();
					wc.state = new WildChase (other.gameObject, wc);
				}
			}
		}
	}
}
