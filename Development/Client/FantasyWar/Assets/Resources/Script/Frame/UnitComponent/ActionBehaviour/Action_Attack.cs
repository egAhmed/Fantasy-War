using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : ActionBehaviour {

	public override Action GetClickAction ()
	{
		return delegate() {
			Debug.Log("攻击");
		};
	}
}
