using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevelopState : PlayerFSMState
{
    public PlayerDevelopState(PlayerAIController AICon)
    {
        StateID = PlayerFSMStateID.Develop;
        AIController = AICon;
    }
    public override void Act(Transform enemy, Transform myself)
    {
        throw new NotImplementedException();
    }

    public override void Reason(Transform enemy, Transform myself)
    {

    }
}


