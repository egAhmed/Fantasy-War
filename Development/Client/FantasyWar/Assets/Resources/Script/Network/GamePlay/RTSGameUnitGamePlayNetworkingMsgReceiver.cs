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
    public void receive(RTSGameUnitGamePlayNetworkingData data) {
        //
        Debug.Log("receive");
		//
		if(data==null||data.unitID==null)
            return;
			//
		if(UnitID!=data.unitID)
            return;
        //
        Vector3 pos = new Vector3(data.positionX,data.positionY,data.positionZ);
        transform.position = pos;
        Quaternion rot = Quaternion.Euler(data.eulerX,data.eulerY,data.eulerZ);
        transform.rotation=rot;
        //
    }

    // Use this for initialization
    void Start () {
        Unit=GetComponent<RTSGameUnit>();
		//
        if (Unit == null) {
            Destroy(this);
        }
        //
        RTSGamePlayNetworkingMsgReceiverManager.ShareInstance.register(this);
        //
    }
}
