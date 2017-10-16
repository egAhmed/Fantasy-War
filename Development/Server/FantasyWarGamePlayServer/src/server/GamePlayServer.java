package server;

import java.net.InetAddress;

import config.ServerConfig;
//
import io.netty.bootstrap.ServerBootstrap;
import io.netty.channel.ChannelFuture;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelOption;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioServerSocketChannel;

//
public class GamePlayServer {

	private GamePlayServer() {
		//
	}

	public void run() throws Exception {
		//
		EventLoopGroup bossGroup = new NioEventLoopGroup();
		EventLoopGroup workerGroup = new NioEventLoopGroup();
		//
		ServerBootstrap b = null;
		ChannelFuture f = null;
		//
		try {
			b = new ServerBootstrap();
			//
			b.group(bossGroup, workerGroup).channel(NioServerSocketChannel.class).option(ChannelOption.SO_BACKLOG, 1024)
					.childOption(ChannelOption.SO_KEEPALIVE, true)
					.childHandler(new ChannelInitializer<SocketChannel>() {
						@Override
						public void initChannel(SocketChannel ch) throws Exception {
							ch.pipeline().addLast(new ServerHandler());
						}
					});
			//
			f = b.bind(InetAddress.getLocalHost(), ServerConfig.GAMEPLAY_SERVER_PORT).sync();
			//
			System.out.println("port = " + ServerConfig.GAMEPLAY_SERVER_PORT);
			//
			f.channel().closeFuture().sync();
			//
		} finally {
			//
			workerGroup.shutdownGracefully();
			bossGroup.shutdownGracefully();
			//
			if (f != null) {
				f.channel().close();
			}
			//
			b = null;
			f = null;
			//
		}
	}

	//
	public static void main(String[] args) throws Exception {
		new GamePlayServer().run();
	}
}
//