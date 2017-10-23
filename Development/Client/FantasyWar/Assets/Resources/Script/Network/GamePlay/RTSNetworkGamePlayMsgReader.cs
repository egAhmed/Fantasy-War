using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSNetworkGamePlayMsgReader : Singleton<RTSNetworkGamePlayMsgReader>
{
    private readonly char[] SPLIT_SEPARATORS = new char[1] { NetworkConfig.MSG_SEPARATOR };
    public void readMsg(string msgStr)
    {
        //
        // try
        // {
        //
        if (msgStr == null)
        {
            Debug.Log("msgStr null");
            return;
        }
        //
        // Debug.Log("msgStr => " + msgStr);
        //
        if (!msgStr.StartsWith(NetworkConfig.GAMEPLAY_CLIENTMSG_HEADER_HEAD)
                || msgStr.Length < NetworkConfig.CLIENTMSG_HEADER_LENGTH)
        {
            Debug.Log("msgStr unknown, invalid format to read...");
            return;
        }
        //
        string[] msgFormattedStrArr = msgStr.Split(SPLIT_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
        //
        if (msgFormattedStrArr == null || msgFormattedStrArr.Length <= 0)
        {
            Debug.Log("msgFormattedStrArr null or empty...");
            return;
        }
        //
        for (int i = 0; i < msgFormattedStrArr.Length; i++)
        {
            //
            readEachMsg(msgFormattedStrArr[i]);
            //
        }
        //
        // }catch (Exception e) { 
        // 	//
        // 	Debug.Log("Exception => " + e.Message);
        // 	//
        // }
    }
    //
    private void readEachMsg(string msgStr)
    {
        // try {
        //
        if (msgStr == null)
        {
            Debug.Log("msgStr null");
            return;
        }
        //
        // Debug.Log("msgStr => " + msgStr);
        //
        if (!msgStr.StartsWith(NetworkConfig.GAMEPLAY_CLIENTMSG_HEADER_HEAD)
                || msgStr.Length < NetworkConfig.CLIENTMSG_HEADER_LENGTH)
        {
            Debug.Log("msgStr unknown, invalid format to read...");
            return;
        }
        //
        string msgTypeStr = msgStr.Substring(NetworkConfig.CLIENTMSG_HEADER_MSGTYPE_INDEX_START,
                NetworkConfig.CLIENTMSG_HEADER_MSGTYPE_LENGTH);
        //
        if (msgTypeStr == null)
        {
            Debug.Log("msgTypeStr null...");
            return;
        }
        //
        int msgType;
        bool result = Int32.TryParse(msgTypeStr, out msgType); // return bool value hint y/n
                                                               //
        if (!result)
        {
            //TODO
            Debug.Log("msgType unknown, invalid format to read...");
            return;
        }
        //
        string playerIDStr = msgStr.Substring(NetworkConfig.CLIENTMSG_HEADER_USERID_INDEX_START,
                NetworkConfig.CLIENTMSG_HEADER_USERID_LENGTH);
        //
        if (playerIDStr == null)
        {
            Debug.Log("playerIDStr null...");
            return;
        }
        //
        string msgContentStr = msgStr.Substring(NetworkConfig.CLIENTMSG_HEADER_USERID_INDEX_END);
        //
        if (msgContentStr == null)
        {
            Debug.Log("msgContentStr null...");
            return;
        }
        //
        //Debug.Log("msgContentStr = >" + msgContentStr);
        //
        switch (msgType)
        {
            case NetworkConfig.MSGTYPE_BATTLE_GAMEUNIT_DATA:
                //
                RTSGameUnitGamePlayNetworkingData data = JsonUtility.FromJson<RTSGameUnitGamePlayNetworkingData>(msgContentStr);
                //
                RTSGamePlayNetworkingMsgReceiverManager.ShareInstance.receivedMsg(data);
                //
                break;
            default:
                break;
        }
        //
        //
        // } catch (Exception e) {
        // 	//
        // 	Debug.Log("Exception => " + e.Message);
        // 	//
        // } 
        //	
    }
}
