using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomData : MonoBehaviour {
    [HideInInspector]
    public string roomName = "";
    [HideInInspector]
    public int connectPlayer = 0;
    [HideInInspector]
    public int maxPlayers = 0;
    //要显示房间名称的TextUI对象 
    public Text textRoomName;
    //要显示的已经进入的玩家数和最大玩家数的TextUI对象
    public Text textConnectInfo;
    //传递房间信息后，在TextUI对象中显示房间名称和玩家数等信息
    public void DispRoomData()
    {
        textRoomName.text = roomName;
        textConnectInfo.text = "("+connectPlayer.ToString() + "/" + maxPlayers.ToString() + ")";
    }
}
