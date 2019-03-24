using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRot : MonoBehaviour,IEnemyControl {

    //Player関連
    GameObject player;

    /// <summary>
    /// Moveメソッドで使用
    /// </summary>
    //回転カウント
    float count;
    //回転方向を指定するランダム値
    int rotateDirection;
    //trueの時Playerを追いかける
    bool isChase = false;

    // Use this for initialization
    void Start()
    {
        //0の時時計回り 1の時反時計回り
        rotateDirection = Random.Range(0, 2);
        //初期回転方向
        count = Random.Range(0, 4);
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
    /// 回転エネミー
    /// </summary>
    public void Move()
    {
        count += Time.deltaTime;
        var rot = 0;
        switch ((int)count)
        {
            case 0:
                rot = 0;
                break;
            case 1:
                rot = (rotateDirection == 0)
                    ? 90 : 270;
                break;
            case 2:
                rot = 180;
                break;
            case 3:
                rot = (rotateDirection == 0)
                    ? 270 : 90;
                break;
            case 4:
                count = 0;
                break;
        }
        transform.rotation = Quaternion.AngleAxis(rot, transform.up);
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
