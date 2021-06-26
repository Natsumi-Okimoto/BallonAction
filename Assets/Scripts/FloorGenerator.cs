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
    private GameDirector gameDirector;
    private bool isActivate;                  // 生成の状態を設定し、生成を行うかどうかの判定に利用する。trueなら 生成し、false なら生成しない
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 停止中は生成を行わない
        if (isActivate == false)
        {
            return;
        }
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
        gameDirector.GenerateCount++;

    }
    /// <summary>
    /// FloorGeneratorの準備
    /// </summary>
    /// <param name="gameDirector"></param>
    public void SetUpGenerator(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;

        // TODO 他にも初期設定したい情報がある場合にはここに処理を追加する

    }

    /// <summary>
    /// 生成状態のオン/オフを切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
    }


}
