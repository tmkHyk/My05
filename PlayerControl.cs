using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    [SerializeField, Tooltip("RealPlayerのStartPosition")]
    Transform rStartPos;
    [SerializeField, Tooltip("VirtualPlayerのStartPosition")]
    Transform vStartPos;
    [SerializeField, Tooltip("Jump条件　trueのときジャンプ中でジャンプ不可 falseの時地面に着いていてジャンプ可能")]
    bool isJumping = false;
    //[SerializeField, Range(0, 1), Tooltip("Playerの歩行速度")]
    //歩行速度
    float speed;

    //Playerのアニメーション
    Animator anim;

    //Playerの操作を変更するための値 1or-1
    public static int changeControl;  //Mirrorで使用

    //GameOver条件　trueの時GameOver
    public static bool isGameOver;  //GameManagerで使用
    //PlayerのHp最大値4　0の時GameOver
    public static int hp;  //Crackで使用

    //RealPlayerがStart位置に着いたらtrue
    public static bool isRealStart;  //GameManagerで使用
    //VituralPlayerがStart位置に着いたらtrue
    public static bool isVirtualStart;  //GameManagerで使用

    //SE Jump時鳴き声
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        //GameOverではない
        isGameOver = false;
        //hpの指定
        hp = 4;
        //初期値は1　RealPlayerメインの操作
        changeControl = 1;

        rStartPos = GameObject.Find("RealStart").GetComponent<Transform>();
        vStartPos = GameObject.Find("VirtualStart").GetComponent<Transform>();

        anim = GetComponent<Animator>();

        //このオブジェクトがRealPlayerの場合、初期位置はRealStartPosition
        //VirtualPlayer場合はVirtualStartPositionに設定
        transform.position = (this.gameObject == GameObject.Find("RealPlayer"))
            ? rStartPos.position
            : vStartPos.position;
        
        //最初は浮いた状態なのでfalse
        isRealStart = false;
        isVirtualStart = false;

        speed = 0.48f;
        
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotationを固定(回転しない)
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //前後左右移動処理
        Move();
        //ジャンプ処理
        Jump();
        //ゲームオーバー処理
        GameOver();
    }

    /// <summary>
    /// 前後左右移動
    /// </summary>
    void Move()
    {
        //歩くモーションはしない
        anim.SetBool("isWalking", false);
        //移動時歩くモーション
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !isJumping)
        {
            anim.SetBool("isWalking", true);
            Rotate();
        }
        //x軸(左右)の移動 changeControlで左右反転
        var moveX = Input.GetAxis("Horizontal") * Speed(speed) * changeControl * Time.timeScale;
        //z軸(前後)移動
        var moveZ = Input.GetAxis("Vertical") * Speed(speed) * Time.timeScale;
        //RealPlayerとVirtualPlayerで左右対称の動きをする
        this.gameObject.transform.position += (this.gameObject.name == "RealPlayer")
            ? new Vector3(moveX, 0, moveZ)
            : new Vector3(-moveX, 0, moveZ);
    }

    /// <summary>
    /// 前後左右回転
    /// </summary>
    void Rotate()
    {
        //前後回転
        if (Input.GetButton("Vertical"))
        {
            //Verticalの値が0より大きいときy軸に対して0(前向き)
            //0以下のとき180回転する(うしろ向き)
            transform.rotation = (Input.GetAxis("Vertical") > 0)
                ? transform.rotation = Quaternion.AngleAxis(0, transform.up)
                : transform.rotation = Quaternion.AngleAxis(180, transform.up);
        }
        //左右回転
        if (Input.GetButton("Horizontal"))
        {
            switch (this.gameObject.name)
            {
                //このオブジェクトがRealPlayerのとき
                case "RealPlayer":
                    //Horizontalの値が0より大きいときy軸に対して90(右向き)
                    //0以下のとき-90回転する(左向き)
                    transform.rotation = (Input.GetAxis("Horizontal") > 0)
                    ? transform.rotation = Quaternion.AngleAxis(90 * changeControl, transform.up)
                    : transform.rotation = Quaternion.AngleAxis(-90 * changeControl, transform.up);
                    break;
                //このオブジェクトがVirtualPlayerのとき
                case "VirtualPlayer":
                    //Horizontalの値が0より大きいときy軸に対して-90(左向き)
                    //0以下のとき90回転する(右向き)
                    transform.rotation = (Input.GetAxis("Horizontal") > 0)
                        ? transform.rotation = Quaternion.AngleAxis(-90 * changeControl, transform.up)
                        : transform.rotation = Quaternion.AngleAxis(90 * changeControl, transform.up);
                    break;
            }
        }
    }

    /// <summary>
    /// ジャンプ処理
    /// </summary>
    void Jump()
    {
        //スペースキーを押して　かつジャンプ中でないとき
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            //ジャンプ中(true)にする
            isJumping = true;
            //ジャンプする
            transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
        }

        //鳴き声(SE)の再生
        //条件 このスクリプトがアタッチされているオブジェクト名がRealPlayer
        //SEが再生中でない、ジャンプ中である、ゲームオーバーでないとき再生可能
        if (this.gameObject.name == "RealPlayer" && !audioSource.isPlaying
            && isJumping && !isGameOver)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    /// <summary>
    /// 歩行速度
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    float Speed(float s)
    {
        //ジャンプ中は減速
        s = (isJumping) ? speed / 1.5f : speed;
        return s;
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        //他のオブジェクトと衝突している時、ジャンプできる
        isJumping = false;
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {        
        switch(other.gameObject.name)
        {
            //RealStartと衝突したとき
            case "RealStart":
                //このオブジェクトがRealPlayerのとき
                if (this.gameObject.name == "RealPlayer")
                {
                    //ゲーム開始(GameManagerで)
                    isRealStart = true;
                }
                break;
            //VirtualStartと衝突したとき
            case "VirtualStart":
                //このオブジェクトがVirtualPlayerのとき
                if (this.gameObject.name == "VirtualPlayer")
                {
                    //ゲーム開始(GameManagerで)
                    isVirtualStart = true;
                }
                break;
        }
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    void GameOver()
    {
        //yの値が-20以下になったらGameOver
        if (transform.position.y < -20)
        {
            isGameOver = true;
        }
    }
}