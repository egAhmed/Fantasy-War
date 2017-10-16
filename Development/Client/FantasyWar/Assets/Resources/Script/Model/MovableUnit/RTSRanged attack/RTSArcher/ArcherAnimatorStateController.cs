using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimatorStateController : MonoBehaviour {
	Animator ArcherAnimator;
	// Use this for initialization
	void Awake () {
		ArcherAnimator =GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Test ();
	}
	public void Archer_Idle()
	{
		if (ArcherAnimator .GetInteger ("Speed")==0&&!ArcherAnimator .GetBool ("Attacking")) {
			return;
		}
		ArcherAnimator.SetInteger ("Speed", 0);
		ArcherAnimator.SetBool ("Attacking", false);
		ArcherAnimator.SetTrigger ("DoIdle");
	}
	public void Archer_Walk()
	{
		if (ArcherAnimator .GetInteger ("Speed")==1&&!ArcherAnimator .GetBool ("Attacking")) {
			return;
		}
		ArcherAnimator.SetInteger ("Speed", 1);
		ArcherAnimator.SetBool ("Attacking", false);
		ArcherAnimator.SetTrigger ("DoWalk");
	}
	public void Archer_Run()
	{
		if (ArcherAnimator .GetInteger ("Speed")==2&&!ArcherAnimator .GetBool ("Attacking")) {
			return;
		}
		ArcherAnimator.SetInteger ("Speed", 2);
		ArcherAnimator.SetBool ("Attacking", false);
		ArcherAnimator.SetTrigger ("DoRun");
	}
	public void Archer_Attack()
	{
		if (ArcherAnimator.GetBool ("Attacking")) {
			return;
		} else {
			ArcherAnimator.SetTrigger ("Ready to attack");
			ArcherAnimator.SetBool ("Attacking", true);
		}
	}
	public void Archer_Death()
	{
		if (ArcherAnimator.GetBool("Death"))
		{
			return;
		}
		//
		ArcherAnimator.SetBool("Death", true);
		ArcherAnimator.SetTrigger ("DoDead");
	}
	void Test()
	{
		if (Input .GetKeyDown (KeyCode .Q )) {
			Archer_Idle ();
		}
		if (Input .GetKeyDown (KeyCode .W )) {
			Archer_Walk ();
		}
		if (Input .GetKeyDown (KeyCode .E )) {
			Archer_Run ();
		}
		if (Input .GetKeyDown (KeyCode .J )) {
			Archer_Attack ();
		}
		if (Input .GetKeyDown (KeyCode .D )) {
			Archer_Death ();
		}
	}
}
