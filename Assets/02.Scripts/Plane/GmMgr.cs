using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmMgr : MonoBehaviour {

    void Awake()
    {
        CreatePlane();
        PhotonNetwork.isMessageQueueRunning = true;
    }

    void CreatePlane()
    {
        float pos = Random.Range(300.0f, 500.0f);
        PhotonNetwork.Instantiate("PlaneBundle",
                                   new Vector3(pos, 20.0f, pos),
                                   Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("UICanvas",
        //                          new Vector3(pos, 20.0f, pos),
        //                          Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("Tank",
        //                          new Vector3(pos, 20.0f, pos),
        //                          Quaternion.identity, 0);
    }

}
