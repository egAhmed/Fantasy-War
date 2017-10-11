using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager :UnitySingleton<UnitManager> {

	private Dictionary<PlayerInfo,List<GameObject>> buildings = new Dictionary<PlayerInfo, List<GameObject>> ();

	public Dictionary<PlayerInfo,List<GameObject>> Buildings{
		get { return buildings; }
	}

	private Dictionary<PlayerInfo,List<GameObject>> armys = new Dictionary<PlayerInfo, List<GameObject>>();

	public Dictionary<PlayerInfo,List<GameObject>> Armys{
		get { return armys; }
	}

	public Dictionary<Racial,List<GameObject>> initialBuilds = new Dictionary<Racial, List<GameObject>> ();
}
