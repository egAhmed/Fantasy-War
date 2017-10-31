using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamionBallisticEffectController : MonoBehaviour
{
    GameObject FireBall;
    Transform FireBallSaver;
    Vector3 OriginalLocalPos{
        get;
        set;
    }
    RTSCamion Camion{
        get;
        set;
    }
    const float FIRE_SPEED = 2f;    //速度 
    bool DoingFire
    {
        get;
        set;
    }
	//
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
        //
        if (FireBall == null) {
            FireBall = GetComponentInChildren<ParticleSystem>().gameObject;
            FireBallSaver = FireBall.transform.parent;
        }
		//
        if (FireBall != null) { 
            FireBall.SetActive(false);
            OriginalLocalPos = FireBall.transform.localPosition;
		}
        //
        if (Camion == null) { 
            Camion = GetComponent<RTSCamion>();
		}
		//
    }
	//
    void resetState() { 
        //
        FireBall.transform.parent = FireBallSaver;
        //
		FireBall.transform.localPosition=OriginalLocalPos;
        //
		if (FireBall != null) { 
            FireBall.SetActive(false);
		}
        //
    }
	//
    public void Fire(RTSGameUnit targetUnit,int force)
    {
        StartCoroutine(StartShoot(targetUnit,force));
    }
    //
    IEnumerator StartShoot(RTSGameUnit targetUnit,int force)
    {
        if (!DoingFire)
        {
            DoingFire = true;
            //
        	FireBall.SetActive(true);
            FireBall.transform.parent = null;
            //
            while (DoingFire)
            {
                Vector3 targetPos = targetUnit.transform.position;
                Vector3 pos = FireBall.transform.position;
                float distanceToTarget = Vector3.Distance(targetPos, pos);
                //
                //让始终它朝着目标  
                FireBall.transform.LookAt(targetPos);

                //计算弧线中的夹角  
                // float angle = Mathf.Min(1, Vector3.Distance(pos, targetPos) / distanceToTarget) * 45;
                // FireBall.transform.rotation = FireBall.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
                // float currentDist = Vector3.Distance(pos, targetPos);
                //
                if (distanceToTarget < 3f) { 
                	break;
				}
				//
                FireBall.transform.Translate(Vector3.forward * Mathf.Min(distanceToTarget * FIRE_SPEED * Time.deltaTime, distanceToTarget));
				//
                yield return null;
                //
            }
            //
            // Debug.LogError("Fuck here");
            targetUnit.getHurt(Camion,force);
            //
            resetState();
            //
            DoingFire = false;
			//
        }
    }
    //
    // public void test(){
    // 	//
    // 	if(Input .GetKeyDown (KeyCode .J )){
    // 		Fire();
    // 	}
    // }

    // void Update(){
    // 	test ();
    // }
}
