using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSMelee : RTSMovableUnit, IGameUnitAttackableMelee
{
    public float AttackFrequency
    {
        get;
        set;
    }

    public bool AttackEnabled
    {
        get;
        set;
    }
    public float AttackDistance
    {
        get;
        set;
    }

    public void attack()
    {
        
    }
}
