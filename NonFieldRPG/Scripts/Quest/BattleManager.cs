using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DOTweenには必ず必要

// PlayerとEnemyの対戦の管理
public class BattleManager : MonoBehaviour
{
    public Transform playerDamagePanel;
    public QuestManager questManager;
    public PlayerUIManager playerUI;
    public EnemyUIManager enemyUI;
    public PlayerManager player;
    EnemyManager enemy;


    private void Start() 
    {
        enemyUI.gameObject.SetActive(false); // enemyUIの非表示設定(しかし、敵に遭遇しても非表示のまま)
        // StartCoroutine(SampleCol());
        playerUI.SetupUI(player);  // 設定しているプレイヤーのhp,atの反映
    }

    // サンプルコルーチン(遅れて実行させる)
        // IEnumerator SampleCol()
        // {
        //     Debug.Log("コルーチン開始");
        //     yield return new WaitForSeconds(2f);
        //     Debug.Log("2秒経過");
        // }

    // 初期設定
    public void Setup(EnemyManager enemyManager)
    {
        SoundManager.instance.PlayBGM("Battle");
        enemyUI.gameObject.SetActive(true);
        enemy = enemyManager;
        enemyUI.SetupUI(enemy);
        playerUI.SetupUI(player);

        enemy.AddEventListenerOnTap(PlayerAttack);

        // enemy transform.DOMove(new Vector3(0,10,0),5f);
    }

    void PlayerAttack()
    {
        StopAllCoroutines();
        SoundManager.instance.PlaySE(1);
        int damage = player.Attack(enemy);
        enemyUI.UpdateUI(enemy);  // UpdateUIを呼びだす
        DialogTextManager.instance.SetScenarios(new string[] {
            "プレイヤーの攻撃\nモンスターに"+damage+"ダメージを与えた。"
            });
        if (enemy.hp <= 0)
        {

            StartCoroutine(EndBattle());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2f); // 2秒待機
        SoundManager.instance.PlaySE(1);
        playerDamagePanel.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);  // カメラを揺らすエフェクト
        int damage = enemy.Attack(player);
        playerUI.UpdateUI(player);
        DialogTextManager.instance.SetScenarios(new string[] {
            "モンスターの攻撃\nプレイヤーは"+damage+"ダメージを受けた。"
            });
        yield return new WaitForSeconds(2f); // 1秒待機


        // --playerが死んだ場合の実装--
        if (player.hp <= 0)
        {
            questManager.PlayerDeath();  // QuestManagerにてPlayerDeath()関数を用意したものを反映
        }
        // --ここまで--
    }

    IEnumerator EndBattle()
    {
        yield return new WaitForSeconds(2f);
        DialogTextManager.instance.SetScenarios(new string[] {
            "モンスターは逃げていった。"
        });
        enemyUI.gameObject.SetActive(false);
        Destroy(enemy.gameObject);
        SoundManager.instance.PlayBGM("Quest");
        questManager.EndBattle();
    }
}
