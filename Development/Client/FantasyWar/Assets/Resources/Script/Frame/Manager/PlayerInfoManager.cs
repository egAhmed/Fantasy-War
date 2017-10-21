using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : UnitySingleton<PlayerInfoManager> {

	public List<PlayerInfo> Players = new List<PlayerInfo>();

	void Start(){
		foreach (PlayerInfo item in Players) {
			item.BuildingUnits.Add ("Base", new List<RTSGameUnit> ());
			item.BuildingUnits.Add ("barracks", new List<RTSGameUnit> ());
			item.ArmyUnits.Add ("worker", new List<RTSGameUnit> ());
			item.ArmyUnits.Add ("Melee", new List<RTSGameUnit> ());
			item.ArmyUnits.Add ("Rider", new List<RTSGameUnit> ());
			item.Resources = 500;
			if (item.isAI) {
				PlayerAIController pac = gameObject.AddComponent<PlayerAIController> ();
				pac.playerInfo = item;
			}
		}
	}
}
