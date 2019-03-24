using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class SceneContorol : MonoBehaviour
{
    //GameOverオブジェクト、GameClearオブジェクト
    GameObject gameOver, gameClear;

    //Result時に表示されるボタンを格納する配列
    GameObject[] resultButton;
    //Result時に表示されるボタンの親オブジェクト　Result時以外は非表示
    GameObject result;
    //表示されたボタンを格納するリスト
    public List<Button> button;

    //現在のシーンの名前を格納
    string activeSceneName;

    //RetryButton GameOverのみ表示
    GameObject retryButton;

    //ボタンが表示された瞬間true それ以外false
    bool setStart;

    //外部ファイルに書き込むためのもの
    StreamWriter sw;

    // Use this for initialization
    void Start()
    {
        //現在のシーンの名前を格納
        activeSceneName = SceneManager.GetActiveScene().name;

        //現在のシーンの名前がTitleまたはSelectStageのとき
        if (activeSceneName == "Title" || activeSceneName == "SelectStage")
        {
            //最初にカーソルを置くボタン
            button[0].Select();
            //まだゲーム クリアではないのでfalse
            Goal.isGameClear = false;
            //まだゲームオーバーではないのでfalse
            PlayerControl.isGameOver = false;
        }
        else
        {
            //HierarchyからGameOverオブジェクトを探して格納
            gameOver = GameObject.Find("GameOver");
            //最初は非アクティブ化
            gameOver.SetActive(false);
            //HierarchyからGameClearオブジェクトを探して格納
            gameClear = GameObject.Find("GameClear");
            //最初は非アクティブ化
            gameClear.SetActive(false);

            //HierarchyからResultButtonオブジェクトを探して格納
            resultButton = GameObject.FindGameObjectsWithTag("ResultButton");

            //表示されているボタンを取得
            for (int i = 0; i < resultButton.Length; i++)
            {
                //リストbuttonに格納
                button.Add(resultButton[i].GetComponent<Button>());
            }

            //HierarchyからResultオブジェクトを探して格納
            result = GameObject.Find("Result");
            //HierarchyからRetryButtonオブジェクトを探して格納
            retryButton = GameObject.Find("RetryButton");
            //最初は非アクティブ化
            result.SetActive(false);
            setStart = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //タイトルシーン以外でゲームクリアしたら
        if (Goal.isGameClear && SceneManager.GetActiveScene().name != "Title")
        {
            //タイムスケールを0に
            Time.timeScale = 0;
            //ゲームクリアオブジェクトをアクティブ化
            gameClear.SetActive(true);
            //リザルトオブジェクトをアクティブ化
            result.SetActive(true);
            //GameCearではRetryButtonは非表示
            retryButton.SetActive(false);
            if (setStart)
            {
                //最初にカーソルを置くボタン = StageSelectButton
                button[1].Select();
                setStart = false;
            }

        }
        //タイトルシーン以外でゲームオーバーしたら
        if (SceneManager.GetActiveScene().name != "Title"
            && PlayerControl.isGameOver)
        {
            //タイムスケールを0に
            Time.timeScale = 0;
            //ゲームオーバーオブジェクトをアクティブ化
            gameOver.SetActive(true);
            //リザルトオブジェクトをアクティブ化
            result.SetActive(true);
            if (setStart)
            {
                //最初にカーソルを置くボタン = RetryButton
                button[0].Select();
                setStart = false;
            }
        }

        //ゲーム終了処理
        GameExit();
    }

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    void GameExit()
    {
        //Escでゲーム終了
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}
    }

    //OnClickで使用
    //保存データを消去し最初から
    public void NewGame()
    {
        sw = new StreamWriter(Application.dataPath + "/Resources/saveStage.txt", append: false);
        sw.Write("0");
        sw.Flush();
        sw.Close();
        SceneManager.LoadScene("Tutorial");
    }

    //保存データを読み込み続きから開始
    public void Continue()
    {
        SceneManager.LoadScene("SelectStage");
    }

    //TitleSceneをロード
    public void BackTitle()
    {
        SceneManager.LoadScene("Title");
    }

    //TutorialSceneをロード
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    //現在のシーンを再ロード
    public void Retry()
    {
        string sname = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sname);
    }
    //Stage0Sceneをロード
    public void Stage0()
    {
        SceneManager.LoadScene("Stage0");
    }
    //Stage1Sceneをロード
    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }
    //Stage2Sceneをロード
    public void Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    //Stage2Sceneをロード
    public void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }
}
