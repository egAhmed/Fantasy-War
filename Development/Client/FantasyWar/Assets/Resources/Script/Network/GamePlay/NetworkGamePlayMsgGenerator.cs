using System.Collections;
using System.Collections.Generic;

public static class NetworkGamePlayMsgGenerator{

    public static NetworkGamePlayMsg generateMsg(int msgType,string msgContent) {
        //
        NetworkGamePlayMsg networkMsg = new NetworkGamePlayMsg();
		string networkMsgHeader=NetworkConfig.GAMEPLAY_CLIENTMSG_HEADER_HEAD+msgType+NetworkUserInfoStorage.UserName;
        string networkMsgContent = msgContent;
        //	
        return networkMsg;
    }
	//
}
