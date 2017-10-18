using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerFSMState
{
    public PlayerDeadState(PlayerAIController AICon)
    {
        StateID = PlayerFSMStateID.Dead;
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
