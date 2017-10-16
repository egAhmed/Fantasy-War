using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInRange : MonoBehaviour {

	RTSGameUnit target;

	/// <summary>
	/// 寻找一个攻击目标，赋值到target
	/// </summary>
	void FindTarget(){
		//TODO
	}

	/// <summary>
	/// 播放攻击动画，处理伤害等
	/// </summary>
	void Attack(){
		if (target == null)
			return;

	}

	void Update () {
		FindTarget ();
		Attack ();
	}
}
