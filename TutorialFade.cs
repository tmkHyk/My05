using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFade : MonoBehaviour {

    Image image;
    //透過値　初期値は0
    float alpha = 0;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        //FadeImageが表示されたら
        if (TutorialMirror.isFadeActive)
        {
            //alpha値を上げて画面を徐々に暗くする
            alpha += 0.01f;
            image.color = new Color(0, 0, 0, Mathf.Clamp(alpha, 0, 0.5f));
            //alpha値が0.5以上になったらTextを表示開始する
            TutorialMirror.isTextActive = (alpha >= 0.5f) ? true : false;
        }
	}
}
