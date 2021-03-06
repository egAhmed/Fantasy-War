﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGamePlayNetworkingMsgReceiverManager : Singleton<RTSGamePlayNetworkingMsgReceiverManager>
{
    private Dictionary<string, RTSGameUnitGamePlayNetworkingMsgReceiver> _networkingMsgReceivers;
    public Dictionary<string, RTSGameUnitGamePlayNetworkingMsgReceiver> NetworkingMsgReceivers
    {
        get
        {
            if (_networkingMsgReceivers == null)
            {
                _networkingMsgReceivers = new Dictionary<string, RTSGameUnitGamePlayNetworkingMsgReceiver>();
            }
            return _networkingMsgReceivers;
        }
    }

    public void receivedMsg(RTSGameUnitGamePlayNetworkingData data){
        //
        Debug.Log("RTSGameUnitGamePlayNetworkingData => " + data);
        //
        if (data==null)
            return;
        //
		if(data.unitID==null)
            return;
        //
        if (!NetworkingMsgReceivers.ContainsKey(data.unitID)) {
            //
            Debug.Log("!ContainsKey");
            //
            foreach (string key in NetworkingMsgReceivers.Keys) {
                Debug.Log("key =>"+ key);
            }
            // NetworkingMsgReceivers.Add(data.unitID)
        }
        else {
            //
            //Debug.Log("RTSGameUnitGamePlayNetworkingData => " + data);
            //
            RTSGameUnitGamePlayNetworkingMsgReceiver receiver =  NetworkingMsgReceivers[data.unitID];
            receiver.receive(data);
            //
        }
		//
    }

	public void register(RTSGameUnitGamePlayNetworkingMsgReceiver receiver){
		if(receiver==null||receiver.UnitID==null)
            return;
        //
        if (!NetworkingMsgReceivers.ContainsKey(receiver.UnitID)) {
            NetworkingMsgReceivers.Add(receiver.UnitID, receiver);
        }else {
            NetworkingMsgReceivers[receiver.UnitID]=receiver;
        }
		//
    }
}
