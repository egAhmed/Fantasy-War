using System.Collections;
using System.Collections.Generic;

public class NetworkUserInfoStorage
{
	private static string _userName;
	public static string UserName { 
			get 
			{
				return _userName;
			}
			set { 
			_userName = value; 
			}
	}
    private static string _password;
	public static string Password { 
		get 
		{
			return _password;
		}
        set { 
        _password = value;
		}
	}

    private static string _playerID="00000001";
	public static string PlayerID { 
		get 
		{
			return _playerID;
		}
        set { 
        _playerID = value;
		}
	}
}
