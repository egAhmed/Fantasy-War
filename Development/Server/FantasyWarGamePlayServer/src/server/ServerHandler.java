package server;
import io.netty.buffer.ByteBuf;
import io.netty.channel.ChannelHandlerContext;
import io.netty.channel.ChannelInboundHandlerAdapter;
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
		System.out.println("channelReadComplete");
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
	}
	
	@Override
	public void channelRegistered(ChannelHandlerContext ctx) throws Exception {
		// TODO Auto-generated method stub
		System.out.println("channelRegistered");
		super.channelRegistered(ctx);
	}
	
	@Override
	public void channelUnregistered(ChannelHandlerContext ctx) throws Exception {
		// TODO Auto-generated method stub
		System.out.println("channelUnregistered");
		super.channelUnregistered(ctx);
	}
	
}

