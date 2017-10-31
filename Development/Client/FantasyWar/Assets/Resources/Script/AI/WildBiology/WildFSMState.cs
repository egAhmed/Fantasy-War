using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WildFSMState{

	public WildController wcon = null;
	public GameObject target;

	public virtual void Reason(){
	}

	public virtual void ActUpdate(){
		
	}
}
