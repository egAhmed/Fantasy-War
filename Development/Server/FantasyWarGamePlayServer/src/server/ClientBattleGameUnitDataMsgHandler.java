package server;

import clientMsg.battle.ClientMsgBattleGameUnitData;

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
		
	}
	//
}
