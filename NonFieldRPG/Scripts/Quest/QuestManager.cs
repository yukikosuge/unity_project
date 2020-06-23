using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// クエスト全体を管理
public class QuestManager : MonoBehaviour
{
    public StageUIManager stageUI;
    public GameObject enemyPrefab;   // prefabの読み込みに必要
    public BattleManager battleManager;
    public SceneTransitionManager sceneTransitionManager; // シーン遷移を管理するもの
    public GameObject questBG;

    // 敵に遭遇するテーブル:-1なら遭遇しない,0なら遭遇
    int[] encountTable = {-1, -1, 0, -1, 0, -1 };

    int currentStage = 0;   // 現在のステージ進行度
    private void  Start() 
    {
        stageUI.UpdateUI(currentStage);   // prefab生成
        DialogTextManager.instance.SetScenarios(new string[] {"森についた。"});
    }

    IEnumerator Seaching()
    {
        DialogTextManager.instance.SetScenarios(new string[] {"探索中..."});
        // 画面フェード--2秒かけて拡大その後元のサイズに戻す--
        questBG.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f)  // xyz,秒
        .OnComplete(() => questBG.transform.localScale = new Vector3(1, 1, 1)); // 元に戻す
        // 画面フェード--ここまで--

        // フェードアウト--2秒かけて消えるようにするその後元の背景に戻る--
        SpriteRenderer questBGSpriteRenderer = questBG.GetComponent<SpriteRenderer>();
        questBGSpriteRenderer.DOFade(0, 2f)  // 0:消える 1:表示 , 秒数  // 2秒かけて消える
        .OnComplete(() => questBGSpriteRenderer.DOFade(1, 0)); // 0秒かけて元(1)に戻す
        // フェードアウト--ここまで--

        // 2秒間処理を待機させる
        yield return new WaitForSeconds(2f);

        currentStage++;   // ステージ数が増加していく
        // 進行度をUIに反映
        stageUI.UpdateUI(currentStage);

        if (encountTable.Length <= currentStage)
        {
            Debug.Log("クエストクリア");
            QuestClear();
            // クリア処理
        }
        else if (encountTable[currentStage] == 0)
        {
            EncountEnemy();
        }
        else
        {
        stageUI.ShowButtons();
        }
    }

    // Nextボタンが押されたら
    public void OnNextButton()
    {
        SoundManager.instance.PlaySE(0);
        stageUI.HideButtons();
        StartCoroutine(Seaching());
    }
    public void OnToTownButton()
    {
        SoundManager.instance.PlaySE(0);
    }

    void EncountEnemy()
    {
        DialogTextManager.instance.SetScenarios(new string[] {"モンスターに遭遇した!"});
        stageUI.HideButtons();  // HideButtonsの発生
        GameObject enemyObj = Instantiate(enemyPrefab);  // enemyprefabの生成
        EnemyManager enemy = enemyObj.GetComponent<EnemyManager>();
        battleManager.Setup(enemy);  // 敵に出会ったら受け取ってセットアップしてあげる
    }

    public void EndBattle()
    {
        stageUI.ShowButtons();
    }

    void QuestClear()
    {
        DialogTextManager.instance.SetScenarios(new string[] {"宝箱を手に入れた。\n街に戻りましょう。"});
        SoundManager.instance.StopBGM();  // 音を止める
        SoundManager.instance.PlaySE(2);
        // クエストクリア!と表示する
        // 街に戻るボタンのみ表示する
        stageUI.ShowClearText();
        
        // sceneTransitionManager.LoadTo("Town"); // シーン遷移
    }

    // player死後の行動の実行(BattleManagerの--playerが死んだ場合の実装--の内容)
    public void PlayerDeath() 
    {
        sceneTransitionManager.LoadTo("Town");
    }
}
