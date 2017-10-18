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
    //
    Socket clientSocket;
    Thread receiveThread;

    const int HEARTBEAT_FREQUENCY = 15000;//15sec
    byte[] result = new byte[1024 * 1024];

    void connect()
    {
        //  
        IPAddress ip = IPAddress.Parse(NetworkConfig.GAMEPLAY_SERVER_HOST);
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //
        try
        {
            clientSocket.Connect(new IPEndPoint(ip, NetworkConfig.GAMEPLAY_SERVER_PORT)); //binding ip and port config  
            //
            Debug.Log("Connecting Success!");
            //
            receiveThread = new Thread(receiveMsg);
            receiveThread.Start();
            //
        }
        catch (Exception e)
        {
            //
            Debug.LogError("Connecting exception, Error...");
            Debug.LogError(e.Message);
            //
        }
    }

    private void send(NetworkGamePlayMsg msg) { 
        if (IsServerConnected)
        {
            //
            // Debug.Log("sending => "+msg);
            //
            clientSocket.Send(Encoding.UTF8.GetBytes(msg.msgHeader+msg.msgContent));
            //
        }else
        {
            connect();
        }
    }

    public void send(RTSGameUnitGamePlayNetworkingData data) { 
        if(data==null)
            return;
            //
        if (IsServerConnected)
        {
            NetworkGamePlayMsg msg = NetworkGamePlayMsgGenerator.generateMsg(NetworkConfig.MSGTYPE_BATTLE_GAMEUNIT_DATA,JsonUtility.ToJson(data));
            //
            if(msg==null)
            return;
            //
            send(msg);
            //
        }else
        {
            connect();
        }
    }

    private void serverMsgReader(string msgStr) { 
                    Debug.Log("received server msg => " + msgStr);
    }

    private void receiveMsg()
    {
        while (clientSocket != null && clientSocket.Connected)
        {
            int receiveLength = clientSocket.Receive(result);
            if (receiveLength > 0)
            {
                try
                {
                    //
                    string msgStr = Encoding.UTF8.GetString(result, 0, receiveLength);
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
        receiveThreadClose();
        //
        if (clientSocket != null)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            clientSocket = null;
        }
        //
        Debug.LogError("clientClose");
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
        connect();
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
}
