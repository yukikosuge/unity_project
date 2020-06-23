using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Text を扱うため
using UnityEngine.UI;

// Scene を読み出すには、冒頭で SceneManagement を読み込んであげる必要があるので記載
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    // 敵のプレハブをあとで Inspector から割り当てていく為
    public GameObject enemy;

    // score を int 型で管理
    // テキストを割り当てたいので、 Inspector で設定できるように public の変数を作成
    private int score;
    public Text scoreText;

    // ScoreText と同じく public で宣言してあげて、あとで Inspector から割り当てていく
    public Text replayText;

    // このままだと、いつスペースキーを押してもリプレイできてしまうので、ゲームオーバーになったかどうかを判定するための変数を用意してあげて、ゲームオーバーになったときだけリプレイできるように
    // private で bool （真偽値）型 で isGameOver と宣言
    private bool isGameOver;

    // コルーチンで敵を生成していきたいので、 IEnumerator 型の返り値を持ち
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // 無限ループを作ってあげて、どんどん敵を生成していきます、敵の生成には Instantiate() を使ってあげれば良い
            Instantiate(
                enemy,
                // x 座標だけランダムにして端っこは -8f, 8fの設定
                // transform.position.yとしつつ、 transform.position.z は 2D ゲームなので 0f で
                // 回転に関しては GameController の値を使ってあげれば良いので、 transform.rotation 
                new Vector3(Random.Range(-8f, 8f), transform.position.y, 0f),
                transform.rotation
            );
            // 今回 0.5 秒ごとに敵が生成されるように
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Start is called before the first frame update
    // Start() のほうで StartCoroutine(); として、 SpawnEnemy を呼び出してあげればいい
    void Start()
    {
        StartCoroutine("SpawnEnemy");

        // スコアの初期化をしたいので
        score = 0;
        UpdateScoreText();

        // 最初はReplay??テキストを非表示にしておきたいので、 Start() のほうで replayText の text プロパティを空文字にしてあげます。
        replayText.text = "";

        // ゲームが始まるときはゲームオーバーになっていないので false にしておけばok
        isGameOver = false;

    }

    // Update is called once per frame
    // Update() のほうでキー入力を受け付ければ良いので、 Update() のほうに記載
    void Update()
    {
        //  isGameOver が false のときはリプレイさせたくないので、 isGameOver が false のとき、つまりゲームオーバーになっていないときは、それ以降の処理をしたくないので return 
        if (!isGameOver)
        {
            return;
        }

        // Input.GetKey() で KeyCode が Space キーだったらと言う意味
        if (Input.GetKey(KeyCode.Space))
        {
            // MainScene を呼び出せばいいので、 SceneManager.LoadScene("MainScene"); としてあげれば OK
            SceneManager.LoadScene("MainScene");
        }
    }

        // GameControllerScript のほうに public で AddScore() というメソッドを作ってあげて、敵に弾が当たったときにこちらのメソッドが呼ばれるようにしてあげる

        // 引数は scoreToAdd にしつつ
        public void AddScore(int scoreToAdd)
    {
        // score に足し上げていってあげれば良いので、このように書いてあげます。
        score += scoreToAdd;

        // UpdateScoreText() というメソッドを作ってあげて、こちらで呼んであげたいと思います。
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        // スコアが加算されていく命令
        scoreText.text = "Score: " + score;
    }

    // GameOver になったときにメッセージを表示したいので、 public メソッドを作っておいて、あとで呼び出していく
    public void GameOver()
    {
        // ゲームオーバー表示
        isGameOver = true;

        // public void GameOver() としつつ、メッセージを表示したいので replayText.text を適当な文字列にする
        replayText.text = "Hit SPACE to replay!";
    }
}