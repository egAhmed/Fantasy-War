using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitGamePlayNetworkingController : MonoBehaviour {
    private RTSGameUnit unit;
    public void send() {
		//
        Debug.Log("send");
		//
		if(unit==null)
        return;
		//
		if(unit.UnitData==null)
        return;
			//
        RTSNetworkGamePlayClientManager.ShareInstance.send(unit.UnitData);
		//
    }

    public void received(RTSGameUnitDataInfo data) {
        Debug.Log("received");
		//
		if(data==null)
            return;
			//
        if(unit==null)
        return;
        //
		if(unit.UnitData==null)
            return;
			//
		if(unit.UnitData.unitID==data.unitID)
        unit.UnitData = data;
    }

    // Use this for initialization
    void Start () {
        unit=GetComponent<RTSGameUnit>();
		//
        if (unit == null) {
            Destroy(this);
        }
		//
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
