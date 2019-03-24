using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    //両Playerがゴール位置についたらtrue
    public static bool isGameClear;
    //RealPLayerがゴール位置に着いたらtrue
    public bool isRGoal = false;
    //VirtualPLayerがゴール位置に着いたらtrue
    public bool isVGoal = false;

	// Use this for initialization
	void Start () {

        //まだゲームクリアしていないのでfalse
        isGameClear = false;
	}
	
	// Update is called once per frame
	void Update () {

        //両PlayerがゴールしたらGameClear
        isGameClear = (isRGoal && isVGoal) ? true : false;

        //よくない　改良必要
        switch (this.gameObject.name)
        {
            //このオブジェクトがRealPlayerのとき
            case "RealGoal":
                //RealPlayerがゴールしていたらVirtualPlayerもゴール
                isVGoal = (isRGoal) ? true : false;
                break;
            //このオブジェクトがVirtualPlayerのとき
            case "VirtualGoal":
                //VirtualPlayerがゴールしていたらRealPlayerもゴール
                isRGoal = (isVGoal) ? true : false;
                break;
        }

        //GameClearしたら図っているタイムをストップ　後ほど
        //GameManager.timer = (isGameClear) ? GameManager.timer: GameManager.timer + Time.deltaTime;
    }
    
    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.name)
        {
            //RealPlayerがGoalに衝突したら(着いたら)ゴール
            case "RealPlayer":
                isRGoal = true;
                break;
            //VirtualPlayerがGoalに衝突したら(着いたら)ゴール
            case "VirtualPlayer":
                isVGoal = true;
                break;
        }
    }
}
