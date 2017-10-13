using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSLayerManager : UnitySingleton<RTSLayerManager>
{

    private int _layerMaskUI;
    private int LayerMaskUI
    {
        get
        {
            return _layerMaskUI;
        }
    }
    private int _layerNumberUI;
    public int LayerNumberUI
    {
        get
        {
            return _layerNumberUI;
        }
    }

    private int _layerMaskPlayerMovableUnit;
    private int LayerMaskPlayerMovableUnit
    {
        get
        {
            return _layerMaskPlayerMovableUnit;
        }
    }
    private int _layerNumberPlayerMovableUnit;
    public int LayerNumberPlayerMovableUnit
    {
        get
        {
            return _layerNumberPlayerMovableUnit;
        }
    }
    private int _layerMaskEnemyGameUnit;
    private int LayerMaskEnemyGameUnit
    {
        get
        {
            return _layerMaskEnemyGameUnit;
        }
    }
    private int _layerNumberEnemyGameUnit;
    public int LayerNumberEnemyGameUnit
    {
        get
        {
            return _layerNumberEnemyGameUnit;
        }
    }
    private int _layerMaskFriendlyGameUnit;
    private int LayerMaskFriendlyGameUnit
    {
        get
        {
            return _layerMaskFriendlyGameUnit;
        }
    }
    private int _layerNumberFriendlyGameUnit;
    public int LayerNumberFriendlyGameUnit
    {
        get
        {
            return _layerNumberFriendlyGameUnit;
        }
    }

    private int _layerMaskEnvironmentGround;
    private int LayerMaskEnvironmentGround
    {
        get
        {
            return _layerMaskEnvironmentGround;
        }
    }

    private int _layerNumberEnvironmentGround;
    public int LayerNumberEnvironmentGround
    {
        get
        {
            return _layerNumberEnvironmentGround;
        }
    }

    private int _layerMaskEnvironmentObstacle;
    private int LayerMaskEnvironmentObstacle
    {
        get
        {
            return _layerMaskEnvironmentObstacle;
        }
    }

    private int _layerNumberEnvironmentObstacle;
    public int LayerNumberEnvironmentObstacle
    {
        get
        {
            return _layerNumberEnvironmentObstacle;
        }
    }

    private int _layerNumberEnvironmentResource;
    public int LayerNumberEnvironmentResource
    {
        get
        {
            return _layerNumberEnvironmentResource;
        }
    }
    //
    private int _layerMaskEnvironmentResource;
    private int LayerMaskEnvironmentResource
    {
        get
        {
            return _layerMaskEnvironmentResource;
        }
    }
    //
    private int _layerNumberPlayerBuildingUnit;
    public int LayerNumberPlayerBuildingUnit
    {
        get
        {
            return _layerNumberPlayerBuildingUnit;
        }
    }
    //
    private int _layerMaskPlayerBuildingUnit;
    private int LayerMaskPlayerBuildingUnit
    {
        get
        {
            return _layerMaskPlayerBuildingUnit;
        }
    }
    //
    private int _layerMaskRayCastMouse0;
    public int LayerMaskRayCastMouse0
    {
        get
        {
            return _layerMaskRayCastMouse0;
        }
    }
    //
    private int _layerMaskRayCastMouse1;
    public int LayerMaskRayCastMouse1
    {
        get
        {
            return _layerMaskRayCastMouse1;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //
        _layerNumberEnemyGameUnit = LayerMask.NameToLayer("EnemyGameUnit");
        _layerNumberFriendlyGameUnit = LayerMask.NameToLayer("FriendlyGameUnit");
        _layerNumberPlayerMovableUnit = LayerMask.NameToLayer("PlayerMovableUnit");
        _layerNumberEnvironmentGround = LayerMask.NameToLayer("EnvironmentGround");
        _layerNumberEnvironmentObstacle = LayerMask.NameToLayer("EnvironmentObstacle");
        _layerNumberEnvironmentResource = LayerMask.NameToLayer("EnvironmentResource");
        _layerNumberPlayerBuildingUnit = LayerMask.NameToLayer("PlayerBuildingUnit");
        _layerNumberUI = LayerMask.NameToLayer("UI");
        //
        _layerMaskEnemyGameUnit = 1 << LayerNumberEnemyGameUnit;
        _layerMaskFriendlyGameUnit = 1 << LayerNumberFriendlyGameUnit;
        _layerMaskPlayerMovableUnit = 1 << LayerNumberPlayerMovableUnit;
        _layerMaskEnvironmentGround = 1 << LayerNumberEnvironmentGround;
        _layerMaskEnvironmentObstacle = 1 << LayerNumberEnvironmentObstacle;
        _layerMaskEnvironmentResource = 1 << LayerNumberEnvironmentResource;
        _layerMaskPlayerBuildingUnit = 1 << LayerNumberPlayerBuildingUnit;
        _layerMaskUI = 1 << LayerNumberUI;
        //
        _layerMaskRayCastMouse0 = LayerMaskEnemyGameUnit | LayerMaskFriendlyGameUnit | LayerMaskPlayerMovableUnit | LayerMaskEnvironmentResource | LayerMaskPlayerBuildingUnit|LayerMaskUI;
        _layerMaskRayCastMouse1 = LayerMaskEnemyGameUnit | LayerMaskEnvironmentGround | LayerMaskEnvironmentObstacle | LayerMaskEnvironmentResource | LayerMaskPlayerBuildingUnit|LayerMaskUI;
        //
    }
}
