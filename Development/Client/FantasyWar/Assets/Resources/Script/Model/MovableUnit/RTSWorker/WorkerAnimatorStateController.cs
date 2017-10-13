using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnimatorStateController : MonoBehaviour {
	Animator WorkerAnimator;
	// Use this for initialization
	private void Awake()
	{
		WorkerAnimator = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Test ();
	}
	public  void WorkerAnimator_dig(){
		if (WorkerAnimator.GetBool ("IsDigging")) {
			return;
		}else{
			WorkerAnimator.SetTrigger("DoDig");
			WorkerAnimator.SetBool ("IsDigging",true );
		}
	}
	public void WorkerAnimator_idle(){
		WorkerAnimator.SetInteger  ("Speed",0 );
	}
	public void WorkerAnimator_walk(){
		WorkerAnimator.SetInteger  ("Speed",1);
	}
	public void WorkerAnimator_run(){
		WorkerAnimator.SetInteger ("Speed",2 );
	}
	public void WorkerAnimator_death(){
		WorkerAnimator.SetBool  ("Death",true );
	}
	public void WorkerAnimator_HarvestIdle(){
		WorkerAnimator.SetTrigger  ("HarvestIdle");
	}
	public void WorkerAnimator_HarvestWalk(){
		WorkerAnimator.SetInteger ("HarvestSpeed",1);
	}
	public void WorkerAnimator_HarvestRun(){
		WorkerAnimator.SetInteger ("HarvestSpeed",2);
	}
    void Test()
	{
		if (Input .GetKeyDown (KeyCode .Q )) {
			WorkerAnimator_idle ();
		}
		if (Input .GetKeyDown (KeyCode .W )) {
			WorkerAnimator_walk ();
		}
		if (Input.GetKeyDown (KeyCode .E )) {
			WorkerAnimator_run ();
		}
		if (Input .GetKeyDown (KeyCode .R )) {
			WorkerAnimator_HarvestIdle ();
		}
		if (Input .GetKeyDown (KeyCode .A )) {
			WorkerAnimator_HarvestWalk ();
		}
		if (Input .GetKeyDown (KeyCode .S )) {
			WorkerAnimator_HarvestRun ();
		}
		if (Input .GetKey (KeyCode .D )) {
			WorkerAnimator_death ();
		}
		if (Input .GetKeyDown (KeyCode .J )) {
			WorkerAnimator_dig ();
		}
	}
}
