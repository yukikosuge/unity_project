using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 上の方に移動させる(x:0, y:10+時間指定, z:0)
        transform.position += new Vector3(0f, 10f * Time.deltaTime, 0f);
    }
}