using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameUnitMovable : IGameUnitBehaviour {
	float MovingSpeed
    {
        get;
        set;
    }
	//
	void move(Vector3 pos);
	//
}
