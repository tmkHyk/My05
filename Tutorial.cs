using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

    public static int tutCount;  //TutrialSCで使用

    //tutCountを3から4にするときのためタイマー
    float timer = 2;
    //tutCountを6から7にするときのためのタイマー
    float timer2 = 2;

    //敵に衝突したかどうか判定
    bool isHit = false;

    //Tut5オブジェクト
    GameObject tut5;
    //Tut5オブジェクトがアクティブかどうか　trueのときアクティブ化 falseのとき非アクティブ化
    public static bool isActive = false;

    // Use this for initialization
    void Start()
    {
        //HierarchyからTut5オブジェクトを発見し格納
        tut5 = GameObject.Find("Tut5");
        isActive = false;

        tutCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Update内は後ほど改良

        //敵に衝突したら
        if (isHit)
        {
            //カウントダウン開始
            timer -= Time.deltaTime;
            //タイマーが0以下になったとき
            if (timer <= 0)
            {
                //tutCountを4に指定
                tutCount = 4;
                isHit = false;
            }
        }

        //鏡が消去可能になったらtutCountを6に指定
        if (Mirror.isDes)
        {
            tutCount = 6;
        }

        //Tut5のアクティブ化をisActiveで指定
        tut5.SetActive(isActive);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        //敵オブジェクトと衝突したとき
        if (other.gameObject.name == "Enemy")
        {
            //tutCountを3に指定
            tutCount = 3;
            //敵との衝突判定をtrueに
            isHit = true;
        }
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "Tut1":
                //tutCountを1に指定
                tutCount = 1;
                break;
            case "Tut2":
                //tutCountを2に指定
                tutCount = 2;
                break;
            case "Tut3":
                //tutCountを2に指定
                tutCount = 5;
                //Tut3オブジェクトを非アクティブ化
                other.gameObject.SetActive(false);
                break;
            case "Tut5":
                //tutCountを7に指定
                tutCount = 7;
                break;
        }
    }
}
