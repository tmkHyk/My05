using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScore : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {

        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        //Result時にタイムをスコアとして表示
        //if (Goal.isGameClear || PlayerControl.isGameOver)
        //{
        //    text.text = GameManager.timer.ToString("f1");
        //}
    }
}
