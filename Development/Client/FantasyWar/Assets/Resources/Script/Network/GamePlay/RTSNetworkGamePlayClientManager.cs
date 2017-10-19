using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Net;
using System.Text;
using System.Threading;


public class RTSNetworkGamePlayClientManager : UnitySingleton<RTSNetworkGamePlayClientManager>
{
    public bool IsServerConnected{ 
        get {
            return clientSocket!=null&&clientSocket.Connected;
        }
    }

    private bool _isOnline;
    public bool IsOnline{ 
        get {
            return _isOnline;
        }
        set {
            _isOnline = value;
        }
    }
    //
    private const int SEND_BUFFER_SIZE=2048*2048;
    private const int RECEIVE_BUFFER_SIZE=2048*2048;
    //
    Socket clientSocket;
    Thread receiveThread;

    const int HEARTBEAT_FREQUENCY = 15000;//15sec

    void connect()
    {
        try
        {
        //  
        IPAddress ip = IPAddress.Parse(NetworkConfig.GAMEPLAY_SERVER_HOST);
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //
       
            clientSocket.Connect(new IPEndPoint(ip, NetworkConfig.GAMEPLAY_SERVER_PORT)); //binding ip and port config  
            //
            Debug.Log("Connecting Success!");
            //
            clientSocket.SendBufferSize =SEND_BUFFER_SIZE;
            clientSocket.ReceiveBufferSize = RECEIVE_BUFFER_SIZE;
            //
            receiveThread = new Thread(receiveMsg);
            receiveThread.Start();
            //
        }
        catch (Exception e)
        {
            //
            Debug.Log("Connecting exception, could not find the GameServer...");
            // Debug.LogError(e.Message);
            //
        }
    }
    //

    private const float CONNECTING_FREQUENCY = 3F;
    private IEnumerator forceConnectingLoop() {
        while (true) {
            if (!IsServerConnected) {
                connect();
            }
            yield return new WaitForSeconds(CONNECTING_FREQUENCY);
        }
    }

    private void send(RTSNetworkGamePlayMsg msg) { 
         if(msg==null||msg.msgHeader==null||msg.msgContent==null)
            return;
            //
        if (IsServerConnected)
        {
            //
            string sendingMsg = msg.msgHeader + msg.msgContent;
            // Debug.Log("sendingMsg => "+sendingMsg);
            //
            if(sendingMsg==null)
                return;
            //
            clientSocket.Send(Encoding.UTF8.GetBytes(sendingMsg),SocketFlags.None);
            //
        }
    }

    public void send(RTSGameUnitGamePlayNetworkingData data) { 
        if(data==null)
            return;
            //
        if (IsServerConnected)
        {
            // return;
            Debug.Log("fucking connected");
            //
            string dataJSON = JsonUtility.ToJson(data);
            // Debug.LogError("dataJSON =>"+dataJSON);
            RTSNetworkGamePlayMsg msg =RTSNetworkGamePlayMsgGenerator.generateMsg(NetworkConfig.MSGTYPE_BATTLE_GAMEUNIT_DATA,dataJSON);
            //
            // Debug.LogError("msg.msgHeader =>"+msg.msgHeader);
            // Debug.LogError("msg.msgContent =>"+msg.msgContent);
            //
            send(msg);
            //
        }
    }

    private void serverMsgReader(string msgStr) { 
        // Debug.Log("received server msg => " + msgStr);
        
    }

    private void receiveMsg()
    {
        while (IsServerConnected)
        {
            Debug.Log("receiving ...");
            //
            byte[] receivedBufferByteArr = new byte[RECEIVE_BUFFER_SIZE];
            //
            int receiveLength = clientSocket.Receive(receivedBufferByteArr);
            if (receiveLength > 0)
            {
                try
                {
                    //
                    string msgStr = Encoding.UTF8.GetString(receivedBufferByteArr, 0, receiveLength);
                    Debug.Log("receiveMsg => "+msgStr);
                    serverMsgReader(msgStr);
                    //
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
            // yield return null;
        }
        //
        clientClose();
    }

    private void receiveThreadClose()
    {
        if (receiveThread != null)
        {
            if (receiveThread.IsAlive)
            {
                receiveThread.Abort();
            }
            receiveThread = null;
        }
    }

    private void clientClose()
    {
        //
        StopAllCoroutines();
        //
        receiveThreadClose();
        //
        if (clientSocket != null)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            clientSocket = null;
        }
        //
        Debug.Log("clientClose");
        //
    }
//
    public void login() {
        //
        if (!loginMsgSending) { 
            //
            StartCoroutine(pendingLogin());
            //
        }
        //
    }
    //
    bool loginMsgSending = false;
    private IEnumerator pendingLogin() {
        //
        loginMsgSending = true;
        //
        float connectionWaitingSecPerTime = 0.1f;
        int connectionWaitingTimesLimit=60;
        int connectionWaitingTimes=0;
        //
        while (!IsServerConnected&&connectionWaitingTimes<connectionWaitingTimesLimit) {
            yield return new WaitForSeconds(connectionWaitingSecPerTime);
            connectionWaitingTimes++;
        }
        //
        if (IsServerConnected) {
            //
            sendLoginMsg();
            //
            float loginWaitingSecPerTime = 0.1f;
            int loginWaitingTimesLimit=60;
            int loginWaitingTimes=0;
            //
            while (!IsOnline&&loginWaitingTimes<loginWaitingTimesLimit) { 
                //
            yield return new WaitForSeconds(loginWaitingSecPerTime);
                loginWaitingTimes++;
            //
            }
            //
            if (IsOnline) { 
                //
                Debug.Log("Login success...");
                //
            }else { 
                Debug.Log("Login failed...");
            }
            //
        }else {
            Debug.Log("connection time out...");
        }
        //
        loginMsgSending = false;
        //        
    }

    private void sendLoginMsg() { 

    }

    private void sendLogoutMsg() { 

    }

    public void logout() {
        //
        if (!logoutMsgSending) { 
            //
            StartCoroutine(pendingLogout());
            //
        }
        //
    }

    bool logoutMsgSending = false;
    private IEnumerator pendingLogout() {
        logoutMsgSending = true;
        //
        float connectionWaitingSecPerTime = 0.1f;
        int connectionWaitingTimesLimit=60;
        int connectionWaitingTimes=0;
        //
        while (!IsServerConnected&&connectionWaitingTimes<connectionWaitingTimesLimit) {
            yield return new WaitForSeconds(connectionWaitingSecPerTime);
            connectionWaitingTimes++;
        }
        //
        if (IsServerConnected) {
            //
            sendLogoutMsg();
            //
            float logoutWaitingSecPerTime = 0.1f;
            int logoutWaitingTimesLimit=60;
            int logoutWaitingTimes=0;
            //
            while (!IsOnline&&logoutWaitingTimes<logoutWaitingTimesLimit) { 
                //
                yield return new WaitForSeconds(logoutWaitingSecPerTime);
                logoutWaitingTimes++;
            //
            }
            //
            if (!IsOnline) { 
                Debug.Log("logout success...");
            }else { 
                Debug.Log("logout failed...");
            }
            //
        }else {
            Debug.Log("connection time out...");
        }
        //
        clientClose();
        logoutMsgSending = false;
        //        
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        clientClose();
    }
    //
    void Start()
    {
        StartCoroutine(forceConnectingLoop());
    }
}
