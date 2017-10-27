using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalrymanAnimatorStateController : RTSMeleeAnimatorStateController {
	// Animator CavalryAnimator;
	// // Use this for initialization
	// void Awake () {
	// 	CavalryAnimator =GetComponent <Animator> ();
	// }
	
	// // Update is called once per frame
	// void Update () {
	// 	Test ();
	// }
	// public void Cavalryman_Idle()
	// {
	// 	if (CavalryAnimator .GetInteger ("Speed")==0&&!CavalryAnimator .GetBool ("Attacking")) {
	// 		return;
	// 	}
	// 	CavalryAnimator.SetInteger ("Speed", 0);
	// 	CavalryAnimator.SetBool ("Attacking", false);
	// 	CavalryAnimator.SetTrigger ("DoIdle");
	// }
	// public void  Cavalryman_Walk()
	// {
	// 	if (CavalryAnimator .GetInteger ("Speed")==1&&!CavalryAnimator .GetBool ("Attacking")) {
	// 		return;
	// 	}
	// 	CavalryAnimator.SetInteger ("Speed", 1);
	// 	CavalryAnimator.SetBool ("Attacking", false);
	// 	CavalryAnimator.SetTrigger ("DoWalk");
	// }
	// public void  Cavalryman_Run()
	// {
	// 	if (CavalryAnimator .GetInteger ("Speed")==2&&!CavalryAnimator .GetBool ("Attacking")) {
	// 		return;
	// 	}
	// 	CavalryAnimator.SetInteger ("Speed", 2);
	// 	CavalryAnimator.SetBool ("Attacking", false);
	// 	CavalryAnimator.SetTrigger ("DoRun");
	// }
	// public void  Cavalryman_Attack()
	// {
	// 	if (CavalryAnimator.GetBool ("Attacking")) {
	// 		return;
	// 	} else {
	// 		CavalryAnimator.SetTrigger ("Ready to attack");
	// 		CavalryAnimator.SetBool ("Attacking", true);
	// 	}
	// }
	// public void  Cavalryman_Death()
	// {
	// 	if (CavalryAnimator.GetBool("Death"))
	// 	{
	// 		return;
	// 	}
	// 	//
	// 	CavalryAnimator.SetBool("Death", true);
	// 	CavalryAnimator.SetTrigger ("DoDead");
	// }
	// void Test()
	// {
	// 	if (Input .GetKeyDown (KeyCode .Q )) {
	// 		Cavalryman_Idle ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .W )) {
	// 		Cavalryman_Walk ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .E )) {
	// 		Cavalryman_Run ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .J )) {
	// 		Cavalryman_Attack ();
	// 	}
	// 	if (Input .GetKeyDown (KeyCode .D )) {
	// 		Cavalryman_Death ();
	// 	}
	// }
}
