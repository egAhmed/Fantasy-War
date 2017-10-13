using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有单位的实例都储存在这里
/// </summary>
public class UnitManager :UnitySingleton<UnitManager> {

	public Dictionary<PlayerInfo,List<GameObject>> buildings;

	public Dictionary<PlayerInfo,List<GameObject>> Buildings{
		get {
            if (buildings == null) {
                buildings = new Dictionary<PlayerInfo, List<GameObject>>();
            }
            return buildings; }
	}

    public Dictionary<PlayerInfo,List<GameObject>> armys;

	public Dictionary<PlayerInfo,List<GameObject>> Armys{
		get {
			if (armys==null) {
                armys = new Dictionary<PlayerInfo, List<GameObject>>();
            }
            return armys;
			  }
	}

	private Dictionary<Racial,List<GameObject>> initialBuilds;
	public Dictionary<Racial,List<GameObject>> InitialBuilds{
		get {
			if (initialBuilds==null) {
                initialBuilds = new Dictionary<Racial, List<GameObject>>();
            }
            return initialBuilds;
			  }
	}
}
