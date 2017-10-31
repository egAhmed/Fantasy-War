using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 没血死亡
/// </summary>
public class DieInNoLife : MonoBehaviour {

	bool isdead = false;

	Material[] mt ;
	SkinnedMeshRenderer smr;
	float count = 1;
	//UnitInfo info;
	RTSGameUnit _info;
	RTSGameUnit Info{
		get{ 
			if (_info == null) {
				_info = gameObject.GetComponent<RTSGameUnit> ();
			}
			return _info;
		}
	}

    void Start () {
		MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer> ();
		if (mr != null) {
			mt = mr.materials;
		}
		else {
			smr = gameObject.GetComponentInChildren<SkinnedMeshRenderer> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Info == null) {
			return;
		}
		if (Info is RTSResource)
			return;
		if (Info.HP <= 0) {
			if (!isdead) {
				isdead = true;
				if (Info.playerInfo.AllUnits.Contains (Info)) {
					Info.playerInfo.AllUnits.Remove (Info);
				}
				if (Info is RTSBuilding) {
					foreach (string item in Info.playerInfo.BuildingUnits.Keys) {
						if (Info.playerInfo.BuildingUnits [item].Contains(Info)) {
							Info.playerInfo.BuildingUnits [item].Remove (Info);
						}
					}
				}
				if (Info is RTSMovableUnit) {
					foreach (string item in Info.playerInfo.ArmyUnits.Keys) {
						if (Info.playerInfo.ArmyUnits [item].Contains (Info)) {
							Info.playerInfo.ArmyUnits [item].Remove (Info);
						}
					}
				}
				StartCoroutine (UnitDie ());
			}
		}
	}

	IEnumerator UnitDie(){
		//Debug.Log (count);
		while (count >= 0) {
			//Debug.Log (mt == null);
			if (mt != null) {
				mt [0].SetFloat ("_Progress", count / 1);
			}
			else {
				//Debug.Log (count);
				smr.materials [0].SetFloat ("_Progress", count / 1);
			}
			count -= Time.deltaTime/3;
//			Debug.Log (count);
			yield return null;
		}
		GameObject.Destroy (gameObject);
	}
}
