using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamionAnimatorStateController : MonoBehaviour {
	Animator CamionAnimator;
	// Use this for initialization
	void Awake () {
		CamionAnimator =GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Test ();
	}
	public void doIdle()
	{
		if (CamionAnimator .GetInteger ("Speed")==0&&!CamionAnimator .GetBool ("Attacking")) {
			return;
		}
		CamionAnimator.SetInteger ("Speed", 0);
		CamionAnimator.SetBool ("Attacking", false);
		CamionAnimator.SetTrigger ("DoIdle");
	}
	public void doWalk()
	{
		if (CamionAnimator .GetInteger ("Speed")==1&&!CamionAnimator .GetBool ("Attacking")) {
			return;
		}
		CamionAnimator.SetInteger ("Speed", 1);
		CamionAnimator.SetBool ("Attacking", false);
		CamionAnimator.SetTrigger ("DoMove");
	}
	public void doAttack()
	{
		if (CamionAnimator.GetBool ("Attacking")) {
			return;
		} else {
			CamionAnimator.SetTrigger ("Ready to attack");
			CamionAnimator.SetBool ("Attacking", true);
		}
	}
	public void doDeath()
	{
		if (CamionAnimator.GetBool("Death"))
		{
			return;
		}
		//
		CamionAnimator.SetBool("Death", true);
		CamionAnimator.SetTrigger ("DoDead");
	}
	// void Test()
	// {
	// 	if (Input .GetKeyDown (KeyCode .Q )) {
	// 		doIdle ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .W )) {
	// 		doWalk ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .J )) {
	// 		doAttack ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .D )) {
	// 		doDeath ();
	// 	}
	// }
}
