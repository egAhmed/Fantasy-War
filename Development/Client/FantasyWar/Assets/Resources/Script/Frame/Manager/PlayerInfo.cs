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
	private List<RTSGameUnit> _allUnits;
	public List<RTSGameUnit> AllUnits
	{
		get
		{
			return _allUnits;
		}
	}

	private Dictionary<string,List<RTSGameUnit>> buildingUnits = new Dictionary<string, List<RTSGameUnit>>();

	public Dictionary<string,List<RTSGameUnit>> BuildingUnits { get { return buildingUnits; } }

	private Dictionary<string,List<RTSGameUnit>> armyUnits = new Dictionary<string, List<RTSGameUnit>> ();

	public Dictionary<string,List<RTSGameUnit>> ArmyUnits {get{ return armyUnits;}}

	public List<RTSGameUnit> SelectedUnits
	{
		get
		{
			if (AllUnits == null)
			{
				return null;
			}
			List<RTSGameUnit> selectedUnits = new List<RTSGameUnit>();
			//

			for (int i = 0; i < AllUnits.Count; i++)
			{
				RTSGameUnit unit = AllUnits[i];
				if (unit != null && unit.IsSelected)
				{
					selectedUnits.Add(unit);
				}
			}
			return selectedUnits;
		}
	}

    public RTSGameUnitBelongSide gameUnitBelongSide;

	public bool isAI;

	public float Resources;
}
