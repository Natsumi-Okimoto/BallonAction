using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject aerialFloorPrefab;  // プレファブにした AerialFloor_Mid ゲームオブジェクトをインスペクターからアサインする
    [SerializeField]
    private Transform generateTran;  // プレファブのクローンを生成する位置の設定
    [Header("生成までの待機時間")]
    public float waitTime;  // １回生成するまでの待機時間。どの位の間隔で自動生成を行うか設定
    private float timer; // 待機時間の計測用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0;
            GenerateFloor();
        }
    }
    /// <summary>
    /// プレファブを元にクローンのゲームオブジェクトを生成
    /// </summary>
    public void GenerateFloor()
    {
        GameObject obj = Instantiate(aerialFloorPrefab, generateTran);
        float randomPosY = Random.Range(-4.0f, 4.0f);
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);
    }
}
