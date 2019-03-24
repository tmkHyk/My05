using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulChange : MonoBehaviour {
    
    Image image;
    //Imageのalpha値の変数
    float alpha;

    //alpha値を増減させるための値
    float alphaCount = 0;
    //alphaが0を取った回数
    float countZero = 0;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();
        //画像サイズを画面いっぱいに指定
        image.rectTransform.localScale = new Vector3(Screen.width, Screen.height, 1);
        //最初は透過している状態
        alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        //画像のカラー　透過値はalphaで変化させる
        image.color = new Color(1, 1, 1, alpha);

        //鏡が削除可能になったら(Mirrorクラス)
        if (Mirror.isDes)
        {
            //魂転換時Playerの動きを停止
            alphaCount += 0.05f;
            //Imageの点滅
            alpha = Mathf.Clamp(Mathf.Sin(alphaCount), 0, 1);
            //alphaが0を取ったら+1
            countZero += (alpha == 0) ? 1 : 0;
            //alphaが2回以上0を取ったらisStartはfalse 取っていなければtrue
            Mirror.isDes = (countZero >= 2) ? false : true;
        }
        else
        {
            //タイムスケールを1に変更
            Time.timeScale = 1;
            //透過する
            alpha = 0;
        }
	}
}
