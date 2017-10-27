using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Patrol : ActionBehaviour {

	void Awake(){
		index = 4;
		shortCutKey = KeyCode.P;
		canRepeat = true;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//巡逻方法
			Debug.Log("巡逻");
		};
	}
}
