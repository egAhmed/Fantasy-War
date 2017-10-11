using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo{

	public Racial racial;

	public string name;

	public Transform location;

	public Color accentColor;

	public List<GameObject> StartingUnits = new List<GameObject>();

	//private List<GameObject> buildingUnits = new List<GameObject> ();

	//public List<GameObject> BuildingUnits { get { return buildingUnits; } }

	//private List<GameObject> armyUnits = new List<GameObject> ();

	//public List<GameObject> ArmyUnits { get { return armyUnits; } }

	public bool IsCurrentPlayer;

	public float Resources;
}
