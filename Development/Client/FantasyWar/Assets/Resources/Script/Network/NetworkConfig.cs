using System.Collections;
using System.Collections.Generic;

public class NetworkConfig{
    public static readonly string CHAT_SERVER_HOST = @"10.20.70.141";
    public static readonly string GAMEPLAY_SERVER_HOST = @"10.20.70.204";
    public static readonly int CHAT_SERVER_PORT = 8686;
	public static readonly int GAMEPLAY_SERVER_PORT = 6868;
	//
	public static readonly string GAMEPLAY_CLIENTMSG_HEADER_HEAD="FW";
	//
	public static readonly int MSGTYPE_BATTLE_GAMEUNIT_DATA=6666;
	public static readonly int CLIENTMSG_HEADER_LENGTH=14;
	public static readonly int CLIENTMSG_HEADER_MSGTYPE_INDEX_START=2;
	public static readonly int CLIENTMSG_HEADER_MSGTYPE_INDEX_END=6;
	public static readonly int CLIENTMSG_HEADER_USERID_INDEX_START=6;
	public static readonly int CLIENTMSG_HEADER_USERID_INDEX_END=14;
	//

}
