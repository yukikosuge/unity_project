using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Bullet プレハブをインスタンス化していく
    // Inspector から割り当てられように public で変数を用意
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * 8f;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime * 8f;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x + dx, -8f, 8f), // x軸の移動範囲の指定
            Mathf.Clamp(transform.position.y + dy, -4.5f, 4.5f), // y軸の移動範囲の指定
            0f
        );
    }

        // Shoot() メソッドをコルーチンとして登録
        IEnumerator Shoot()
    {
        // ずっと連射したいので無限ループを while で作りつつ、あとは Instantiate() を使って bullet をインスタンス化
        while (true)
        {
            // 第 2 引数の位置ですが、 Player の位置
            // 回転も同じように Player の rotation
            Instantiate(bullet, transform.position, transform.rotation);
            // 0.2秒ごとに球を発射する処理を実行する
            yield return new WaitForSeconds(0.2f);
        }
    }
}