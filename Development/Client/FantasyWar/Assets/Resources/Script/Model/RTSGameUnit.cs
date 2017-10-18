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
//
[RequireComponent(typeof(MapBlip))]
[RequireComponent(typeof(RTSGameUnitFogController))]
public class RTSGameUnit : MonoBehaviour
{
	public int maxHP;
    //
    [SerializeField]
    public PlayerInfo playerInfo;
//
	protected List<Interaction> interactionList = new List<Interaction> ();
	public List<ActionBehaviour> ActionList = new List<ActionBehaviour> ();
    //

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
    //protected RTSGameUnitSelectionBottomCircleController unitSelectionBottomCircleController;
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
            //
            interactionAutoSwitch();
            //
            //selectionSpriteSwitch();
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
    [SerializeField]
    public RTSGameUnitDataInfo unitInfo;
 
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

//    private void selectionBottomCircleControllerInit()
//    {
//        Vector3 bottomPos = new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z);
//        //
//        unitSelectionBottomCircleController = PrefabFactory.ShareInstance.createClone<RTSGameUnitSelectionBottomCircleController>(RTSGameUnitSelectionBottomCircleController.prefabPath, bottomPos, Quaternion.Euler(0, 0, 0), gameObject.transform);
//        unitSelectionBottomCircleController.transform.localEulerAngles = Vector3.zero;
//    }
    //
//    private void selectionSpriteSwitch()
//    {
//        // Debug.Log(gameObject.name+" selected => "+_isSelected);
//        if (unitSelectionBottomCircleController)
//        {
//            if (IsAllowMultipleSelection || IsAllowSingleSelection)
//            {
//                if (!IsSelected)
//                {
//                    unitSelectionBottomCircleController.hide();
//                }
//                else
//                {
//                    unitSelectionBottomCircleController.show(gameUnitBelongSide);
//                }
//            }
//        }
//    }
    //
    protected virtual void Awake()
    {
        //
    }

    // Use this for initialization
    protected virtual void Start()
    {
        //selectionBottomCircleControllerInit();
        //
        RTSGameUnitManager.ShareInstance.unitRegister(this);
        //
        actionBehaviourInit();
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
    private void testingCreatePlayInfo() { 
        PlayerInfo p1=new PlayerInfo();
        p1.name = "p1";
        p1.gameUnitBelongSide = RTSGameUnitBelongSide.Player;
        playerInfo = p1;
        if (!UnitManager.ShareInstance.Buildings.ContainsKey(p1)) { 
        UnitManager.ShareInstance.Buildings.Add(p1,new List<GameObject> ());
        }
        if (!UnitManager.ShareInstance.Armys.ContainsKey(p1))
        {
            UnitManager.ShareInstance.Armys.Add(p1, new List<GameObject>());
        }
    }

    protected virtual void actionBehaviourInit() {
        //
        testingCreatePlayInfo();
        //
        gameObject.AddComponent<DieInNoLife> ();
		gameObject.AddComponent<MapBip> ();
		// gameObject.AddComponent<MarkColor> ();
//
		// Debug.Log ("我是" + this.GetType ().ToString());
        if(gameUnitBelongSide==RTSGameUnitBelongSide.Player){
			//Debug.Log (this.GetType () + "是自己人");
			Interaction au = gameObject.AddComponent<ActionUpdate> ();
			interactionList.Add (au);
		}
//
		if (IsAllowSingleSelection||IsAllowMultipleSelection) {
			gameObject.AddComponent<Interactive> ();
		}
//
		Interaction hl = gameObject.AddComponent<HightLight> ();
		interactionList.Add (hl);
		Interaction si = gameObject.AddComponent<ShowInfoUI> ();
		interactionList.Add (si);
        //
    }
    //
    public void ActiveInteractions(){
		//Debug.Log ("组件数量:"+interactionList.Count);
		foreach (Interaction selection in interactionList) {
			selection.Select();
		}
	}

	public void InactiveInteractions(){
		foreach (Interaction selection in interactionList) {
			//Debug.Log ("取消"+selection.GetType());
			selection.Deselect();
		}
	}
    //
    private void interactionAutoSwitch() {
        if (IsSelected) {
            //activate
            ActiveInteractions();
        }else {
            //
            InactiveInteractions();
        }
    }
}
