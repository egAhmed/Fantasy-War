using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitGamePlayNetworkingMsgReceiver : MonoBehaviour {
    //
    private RTSGameUnit Unit{
        get;
        set;
    }
    //
    public string UnitID{
        get { 
            if(Unit==null||Unit.unitInfo==null)
                return null;
                //
            return Unit.unitInfo.unitID;
        }
    }
    //
    private RTSGameUnitGamePlayNetworkingData UnitData{
        get;
        set;
    }
    //
    public void receive(RTSGameUnitGamePlayNetworkingData data) {
        //
        // Debug.Log("receive");
		//
		if(data==null||data.unitID==null)
            return;
			//
		if(UnitID!=data.unitID)
            return;
        //
        UnitData = data;
     
        //
    }

    // Use this for initialization
    void Start () {
        Unit=GetComponent<RTSGameUnit>();
		//
        if (UnitID == null) {
            Destroy(this);
        }
        //
        UnitData = new RTSGameUnitGamePlayNetworkingData();
        UnitData.unitID = UnitID;
        //
        RTSGamePlayNetworkingMsgReceiverManager.ShareInstance.register(this);
        //
    }
    //
    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if(UnitData==null)
            return;
            //
        Vector3 pos = new Vector3(UnitData.positionX,UnitData.positionY,UnitData.positionZ);
        transform.localPosition = pos;
        Quaternion rot = Quaternion.Euler(UnitData.eulerX,UnitData.eulerY,UnitData.eulerZ);
        transform.rotation=rot;
        //
    }
}
