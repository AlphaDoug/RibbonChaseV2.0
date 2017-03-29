using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour {
    private Transform tr;
    private RaycastHit hit;
    public float rotSpeed = 5.0f;

    private PhotonView pv = null;
    private Quaternion currRot = Quaternion.identity;

	// Use this for initialization
	void Awake () {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        //将Photon View的Observed属性设置为当前脚本
        pv.ObservedComponents[0] = this;
        //设置Photon View同步属性
        pv.synchronization = ViewSynchronization.UnreliableOnChange;

        //初始化旋转值
        currRot = tr.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (pv.isMine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
            {
                Vector3 relative = tr.InverseTransformPoint(hit.point);
                float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
                tr.Rotate(0, angle * Time.deltaTime * rotSpeed, 0);
            }
        }
        else//如果是远程坦克
        {
            //从当前位置平滑旋转到目标角度
            tr.localRotation=Quaternion.Slerp(tr.localRotation, currRot, Time.deltaTime * 3.0f);
        }
        
	}
    //收发回调函数
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
