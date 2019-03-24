using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crack : MonoBehaviour {
    
    //現在選択されているImage
    Image crack;
    //CrackSpriteを格納
    public Sprite c1, c2, c3, c4;

	// Use this for initialization
	void Start () {

        //CrackオブジェクトをHierarchyから取得
        crack = GameObject.Find("Crack").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        //プレイヤーのHpによって鏡のヒビを増やす
        switch (PlayerControl.hp)
        {
            //HpがMaxのときヒビ画像は無し
            case 4:
                crack.sprite = null;
                //透過
                crack.color = new Color(0, 0, 0, 0);
                break;
            case 3:
                //画像表示
                crack.color = new Color(1, 1, 1, 1);
                crack.sprite = c1;
                break;
            case 2:
                crack.sprite = c2;
                break;
            case 1:
                crack.sprite = c3;
                break;
            //Hpが0になったとき
            case 0:
                crack.sprite = c4;
                //ゲームオーバー
                PlayerControl.isGameOver = true;
                break;
        }
    }
}
