using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Army : UnitInfo {

	protected override void Start ()
	{
		base.Start ();

		//在单位列表里面添加自己
		UnitManager.ShareInstance.Armys[belong].Add (gameObject);

		if(belong.IsCurrentPlayer){
			Interaction m = gameObject.AddComponent<Move> ();
			interactionList.Add (m);
			ActionBehaviour aa = gameObject.AddComponent<Action_Attack> ();
			ActionList.Add (aa);
		}
	}

}
