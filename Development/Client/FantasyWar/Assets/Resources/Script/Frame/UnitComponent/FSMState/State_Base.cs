using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State_Base  {

	protected UnitInfo master;

	public void SetMaster(UnitInfo m){
		master = m;
	}

	public virtual void SetTarget(Vector3 v3){
	}

	public abstract void Update ();

	public virtual void RemoveTarget(UnitInfo t){
	}
}
