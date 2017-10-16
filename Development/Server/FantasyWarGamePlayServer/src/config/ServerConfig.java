package config;

public final class ServerConfig {
	//
	public static final int GAMEPLAY_SERVER_PORT=6868;
	//
	public static final String GAMEPLAY_SERVER_MSG_FORMAT="utf-8";
	//
	public static final int CLIENTMSG_HEADER_LENGTH=14;
	public static final String CLIENTMSG_HEADER_HEAD="FW";
	public static final int CLIENTMSG_HEADER_MSGTYPE_INDEX_START=2;
	public static final int CLIENTMSG_HEADER_MSGTYPE_INDEX_END=6;
	public static final int CLIENTMSG_HEADER_USERID_INDEX_START=6;
	public static final int CLIENTMSG_HEADER_USERID_INDEX_END=14;
	//
	public static final int CLIENTMSG_CONTENT_INDEX_START=14;
	//
	public static final int MSGTYPE_BATTLE_GAMEUNIT_DATA=6666;
	//
}
