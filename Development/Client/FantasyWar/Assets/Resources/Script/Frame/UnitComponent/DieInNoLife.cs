using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInNoLife : MonoBehaviour {

	UnitInfo info;

	void Start () {
		info = gameObject.GetComponent<UnitInfo> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (info.HP <= 0) {
			UnitDie ();
		}
	}

	void UnitDie(){
		
	}
}
