using System.Collections;
using System.Collections.Generic;

public class NetworkConfig{
    public static readonly string SERVER_HOST = @"127.0.0.1";
    public static readonly int SERVER_PORT = 8888;

    private static string _userName;
	public static string UserName { 
		get 
	{
		return _userName;
	}
        set { 
        _userName = value; }
		}
    private static string _password;
public static string Password { 
		get 
	{
		return _password;
	}
        set { 
        _password = value; }
		}
    
}
