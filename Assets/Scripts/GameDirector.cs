using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private GoalChecker goalHousePrefab;            // ゴール地点のプレファブをアサイン

    [SerializeField]
    private PlayerController playerController;      // ヒエラルキーにある Yuko_Player ゲームオブジェクトをアサイン

    [SerializeField]
    private FloorGenerator[] floorGenerators;       // floorGenerator スクリプトのアタッチされているゲームオブジェクトをアサイン

    private bool isSetUp;                           // ゲームの準備判定用。true になるとゲーム開始

    private bool isGameEnd;                          // ゲーム終了判定用。true になるとゲーム終了

    private int generateCount;                      // 空中床の生成回数

    [SerializeField]
    private RandomObjectGenerator[] randomObjectGenerators;           // RandomObjectGenerator スクリプトのアタッチされているゲームオブジェクトをアサイン

    [SerializeField]
    private AudioManager audioManager;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(audioManager.PlayBGM(0));
        // ゲーム開始状態にセット
        isGameEnd = false;
        isSetUp = false;

        // FloorGeneratorの準備
        SetUpFloorGenerators();

        
        // 各ジェネレータの生成を停止
        StopGenerators();

    }

    /// <summary>
    /// FloorGeneratorの準備
    /// </summary>
    private void SetUpFloorGenerators()
    {
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            // FloorGeneratorの準備・初期設定を行う
            floorGenerators[i].SetUpGenerator(this);          
        }
    }
    // Update is called once per frame
    void Update()
    {
        // プレイヤーがはじめてバルーンを生成したら
        if (playerController.isFirstGenerateBallon && isSetUp == false)
        {

            // 準備完了
            isSetUp = true;

            // 各ジェネレータの生成をスタート
            ActivateGenerators();

            StartCoroutine(audioManager.PlayBGM(1));
        }
        // ゲーム終了したら
        if (isGameEnd == true)
        {
            // 処理をここで終了する
            return;
        }
    }

    // generateCount 変数のプロパティ
    public int GenerateCount
    {
        set
        {
            generateCount = value;

            Debug.Log("生成数 / クリア目標数 : " + generateCount + " / " + clearCount);

            if (generateCount >= clearCount)
            {
                // ゴール地点を生成
                GenerateGoal();

                isGameEnd = true;
                // ゲーム終了
                GameUp();
            }
        }
        get
        {
            return generateCount;
        }
    }

    public int clearCount;　　　　　　　　　　　 　// ゴール地点を生成するまでに必要な空中床の生成回数
    /// <summary>
    /// ゴール地点の生成
    /// </summary>
    private void GenerateGoal()
    {
        // ゴール地点を生成
        GoalChecker goalHouse = Instantiate(goalHousePrefab);

        // TODO ゴール地点の初期設定
        Debug.Log("ゴール地点 生成");
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void GameUp()
    {

        // ゲーム終了
        isGameEnd = true;

        // 各ジェネレータの生成を停止
        StopGenerators();

        // TODO 各ジェネレータを停止
        Debug.Log("生成停止");
    }
    /// <summary>
    /// 各ジェネレータを停止する
    /// </summary>
    private void StopGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }


    /// <summary>
    /// 各ジェネレータを動かし始める
    /// </summary>
    private void ActivateGenerators()
    {
        for (int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }

        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }


}
