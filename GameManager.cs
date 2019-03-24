using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //外部ファイルに書き込むための
    StreamWriter sw;
    //Clearしたステージのナンバー(外部保存)
    int saveStage;

    //提出に間に合わなかった　後ほど導入
    //Resultで表示するタイムスコア
    //public static float timer; //Goalクラス、TimeScoreクラスで使用

    //BGM
    [SerializeField,Tooltip("BGMをアタッチ 0:Title 1:Play")]
    public AudioClip[] ac;
    AudioSource audio;

    // Use this for initialization
    void Start()
    {
        //timer = 0; //後ほど
        Time.timeScale = 1;

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //RealPlayerとVirutalPlayerがStart位置に着いたらカウント開始 後ほど
        //timer += (PlayerControl.isRealStart && PlayerControl.isVirtualStart)
        //    ? Time.deltaTime : 0;

        //SelectStageを更新 次のステージのボタンを表示
        //現在のシーンがTitleまたはSelecetScene以外ならNextStageメソッドを更新
        if (SceneManager.GetActiveScene().name != "Title"
            || SceneManager.GetActiveScene().name != "SelectStage")
        {
            NextStage();
        }
        //ゲーム終了処理
        GameExit();
        //BGM再生
        IsPlaying();
    }

    /// <summary>
    /// プレイ可能なステージ数の更新
    /// </summary>
    void NextStage()
    {
        //エンターキーを押した　かつGameClearしたら
        if (Input.GetKeyDown(KeyCode.Return) && Goal.isGameClear)
        {
            //現在保存されているステージの1つ次のステージを解放
            saveStage = StageSelect.saveStage + 1;
            //1つ次のステージを上書き保存
            sw = new StreamWriter(Application.dataPath + "/Resources/saveStage.txt", append: false);
            sw.Write(saveStage);
            sw.Flush();
            //StreamWriterを終了
            sw.Close();
            //SelectStageSceneへ
            SceneManager.LoadScene("SelectStage");
        }
    }

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    void GameExit()
    {
        //Escでゲーム終了
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    void PlayBGM()
    {
        //現在のシーン名がTitleもしくはSelectStageのとき配列acの0を
        //それ以外の時は配列acの1をnowAcに格納
        var nowAc = (SceneManager.GetActiveScene().name == "Title"
            || SceneManager.GetActiveScene().name == "SelectStage")
            ? ac[0] : ac[1];

        //現在指定されたAudioClipを再生
        audio.PlayOneShot(nowAc);
    }

    /// <summary>
    /// BGMが再生中か
    /// trueのとき再生中 falseの時再生していない
    /// </summary>
    /// <returns></returns>
    bool IsPlaying()
    {
        //現在BGMが再生されていなければBGMを再生
        if (!audio.isPlaying)
        {
            PlayBGM();
        }
        return audio.isPlaying;
    }
}
