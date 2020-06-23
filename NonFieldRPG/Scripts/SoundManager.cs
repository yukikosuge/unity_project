
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //--シングルトン開始--
    // ゲーム内に一つしか存在しないもの(音を管理する物とか)
    // 利用場所：シーン間でのデータ共有/オブジェクト共有
    // シーン遷移しても破壊されずにBGMが鳴り続く
    // 書き方
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            // シーン遷移してもSoundManagerを共有し続ける
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // 上記でシーン遷移した際にSoundManagerが共有されている為、重複してしまうのを防ぐ為に片方破壊する
            // 同じものがあったら破壊する
            Destroy(this.gameObject);
        }
    }
    // --シングルトン終わり--

    public AudioSource audioSourceBGM; // BGMのスピーカー
    public AudioClip[] audioClipsBGM;  // BGMの素材 (0:Title, 1:Town, 2:Quest, 3:Battle)

    // ①-③音を鳴らす
    public AudioSource audioSourceSE; // ①SEのスピーカー
    public AudioClip[] audioClipsSE; // ②鳴らす素材

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }


    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();  // 音を止める
        switch (sceneName)
        {
            default:  // ラジカセセッティング
            case "Title":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "Town":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "Quest":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "Battle":
                audioSourceBGM.clip = audioClipsBGM[3];
                break;
        }
        audioSourceBGM.Play();  // 再生
    }

    // ③ボタンを押したタイミングで鳴らす
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]); // SEを一度だけ鳴らす
    }
}
