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
		if (Vector3.Distance (target.transform.position, wcon.oriPos) > 10) {
			wcon.state = new WildIdle (wcon);
		}
	}
}
