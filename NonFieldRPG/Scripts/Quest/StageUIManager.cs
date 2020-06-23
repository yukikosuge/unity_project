using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// StageUI(ステージ数のUI/進行ボタン/街に戻るボタン)の管理
public class StageUIManager : MonoBehaviour
{
    public Text stageText;
    public GameObject nextButton;
    public GameObject toTownButton;
    public GameObject stageClearImage;  // 表示非表示の設定を行うのでTextではなく、GameObjectで設定

    private void Start()
    {
        stageClearImage.SetActive(false); // ステージクリアテキストの非表示
    }

    // stagetextを変更する関数
    public void UpdateUI(int currentStage)
    {
        stageText.text = string.Format("ステージ:{0}",currentStage+1);  // {0}の中にcurrentStageの値が入る
    }

    public void HideButtons()
    {
        nextButton.SetActive(false);
        toTownButton.SetActive(false);
    }

    public void ShowButtons()
    {
        nextButton.SetActive(true);
        toTownButton.SetActive(true);
    }

    // 引数使用パターン(ShowButtonsを表示)
    // public void ShowButtons(bool isTrue)
    // {
    //     nextButton.SetActive(isTrue);
    //     toTownButton.SetActive(isTrue);
    // }

    public void ShowClearText()
    {
        stageClearImage.SetActive(true);  // textボタン表示
        nextButton.SetActive(false);  // nextボタン非表示
        toTownButton.SetActive(true);  // townボタン表示
    }
}
