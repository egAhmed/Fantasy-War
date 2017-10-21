using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : UnitySingleton<PlayerInfoManager> {

	public List<PlayerInfo> Players = new List<PlayerInfo>();
	List<Vector3> initialPos = new List<Vector3> ();
	public PlayerInfo currentPlayer = null;

	void Awake(){
		initialPos.Add (GameObject.FindWithTag ("P1pos").transform.position);
		initialPos.Add (GameObject.FindWithTag ("P2pos").transform.position);
		initialPos.Add (GameObject.FindWithTag ("P3pos").transform.position);
		initialPos.Add (GameObject.FindWithTag ("P4pos").transform.position);
		initialPos.Add (GameObject.FindWithTag ("P5pos").transform.position);
	}

	public void AddAIPlayerInfo(){
		PlayerInfo p = new PlayerInfo ();
		p.name = "AI" + (Players.Count + 1);
		p.accentColor = new Color (Random.Range (0, 1), Random.Range (0, 1), Random.Range (0, 1));
		p.isAI = true;
		Players.Add (p);
	}

	public void AddPlayer(string name,Color color){
		PlayerInfo p = new PlayerInfo ();
		p.name = name;
		p.accentColor = color;
		Players.Add (p);
	}

	/// <summary>
	/// 初始化playerinfo里面的信息
	/// </summary>
	public void InitPlayers(){
		for (int i = 0; i < Players.Count; i++) {
			PlayerInfo item = Players[i];
			item.BuildingUnits.Add ("Base", new List<RTSGameUnit> ());
			item.BuildingUnits.Add ("barracks", new List<RTSGameUnit> ());
			item.ArmyUnits.Add ("worker", new List<RTSGameUnit> ());
			item.ArmyUnits.Add ("Melee", new List<RTSGameUnit> ());
			item.ArmyUnits.Add ("Rider", new List<RTSGameUnit> ());
			item.Resources = 500;
			item.location = initialPos[i];
			if (item.isAI) {
				PlayerAIController pac = gameObject.AddComponent<PlayerAIController> ();
				pac.playerInfo = item;
			}
			if (item.name != TestingScript.virCurrentName) {
				item.gameUnitBelongSide = RTSGameUnitBelongSide.EnemyGroup;
			}
			if (item.name == TestingScript.virCurrentName) {
				item.gameUnitBelongSide = RTSGameUnitBelongSide.Player;
			}
		}
		Debug.Log ("playerinfo初始化完毕");
	}

	public void LoadInitBuild(){
		Debug.Log ("加载建筑列表");
		foreach (PlayerInfo item in Players) {
			GameObject buildPrefab = Resources.Load<GameObject> ("3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house");
			GameObject go = GameObject.Instantiate (buildPrefab, item.location, Quaternion.identity);
			//go.SetActive (true);
			go.AddComponent<RTSBuilding> ();
		}
		Debug.Log ("加载建筑列表完毕");
	}
}
