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
import io.netty.handler.codec.string.StringDecoder;
import io.netty.handler.codec.string.StringEncoder;
import io.netty.util.CharsetUtil;

//
public class GamePlayServer {
	//
	private final static Integer CACHE_SIZE=4096*4096;
	//
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
			b.group(bossGroup, workerGroup).channel(NioServerSocketChannel.class).option(ChannelOption.SO_RCVBUF, CACHE_SIZE)
					.childOption(ChannelOption.SO_KEEPALIVE, true).childOption(ChannelOption.TCP_NODELAY, true)
					.childHandler(new ChannelInitializer<SocketChannel>() {
						@Override
						public void initChannel(SocketChannel ch) throws Exception {
							//
//							ch.pipeline().addLast("decoder", new StringDecoder(CharsetUtil.UTF_8));
//							ch.pipeline().addLast("encoder", new StringEncoder(CharsetUtil.UTF_8));
							//
							ch.pipeline().addLast(new ServerHandler());
							//
						}
					});
			//
			f = b.bind(InetAddress.getLocalHost(), ServerConfig.GAMEPLAY_SERVER_PORT).sync();
			//
			System.out.println("HOST:"+InetAddress.getLocalHost()+"\n"+"PORT:" + ServerConfig.GAMEPLAY_SERVER_PORT);
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