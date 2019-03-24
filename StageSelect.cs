using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    //ステージに繋がるボタンを格納
    static GameObject[] stage;
    
    //現在プレイ可能のステージ数
    public static int saveStage;  //GameManagerで使用
    //外部ファイルに書き込むためのもの
    StreamReader sr;

    // Use this for initialization
    void Start()
    {
        //タグがStageのボタンオブジェクトを格納
        stage = GameObject.FindGameObjectsWithTag("Stage");
        //stage配列の中身を逆順にソート
        Array.Reverse(stage);
    }

    // Update is called once per frame
    void Update()
    {
        //外部ファイルを読み込み
        sr = new StreamReader(Application.dataPath + "/Resources/saveStage.txt");
        //読み込んだテキストをintに変換、格納
        saveStage = int.Parse(sr.ReadToEnd());
        //StreamReaderを終了
        sr.Close();
    }
}