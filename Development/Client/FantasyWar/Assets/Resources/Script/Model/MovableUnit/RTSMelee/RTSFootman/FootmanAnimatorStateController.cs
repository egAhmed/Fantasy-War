using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootmanAnimatorStateController : MonoBehaviour {
	Animator FootmanAnimator;
	// Use this for initialization
	void Awake () {
		FootmanAnimator =GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Test ();
	}
	public void Footman_Idle()
	{
		FootmanAnimator.SetInteger ("Speed", 0);
	}
	public void Footman_Walk()
	{
		FootmanAnimator.SetInteger ("Speed", 1);
	}
	public void Footman_Run()
	{
		FootmanAnimator.SetInteger ("Speed", 2);
	}
	public void Footman_Attack()
	{
		if (FootmanAnimator.GetBool ("Attacking")) {
			return;
		} else {
			FootmanAnimator.SetTrigger ("Ready to attack");
			FootmanAnimator.SetBool ("Attacking", true);
		}
	}
	public void Footman_Death()
	{
		FootmanAnimator.SetBool ("Death", true);
	}
	void Test()
	{
		if (Input .GetKeyDown (KeyCode .Q )) {
			Footman_Idle ();
		}
		if (Input .GetKeyDown (KeyCode .W )) {
			Footman_Walk ();
		}
		if (Input .GetKeyDown (KeyCode .E )) {
			Footman_Run ();
		}
		if (Input .GetKeyDown (KeyCode .J )) {
			Footman_Attack ();
		}
		if (Input .GetKeyDown (KeyCode .D )) {
			Footman_Death ();
		}
	}
}
