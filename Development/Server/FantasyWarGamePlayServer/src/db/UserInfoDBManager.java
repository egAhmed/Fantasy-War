package db;

import java.util.Hashtable;
import java.util.Vector;

import clientMsg.battle.ClientMsgBattleGameUnitData;

public class UserInfoDBManager {
	//
	private static UserInfoDBManager instance;
	
	//<battleID,<playerID,units>>
	private Hashtable<Integer,Hashtable<String,Vector<ClientMsgBattleGameUnitData>>> datas;
	public Hashtable<Integer, Hashtable<String, Vector<ClientMsgBattleGameUnitData>>> getDatas() {
		if(datas==null){
			datas=new Hashtable<Integer, Hashtable<String, Vector<ClientMsgBattleGameUnitData>>>();
		}
		return datas;
	}
	//
	public void add(Integer battleID,String playerID,ClientMsgBattleGameUnitData data){
		if(!getDatas().containsKey(battleID)){
			getDatas().put(battleID, new Hashtable<String, Vector<ClientMsgBattleGameUnitData>>());
		}
		//
		Hashtable<String, Vector<ClientMsgBattleGameUnitData>> playersDatas=getDatas().get(battleID);
		//
		if(!playersDatas.containsKey(playerID)){
			playersDatas.put(playerID, new Vector<ClientMsgBattleGameUnitData>());
		}
		//
		Vector<ClientMsgBattleGameUnitData> playerDatas=playersDatas.get(playerID);
		if(playerDatas.contains(data))
		{
			
		}
	}
	//
	public static UserInfoDBManager shareInstance() {
		if (instance == null) {
			instance = new UserInfoDBManager();
		}
		//
		return instance;
	}
	//
	//
}
