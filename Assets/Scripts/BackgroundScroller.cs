using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float restartPosition;
    public float stopPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 画面の左方向にこのゲームオブジェクト(背景)の位置を移動する
        transform.Translate(-scrollSpeed, 0, 0);

        // このゲームオブジェクトの位置がstopPositionに到達したら
        if (transform.position.x < stopPosition)
        {

            // ゲームオブジェクトの位置を再スタート地点へ移動する
            transform.position = new Vector2(restartPosition, 0);
        }
    }
}
