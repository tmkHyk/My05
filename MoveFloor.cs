using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour {

    //移動範囲
    [SerializeField, Range(0, 100), Tooltip("変位")]
    float displacment;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        switch (this.gameObject.tag)
        {
            //タグの名前がFloorUDのとき上下移動
            case "FloorUD":
                transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * 2) * displacment, transform.position.z);
                break;
            //タグの名前がFloorUDのとき左右移動
            case "FloorRL":
                transform.position = new Vector3(Mathf.Sin(Time.time * 2) * displacment, transform.position.y, transform.position.z);
                break;
            //タグの名前がFloorUDのとき前後移動
            case "FloorFB":
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Sin(Time.time * 2) * displacment);
                break;
        }
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        //Playerと衝突したとき
        if (other.gameObject.tag == "Player")
        {
            //Playerが乗っている(衝突している)ときPlayerの親オブジェクトになる
            other.transform.SetParent(transform);
            //other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// 衝突判定　オブジェクトから離れたかどうか
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionExit(Collision other)
    {
        //床オブジェクトから離れた時
        switch (other.gameObject.name)
        {
            //PlayerがRealPlayerのときRealオブジェクトの子オブジェクトにする
            case "RealPlayer":
                other.transform.SetParent(GameObject.Find("Real").transform);
                break;
            //PlayerがVitualPlayerのときVitualオブジェクトの子オブジェクトにする
            case "VirtualPlayer":
                other.transform.SetParent(GameObject.Find("Virtual").transform);
                break;
        }
    }
}
