package server;

import io.netty.channel.group.ChannelGroup;
import io.netty.channel.group.DefaultChannelGroup;
import io.netty.util.concurrent.GlobalEventExecutor;

public class ServerMsgBroadcastManager {
	//
	private static ServerMsgBroadcastManager instance;
	//
	public static ServerMsgBroadcastManager shareInstance() {
		if (instance == null) {
			instance = new ServerMsgBroadcastManager();
		}
		return instance;
	}
	//
	private ServerMsgBroadcastManager() {
		
	}
	//
	public void broadcast(String msg) {
		//
//		System.out.println("broadcast numbers =>"+ServerChanelGroupManager.shareInstance().getServerChannelGroup().size());
		//
//		System.out.println("broadcast msg =>"+msg);
		//
		ServerChanelGroupManager.shareInstance().getServerChannelGroup().writeAndFlush(msg);
	}
	//
}
