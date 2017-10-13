using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 没血死亡
/// </summary>
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
		//TODO
		//在对象池中隐藏
		UnitInfo unitinfo = gameObject.GetComponent<UnitInfo>();
		if (unitinfo is Building) {
			UnitManager.ShareInstance.Buildings [unitinfo.belong].Remove(gameObject);
		}
		if (unitinfo is Building) {
			UnitManager.ShareInstance.Armys [unitinfo.belong].Remove (gameObject);
		}
		else {
			return;
		}
	}
}
