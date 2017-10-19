package server;

import java.net.InetAddress;

import clientMsg.battle.ClientMsgBattleGameUnitData;
import config.ServerConfig;

public class ClientBattleGameUnitDataMsgHandler {
	//
	private static ClientBattleGameUnitDataMsgHandler instance;

	//
	public static ClientBattleGameUnitDataMsgHandler shareInstance() {
		if (instance == null) {
			instance = new ClientBattleGameUnitDataMsgHandler();
		}
		return instance;
	}
	//
	private ClientBattleGameUnitDataMsgHandler() {
		
	}
	//
	public void handleClientBattleMsg(ClientMsgBattleGameUnitData unitData) {
		System.out.println("ClientMsgBattleGameUnitData : "+ unitData);
		//
	}
	//
}
