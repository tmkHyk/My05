using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyControl
{
    //行動指定処理
    void Action(bool isChase);
    //追従処理
    void Chase();
    //移動処理
    void Move();
    //死亡処理
    void Death();
    //衝突判定
    void OnCollisionEnter(Collision other);
    //衝突判定
    void OnTriggerEnter(Collider other);
}
