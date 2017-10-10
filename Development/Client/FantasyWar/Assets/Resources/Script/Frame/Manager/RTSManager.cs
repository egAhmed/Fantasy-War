using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSManager : MonoBehaviour {

	public static RTSManager Current = null;

	public List<PlayerInfo> Players = new List<PlayerInfo>();

	void Awake(){
		Current = this;
	}

	void Start(){
		BuildOriginal ();
	}
		
	/// <summary>
	/// 建造初始单位
	/// </summary>
	private void BuildOriginal(){
		foreach (PlayerInfo p in Players) {
			if (p.IsCurrentPlayer) {
				BelongPlayer.Default = p;
			}
			foreach (GameObject u in p.StartingUnits)
			{
				//把初始建築群初始化出來
				//每個對象添加所屬玩家信息
				GameObject go = (GameObject)GameObject.Instantiate(u, p.location.position, p.location.rotation);
				BelongPlayer bp = go.AddComponent<BelongPlayer> ();
				bp.Info = p;
				if (p.IsCurrentPlayer) {
					go.AddComponent<ActionUpdate> ();
				}
				Debug.Log("初始建筑建造");
			}
		}
	}

	public Vector3? ScreenPointToMapPosition(Vector2 point)
	{
		return null;
		//TODO
		//得到屏幕上的點的3D坐標對應的地面坐標點
		Debug.Log("轉換坐標");

	}


}
