using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {
    private PhotonView pv;
	// Use this for initialization
	void Awake () {
        pv = GetComponent<PhotonView>();
        //设置传输数据类型
        pv.synchronization = ViewSynchronization.UnreliableOnChange;
        //将PhotonView组件的Observed属性设置为TankMove脚本
        pv.ObservedComponents[0] = this;

        this.gameObject.SetActive(pv.isMine);   
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}
