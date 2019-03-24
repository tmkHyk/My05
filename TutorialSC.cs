using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSC : MonoBehaviour {

    //HierarchyからTutorialTextを格納
    Text tutText;

    //Tut4オブジェクト
    GameObject tut4;

	// Use this for initialization
	void Start () {

        tutText = GetComponent<Text>();
        //初期テキスト
        tutText.text = "WASD 矢印キーで移動";

        //HierarchyからTUt4オブジェクトを探して格納
        tut4 = GameObject.Find("Tut4");
    }
	
	// Update is called once per frame
	void Update () {

        //チュートリアルテキスト
        Text();

        //ゴールしたらチュートリアルテキストは表示しない
        if (Goal.isGameClear)
        {
            tutText.text = "";
        }
    }

    /// <summary>
    /// チュートリアルテキスト
    /// 後ほど外部ファイルからの読み込みに変更
    /// </summary>
    void Text()
    {
        switch (Tutorial.tutCount)
        {
            case 1:
                tutText.text = "Spaceキーでジャンプ";
                break;

            case 2:
                tutText.text = "正面の敵に近づいてください";
                break;
            case 3:
                tutText.text = "ヒビが入りました\n"
                    + "敵に4回衝突すると\n"
                    + "鏡が割れ切ってしまいゲームオーバーです";
                break;
            case 4:
                tutText.text = "右側(現実)のプレイヤーを左に移動してください";
                break;
            case 5:
                tutText.text = "鏡があります\n"
                    + "鏡の前に立ってください";
                break;
            case 6:
                tutText.text = "鏡に映ると\n"
                    +"左右移動が反転します";
                
                //Tut4オブジェクトを非アクティブ化
                tut4.SetActive(false);

                //isAcitveをtrueにしてTutrialクラスでTut5オブジェクトをアクティブ化
                Tutorial.isActive = true;

                break;
            case 7:
                tutText.text = "ステージ奥中央にゴールがあります\n"
                    + "穴を避けつつ赤い旗を目印に\n"
                    + "ゴールを目指してください";
                break;
        }
    }
}
