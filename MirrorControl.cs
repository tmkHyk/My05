using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MirrorControl : MonoBehaviour {

    Image mirrorImage;

	// Use this for initialization
	void Start () {

        mirrorImage = GetComponent<Image>();
        //高さは画面いっぱいに、幅は画面の半分に指定
        mirrorImage.rectTransform.localScale = new Vector3(0.2f, Screen.height, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
