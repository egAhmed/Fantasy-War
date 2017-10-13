using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有单位的实例都储存在这里
/// </summary>
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
