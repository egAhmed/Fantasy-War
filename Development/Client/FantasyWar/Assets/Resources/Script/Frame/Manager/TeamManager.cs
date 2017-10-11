using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : UnitySingleton<TeamManager> {


	public List<GameObject> currentSelections = new List<GameObject>();	//當前已選中的單位

	public List<GameObject> Team1 = new List<GameObject>();		//玩家自行編隊編隊
	public List<GameObject> Team2 = new List<GameObject>();
	public List<GameObject> Team3 = new List<GameObject>();
	public List<GameObject> Team4 = new List<GameObject>();
	public List<GameObject> Team5 = new List<GameObject>();
	public List<GameObject> Team6 = new List<GameObject>();
	public List<GameObject> Team7 = new List<GameObject>();

	void Update () {
		
	}
}
