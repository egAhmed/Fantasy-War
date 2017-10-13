using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : UnitInfo {

	protected override void Start ()
	{
		base.Start ();
		UnitManager.ShareInstance.Buildings[belong].Add (gameObject);

		ActionBehaviour aa = gameObject.AddComponent<Action_Attack> ();
		ActionList.Add (aa);
		ActionBehaviour ab = gameObject.AddComponent<Action_Build> ();
		ActionList.Add (ab);
	}

}
