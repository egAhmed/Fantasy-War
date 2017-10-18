package server;

import com.google.gson.Gson;

import clientMsg.ClientMsgContent;
import clientMsg.battle.ClientMsgBattleGameUnitData;
import config.ServerConfig;

//
public class ClientMsgHandler {
	//
	private static ClientMsgHandler instance;

	//
	public static ClientMsgHandler shareInstance() {
		if (instance == null) {
			instance = new ClientMsgHandler();
		}
		return instance;
	}

	//
	private ClientMsgHandler() {

	}

	//
	public void handleClientMsg(String clientMsgStr) {
		try {
			//
			if (clientMsgStr == null) {
//				System.out.println("clientMsgStr null");
				return;
			}
			//
//			System.out.println("clientMsgStr => " + clientMsgStr);
			//
			if (!clientMsgStr.startsWith(ServerConfig.CLIENTMSG_HEADER_HEAD)
					|| clientMsgStr.length() < ServerConfig.CLIENTMSG_HEADER_LENGTH) {
//				System.out.println("clientMsgStr unknown, invalid format to read...");
//				System.out.println(clientMsgStr);
				return;
			}
			//
			String userIDStr = clientMsgStr.substring(ServerConfig.CLIENTMSG_HEADER_USERID_INDEX_START,
					ServerConfig.CLIENTMSG_HEADER_USERID_INDEX_END);
			//
			if (userIDStr == null) {
//				System.out.println("userIDStr null...");
				return;
			}
			//
			String msgTypeStr = clientMsgStr.substring(ServerConfig.CLIENTMSG_HEADER_MSGTYPE_INDEX_START,
					ServerConfig.CLIENTMSG_HEADER_MSGTYPE_INDEX_END);
			//
			if (msgTypeStr == null) {
//				System.out.println("msgTypeStr null...");
				return;
			}
			//
			int msgType = Integer.parseInt(msgTypeStr);
			//
			String msgContent = clientMsgStr.substring(ServerConfig.CLIENTMSG_CONTENT_INDEX_START);
			//
			if (msgContent == null) {
//				System.out.println("msgContent null...");
				return;
			}
			//
//			System.out.println("clientMsgStr msgContent = >"+msgContent);
			//
//			Gson gson = new Gson();
			//
			switch (msgType) {
			case ServerConfig.MSGTYPE_BATTLE_GAMEUNIT_DATA:
				//
				ServerMsgBroadcastManager.shareInstance().broadcast(clientMsgStr);
				//
//				ClientMsgBattleGameUnitData clientMsgBattleGameUnitData = gson.fromJson(msgContent,
//						ClientMsgBattleGameUnitData.class);
//				ClientBattleGameUnitDataMsgHandler.shareInstance().handleClientBattleMsg(clientMsgBattleGameUnitData);
				//
				break;
			default:
				break;
			}
			//
			//
		} catch (Exception e) {
			//
			System.out.println("Exception => " + e.getMessage());
			//
		} finally {
			//

			//
		}
		//
	}

}
