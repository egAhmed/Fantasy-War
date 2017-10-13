using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Build : ActionBehaviour {

	void Awake(){
		index = 6;
		shortCutKey = KeyCode.B;
	}

	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//建造方法
			Debug.Log("加载建造列表");
		};
	}

}
