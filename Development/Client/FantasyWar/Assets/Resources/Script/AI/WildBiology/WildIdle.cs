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
		//Debug.Log ("IDLE:  "+(Vector3.Distance (wcon.transform.position, wcon.oriPos)));
		//Debug.Log ("回去1");
		if (Vector3.Distance (wcon.transform.position, wcon.oriPos) > 0.1f) {
			//Debug.Log ("回去1");
			if (!isMove) {
				isMove = true;
				//Debug.Log ("回去2");
				RTSWild rw = wcon.GetComponent<RTSWild> ();
				rw.stop ();
				rw.move (wcon.oriPos);
			}
		}
		else {
			isMove = false;
		}
	}
}
