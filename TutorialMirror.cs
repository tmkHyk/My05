using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMirror : MonoBehaviour {

    //現在のSprite
    Image image;
    //0割れていない鏡 1割れている鏡　のsprite
    public Sprite[] sprite;
    public float timer;

    //FadeImageオブジェクト
    GameObject fadeImage;
    //trueのときtfadeImageをアクティブ　falseのとき非アクティブ化
    public static bool isFadeActive;  //TutorialFadeで使用

    //Textオブジェクト
    GameObject text;
    //trueのときtextをアクティブ　falseのとき非アクティブ化
    public static bool isTextActive;  //TutorialFadeで使用

    //Discriptionオブジェクト
    GameObject discription;
    //trueのときDiscriptionをアクティブ化 falseのとき非アクティブ化
    public static bool isDiscriptionActive;  //TutorialTextで使用

    // Use this for initialization
    void Start(){

        image = GetComponent<Image>();
        //最初は割れていない鏡
        image.sprite = sprite[0];
        //上から落ちてくるようにするため描画開始位置はスクリーン上部
        transform.localPosition = new Vector3(0, 150, 0);

        //画面を暗くするための黒いImage
        fadeImage = GameObject.Find("FadeImage");
        //最初は非アクティブ化
        isFadeActive = false;
        fadeImage.SetActive(isFadeActive);
        //Storyのテキスト
        text = GameObject.Find("Text");
        //最初は非アクティブ化
        isTextActive = false;
        text.SetActive(isTextActive);

        discription = GameObject.Find("Discription");
        isDiscriptionActive = false;
        discription.SetActive(isDiscriptionActive);

        Time.timeScale = 1;
        timer = 0;
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        //imageのy座標が-60以下になったとき
        if (timer <= 1.7f)
        {
            //z軸のみ回転
            transform.localRotation *= Quaternion.Euler(0, 0, 1);
            //y軸のみ移動　鏡を落とす
            transform.localPosition += new Vector3(0, -2, 0);
        }
        else
        {
            //鏡の画像を割れた鏡に更新
            image.sprite = sprite[1];
            //Fade可能
            isFadeActive = true;
            //FadeImageをアクティブ化
            fadeImage.SetActive(isFadeActive);
            //Textは非アクティブ
            text.SetActive(isTextActive);
            //Discriptionは非アクティブ
            discription.SetActive(isDiscriptionActive);
        }

        //Escでゲーム終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
