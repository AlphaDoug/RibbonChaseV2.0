using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhotonInit : MonoBehaviour {
    //App版本信息
    public string version = "v1.0";
    //玩家昵称UI
    public InputField userId;
    //输入房间名称的UI
    public InputField roomName;
    public GameObject scrollContents;
    public GameObject roomItem;
	// Use this for initialization
	void Awake () {
        PhotonNetwork.ConnectUsingSettings(version);
        roomName.text = "ROOM_" + UnityEngine.Random.Range(0, 999).ToString("000");
	}

    void onjoinedlobby()
    {
        Debug.Log("entered lobby");
       // photonnetwork.joinrandomroom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No Rooms!");
        PhotonNetwork.CreateRoom("MyRoom", new RoomOptions() { MaxPlayers = 4 }, null);
        Debug.Log("Create Room!");
    }
    void OnJoinedRoom()
    {
        Debug.Log("Enter room");
        // CreateTank();
        StartCoroutine(this.LoadBattleField());

    }
    IEnumerator LoadBattleField()
    {
        //切换场景期间，中断PhotonCloud服务器的网络信息传输
        PhotonNetwork.isMessageQueueRunning = false;
        //加载场景
        AsyncOperation ao = Application.LoadLevelAsync("scSimplify");
        yield return ao;

    }
    //void CreateTank()
    //{
    //    float pos = UnityEngine.Random.Range(-100.0f, 100.0f);
    //    PhotonNetwork.Instantiate("Tank", new Vector3(pos, 20.0f, pos), Quaternion.identity, 0);
    //}
    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        userId.text = GetUserId();
       // PhotonNetwork.JoinRandomRoom();
    }
    public void OnClickJoinRandomRoom()
    {
        //设置本地玩家昵称
        PhotonNetwork.player.name = userId.text;
        //保存玩家昵称
        PlayerPrefs.SetString("USER_ID", userId.text);
        //随即进入房间
        PhotonNetwork.JoinRandomRoom();
    }
    //点击MakeRoom按钮时调用的函数
    public void OnClickCreateRoom()
    {
        string _roonName = roomName.text;
        //没有房间名字时随机指定房间名称
        if (string.IsNullOrEmpty(roomName.text))
        {
            _roonName = "ROOM_" + UnityEngine.Random.Range(0, 999).ToString("000");
        }
        //设置本地玩家昵称
        PhotonNetwork.player.name = userId.text;
        //保存玩家昵称
        PlayerPrefs.SetString("USER_ID", userId.text);
        //设置生成的房间属性
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.maxPlayers = 4;
        //生成满足指定属性的房间
        PhotonNetwork.CreateRoom(_roonName, roomOptions, TypedLobby.Default);
    }

    void OnPhotonCreateRoomFailed(UnityEngine.Object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
    }
    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");
        if (string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + UnityEngine.Random.Range(0, 999).ToString("000");

        }
        return userId;
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    //房间列表变更时调用的回调函数
    void OnReceivedRoomListUpdate()
    {
        //重新接收房间目录信息时，先删除现有RoomItem对象
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }
        foreach (RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(_room.Name);
            //动态生成RoomItem预设
            GameObject room = (GameObject)Instantiate(roomItem);
            //指定RoomItem预设的父对象
            room.transform.SetParent(scrollContents.transform, false);
            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.Name;
            roomData.connectPlayer = _room.PlayerCount;
            roomData.maxPlayers = _room.MaxPlayers;
            //显示相关信息
            roomData.DispRoomData();
            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnClickRoomItem(roomData.roomName); });
        }
    }
    void OnClickRoomItem(string roomName)
    {
        //设置本地玩家昵称
        PhotonNetwork.player.name = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        //进入符合条件的房间
        PhotonNetwork.JoinRoom(roomName);
    }

}
