using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Stop : ActionBehaviour {

	void Awake(){
		index = 1;
		shortCutKey = KeyCode.S;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//停止方法
			Debug.Log("停止");
		};
	}
}
