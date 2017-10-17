using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public GameObject target;   //要到达的目标  
	public float speed = 2f;    //速度 
	private float distanceToTarget;   //两者之间的距离  
	private bool move = true;       
	void Start()  
	{  
		//计算两者之间的距离  
		distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);    
	}  
	public void Fire()
	{
		StartCoroutine (StartShoot());
	}
	//
	IEnumerator StartShoot()  
	{  
		while (move)  
		{  
			Vector3 targetPos = target.transform.position;  

			//让始终它朝着目标  
			this.transform.LookAt(targetPos);  

			//计算弧线中的夹角  
			float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, targetPos) / distanceToTarget) * 45;  
			this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);  
			float currentDist = Vector3.Distance(this.transform.position, target.transform.position);  
			if (currentDist < 0.5f)  
				move = false;  
			this.transform.Translate(Vector3.forward * Mathf.Min(distanceToTarget*speed*Time .deltaTime , currentDist));  
			yield return null;  
		}  
	}  
	//
	public void test(){
		//
		if(Input .GetKeyDown (KeyCode .J )){
			Fire();
		}
	}

	void Update(){
		test ();
	}
}
