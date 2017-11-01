using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildChase : WildFSMState {

	bool isatk = false;

	public WildChase(GameObject theTarget,WildController con){
		target = theTarget;
		wcon = con;
	}

	public override void ActUpdate ()
	{
		Action_Attack aa = wcon.GetComponent<Action_Attack> ();
		if (!isatk) {
			if (aa.attackDelegate != null) {
				aa.attackDelegate (target.GetComponent<RTSGameUnit>());
			}
			isatk = true;
		}
	}

	public override void Reason ()
	{
		//Debug.Log ("条件1  "+target == null);
		//Debug.Log ("条件2  "+(Vector3.Distance (target.transform.position, wcon.oriPos)));
		if (target == null) {
			Debug.Log ("目标为空");
			wcon.state = new WildIdle (wcon);
		}
		if (Vector3.Distance (target.transform.position, wcon.oriPos) > 20) {
			Debug.Log ("目标太远");
			wcon.state = new WildIdle (wcon);
		}
	}
}
