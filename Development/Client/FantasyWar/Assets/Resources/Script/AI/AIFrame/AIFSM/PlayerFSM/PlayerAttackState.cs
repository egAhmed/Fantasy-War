using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerFSMState
{
    public PlayerAttackState(PlayerAIController AICon)
    {
        StateID = PlayerFSMStateID.Attack;
        AIController = AICon;
    }
    public override void Act(Transform enemy, Transform myself)
    {
        throw new NotImplementedException();
    }

    public override void Reason(Transform enemy, Transform myself)
    {
        throw new NotImplementedException();
    }
    
}
