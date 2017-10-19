package server;
import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.Channel;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;
import io.netty.channel.group.ChannelGroup;
import io.netty.channel.group.DefaultChannelGroup;
import io.netty.util.CharsetUtil;
import io.netty.util.concurrent.GlobalEventExecutor;

import java.io.UnsupportedEncodingException;
import config.ServerConfig;
//
public class ServerHandler extends ChannelInboundHandlerAdapter {
	@Override
	public void channelRead(ChannelHandlerContext ctx, Object msg) throws UnsupportedEncodingException {
		ByteBuf in = (ByteBuf) msg;
		byte[] req = new byte[in.readableBytes()];
		in.readBytes(req);
		//
		String clientMsgStr = new String(req,ServerConfig.GAMEPLAY_SERVER_MSG_FORMAT);
		//
		ClientMsgHandler.shareInstance().handleClientMsg(clientMsgStr);
		//
	}

	@Override
	public void channelReadComplete(ChannelHandlerContext ctx) throws Exception {
		ctx.flush();
//		ctx.channel().flush();
//		ServerChanelGroupManager.shareInstance().getServerChannelGroup().flush();
//		ctx.channel().
		System.out.println("channelReadComplete");
		//
//		ctx.writeAndFlush(Unpooled.copiedBuffer("Netty fuck you!", CharsetUtil.UTF_8)); 
//		ServerMsgBroadcastManager.shareInstance().broadcast("fuck you");
//		ServerMsgBroadcastManager.shareInstance().broadcast("fuck you");
		//
	}

	@Override
	public void exceptionCaught(ChannelHandlerContext ctx, Throwable cause) {
		System.out.println("exceptionCaught");
		cause.printStackTrace();
		ctx.close();
	}
	
	@Override
	public void channelActive(ChannelHandlerContext ctx) throws Exception {
		// TODO Auto-generated method stub
		super.channelActive(ctx);
		//
		System.out.println("channelActive");
		//
		ServerChanelGroupManager.shareInstance().getServerChannelGroup().add(ctx.channel());
		//
//		ServerChanelGroupManager.shareInstance().ClientChannelList().add(ctx);
//		ServerMsgBroadcastManager.shareInstance().broadcast("fuck you");
		//
	}
	
	@Override
	public void channelInactive(ChannelHandlerContext ctx) throws Exception {
		// TODO Auto-generated method stub
		super.channelInactive(ctx);
		//
		System.out.println("channelInactive");
		//
		ServerChanelGroupManager.shareInstance().getServerChannelGroup().remove(ctx.channel());
		//
//		ServerChanelGroupManager.shareInstance().ClientChannelList().remove(ctx);
		//
	}
	
	@Override
	public void channelRegistered(ChannelHandlerContext ctx) throws Exception {
		// TODO Auto-generated method stub
//		System.out.println("channelRegistered");
		super.channelRegistered(ctx);
		//
		//
	}
	
	@Override
	public void channelUnregistered(ChannelHandlerContext ctx) throws Exception {
		// TODO Auto-generated method stub
//		System.out.println("channelUnregistered");
		super.channelUnregistered(ctx);
		//
		
	}
}

