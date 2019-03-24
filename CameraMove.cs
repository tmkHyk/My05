using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    //RealPlayerとVirtualPlayerオブジェクト
    GameObject rPlayer, vPlayer;
    //Cameraの動けるZ軸範囲　Hierarchyから取得
    Vector3 rCamMin, rCamMax,vCamMin,vCamMax;
    //Hierarchyから取得した範囲を格納するための変数
    float rPosZ, vPosZ;

    //SE Playerと衝突時割れる音
    public AudioClip ac;
    AudioSource audioSource;

    //今の位置
    Vector3 nowPos;
    //プレイヤーがダメージを受けた時画面を振動させる時間
    float time = 0.3f;

    //各Enemyスクリプトで使用
    //RealPlayerが敵と衝突したときtrue
    public static bool isRHit = false;
    //VirtualPlayerが敵と衝突したときtrue
    public static bool isVHit = false;

    // Use this for initialization
    void Start () {
        //RealPlayerオブジェクト
        rPlayer = GameObject.Find("RealPlayer");
        //VirtualPlayerオブジェクト
        vPlayer = GameObject.Find("VirtualPlayer");

        //RealCameraの移動範囲の最低値
        rCamMin = GameObject.Find("RCamMinZ").transform.position;
        //RealCameraの移動範囲の最大値
        rCamMax = GameObject.Find("RCamMaxZ").transform.position;
        //VirtualCameraの移動範囲の最低値
        vCamMin = GameObject.Find("VCamMinZ").transform.position;
        //VirtualCameraの移動範囲の最大値
        vCamMax = GameObject.Find("VCamMaxZ").transform.position;
        
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        //カメラの移動処理
        Move();
        //カメラの移動範囲指定  壁の先が見えないようにするため
        Shake();
    }

    /// <summary>
    /// カメラの移動処理
    /// </summary>
    void Move()
    {
        //RealPlayerのPosition
        Vector3 rPos = rPlayer.transform.position;
        //VirtualPlayerのPosition
        Vector3 vPos = vPlayer.transform.position;

        switch (this.gameObject.name)
        {
            //このオブジェクトがRealCameraのとき
            case "RealCamera":
                //rCamMinからrCamMaxまで移動可能
                rPosZ = Mathf.Clamp(rPos.z - 14, rCamMin.z, rCamMax.z);
                //Playerに追従したカメラ移動
                transform.position = new Vector3(rPos.x, rPos.y + 10, rPosZ);
                break;
            //このオブジェクトがVirtualCameraのとき
            case "VirtualCamera":
                //vCamMinからvCamMaxまで移動可能
                vPosZ = Mathf.Clamp(vPos.z - 14, vCamMin.z, vCamMax.z);
                //Playerに追従したカメラ移動
                transform.position = new Vector3(vPos.x, vPos.y + 10, vPosZ);
                break;
        }
    }

    /// <summary>
    /// 振動処理
    /// SE再生
    /// </summary>
    void Shake()
    {
        //GameOverでないとき
        if (!PlayerControl.isGameOver)
        {
            //RealPlayerがダメージを受けたらRealCameraを振動させる
            if (isRHit)
            {
                //振動処理
                ShakeRealCamera();
                //割れる音を再生
                PlaySE(audioSource.isPlaying);

            }
            //VirtualPlayerがダメージを受けたらVirtualCameraを振動させる
            if (isVHit)
            {
                //振動処理
                ShakeVirtualCamera();
                //割れる音を再生
                PlaySE(audioSource.isPlaying);
            }
        }
    }

    /// <summary>
    /// SE再生処理
    /// </summary>
    /// <param name="isPlaying"></param>
    void PlaySE(bool isPlaying)
    {
        if (!isPlaying)
        {
            audioSource.PlayOneShot(ac);
        }
    }

    /// <summary>
    /// RealCameraの振動処理
    /// </summary>
    void ShakeRealCamera()
    {
        //現在のカメラ位置を保存
        nowPos = transform.position;
        //上下左右に現在の位置からそれぞれ2だけ動かす
        var maxY = nowPos.y + 2;
        var minY = nowPos.y - 2;
        var maxX = nowPos.x + 2;
        var minX = nowPos.x - 2;

        //timerが0以上のとき振動させる
        if (time > 0)
        {
            //カウントダウン開始
            time -= Time.deltaTime;
            //現在の座標から上下左右でランダムに振動させる
            transform.position = (this.gameObject.name == "RealCamera")
                ? new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z)
                : transform.position;
        }
        //timerが0以下のとき振動しない
        if (time <= 0)
        {
            //Enemyとの衝突判定を初期値に(衝突していない状態にする)
            isRHit = false;
            //time初期値に戻す
            time = 0.3f;
        }
    }

    /// <summary>
    /// VirtualCameraの振動処理
    /// </summary>
    void ShakeVirtualCamera()
    {
        //現在のカメラ位置を保存
        nowPos = transform.position;
        //上下左右に現在の位置からそれぞれ2だけ動かす
        var maxY = nowPos.y + 2;
        var minY = nowPos.y - 2;
        var maxX = nowPos.x + 2;
        var minX = nowPos.x - 2;

        //timerが0以上のとき振動させる
        if (time > 0)
        {
            //カウントダウン開始
            time -= Time.deltaTime;
            //現在の座標から上下左右でランダムに振動させる
            transform.position = (this.gameObject.name == "VirtualCamera")
                ? new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z)
                : transform.position;
        }
        //timerが0以下のとき振動しない
        if (time <= 0)
        {
            //Enemyとの衝突判定を初期値に(衝突していない状態にする)
            isVHit = false;
            //time初期値に戻す
            time = 0.3f;
        }
    }
}
