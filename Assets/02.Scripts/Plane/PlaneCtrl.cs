using System;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Vehicles.Aeroplane;

namespace FlightKit
{
    public class PlaneCtrl : MonoBehaviour
    {
        private Rigidbody rbody;
        private Transform tr;
        //声明Photon组件
        private PhotonView pv;
        private Vector3 currPos = Vector3.zero;
        private Quaternion currRot = Quaternion.identity;
        void Awake()
        {
            rbody = GetComponent<Rigidbody>();
            tr = GetComponent<Transform>();
            pv = GetComponent<PhotonView>();
            //设置传输数据类型
            pv.synchronization = ViewSynchronization.UnreliableOnChange;
            //将PhotonView组件的Observed属性设置为TankMove脚本
            pv.ObservedComponents[0] = this;
            if (pv.isMine)
            {
                GetComponent<AirPlaneJoystickController>().enabled = true;

            }
            else
            {
                GetComponent<AirPlaneJoystickController>().enabled = false;
                rbody.isKinematic = true;
            }
            currPos = tr.position;
            currRot = tr.rotation;
        }
        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            //传递本地飞机的位置和旋转信息
            if (stream.isWriting)
            {
                stream.SendNext(tr.position);
                stream.SendNext(tr.rotation);
            }
            else
            {
                currPos = (Vector3)stream.ReceiveNext();
                currRot = (Quaternion)stream.ReceiveNext();
            }
        }
        // Update is called once per frame
        void Update()
        {
            //如果不是本地玩家，返回
            if (!pv.isMine)
            {
                //将远程玩家的坦克平滑移动到目标位置
                tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 1.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 1.0f);
            }
        }
    }

}
