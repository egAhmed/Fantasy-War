using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionBehaviour : MonoBehaviour {

	public abstract Action GetClickAction ();
	public int index;
	public Sprite actionIcon;

}
