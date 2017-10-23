using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSResource : RTSGameUnit
{
	protected override void Start ()
	{
		base.Start ();

		PlayerInfoManager.ShareInstance.resourceses.Add (this);
	}
}
