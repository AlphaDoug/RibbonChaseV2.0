using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HpCtrl : MonoBehaviour
{
    public Image hp;
    private float fullHP = 100;
    private float currHP = 100;
    private PhotonView pv;

    // Use this for initialization
    void Awake()
    {
        pv = GetComponent<PhotonView>();
        //设置传输数据类型
        pv.synchronization = ViewSynchronization.ReliableDeltaCompressed;
    }

    // Update is called once per frame
    void Update()
    {
        // hp.fillAmount = (float)currHP / fullHP;
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "CANNON")
        {
            if (currHP > 0)
            {
                GetDamaged();
                pv.RPC("GetDamaged", PhotonTargets.Others, null);
            }

        }
    }

    [PunRPC]
    void GetDamaged()
    {
        currHP -= 20;
        hp.fillAmount = (float)currHP / fullHP;
    }
    /*   void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
       {
           //传递本地飞机的位置和旋转信息
           if (stream.isWriting)
           {
               stream.SendNext(currHP);
           }
           else
           {
               float _tempHP = (float)stream.ReceiveNext();
               if (_tempHP < currHP)
               {
                   currHP = _tempHP;
               }
           }
       }*/
}
