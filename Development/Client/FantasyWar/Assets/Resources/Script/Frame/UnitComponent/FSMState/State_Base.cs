using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Base  {

	protected RTSGameUnit master;

	public void SetMaster(RTSGameUnit m){
		master = m;
	}

	public virtual void SetTarget(Vector3 v3){
	}

	public abstract void Update ();

	public virtual void RemoveTarget(RTSGameUnit t){
	}
}
