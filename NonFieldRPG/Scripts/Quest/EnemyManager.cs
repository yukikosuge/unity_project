using System;
using UnityEngine;
using DG.Tweening;

// 敵を管理するもの(ステータス/クリック検出)
public class EnemyManager : MonoBehaviour
{
    // 関数登録
    Action tapAction; // クリックされた時に実行したい関数(外部から設定したい)

    public new string name;  // new:ヒエラルキー内の名前とは違う事を指す
    public int hp;
    public int at;
    public GameObject hitEffect; // 1 effect用意

    // 攻撃する
    public int Attack(PlayerManager player)
    {
        int damage = player.Damage(at);
        return damage;
    }
    // ダメージを受ける
    public int Damage(int damage)
    {
        Instantiate(hitEffect, this.transform, false); // 2 effect生成  // effectの表示位置を整える
        transform.DOShakePosition(0.3f, 0.5f, 20, 0, false, true);  // (何秒かけて振動, 強さ, 振動回数, ランダム, スナッピング, フェードアウト)  // 敵を揺らすエフェクト
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }

    // tapActionに関数を登録する関数を作る
    public void AddEventListenerOnTap(Action action)  // tapActionに関数を登録するもの()ないは関数名
    {
        tapAction += action;
    }

    public void OnTap()  // タップした時に下記タップアクションが実行される
    {
        Debug.Log("クリックされた");
        tapAction();  // タップアクションは登録したPlayerAttack
    }
}
