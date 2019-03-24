using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRL : MonoBehaviour ,IEnemyControl{

    //Player関連
    GameObject player;
    
    //trueの時Playerを追いかける
    bool isChase = false;

    /// <summary>
    /// Moveメソッドで使用
    /// </summary>
    //distance値MAXで保存したPosition(回転が切り替わった瞬間のPosition)
    public Vector3 savePos;
    [SerializeField, Range(0, 10), Tooltip("Enemy移動速度")]
    float speed;
    [SerializeField, Range(0, 20), Tooltip("MoveEnemy移動距離")]
    float distance;
    //現在の距離
    float nowDistance;
    //Y軸Rotate変数
    float rot;
    //現在のY軸Rotate値
    float nowRot;

    // Use this for initialization
    void Start()
    {
        //現在のPositionをsavePosに保存
        savePos = transform.position;
        //現在のY軸Rotate値をrotに保存
        rot = transform.localRotation.y;
        //現在のY軸Rotate値はrot
        nowRot = rot;
    }

    // Update is called once per frame
    void Update()
    {
        //行動指定処理
        Action(isChase);
        //移動処理
        Move();
    }

    /// <summary>
    /// 行動指定処理
    /// </summary>
    /// <param name="isChase"></param>
    public void Action(bool isChase)
    {
        //trueのとき
        if (isChase)
        {
            //追従処理
            Chase();
        }
        //falseのとき
        else
        {
            //移動処理
            Move();
        }
    }

    /// <summary>
    /// 追従メソッド
    /// </summary>
    public void Chase()
    {
        //
        if (isChase)
        {
            //Playerに追従
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1f);
            //回転を中止してPlayerの方向を向く
            transform.LookAt(player.transform.forward);
        }
    }

    /// <summary>
    /// 左右移動エネミー
    /// </summary>
    public void Move()
    {
        //進行方向へ前進
        transform.position -= transform.forward * Time.deltaTime * speed;
        //savePosと現在の位置の距離を計算
        nowDistance = Vector3.Distance(savePos, transform.position);

        //現在の距離が指定した移動距離よりも大きいなら
        if (nowDistance > distance)
        {
            //現在のY軸Rotateが90のときrotを-90、そうでないとき90に
            rot = (nowRot == 90) ? -90 : 90;
            //rotの向きに回転
            transform.rotation = Quaternion.AngleAxis(rot, transform.up);
            //現在のPositionをsavePosに保存
            savePos = transform.position;
            //現在のY軸RotateをnowRotに保存
            nowRot = rot;
        }
        //savePosと現在の位置の距離を計算
        nowDistance = Vector3.Distance(savePos, transform.position);
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void Death()
    {
        //y軸が - 20以下になったとき強制的にDestroy
        if (transform.position.y <= -20)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.name)
        {
            //RealPlayerと衝突したら
            case "RealPlayer":
                CameraMove.isRHit = true;
                //PlayerのHpを1減らす
                PlayerControl.hp--;
                //このオブジェクトを削除
                Destroy(this.gameObject);
                break;
            //VirtualPlayerと衝突したら
            case "VirtualPlayer":
                CameraMove.isVHit = true;
                //PlayerのHpを1減らす
                PlayerControl.hp--;
                //このオブジェクトを削除
                Destroy(this.gameObject);
                break;
        }
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        //Playerが一定の距離(衝突範囲)に近づいたら
        if (other.gameObject.tag == "Player")
        {
            //追従開始
            isChase = true;
            //RealPlayerかVirtualPlayerか判別する
            //変数playerに格納されたPlayerに追従
            player = other.gameObject;
        }
    }
}
