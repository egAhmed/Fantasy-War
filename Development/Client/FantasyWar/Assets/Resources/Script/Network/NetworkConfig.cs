using System.Collections;
using System.Collections.Generic;

public static class NetworkConfig{
    public const char MSG_SEPARATOR = ';';
    public const string CHAT_SERVER_HOST = @"10.20.70.141";
    public const string GAMEPLAY_SERVER_HOST = @"192.168.0.129";
    // public static readonly string GAMEPLAY_SERVER_HOST = @"127.0.0.1";
	//	
    public const int CHAT_SERVER_PORT = 8686;
	public const int GAMEPLAY_SERVER_PORT = 6868;
	//
	public const string GAMEPLAY_CLIENTMSG_HEADER_HEAD="FW";
	//
	public const int MSGTYPE_BATTLE_GAMEUNIT_DATA=6666;
	public const int CLIENTMSG_HEADER_LENGTH=14;
	public const int CLIENTMSG_HEADER_MSGTYPE_LENGTH=4;
	public const int CLIENTMSG_HEADER_USERID_LENGTH=8;
	public const int CLIENTMSG_HEADER_MSGTYPE_INDEX_START=2;
	public const int CLIENTMSG_HEADER_MSGTYPE_INDEX_END=6;
	public const int CLIENTMSG_HEADER_USERID_INDEX_START=6;
	public const int CLIENTMSG_HEADER_USERID_INDEX_END=14;
	//

}
