package server;

import java.util.Vector;

import clientMsg.battle.ClientMsgBattleGameUnitData;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.group.ChannelGroup;
import io.netty.channel.group.DefaultChannelGroup;
import io.netty.util.concurrent.GlobalEventExecutor;

public class ServerChanelGroupManager {
	//
	private static ServerChanelGroupManager instance;
	//
	private Vector<ChannelHandlerContext> _clientChannelList;
	public Vector<ChannelHandlerContext> ClientChannelList() {
		if(_clientChannelList==null){
			_clientChannelList =new Vector<ChannelHandlerContext>();
		}
		return _clientChannelList;
	}
	//
	private ChannelGroup serverChannelGroup;
	public ChannelGroup getServerChannelGroup() {
		if(serverChannelGroup==null){
			serverChannelGroup =new DefaultChannelGroup(GlobalEventExecutor.INSTANCE);
		}
		return serverChannelGroup;
	}
	//
	public static ServerChanelGroupManager shareInstance() {
		if (instance == null) {
			instance = new ServerChanelGroupManager();
		}
		return instance;
	}
	//
	private ServerChanelGroupManager() {
		
	}
	//
}
