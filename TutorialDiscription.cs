using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialDiscription : MonoBehaviour {
    
    //Spriteをアタッチ
    [SerializeField,Tooltip("[0]DiscriptionImage1 [1]DiscriptionImage2")]
    Sprite[] sprite;

    //今現在選ばれているSprite
    Image image;

    //Enterを押した回数
    float count;
    //trueのときTutorialを終わす  falseのときTutorialを表示する
    bool isEnd;

	// Use this for initialization
	void Start () {

        image = GetComponent<Image>();
        image.sprite = sprite[0];

        count = 0;
        isEnd = false;
	}

    // Update is called once per frame
    void Update()
    {

        if (TutorialMirror.isDiscriptionActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                image.sprite = sprite[1];
                count++;
                if (count >= 2)
                {
                    isEnd = true;
                }
            }
        }

        if (isEnd)
        {
            SceneManager.LoadScene("SelectStage");
        }
    }
}
