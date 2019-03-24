using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class TutorialText : MonoBehaviour {

    //表示する文字列
    string Sentence;
    //文字列の文字数
    int length;
    //最後に表示された文字数
    int lastSentence;
    //描画間隔時間
    [SerializeField, Range(0, 5), Tooltip("テキストの表示速度")]
    float interval;
    //セットしたintervalを格納する
    float saveInterval;
    //表示する文字列
    Text t;

    //PushEnterText
    GameObject pushText;
    //PushEnterTextを表示するまでの時間
    float timer = 1;

    //外部に保存したストーリーテキストを読みこむ
    StreamReader sr;

    void Start(){

        //AseetのResources内にあるstory.txtを読み込む
        sr = new StreamReader(Application.dataPath + "/Resources/story.txt");
        //読み込んだテキストを格納
        Sentence = sr.ReadToEnd();
        //読み込みを終了
        sr.Close();

        //0文字目から開始
        lastSentence = 0;

        //intervalの初期値を格納
        saveInterval = interval;
        
        t = GetComponent<Text>();
        //ストーリーテキストを表示する
        NextSentence();

        //Enterを押すのを催促させるテキストをHierarchyから発見する
        pushText = GameObject.Find("PushEnterText");
        //最初は非アクティブ化
        pushText.SetActive(false);
    }

    void Update()
    {
        //文字列の文字数と現在表示している文字数が等しくなるまで繰り替えす
        if (!IsEnd())
        {
            interval -= Time.deltaTime;
            //テキスト表示が終了していないときEnterを押したらTextのカット
            if (Input.GetKeyDown(KeyCode.Return))
            {
                AllText();
            }
            //0.1s経ったら描画更新
            else if (interval <= 0)
            {
                NextSentence();
            }
        }
        else
        {
            //カウントダウン開始
            timer -= Time.deltaTime;
            //timerが0以下になったらPushEnterTectを表示
            if (timer <= 0)
            {
                pushText.SetActive(true);

                //PushEnterTextが表示されてからEnterを押したときStage0へ
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    TutorialMirror.isDiscriptionActive = true;

                }
            }
        }
    }

    //テキストを1文字ずつ表示させる
    void NextSentence()
    {
        //文字列の文字数を取得
        length = Sentence.Length;
        //現在の文字列の頭(0番目)からlastSentence番目までを取得、表示する
        t.text = Sentence.Substring(0,lastSentence+1);
        //表示する文字数を1増やす
        lastSentence++;
        //intervalを初期値に
        interval = saveInterval;
    }

    //Text表示のカット
    void AllText()
    {
        t.text = Sentence;
        lastSentence = Sentence.Length;
    }

    //現在表示されているテキストの文字数とストーリーテキストの総文字数が等しいかどうか
    bool IsEnd()
    {
        //等しいときtureを返す
        if (length == lastSentence)
        {
            return true;
        }
        return false;
    }
}