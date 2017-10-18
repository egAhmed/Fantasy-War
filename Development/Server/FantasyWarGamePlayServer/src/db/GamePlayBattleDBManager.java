package db;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;
import java.util.Vector;

//
import clientMsg.battle.ClientMsgBattleGameUnitData;
//

public class GamePlayBattleDBManager {
	//
	private static GamePlayBattleDBManager instance;
	//
	private Vector<UserInfo> userInfos=new Vector<UserInfo>();
	//
	//<battleID,<playerID,units>>
	private void testingUserInfoInit(){
		//
		UserInfo userInfo1=new UserInfo();
		userInfo1.userName="checkmate";
		userInfo1.passWord="666666";
		userInfo1.playerID="00000001";
		userInfo1.isOnline=true;
		userInfo1.nickName="李俊佐";
		userInfo1.userCurrentState=-1;
		//
		UserInfo userInfo2=new UserInfo();
		userInfo2.userName="finalsola";
		userInfo2.passWord="666666";
		userInfo2.playerID="00000002";
		userInfo2.isOnline=true;
		userInfo2.nickName="李础翰";
		userInfo2.userCurrentState=-1;
		//
		UserInfo userInfo3=new UserInfo();
		userInfo3.userName="zky002829";
		userInfo3.passWord="666666";
		userInfo3.playerID="00000003";
		userInfo3.isOnline=true;
		userInfo3.nickName="郑康宇";
		userInfo3.userCurrentState=-1;
		//
		UserInfo userInfo4=new UserInfo();
		userInfo4.userName="szmalqp";
		userInfo4.passWord="666666";
		userInfo4.playerID="00000004";
		userInfo4.isOnline=true;
		userInfo4.nickName="沈晟";
		userInfo4.userCurrentState=-1;
		//
		UserInfo userInfo5=new UserInfo();
		userInfo5.userName="Leslie4";
		userInfo5.passWord="666666";
		userInfo5.playerID="00000005";
		userInfo5.isOnline=true;
		userInfo5.nickName="李亚丁";
		userInfo5.userCurrentState=-1;
		//
		userInfos.add(userInfo1);
		userInfos.add(userInfo2);
		userInfos.add(userInfo3);
		userInfos.add(userInfo4);
		userInfos.add(userInfo5);
		//
	}
	//
	public UserInfo getUserInfo(String un,String pw){
		UserInfo user=null;
		//
		if(un==null||un.isEmpty())
			return user;
		  if(pw==null||pw.isEmpty())
			  return user;
		  //
		for(int i=0;i<userInfos.size();i++){
		  UserInfo temp=userInfos.get(i);
		  if(temp==null)
			  continue;
		  if(temp.userName==null||temp.userName.isEmpty())
			  continue;
		  if(temp.passWord==null||temp.passWord.isEmpty())
			  continue;
		  if(temp.passWord==null||temp.passWord.isEmpty())
			  continue;
		  if(temp.userName.equalsIgnoreCase(un)&&temp.passWord.equalsIgnoreCase(pw)){
			  user=temp;
			  break;
		  }
		}
		//
		return user;
	}
	//
	public static GamePlayBattleDBManager shareInstance() {
		if (instance == null) {
			//
			instance = new GamePlayBattleDBManager();
			//
			instance.testingUserInfoInit();
			//
		}
		//
		return instance;
	}
	//
	//
}
