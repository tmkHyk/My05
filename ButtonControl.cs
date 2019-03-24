using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{

    Button button;
    int buttonNumber;
    bool isAcitve = false;

    // Use this for initialization
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveStage();
        Select();
    }

    /// <summary>
    /// ステージセレクト可か不可か
    /// </summary>
    void ActiveStage()
    {
        //Button名の値を取得 (ex:Stage1なら1(6文字目)を取得)
        buttonNumber = int.Parse(this.gameObject.name.Substring(6));
        //外部に保存されているstageSelect値よりButtonの値が小さければ表示
        //大きければ非表示
        isAcitve = (StageSelect.saveStage >= buttonNumber) ? true : false;
    }

    /// <summary>
    /// 選択可能か不可か
    /// </summary>
    void Select()
    {
        //表示されているとき選択可能
        //非表示の時選択不可
        button.interactable = (isAcitve) ? true : false;
    }
}