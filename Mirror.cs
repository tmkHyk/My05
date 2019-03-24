using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour {

    //削除可能かどうか　trueのときDestroy可能 falseのときDestroy不可
    public static bool isDes = false;

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {

        //移動、回転不可　全固定
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        if (isDes)
        {
            //削除可能時全てのTimeScaleが働くオブジェクトは動かさない(Player,Enemy)
            Time.timeScale = 0;
            //0.5s後削除
            Destroy(this.gameObject, 0.5f);
        }
	}
    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            //鏡に写った(衝突したら)player同士操作が左右反転する changeControlが-1のとき1　1のとき-1
            PlayerControl.changeControl = (PlayerControl.changeControl == 1)
                ? -1 : 1;
            //左右反転が終わったら削除可能
            isDes = true;
        }
    }
}
