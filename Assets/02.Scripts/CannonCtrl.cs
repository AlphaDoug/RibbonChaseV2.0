using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCtrl : MonoBehaviour {
    private Transform tr;
    //PhotonView组件
    private PhotonView pv = null;
    private Quaternion currRot = Quaternion.identity;
    public float rotSpeed = 100.0f;
	// Use this for initialization
	void Awake () {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
        //将PhotonView的Obsered属性设置为当前脚本
        pv.ObservedComponents[0] = this;
        //初始化数据类型
        pv.synchronization = ViewSynchronization.UnreliableOnChange;
        //初始化旋转值
        currRot = tr.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (pv.isMine)
        {
            float angle = -Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * rotSpeed;
            tr.Rotate(angle, 0, 0);
        }
        else
        {
            //从当前位置平滑过渡到目标角度
            tr.localRotation = Quaternion.Slerp(tr.localRotation, currRot, Time.deltaTime * 3.0f);

        }
       
	}
    void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(tr.localRotation);
        }
        else
        {
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
