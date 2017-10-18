using System.Collections;
using System.Collections.Generic;

public static class NetworkGamePlayMsgGenerator{

    public static NetworkGamePlayMsg generateMsg(int msgType,string msgContent) {
        //
        NetworkGamePlayMsg networkMsg = new NetworkGamePlayMsg();
        //
		networkMsg.msgHeader=NetworkConfig.GAMEPLAY_CLIENTMSG_HEADER_HEAD+msgType+NetworkUserInfoStorage.UserName;
        networkMsg.msgContent = msgContent;
        //	
        return networkMsg;
    }
    //
    static NetworkGamePlayMsgGenerator() { 
        
    }
}
