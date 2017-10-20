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
        // data.positionX =gameObject.transform.position.x;
        // data.positionY = gameObject.transform.position.y;
        // data.positionZ = gameObject.transform.position.z;
        //
        data.positionX =transform.localPosition.x;
        data.positionY = transform.localPosition.y;
        data.positionZ = transform.localPosition.z;
        //
        data.eulerX = gameObject.transform.rotation.x;
        data.eulerY = gameObject.transform.rotation.y;
        data.eulerZ = gameObject.transform.rotation.z;
        //
        //
        // Debug.Log(data);
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
        send();
    }
}
