using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所屬陣營
/// </summary>
public class BelongPlayer : MonoBehaviour {

	public PlayerInfo Info;
	UnitInfo info;

	public static PlayerInfo Default;

	void Start()
	{
		//根據該單位是建築還是士兵，添加到Info中建築/單位列表
		info = gameObject.GetComponent<UnitInfo>();
		switch (info.unitType) {
		case UnitType.army:
			Info.ArmyUnits.Add (gameObject);
			break;
		case UnitType.building:
			Info.BuildingUnits.Add (gameObject);
			break;
		default:
			break;
		}
	}

	void OnDestroy()
	{
		//根據該單位是建築還是士兵，刪除Info中建築/單位列表對應對象
		switch (info.unitType) {
		case UnitType.army:
			Info.ArmyUnits.Remove (gameObject);
			break;
		case UnitType.building:
			Info.BuildingUnits.Add (gameObject);
			break;
		default:
			break;
		}
	}
}
