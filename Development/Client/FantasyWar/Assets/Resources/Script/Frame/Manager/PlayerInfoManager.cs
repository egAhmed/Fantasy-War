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

	public List<RTSResource> resourceses = new List<RTSResource> ();

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
//			Debug.Log ("进循环");
			PlayerInfo item = Players[i];
//
//			if (Settings.ResourcesTable.idList == null)
//				Settings.TableManage.Start ();
//			
//
			for (int j= 0; j < Settings.ResourcesTable.idList.Count; j++) {
				int id = Settings.ResourcesTable.Get (Settings.ResourcesTable.idList [j]).id;
				//Debug.LogError (Settings.ResourcesTable.idList[i]);
				int typeid = (id % 1000) / 100;
//				int index = Settings.ResourcesTable.Get (Settings.ResourcesTable.idList [j]).path.LastIndexOf (@"/")+1;
				string name = Settings.ResourcesTable.Get (Settings.ResourcesTable.idList [j]).type;
				if (typeid==0) {
					//Debug.LogError (item.ArmyUnits.ContainsKey(name));
					item.ArmyUnits.Add (name, new List<RTSGameUnit> ());
				}
				if (typeid==1) {
					item.BuildingUnits.Add (name, new List<RTSGameUnit> ());
				}
			}
//			item.BuildingUnits.Add ("Base", new List<RTSGameUnit> ());
//			item.ArmyUnits.Add ("worker", new List<RTSGameUnit> ());
//			item.BuildingUnits.Add ("barracks", new List<RTSGameUnit> ());
//			item.ArmyUnits.Add ("Melee", new List<RTSGameUnit> ());
//			item.ArmyUnits.Add ("Rider", new List<RTSGameUnit> ());
			item.Resources = 500;
			item.location = initialPos[i];
			if (item.name != TestingScript.virCurrentName) {
				item.gameUnitBelongSide = RTSGameUnitBelongSide.EnemyGroup;
			}
			if (item.name == TestingScript.virCurrentName) {
				item.gameUnitBelongSide = RTSGameUnitBelongSide.Player;
			}
		}
		//Debug.Log ("playerinfo初始化完毕");
	}

	public void LoadInitBuild(){
		//Debug.Log ("加载建筑列表");
		foreach (PlayerInfo item in Players) {

			GameObject go = PrefabFactory.ShareInstance.createClone ("Prefab/RTSBuilding/Human/house", item.location, Quaternion.identity);
			//GameObject buildPrefab = Resources.Load<GameObject> ("3rdPartyAssetPackage/Bitgem_RTS_Pack/Human_Buildings/Prefabs/house");
			//GameObject go = GameObject.Instantiate (buildPrefab, item.location, Quaternion.identity);
			//go.SetActive (true);
			RTSBuilding rb = go.AddComponent<RTSBuilding> ();
			rb.playerInfo = item;
			item.BuildingUnits [Settings.ResourcesTable.Get(1101).type].Add (go.GetComponent<RTSBuilding> ());
			item.AllUnits.Add (rb);
		}
		//Debug.Log ("加载建筑列表完毕");
	}

	public void LoadAIController(){
		foreach (PlayerInfo item in Players) {
			if (item.isAI) {
				//Debug.Log ("我是AI哦");
				PlayerAIController pac = gameObject.AddComponent<PlayerAIController> ();
				pac.playerInfo = item;
				pac.enabled = true;
//				Debug.Log (pac.playerInfo.name);
			}
		}

	}
}
