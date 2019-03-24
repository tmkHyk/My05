using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour {

    //RealPlayerとVirtualPlayerオブジェクト
    GameObject rPlayer, vPlayer;

	// Use this for initialization
	void Start () {

        rPlayer = GameObject.Find("RealPlayer");
        vPlayer = GameObject.Find("VirtualPlayer");
	}
	
	// Update is called once per frame
	void Update () {

        //このオブジェクト名がRealLightのときRealPlayerの上にPositionを設定
        //VirtualLightのときVirtualPlayerの上にPositionを設定
        transform.position = (this.gameObject.name == "RealLight")
            ? new Vector3(rPlayer.transform.position.x, rPlayer.transform.position.y + 15, rPlayer.transform.position.z)
            : new Vector3(vPlayer.transform.position.x, vPlayer.transform.position.y + 15, vPlayer.transform.position.z);
    }
}
