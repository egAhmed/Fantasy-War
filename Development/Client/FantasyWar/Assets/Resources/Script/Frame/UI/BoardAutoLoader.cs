using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAutoLoader : MonoBehaviour {
	public int index;
	void Start () {
		ActionManager.ShareInstance.Board[index] = gameObject;
	}

}
