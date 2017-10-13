using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RTSGameUnitBelongSide
{
    Player = 1,
    EnemyGroup = 1 << 1,
    FriendlyGroup = 1 << 2,
    Neutrality = 1 << 3,
}

public class RTSGameUnit : MonoBehaviour
{
    public string UnitTag
    {
        get
        {
            return gameObject.tag;
        }
        set
        {
            gameObject.tag = value;
        }
    }
    //
    public int UnitLayer
    {
        get
        {
            return gameObject.layer;
        }
        set
        {
            gameObject.layer = value;
        }
    }
    //
    private float _hpMax;
    public float HPMax
    {
        get
        {
            return _hpMax;
        }
        set
        {
            _hpMax = value;
        }
    }
    //
    private float _hp;
    public float HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    //
    protected virtual float TargetDistance
    {
        get
        {
            if (targetGameUnit != null)
            {
                return Vector3.Distance(transform.position, targetGameUnit.transform.position);
            }
            else
            {
                return -1f;
            }
        }
    }
    //
    protected Vector3 targetPosition;
    protected RTSGameUnit targetGameUnit;
    //
    public RTSGameUnitBelongSide gameUnitBelongSide;
    protected RTSGameUnitSelectionBottomCircleController unitSelectionBottomCircleController;
    //
    //
    //determine if it can be select by player
    private bool _isAllowSingleSelection = true;
    public bool IsAllowSingleSelection
    {
        get
        {
            return _isAllowSingleSelection;
        }
        set
        {
            _isAllowSingleSelection = value;
        }
    }
    //
    private bool _isAllowMultipleSelection = true;
    public bool IsAllowMultipleSelection
    {
        get
        {
            return _isAllowMultipleSelection;
        }
        set
        {
            _isAllowMultipleSelection = value;
        }
    }
    //
    private bool _isSelected;
    public bool IsSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            _isSelected = value;
            selectionSpriteSwitch();
            unitActionEventControl();
        }
    }
    //
    private bool _isVisible=true;
    public bool IsVisible
    {
        get
        {
            return _isVisible;
        }
    }
    //
    private RTSGameUnitNetworkData _unitData;
    public RTSGameUnitNetworkData UnitData
    {
        get
        {
            return _unitData;
        }

        set
        {
            _unitData = value;
        }
    }

    private void OnBecameVisible()
    {
        //Debug.Log("is visible");
        _isVisible = true;
        //RTSGameUnitSelectionManager.addSelectionListener(this);
    }

    private void OnBecameInvisible()
    {
        _isVisible = false;
        //RTSGameUnitSelectionManager.removeSelectionListener(this);
    }

    private void unitActionEventControl()
    {
        if (IsSelected)
        {
            selectedEventRegister();
        }
        else
        {
            selectedEventUnRegister();
        }
    }

    private void selectedEventRegister()
    {
        //RTSGameUnitSelectionManager.addSelectionReleaseListener(this);
        if (gameUnitBelongSide == RTSGameUnitBelongSide.Player)
        {
            RTSGameUnitActionManager.eventRegister(OnSetTargetPosition);
            RTSGameUnitActionManager.eventRegister(OnSetTargetUnit);
        }
    }

    private void selectedEventUnRegister()
    {
        //RTSGameUnitSelectionManager.removeSelectionReleaseListener(this);
        if (gameUnitBelongSide == RTSGameUnitBelongSide.Player)
        {
            RTSGameUnitActionManager.eventUnRegister(OnSetTargetPosition);
            RTSGameUnitActionManager.eventUnRegister(OnSetTargetUnit);
        }
        //
    }

    protected virtual void OnSetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }

    protected virtual void OnSetTargetUnit(RTSGameUnit unit)
    {
        if (unit == null)
        {
            return;
        }
        targetGameUnit = unit;
    }

    private void selectionBottomCircleControllerInit()
    {
        unitSelectionBottomCircleController = PrefabFactory.ShareInstance.createClone<RTSGameUnitSelectionBottomCircleController>(RTSGameUnitSelectionBottomCircleController.prefabPath, transform.position, Quaternion.Euler(0, 0, 0), gameObject.transform);
        unitSelectionBottomCircleController.transform.localEulerAngles = Vector3.zero;
    }
    //
    private void selectionSpriteSwitch()
    {
        // Debug.Log(gameObject.name+" selected => "+_isSelected);
        if (unitSelectionBottomCircleController)
        {
            if (IsAllowMultipleSelection || IsAllowSingleSelection)
            {
                if (!IsSelected)
                {
                    unitSelectionBottomCircleController.hide();
                }
                else
                {
                    unitSelectionBottomCircleController.show(gameUnitBelongSide);
                }
            }
        }
    }
    //
    protected virtual void Awake()
    {

    }

    // Use this for initialization
    protected virtual void Start()
    {
        selectionBottomCircleControllerInit();
        //
        RTSGameUnitManager.ShareInstance.unitRegister(this);
    }

    protected virtual void OnDestroy()
    {
        RTSGameUnitManager.ShareInstance.unitUnRegister(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    //
}
