using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlaneDamage : MonoBehaviour
{
    //为实现坦克爆炸后的透明处理，声明MeshRenderer组件数组变量
    private MeshRenderer[] renderers;
    //坦克爆炸预设
    private GameObject expEffect = null;
    //坦克初始生命值
    private int initHp = 100;
    //坦克当前生命值
    private int currHp = 0;
    //Canvas对象
    public Canvas hudCanvas;
    //Filed类型的ImageUI对象
    public Image hpBar;

    //初始化PhotonView组件
    private PhotonView pv = null;
    //保存玩家ID的变量
    public int playerId = -1;
    //保存破坏敌对坦克数量的变量
    public int killCount = 0;
    //要显示到坦克HUD的分数TextUI项目
    public Text textKillCount;
    void Awake()
    {
        //获取坦克的MeshRenderer组件
        renderers = GetComponentsInChildren<MeshRenderer>();
        //将当前生命值作为初始生命值
        currHp = initHp;
        //加载坦克爆炸时需要生成的爆炸效果
        expEffect = Resources.Load<GameObject>("Med Explosion");
        //设置生命值条图像颜色设置为绿色
        hpBar.color = Color.green;
        //分配PhotonView组件
        pv = GetComponent<PhotonView>();
        //将PhotonView的ownerId保存到PlayerId
        playerId = pv.ownerId;

    }

    void OnTriggerEnter(Collider coll)
    {
        //根据tag判断碰撞是否为炮弹
        if (coll.tag == "CANNON" && currHp > 0)
        {
            currHp -= 20;
            //当前生命百分比=（当前生命值）/（初始生命值）
            hpBar.fillAmount = (float)currHp / (float)initHp;
            //根据生命值大小更改生命值条图像颜色
            if (hpBar.fillAmount <= 0.4f)
            {
                hpBar.color = Color.red;
            }
            else if (hpBar.fillAmount <= 0.6f)
            {
                hpBar.color = Color.yellow;
            }
            if (currHp <= 0)
            {
                SaveKillCount(coll.GetComponent<Cannon>().playerId);
                StartCoroutine(this.ExplotionTank());
            }
        }
    }

    //生成爆炸效果并处理坦克复活
    IEnumerator ExplotionTank()
    {
        //生成爆炸效果
        Object effect = GameObject.Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3.0f);
        //禁用HUD
        hudCanvas.enabled = false;
        //透明处理坦克
        SetTankVisible(false);
        //等待三秒
        yield return new WaitForSeconds(3.0f);

        //复活时设置初始生命值
        hpBar.fillAmount = 1.0f;
        hpBar.color = Color.green;
        hudCanvas.enabled = true;
        //复活时重新设置初始生命值
        currHp = initHp;
        //使坦克再次可见
        SetTankVisible(true);
    }
    void SetTankVisible(bool isVisible)
    {
        foreach (MeshRenderer _renderer in renderers)
        {
            _renderer.enabled = isVisible;
        }
    }
    //获得破坏自己的敌对坦克并调用加分的函数
    void SaveKillCount(int firePlayerID)
    {
        //获取所有标签为TANK的坦克并保存到数组
        GameObject[] tanks = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject tank in tanks)
        {
            var planeDamage = tank.GetComponent<PlaneDamage>();
            //判断坦克的playerId是否与炮弹的playerId一致
            if (planeDamage != null && planeDamage.playerId == firePlayerID)
            {
                planeDamage.IncKillCount();
                break;

            }
        }
    }
    void IncKillCount()
    {
        //增加分数
        ++killCount;
        //在HUD界面的TextUI项目中显示分数
        textKillCount.text = killCount.ToString();
    }
}
