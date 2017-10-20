using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSNetworkGamePlayMsgGenerator: Singleton<RTSNetworkGamePlayMsgGenerator>{

    public RTSNetworkGamePlayMsg generateMsg(int msgType,string msgContent) {
        //
        RTSNetworkGamePlayMsg networkMsg = new RTSNetworkGamePlayMsg();
        //
		networkMsg.msgHeader=NetworkConfig.GAMEPLAY_CLIENTMSG_HEADER_HEAD+msgType+NetworkUserInfoStorage.PlayerID;
        networkMsg.msgContent = msgContent;
        //	
        return networkMsg;
    }
    //
}
