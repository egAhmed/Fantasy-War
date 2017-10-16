package userInfo;

public class UserInfoManager {
	//
	private static UserInfoManager instance;
	//
	
	//
	public static UserInfoManager shareInstance() {
		if (instance == null) {
			instance = new UserInfoManager();
		}
		return instance;
	}

	//
	private UserInfoManager() {
		
	}
	//
	//
}
