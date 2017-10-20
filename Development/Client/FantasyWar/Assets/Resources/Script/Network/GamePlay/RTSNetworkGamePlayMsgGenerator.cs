using System.Collections;
using System.Collections.Generic;

public static class RTSNetworkGamePlayMsgGenerator{

    public static RTSNetworkGamePlayMsg generateMsg(int msgType,string msgContent) {
        //
        RTSNetworkGamePlayMsg networkMsg = new RTSNetworkGamePlayMsg();
        //
		networkMsg.msgHeader=NetworkConfig.GAMEPLAY_CLIENTMSG_HEADER_HEAD+msgType+NetworkUserInfoStorage.PlayerID;
        networkMsg.msgContent = msgContent;
        //	
        return networkMsg;
    }
    //
    static RTSNetworkGamePlayMsgGenerator() { 
        
    }
}
