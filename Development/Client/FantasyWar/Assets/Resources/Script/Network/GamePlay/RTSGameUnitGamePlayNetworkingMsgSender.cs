using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitGamePlayNetworkingMsgSender : MonoBehaviour {
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
    public void send() {
		 //
        if (Unit == null||UnitID==null) {
            return;
        }
        //
        RTSGameUnitGamePlayNetworkingData data=new RTSGameUnitGamePlayNetworkingData();
        //
        //
        data.unitID = Unit.unitInfo.unitID;
        data.positionX = transform.position.x;
        data.positionY = transform.position.y;
        data.positionZ = transform.position.z;
        //
        data.eulerX = transform.rotation.x;
        data.eulerY = transform.rotation.y;
        data.eulerZ = transform.rotation.z;
        //
        RTSNetworkGamePlayClientManager.ShareInstance.send(data);
		//
    }
	//
	void Start () {
        Unit=GetComponent<RTSGameUnit>();
		//
        if (Unit == null) {
            Destroy(this);
        }
        //
    }
	//
	void LateUpdate()
    {
        //send();
    }
}
