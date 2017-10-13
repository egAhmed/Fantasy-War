using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action_Production : ActionBehaviour  {


	public override Action GetClickAction ()
	{
		return delegate() {
			//TODO
			//生产方法
			Debug.Log("生产");
		};
	}
}
