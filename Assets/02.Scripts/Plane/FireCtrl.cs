﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public GameObject cannon = null;
    public Transform []firePos;
    private AudioClip fireSfx = null;
    private AudioSource sfx = null;
    private bool isShoot = false;
    private PhotonView pv = null;
    private float preTime = 0;
    public float fireRate = 1f;
    // Use this for initialization
    void Start()
    {
        cannon = (GameObject)Resources.Load("Cannon");
        fireSfx = Resources.Load<AudioClip>("CannonFire");
        sfx = GetComponent<AudioSource>();
        pv = GetComponent<PhotonView>();
        //设置传输数据类型
        pv.synchronization = ViewSynchronization.ReliableDeltaCompressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.isMine && isShoot&&Time.time>(preTime+ fireRate))
        {
            preTime = Time.time;
            //如果是自己本地的坦克，则调用本地函数并发射炮弹
            Fire();
            //地用远程玩家客户端的RPC函数Fire函数
            pv.RPC("Fire", PhotonTargets.Others, null);


        }
    }
    [PunRPC]
    void Fire()
    {
        sfx.PlayOneShot(fireSfx, 1.0f);
        for(int i = 0; i < firePos.Length; i++)
        {
            GameObject _cannon = (GameObject)Instantiate(cannon, firePos[i].position, firePos[i].rotation);
            _cannon.GetComponent<Cannon>().playerId = pv.ownerId;
        }
        
        
    }
    void OnClickShoot()
    {
        isShoot = true;
    }
    void OnUpShoot()
    {
        isShoot = false;
    }
}
