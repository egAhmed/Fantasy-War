package server;

import io.netty.buffer.Unpooled;
import io.netty.channel.group.ChannelGroup;
import io.netty.channel.group.DefaultChannelGroup;
import io.netty.util.CharsetUtil;
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
		ServerChanelGroupManager.shareInstance().getServerChannelGroup().writeAndFlush(Unpooled.copiedBuffer(msg, CharsetUtil.UTF_8));
//		for(int i=0;i<ServerChanelGroupManager.shareInstance().ClientChannelList().size();i++){
//			//
//			ServerChanelGroupManager.shareInstance().ClientChannelList().get(i).writeAndFlush(msg);
//		}
		//
	}
	//
}
