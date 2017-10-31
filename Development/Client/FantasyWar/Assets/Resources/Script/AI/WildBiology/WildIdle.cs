using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildIdle : WildFSMState {

	bool isMove = false;

	public WildIdle(WildController con){
		wcon = con;
	}

	public override void Reason ()
	{
	}

	public override void ActUpdate ()
	{
		if (Vector3.Distance (wcon.transform.position, wcon.oriPos) < 1) {
			if (!isMove) {
				isMove = true;
				wcon.GetComponent<RTSWild> ().move (wcon.oriPos);
			}
		}
		else {
			isMove = false;
		}
	}
}
