using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavalrymanAnimatorStateController : MonoBehaviour {
	Animator CavalryAnimator;
	// Use this for initialization
	void Awake () {
		CavalryAnimator =GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Test ();
	}
	public void Cavalryman_Idle()
	{
		CavalryAnimator.SetInteger ("Speed", 0);
	}
	public void Cavalryman_Walk()
	{
		CavalryAnimator.SetInteger ("Speed", 1);
	}
	public void Cavalryman_Run()
	{
		CavalryAnimator.SetInteger ("Speed", 2);
	}
	public void Cavalryman_Attack()
	{
		if (CavalryAnimator.GetBool ("Attacking")) {
			return;
		} else {
			CavalryAnimator.SetTrigger ("Ready to attack");
			CavalryAnimator.SetBool ("Attacking", true);
		}
	}
	public void Cavalryman_Death()
	{
		CavalryAnimator.SetBool ("Death", true);
	}
	void Test()
	{
		if (Input .GetKeyDown (KeyCode .Q )) {
			Cavalryman_Idle ();
		}
		if (Input .GetKeyDown (KeyCode .W )) {
			Cavalryman_Walk ();
		}
		if (Input .GetKeyDown (KeyCode .E )) {
			Cavalryman_Run ();
		}
		if (Input .GetKeyDown (KeyCode .J )) {
			Cavalryman_Attack ();
		}
		if (Input .GetKeyDown (KeyCode .D )) {
			Cavalryman_Death ();
		}
	}
}
