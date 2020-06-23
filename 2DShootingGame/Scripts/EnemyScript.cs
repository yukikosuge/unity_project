using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
        private float phase;

        // Enemy が弾と当たったときに、プレハブの Explosion を Instantiate() したいので、まずは Inspector で割り当てられるように public で変数を宣言
        public GameObject explosion;

        // 他のクラスのメソッドを呼び出したいので、まずはそのクラス型の変数を用意してあげて、あとで取得していきます。
        // private としつつ GameControllerScript クラスなのでこのようにしてあげて、変数名は gameController としておきましょう。
        private GameControllerScript gameController;

    // Start is called before the first frame update
    void Start()
    {
        // 位相をずらしてあげる(位相は phase で管理していく)
        // phase を Random.Range() でランダムに決めてあげる
        // 0 から 2π の間にしてあげれば良いので、 0f から Mathf.PI * 2
        phase = Random.Range(0f, Mathf.PI * 2);

        // GameControllerScript を探せばいいので、 GameObject.FindWithTag でまずは探してあげます。
        gameController = GameObject
        // タグは前回 GameController にしたので、このように書いてあげれば OK
        .FindWithTag("GameController")
        // GameController にくっついた Component を探せば良いので GetComponent としつつ、 Component の型は GameControllerScript
        .GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    // ポジションをいじりたいのでUpdateに記載
    void Update()
    {
        // Vector3()でtransform.position で y 方向に値を加えていけばいい
        // x は 0f 、 y は速度を適当に -2f としつつ、 Time.deltaTime で調整してあげて、 z も 0f で OK
        // 下に動かす指示
        // transform.position += new Vector3(0f, -2f * Time.deltaTime, 0f);

        // 三角関数を使って敵の動きに変化をつけてみる
        transform.position += new Vector3(
            
        // x 座標に変化を入れていきたいので、こちらでコサインを使用
        // なんらかの増える値を与えてあげたいので、今回は経過したフレーム数である Time.frameCount を使用
        // 周期と振幅を操作したいので、周期に関しては 0.05f をかけて小さくしてあげつつ、振幅も 0.05f
        Mathf.Cos(Time.frameCount * 0.05f + phase) * 0.05f,
        -2f * Time.deltaTime,
        0f
        );
    }
        // この Enemy の範囲に入ってきたらという意味になる
        private void OnTriggerEnter2D(Collider2D collision)
    {
        // もし入ってきたものが Bullet だったらと言う意味
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 10 点加算されるよう
            gameController.AddScore(10);

            // 爆発するときに Instantiate() したいのですが、 Destroy() の前に記載
            // 位置と回転は、このスクリプトが割り当てられている敵の位置と敵の回転をそのまま使えば良いので、これで OK
            Instantiate(explosion, transform.position, transform.rotation);

            // 弾と当たったときの処理、弾も敵も消してあげたいので Destroy() を使用

            // 弾のほうは collision で拾えるので Destroy(collision.gameObject); としつつ
            Destroy(collision.gameObject);

            // 自分自身、つまり敵のオブジェクトを消すには Destroy(gameObject); と書いてあげれば OK
            Destroy(gameObject);
        }


        // EnemyScript で Player と当たったとき、と書いていきたいので EnemyScript を編集していきます
        // CompareTag("Player") としつつ、インスタンス化するのは explosion と、もうひとつ explosion を Player の位置に出したいのでコピーしてあげつつ、位置と回転を collision のものを使ってあげればOK

        if (collision.gameObject.CompareTag("Player"))
        {
            // EnemyScript で Player と衝突したときにゲームオーバーにすればいいので、このあたりに記載し、gameController はもう取得できているので gameController の GameOver() メソッドを呼んであげれば OK
            gameController.GameOver();

            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}