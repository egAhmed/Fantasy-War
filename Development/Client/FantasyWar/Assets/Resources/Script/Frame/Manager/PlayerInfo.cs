using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo{

	public Racial racial;

	public string name;

	public Vector3 location;

	public Color accentColor;

    //public List<GameObject> StartingUnits = new List<GameObject>();

	private Dictionary<string,List<RTSGameUnit>> buildingUnits = new Dictionary<string, List<RTSGameUnit>>();

	public Dictionary<string,List<RTSGameUnit>> BuildingUnits { get { return buildingUnits; } }

	private Dictionary<string,List<RTSGameUnit>> armyUnits = new Dictionary<string, List<RTSGameUnit>> ();

	public Dictionary<string,List<RTSGameUnit>> ArmyUnits {get{ return armyUnits;}}

    public RTSGameUnitBelongSide gameUnitBelongSide;

	//public bool IsCurrentPlayer;

	public float Resources;
}
