using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanAnimatorStateController : RTSMeleeAnimatorStateController {
	// Animator FootmanAnimator;
	// // Use this for initialization
	// void Awake () {
	// 	FootmanAnimator =GetComponent <Animator> ();
	// }
	
	// // Update is called once per frame
	// void Update () {
	// 	// Test ();
	// }
	// public void Footman_Idle()
	// {
	// 	if (FootmanAnimator .GetInteger ("Speed")==0&&!FootmanAnimator .GetBool ("Attacking")) {
	// 		return;
	// 	}
	// 	FootmanAnimator.SetInteger ("Speed", 0);
	// 	FootmanAnimator.SetBool ("Attacking", false);
	// 	FootmanAnimator.SetTrigger ("DoIdle");
	// }
	// public void Footman_Walk()
	// {
	// 	if (FootmanAnimator .GetInteger ("Speed")==1&&!FootmanAnimator .GetBool ("Attacking")) {
	// 		return;
	// 	}
	// 	FootmanAnimator.SetInteger ("Speed", 1);
	// 	FootmanAnimator.SetBool ("Attacking", false);
	// 	FootmanAnimator.SetTrigger ("DoWalk");
	// }
	// public void Footman_Run()
	// {
	// 	if (FootmanAnimator .GetInteger ("Speed")==2&&!FootmanAnimator .GetBool ("Attacking")) {
	// 		return;
	// 	}
	// 	FootmanAnimator.SetInteger ("Speed", 2);
	// 	FootmanAnimator.SetBool ("Attacking", false);
	// 	FootmanAnimator.SetTrigger ("DoRun");
	// }
	// public void Footman_Attack()
	// {
	// 	if (FootmanAnimator.GetBool ("Attacking")) {
	// 		return;
	// 	} else {
	// 		FootmanAnimator.SetTrigger ("Ready to attack");
	// 		FootmanAnimator.SetBool ("Attacking", true);
	// 	}
	// }
	// public void Footman_Death()
	// {
	// 	if (FootmanAnimator.GetBool("Death"))
	// 	{
	// 		return;
	// 	}
	// 	//
	// 	FootmanAnimator.SetBool("Death", true);
	// 	FootmanAnimator.SetTrigger ("DoDead");
	// }
	// void Test()
	// {
	// 	if (Input .GetKeyDown (KeyCode .Q )) {
	// 		Footman_Idle ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .W )) {
	// 		Footman_Walk ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .E )) {
	// 		Footman_Run ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .J )) {
	// 		Footman_Attack ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .D )) {
	// 		Footman_Death ();
	// 	}
	// }
}
