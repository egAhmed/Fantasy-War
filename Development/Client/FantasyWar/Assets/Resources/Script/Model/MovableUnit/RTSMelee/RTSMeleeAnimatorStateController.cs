using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSMeleeAnimatorStateController : MonoBehaviour {

	Animator meleeAnimator;
	// Use this for initialization
	void Awake () {
		meleeAnimator =GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Test ();
	}
	public void doIdle()
	{
		if (meleeAnimator .GetInteger ("Speed")==0&&!meleeAnimator .GetBool ("Attacking")) {
			return;
		}
		meleeAnimator.SetInteger ("Speed", 0);
		meleeAnimator.SetBool ("Attacking", false);
		meleeAnimator.SetTrigger ("DoIdle");
	}
	public void doWalk()
	{
		if (meleeAnimator .GetInteger ("Speed")==1&&!meleeAnimator .GetBool ("Attacking")) {
			return;
		}
		meleeAnimator.SetInteger ("Speed", 1);
		meleeAnimator.SetBool ("Attacking", false);
		meleeAnimator.SetTrigger ("DoWalk");
	}
	public void doRun()
	{
		if (meleeAnimator .GetInteger ("Speed")==2&&!meleeAnimator .GetBool ("Attacking")) {
			return;
		}
		meleeAnimator.SetInteger ("Speed", 2);
		meleeAnimator.SetBool ("Attacking", false);
		meleeAnimator.SetTrigger ("DoRun");
	}
	public void doAttack()
	{
		if (meleeAnimator.GetBool ("Attacking")) {
			return;
		} else {
			meleeAnimator.SetBool ("Attacking", true);
			meleeAnimator.SetTrigger ("Ready to attack");
		}
	}
	public void doDeath()
	{
		if (meleeAnimator.GetBool("Death"))
		{
			return;
		}
		//
		meleeAnimator.SetBool("Death", true);
		meleeAnimator.SetTrigger ("DoDead");
	}
	void Test()
	{
		if (Input .GetKeyDown (KeyCode .Q )) {
			doIdle ();
		}
		if (Input .GetKeyDown (KeyCode .W )) {
			doWalk ();
		}
		if (Input .GetKeyDown (KeyCode .E )) {
			doRun ();
		}
		if (Input .GetKeyDown (KeyCode .J )) {
			doAttack ();
		}
		if (Input .GetKeyDown (KeyCode .D )) {
			doDeath ();
		}
	}
}
