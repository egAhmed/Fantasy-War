using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示人口和资源
/// </summary>
public class PlayInfo : MonoBehaviour {

	public Text resources;
	public Text population;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		resources.text = UnitManager.Current.Armys[RTSManager.Current.currentPlayer] +"";
		//population.text = BelongPlayer.Default.ArmyUnits.Count+"";
	}
}
