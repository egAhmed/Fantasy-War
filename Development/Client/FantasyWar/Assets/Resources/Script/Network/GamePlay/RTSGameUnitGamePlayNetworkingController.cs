using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitGamePlayNetworkingController : MonoBehaviour {
    private RTSGameUnit Unit{
        get;
        set;
    }
    private RTSGameUnitGamePlayNetworkingData UnitNetworkingData{
        get;
        set;
    }
    //
    private void networkingDataInit() {
        //
        if (Unit == null||Unit.unitInfo==null) {
            UnitNetworkingData = null;
            return;
        }
        //
        if(UnitNetworkingData==null)
            UnitNetworkingData=new RTSGameUnitGamePlayNetworkingData();
        //
        //
        UnitNetworkingData.unitID = Unit.unitInfo.unitID;
        UnitNetworkingData.positionX = transform.position.x;
        UnitNetworkingData.positionY = transform.position.y;
        UnitNetworkingData.positionZ = transform.position.z;
        //
        UnitNetworkingData.eulerX = transform.rotation.eulerAngles.x;
        UnitNetworkingData.eulerY = transform.rotation.eulerAngles.y;
        UnitNetworkingData.eulerZ = transform.rotation.eulerAngles.z;
        //
    }

    public void send() {
		//
        // Debug.Log("send");
        //
        networkingDataInit();
        //
        if(UnitNetworkingData==null)
        return;
			//
        RTSNetworkGamePlayClientManager.ShareInstance.send(UnitNetworkingData);
		//
    }

    public void received(RTSGameUnitGamePlayNetworkingData data) {
        Debug.Log("received");
		//
		if(data==null)
            return;
			//
        if(Unit==null)
        return;
        //
		if(Unit.unitInfo==null)
            return;
			//
		if(Unit.unitInfo.unitID!=data.unitID)
            return;
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
    }
	
	void LateUpdate()
    {
        // send();
    }
}
