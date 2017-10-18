using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUnitChaseState : MoveUnitFSMState
{
    

    public MoveUnitChaseState()
    {
        StateID = MoveUnitFSMStateID.Chasing;

    }

    //用来确定是否需要转到其他状态
    public override void Reason(Transform enemy, Transform myself)
    {
        if(enemy==null)
        {
            Debug.LogError(StateID+"Enemy is null");
        }
            
        destPos = enemy.position;


        //Check the distance with player tank
        //When the distance is near, transition to attack state
        float dist = Vector3.Distance(myself.position, destPos);
        if (dist <= attackDistance)
        {
            Debug.Log("Switch to Attack state");
            myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.ReachEnemy);
        }
        //Go back to patrol is it become too far
        else if (dist >= chaseDistance)
        {
            Debug.Log("Switch to Patrol state");
            myself.GetComponent<MoveUnitAIController>().SetTransition(MoveUnitFSMTransition.LostEnemy);
        }
    }

    public override void Act(Transform enemy, Transform myself)
    {
        //Rotate to the target point
        destPos = enemy.position;
        MoveUnitAIController AICon = myself.GetComponent<MoveUnitAIController>();
        if (AICon.AIMove != null)
            AICon.AIMove(destPos);
    }
}
