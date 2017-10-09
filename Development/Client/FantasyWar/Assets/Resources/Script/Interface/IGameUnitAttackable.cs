using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameUnitAttackable : IGameUnitBehaviour {
    //
    float AttackFrequency{ 
		get;
        set;
	}
    bool AttackEnabled
    {
        get;
        set;
    }
	//
	float AttackDistance
    {
        get;
        set;
    }

//
    void attack();
}
