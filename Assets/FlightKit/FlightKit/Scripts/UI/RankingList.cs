using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Data.SqlClient;

public class RankingList : MonoBehaviour {
    public Text rank;
    private List<OnePlayerGrade> rankList;
    private struct OnePlayerGrade
    {
        public string name;
        public string time;
    }
    void Start()
    {
        int time_seconds;
        time_seconds = int.Parse(FlightKit.GamePickUpsAmount.time.Substring(0, 2)) * 60 + int.Parse(FlightKit.GamePickUpsAmount.time.Substring(3, 2));
        rankList = new List<OnePlayerGrade>();
        string sql = @"Server=tcp:ribbonchase.database.windows.net,1433;Database=RibbonChaseDB;Uid=mashang2907@ribbonchase;Pwd=M19950729a.;";
        SqlConnection con = new SqlConnection(sql);
        con.Open();
        SqlCommand com = new SqlCommand();
        SqlCommand insert = new SqlCommand();
        insert.Connection = con;
        insert.CommandType = CommandType.Text;
        insert.CommandText = "insert into LV_1Grade (name , time , time_seconds) values (N'玩家' , N'" + FlightKit.GamePickUpsAmount.time + "'," + time_seconds.ToString() + ")";
        com.Connection = con;
        com.CommandType = CommandType.Text;
        com.CommandText = "select * from LV_1Grade order by time_seconds";
        SqlDataReader dr = com.ExecuteReader();//执行SQL语句
        while (dr.Read())
        {
            var newPlayerGrade = new OnePlayerGrade();
            newPlayerGrade.name = dr[0].ToString();
            newPlayerGrade.time = dr[1].ToString();
            //rank.text += dr[0].ToString() + dr[1].ToString() + "\n";
            rankList.Add(newPlayerGrade);
        }
        dr.Close();//关闭执行
        SqlDataReader ins = insert.ExecuteReader();//插入数据
        ins.Close();
        con.Close();//关闭数据库
        for (int i = 0; i < rankList.Count; i++)
        {
            if (i == 5)
            {
                break;
            }
            rank.text = rank.text + rankList[i].name + "    " + rankList[i].time + "\n";
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
