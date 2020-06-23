using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public int hp;
    public int at;

    // 攻撃する
    public int Attack(EnemyManager enemy)
    {
        int damage = enemy.Damage(at);
        return damage;
    }
    // ダメージを受ける
    public int Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
        }
        return damage;
    }

    // // セーブ&ロード&削除
    // 別内容の講座の為断念
    // const string SAVEKEY = "PLAYER-SAVE-KEY";
    // public void Save()
    // {
    //     PlayerPrefs.SetString(SAVEKEY, JsonUtility.ToJson(this));
    //     PlayerPrefs.Save();
    // }
    // public void Load()
    // {
    //     string jsonPlayer =  PlayerPrefs.GetString(SAVEKEY, JsonUtility.ToJson(new PlayerManager()));
    //     Instance = JsonUtility.FromJson<PlayerManager>(jsonPlayer);
    // }
    // public void DeleteSaveData()
    // {
    //     PlayerPrefs.DeleteKey(SAVEKEY);
    //     PlayerPrefs.Save();
    //     Load();
    // }

}
