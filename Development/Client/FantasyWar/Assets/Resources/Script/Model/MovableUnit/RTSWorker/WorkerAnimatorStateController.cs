using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerAnimatorStateController : MonoBehaviour
{
    Animator WorkerAnimator;
    // Use this for initialization
    private void Awake()
    {
        WorkerAnimator = GetComponent<Animator>();
    }

//     Update is called once per frame
    void Update()
    {
       Test();
    }
    
    public void WorkerAnimator_dig()
    {
        if (WorkerAnimator.GetBool("IsDigging") && !WorkerAnimator.GetBool("IsHarvest"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("IsDigging", true);
        WorkerAnimator.SetBool("IsHarvest", false);
        WorkerAnimator.SetTrigger("DoDig");
    }
    public void WorkerAnimator_digHarvest()
    {
        if (WorkerAnimator.GetBool("IsDigging") && WorkerAnimator.GetBool("IsHarvest"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("IsDigging", true);
        WorkerAnimator.SetBool("IsHarvest", true);
        WorkerAnimator.SetTrigger("DoDig");
    }
    public void WorkerAnimator_idle()
    {
        if (WorkerAnimator.GetInteger("Speed") == 0 && !WorkerAnimator.GetBool("IsHarvest") && !WorkerAnimator.GetBool("IsDigging"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("IsHarvest", false);
        WorkerAnimator.SetBool("IsDigging", false);
        WorkerAnimator.SetInteger("Speed", 0);
        WorkerAnimator.SetTrigger("DoIdle");
    }
    public void WorkerAnimator_idleHarvest()
    {
        if (WorkerAnimator.GetInteger("Speed") == 0 && WorkerAnimator.GetBool("IsHarvest") && !WorkerAnimator.GetBool("IsDigging"))
        {
            return;
        }
        else
        {
            //
            WorkerAnimator.SetBool("IsHarvest", true);
            WorkerAnimator.SetInteger("Speed", 0);
            WorkerAnimator.SetBool("IsDigging", false);
            //
        }
    }
    public void WorkerAnimator_walk()
    {
        if (WorkerAnimator.GetInteger("Speed") == 1 && !WorkerAnimator.GetBool("IsHarvest") && !WorkerAnimator.GetBool("IsDigging"))
        {
            return;
        }
        //
        //
        WorkerAnimator.SetBool("IsHarvest", false);
        WorkerAnimator.SetBool("IsDigging", false);
        WorkerAnimator.SetInteger("Speed", 1);
        WorkerAnimator.SetTrigger("DoWalk");
        //
    }
    public void WorkerAnimator_walkHarvest()
    {
        if (WorkerAnimator.GetInteger("Speed") == 1 && WorkerAnimator.GetBool("IsHarvest") && !WorkerAnimator.GetBool("IsDigging"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("IsHarvest", true);
        WorkerAnimator.SetBool("IsDigging", false);
        WorkerAnimator.SetInteger("Speed", 1);
        WorkerAnimator.SetTrigger("DoWalkHarvest");
        //
    }
    public void WorkerAnimator_run()
    {
        if (WorkerAnimator.GetInteger("Speed") == 2 && !WorkerAnimator.GetBool("IsHarvest") && !WorkerAnimator.GetBool("IsDigging"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("IsHarvest", false);
        WorkerAnimator.SetBool("IsDigging", false);
        WorkerAnimator.SetInteger("Speed", 2);
        WorkerAnimator.SetTrigger("DoRun");
        //
    }
    public void WorkerAnimator_runHarvest()
    {
        if (WorkerAnimator.GetInteger("Speed") == 2 && WorkerAnimator.GetBool("IsHarvest") && !WorkerAnimator.GetBool("IsDigging"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("IsHarvest", true);
        WorkerAnimator.SetBool("IsDigging", false);
        WorkerAnimator.SetInteger("Speed", 2);
        WorkerAnimator.SetTrigger("DoRunHarvest");
        //
    }
    //
    public void WorkerAnimator_death()
    {
        if (WorkerAnimator.GetBool("Death"))
        {
            return;
        }
        //
        WorkerAnimator.SetBool("Death", true);
		WorkerAnimator.SetTrigger ("DoDead");
    }
    //
    void Test()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WorkerAnimator_idle();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            WorkerAnimator_walk();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            WorkerAnimator_run();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            WorkerAnimator_walkHarvest();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            WorkerAnimator_digHarvest();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            WorkerAnimator_runHarvest();
        }
        if (Input.GetKey(KeyCode.D))
        {
            WorkerAnimator_death();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            WorkerAnimator_dig();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            WorkerAnimator_idleHarvest();
        }
    }
}
