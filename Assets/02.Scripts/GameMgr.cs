using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMgr : MonoBehaviour {
    //显示玩家数的Text UI
    public Text textConnect;
	void Awake () {
        CreateTank();
        PhotonNetwork.isMessageQueueRunning = true;
    }
	
	void CreateTank()
    {
        float pos = Random.Range(-100.0f, 100.0f);
        PhotonNetwork.Instantiate("Starting Components",
                                   new Vector3(pos, 20.0f, pos),
                                   Quaternion.identity, 0);
        PhotonNetwork.Instantiate("UICanvas",
                                  new Vector3(pos, 20.0f, pos),
                                  Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("Tank",
        //                          new Vector3(pos, 20.0f, pos),
        //                          Quaternion.identity, 0);
    }

    void GetConnectPlayerCount()
    {
        //接收当前房间的信息
        Room currRoom = PhotonNetwork.room;
        textConnect.text = currRoom.playerCount.ToString()+"/"+currRoom.MaxPlayers.ToString();


    }
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GetConnectPlayerCount();
    }
    void OnPhotonPlayerDisconnected(PhotonPlayer outPlayer)
    {
        GetConnectPlayerCount();
    }
}
