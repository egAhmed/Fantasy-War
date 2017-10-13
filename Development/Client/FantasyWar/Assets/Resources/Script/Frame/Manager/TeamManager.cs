using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 编队管理
/// </summary>
public class TeamManager : UnitySingleton<TeamManager> {

	public List<GameObject> currentSelections = new List<GameObject>();	//當前已選中的單位

	public List<GameObject>[] teams = new List<GameObject>[9];
}
