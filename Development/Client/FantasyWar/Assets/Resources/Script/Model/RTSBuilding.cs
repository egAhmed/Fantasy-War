using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSBuilding : RTSGameUnit
{
    public bool allowRepair;
    public bool IsNeedRepair
    {
        get
        {
            return HP < HPMax;
        }
    }
}
